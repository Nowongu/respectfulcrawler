namespace Crawler.Lib.Crawler
{
    public class ResultArgs : EventArgs
    {
        public string? Url { get; set; }
        public string? StatusCode { get; set; }
        public string? Body { get; set; }
        public string? ContentType { get; set; }
        public long DownloadTime_ms { get; set; }
    }

    public class ProgressArgs : EventArgs
    {
        public int QueueCount { get; set; }
    }
}