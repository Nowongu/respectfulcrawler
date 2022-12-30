using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RobotsSharpParser;

namespace Crawler.Lib.Crawler
{

    public class SitemapFeeder
    {
        private readonly IRobots _robotsParser;

        public event EventHandler<UrlFoundArgs>? UrlFound;

        private void RaiseUrlFound(string url) => UrlFound?.Invoke(this, new UrlFoundArgs(url));

        public SitemapFeeder(IRobots robotsParser)
        {
            _robotsParser = robotsParser;
        }

        public async Task Start()
        {
            await _robotsParser.Load();

            var sitemaps = await _robotsParser.GetSitemapIndexes();
            foreach (var sitemap in sitemaps)
            {
                var urls = await _robotsParser.GetUrls(sitemap);
                foreach (var url in urls)
                {
                    RaiseUrlFound(url.loc);
                }
            }
        }
    }
}