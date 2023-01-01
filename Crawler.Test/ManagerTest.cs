using Crawler.Lib.Crawler;
using Crawler.Test.Mocks;

namespace Crawler.Test
{
    [TestClass]
    public class ManagerTest
    {
        [TestMethod]
        public async Task Manager_Sitemap_Test()
        {
            var mockRobots = new MockRobots();
            _ = new Downloader(new MockDownloader()); //Overwrites the downloader singleton instance in Downloader.Instance
            Assert.IsInstanceOfType(Downloader.Instance, typeof(MockDownloader));

            var mockDocumentRepository = new MockDocumentRepository();
            var tokenSource = new CancellationTokenSource();
            var context = new ManagerContext()
            {
                FollowSitemap = true,
                FollowInternalLinks = false,
                CancellationToken = tokenSource.Token,
                DocumentRepository = mockDocumentRepository,
                RobotsParser = mockRobots
            };
            var manager = new Manager(context);

            await manager.Start("https://fakeseed.co.za");

            Assert.AreEqual(10, mockDocumentRepository.Database.Count);
        }

        [TestMethod]
        public async Task Manager_InternalLinks_Test()
        {
            var mockRobots = new MockRobots();
            _ = new Downloader(new MockDownloader()); //Overwrites the downloader singleton instance in Downloader.Instance
            Assert.IsInstanceOfType(Downloader.Instance, typeof(MockDownloader));

            var mockDocumentRepository = new MockDocumentRepository();
            var tokenSource = new CancellationTokenSource();
            var context = new ManagerContext()
            {
                FollowSitemap = false,
                FollowInternalLinks = true,
                CancellationToken = tokenSource.Token,
                DocumentRepository = mockDocumentRepository,
                RobotsParser = mockRobots
            };
            var manager = new Manager(context);

            await manager.Start("https://fakeseed.co.za");

            Assert.AreEqual(1, mockDocumentRepository.Database.Count);
        }

        [TestMethod]
        public async Task Manager_Sitemap_and_InternalLinks_Test()
        {
            var mockRobots = new MockRobots();
            _ = new Downloader(new MockDownloader()); //Overwrites the downloader singleton instance in Downloader.Instance
            Assert.IsInstanceOfType(Downloader.Instance, typeof(MockDownloader));

            var mockDocumentRepository = new MockDocumentRepository();
            var tokenSource = new CancellationTokenSource();
            var context = new ManagerContext()
            {
                FollowSitemap = true,
                FollowInternalLinks = true,
                CancellationToken = tokenSource.Token,
                DocumentRepository = mockDocumentRepository,
                RobotsParser = mockRobots
            };
            var manager = new Manager(context);

            await manager.Start("https://fakeseed.co.za");

            Assert.AreEqual(11, mockDocumentRepository.Database.Count);
        }
    }
}