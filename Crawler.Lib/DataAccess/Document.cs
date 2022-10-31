namespace Crawler.Lib;

public class Document : IDataObject
{
    public long Id { get; set; }
    public string? Url { get; set; }
    public DateTime LastUpdated { get; set; }
    public string? Status { get; set; }
    public string? Body { get; set; }
    public string? ContentType { get; set; }
}
