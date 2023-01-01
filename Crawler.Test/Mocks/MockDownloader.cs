using Crawler.Lib.Crawler;

namespace Crawler.Test.Mocks
{
    public class MockDownloader : IDownloader
    {
        public Task<DownloadResult> GetResult(string url, CancellationToken cancellationToken)
        {
            var timeTaken = Random.Shared.NextInt64(2000);

            return Task.FromResult(new DownloadResult()
            {
                Content = "<html><head></head><body>Hello! I'm some text in the body</body></html>",
                ContentType = "text/html",
                DownloadTime_ms = timeTaken,
                StatusCode = 200,
                Url = "https://www.testurl.com"
            });
        }
    }
}
