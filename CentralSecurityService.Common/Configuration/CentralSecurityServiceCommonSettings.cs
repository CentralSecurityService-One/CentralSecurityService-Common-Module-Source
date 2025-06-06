namespace CentralSecurityService.Common.Configuration
{
    public class CentralSecurityServiceCommonSettings
    {
        public const string SectionName = "CentralSecurityServiceCommon";

        public static CentralSecurityServiceCommonSettings Instance { get; private set; }

        public CentralSecurityServiceCommonSettings()
        {
            Instance = this;
        }

        public class DatabaseSettings
        {
            public string DatabaseServer { get; set; }

            public string DatabaseName { get; set; }

            public string DatabaseSchema { get; set; }

            public string ApplicationName { get; set; }

            public string UserName { get; set; }

            public string Password { get; set; }

            public string ConnectionString => $"Server={DatabaseServer};Database={DatabaseName};Application Name={ApplicationName};User Id={UserName};Password={Password};Encrypt=false;";
        }

        public class GoogleReCaptchaSettings
        {
            public string SiteKey { get; set; }

            public string Secret { get; set; }

            public decimal MinimumScore { get; set; }
        }

        public DatabaseSettings Database { get; set; }

        public GoogleReCaptchaSettings GoogleReCaptcha { get; set; }
    }
}
