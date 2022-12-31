namespace Crawler.Lib.Crawler
{
    public class UrlFoundArgs : EventArgs
    {
        public string Url { get; }

        public UrlFoundArgs(string url) => Url = url;
    }

    public class ResultArgs : EventArgs
    {
        public string? Url { get; set; }
        public string? StatusCode { get; set; }
        public string? Body { get; set; }
        public string? ContentType { get; set; }
        public long DownloadTime_ms { get; set; }
    }
}