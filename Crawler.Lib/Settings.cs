namespace Crawler.Lib
{
    public class Settings
    {
        public static Settings Instance { get; } = new Settings();

        public string? ConnectionString { get; set; }
    }
}
