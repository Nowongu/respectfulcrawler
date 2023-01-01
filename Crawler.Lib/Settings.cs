using System.Configuration;
using System.Reflection;

namespace Crawler.Lib
{
    public class Settings
    {
        public static Settings Instance { get; } = new Settings();

        public string DatabaseName
        {
            get
            {
                return ConfigurationManager.AppSettings["database_name"] ?? "respectful_crawler.db";
            }
            set
            {
                AddUpdateAppSettings("database_name", value);
            }
        }

        public string ConnectionString => $"Data Source={new FileInfo(Assembly.GetEntryAssembly()?.Location ?? string.Empty).Directory}\\{DatabaseName}";

        public string? SeedUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["seed_url"];
            }
            set
            {
                if (string.IsNullOrEmpty(value)) return;

                AddUpdateAppSettings("seed_url", value);
            }
        }

        public int MaxInternalDepth
        {
            get
            {
                if (int.TryParse(ConfigurationManager.AppSettings["max_internal_depth"], out int settingValue)) return settingValue;
                return 0;
            }
            set
            {
                if (value < 0) return;

                AddUpdateAppSettings("max_internal_depth", value.ToString());
            }
        }

        public bool FollowInternalLinks
        {
            get
            {
                return bool.TryParse(ConfigurationManager.AppSettings["follow_internal_links"], out bool settingValue) && settingValue;
            }
            set
            {
                AddUpdateAppSettings("follow_internal_links", value.ToString());
            }
        }

        public bool FollowSitemap
        {
            get
            {
                return bool.TryParse(ConfigurationManager.AppSettings["follow_sitemap"], out bool settingValue) && settingValue;
            }
            set
            {
                AddUpdateAppSettings("follow_sitemap", value.ToString());
            }
        }

        private void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }
    }
}
