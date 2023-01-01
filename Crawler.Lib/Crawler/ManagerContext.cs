using RobotsParser;

namespace Crawler.Lib.Crawler
{
    public class ManagerContext
    {
        public bool FollowInternalLinks { get; set; }
        public IRobots? RobotsParser { get; set; }
        public IRepository<Document>? DocumentRepository { get; set; }
        public CancellationToken CancellationToken { get; set; }
        public int MaxInternalDepth { get; set; }
        public bool FollowSitemap { get; set; }
    }
}
