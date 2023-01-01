using HtmlParser;
using RobotsParser;

namespace Crawler.Lib.Crawler
{
    public class Manager
    {
        private readonly IRobots _robotsParser;
        private readonly IRepository<Document> _documentRepository;
        private readonly SemaphoreSlim _semaphore;
        private readonly CancellationToken _cancellationToken;
        private readonly bool _followInternalLinks;
        private readonly int _maxInternalDepth;
        private readonly bool _followSitemap;
        private readonly HashSet<string> _visited = new HashSet<string>();


        //Can update Manger to take multiple feeders
        public Manager(ManagerContext managerContext)
        {
            _robotsParser = managerContext.RobotsParser ?? throw new ArgumentNullException("RobotsParser instance is required in Manager");
            _documentRepository = managerContext.DocumentRepository ?? throw new ArgumentNullException("DocumentRepository instance is required in Manager");
            _cancellationToken = managerContext.CancellationToken;
            _semaphore = new SemaphoreSlim(10);
            _followInternalLinks = managerContext.FollowInternalLinks;
            _maxInternalDepth = managerContext.MaxInternalDepth;
            _followSitemap = managerContext.FollowSitemap;
        }

        public event EventHandler<ResultArgs>? Result;
        public event EventHandler<ProgressArgs>? Progress;
        public event EventHandler<CompletedArgs>? Completed;

        private void RaiseResult(DownloadResult result) => Result?.Invoke(this,
            new ResultArgs()
            {
                Body = result.Content,
                ContentType = result.ContentType,
                StatusCode = result.StatusCode.ToString(),
                Url = result.Url,
                DownloadTime_ms = result.DownloadTime_ms,
            });

        private void RaiseProgress(int queueCount) => Progress?.Invoke(this,
            new ProgressArgs()
            {
                QueueCount = queueCount
            });

        private void RaiseCompleted(string? error = null) => Completed?.Invoke(this, new CompletedArgs() { Error = error });

        public async Task Start(string seed)
        {
            try
            {
                _visited.Clear();

                if (_followSitemap) await GetSitemapUrls(seed);
                if (_followInternalLinks) await FollowInternalLinks(seed);
            }
            catch (AggregateException e)
            {
                RaiseCompleted(e.GetBaseException().Message);
            }
            catch (Exception e)
            {
                RaiseCompleted(e.Message);
            }
            finally
            {
                RaiseCompleted();
            }
        }

        private async Task GetSitemapUrls(string seed)
        {
            await _robotsParser.LoadRobotsFromUrl($"{seed}/robots.txt");
             
            if (!_robotsParser.Sitemaps.Any()) return;

            var sitemaps = await _robotsParser.GetSitemapIndexes();
            foreach (var sitemap in sitemaps)
            {
                if (_cancellationToken.IsCancellationRequested) return;
                var urls = await _robotsParser.GetUrls(sitemap);
                var urlQueue = urls
                        .Select(x => x.loc)
                        .Distinct()
                        .Except(_visited);

                RaiseProgress(urlQueue.Count());
                foreach (var url in urlQueue)
                {
                    if (_cancellationToken.IsCancellationRequested) return;

                    if (_followInternalLinks)
                    {
                        await FollowInternalLinks(url);
                    }
                    else
                    {
                        var result = await DownloadAndSave(url);
                        if (result == null) break;
                        RaiseResult(result);
                    }
                }
            }
        }

        private async Task FollowInternalLinks(string url, int depth = 0)
        {
            if (depth > _maxInternalDepth) return;

            var result = await DownloadAndSave(url);
            if (result == null) return;
            RaiseResult(result);

            if (depth + 1 > _maxInternalDepth || string.IsNullOrWhiteSpace(result?.Content)) return;

            var pageUrls = GetPageUrls(url, result.Content);
            foreach (var pageUrl in pageUrls)
            {
                if (_cancellationToken.IsCancellationRequested) return;

                var pageResult = await DownloadAndSave(pageUrl);
                if (pageResult == null) break;

                RaiseResult(pageResult);
                await FollowInternalLinks(pageUrl, ++depth);
            }
        }

        //This is running on the main thread, parsing a large page
        //could make the UI stutter if the CPU is under strain.
        private IEnumerable<string> GetPageUrls(string pageUrl, string content)
        {
            var nodes = Parser.Parse(content);
            foreach (var node in nodes.Where(x => x.Type == NodeType.a))
            {
                if (_cancellationToken.IsCancellationRequested) yield break;

                var baseUrl = new Uri(pageUrl);
                if (node.Attributes.TryGetValue("href", out string? tagValue)
                    && Uri.TryCreate(tagValue, UriKind.Relative, out Uri? relative)
                    && Uri.TryCreate(baseUrl, relative, out Uri? resultUri)
                    && !_visited.Contains(resultUri.AbsoluteUri))
                {
                    yield return resultUri.AbsoluteUri;
                }
            }
        }

        private async Task<DownloadResult?> DownloadAndSave(string url)
        {
            if (!_visited.Add(url)) return null;

            try
            {
                await _semaphore.WaitAsync(_cancellationToken);
                var result = await Downloader.Instance.GetResult(url, _cancellationToken);

                var document = new Document
                {
                    Url = result.Url,
                    Body = result.Content,
                    ContentType = result.ContentType,
                    Status = result.StatusCode.ToString(),
                    LastUpdated = DateTime.UtcNow
                };

                var sqlResult = await _documentRepository.Save(document);
                if (sqlResult == 1) return result;
            }
            catch (OperationCanceledException)
            {
                // Cancel called on cancellation token, ignore exceptions.
            }
            finally
            {
                _semaphore.Release();
            }

            return null;
        }
    }
}
