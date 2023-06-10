using Microsoft.Extensions.Configuration;

namespace TestJamaTestMgmt.Config
{
    public class ConfigurationHelper
    {
        private readonly IConfiguration _config;

        public ConfigurationHelper()
        {
            _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }

        public string WebDrivers => $"{_config.GetSection("WebDrivers").Value}";

        public string DomainUrl => _config.GetSection("DomainUrl").Value;

        public string MainUrl => $"{DomainUrl}{_config.GetSection("MainUrl").Value}";

        public string LoginUrl => $"{DomainUrl}{_config.GetSection("LoginUrl").Value}";

        public string ProjectsUrl => $"{DomainUrl}{_config.GetSection("ProjectsUrl").Value}";

        public string TestPlansUrl => $"{DomainUrl}{_config.GetSection("TestPlansUrl").Value}";

        public string TestRunsUrl => $"{DomainUrl}{_config.GetSection("TestRunsUrl").Value}";

        public string FolderPath => Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));

        public string FolderPicture => $"{FolderPath}{_config.GetSection("FolderPicture").Value}";

    }
}
