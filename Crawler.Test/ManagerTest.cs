using Crawler.Lib;
using Crawler.Lib.Crawler;
using Crawler.Test.Mocks;
using RobotsParser;

namespace Crawler.Test
{
    [TestClass]
    public class ManagerTest
    {
        [TestMethod]
        public async Task Manager_Basic_Test()
        {
            var mockRobots = new MockRobots();
            new Downloader(new MockDownloader()); //Overwrites the downloader single instance in Downloader.Instance
            var mockDocumentRepository = new MockDocumentRepository();
            var tokenSource = new CancellationTokenSource();
            var context = new ManagerContext()
            {
                FollowInternalLinks = false,
                CancellationToken = tokenSource.Token,
                DocumentRepository = mockDocumentRepository,
                RobotsParser = mockRobots
            };
            var manager = new Manager(context);

            await manager.Start("https://fakeseed.co.za");

            Assert.AreEqual(100, mockDocumentRepository.Database.Count);
        }
    }
}