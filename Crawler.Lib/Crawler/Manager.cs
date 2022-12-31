using RobotsParser;
using System.Collections.Concurrent;

namespace Crawler.Lib.Crawler
{
    public class Manager
    {
        private readonly IRobots _robotsParser;
        private readonly DocumentRepository _repository;
        private readonly SemaphoreSlim _semaphore;
        private readonly CancellationToken _cancellationToken;
        private readonly BlockingCollection<string> _urls = new BlockingCollection<string>(new ConcurrentQueue<string>(), 10);

        //Can update Manger to take multiple feeders
        public Manager(IRobots robotsParser, DocumentRepository repository, CancellationToken cancellationToken)
        {
            _robotsParser = robotsParser;
            _repository = repository;
            _cancellationToken = cancellationToken;
            _semaphore = new SemaphoreSlim(10);
        }

        public event EventHandler<ResultArgs>? Result;

        private void RaiseResult(DownloadResult result) => Result?.Invoke(this,
            new ResultArgs()
            {
                Body = result.Content,
                ContentType = result.ContentType,
                StatusCode = result.StatusCode.ToString(),
                Url = result.Url,
                DownloadTime_ms = result.DownloadTime_ms,
            });

        public async Task Start()
        {
            await GetSitemapUrls();
        }

        private async Task GetSitemapUrls()
        {
            await _robotsParser.Load();

            var sitemaps = await _robotsParser.GetSitemapIndexes();
            foreach (var sitemap in sitemaps)
            {
                if (_cancellationToken.IsCancellationRequested) return;
                var urls = await _robotsParser.GetUrls(sitemap);
                foreach (var url in urls)
                {
                    if (_cancellationToken.IsCancellationRequested) return;
                    var result = await DownloadAndSave(url.loc);
                    if (result != null)
                    {
                        RaiseResult(result);
                    }
                }
            }
        }

        private async Task<DownloadResult?> DownloadAndSave(string url)
        {
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

                var sqlResult = await _repository.Save(document);
                if (sqlResult == 1) return result;
            }
            catch (OperationCanceledException)
            {
                _semaphore.Dispose();
            }
            finally
            {
                _semaphore.Release();
            }

            return null;
        }
    }
}
