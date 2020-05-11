using System.Collections.Generic;

namespace TitanGate.Website.Api.Domain.Settings
{
    public class AppSettings
    {
        public string ConnectionString { get; set; }

        public string Directory { get; set; }

        public string ImagesSubFolder { get; set; }

        public string ImageExtention { get; set; }

        public List<string> Whitelist { get; set; }
    }
}
