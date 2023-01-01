using RobotsParser;

namespace Crawler.Test.Mocks
{
    internal class MockRobots : IRobots
    {
        public HashSet<string> Sitemaps { get; } = new HashSet<string>();

        public IReadOnlyList<Useragent> UserAgents => throw new NotImplementedException();

        public IEnumerable<string> GetAllowedPaths(string userAgent = "*")
        {
            throw new NotImplementedException();
        }

        public int GetCrawlDelay(string userAgent = "*")
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetDisallowedPaths(string userAgent = "*")
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<tSitemap>> GetSitemapIndexes(string? sitemapUrl = null)
        {
            List<tSitemap> items = new List<tSitemap>();
            for (int i = 0; i < 10; i++)
            {
                items.Add(new tSitemap()
                {
                    loc = $"https://index{i}"
                });
            }
            IReadOnlyList<tSitemap> r = items;
            return Task.FromResult(r);
        }

        public Task<IReadOnlyList<tUrl>> GetUrls(tSitemap tSitemap)
        {
            List<tUrl> items = new List<tUrl>();
            for (int i = 0; i < 10; i++)
            {
                items.Add(new tUrl()
                {
                    loc = $"https://url{i}"
                });
            }
            IReadOnlyList<tUrl> r = items;
            return Task.FromResult(r);
        }

        public bool IsPathAllowed(string path, string userAgent = "*")
        {
            throw new NotImplementedException();
        }

        public bool IsPathDisallowed(string path, string userAgent = "*")
        {
            throw new NotImplementedException();
        }

        public Task<bool> LoadRobotsContent(string robotsContent)
        {
            throw new NotImplementedException();
        }

        public Task<bool> LoadRobotsFromUrl(string robotsUrl)
        {
            Sitemaps.Add("https://fakesitemap.com");
            return Task.FromResult(true);
        }
    }
}
