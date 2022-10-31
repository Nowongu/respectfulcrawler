using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Lib
{
    public class Settings
    {
        public static Settings Instance { get; set; } = new Settings();

        public string ConnectionString { get; set; }
    }
}
