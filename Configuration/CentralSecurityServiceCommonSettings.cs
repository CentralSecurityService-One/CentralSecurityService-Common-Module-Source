using Eadent.Common.Configuration;

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

        public string DatabaseTypeName { get; set; }

        // The following are Derived rather than explicitly Configured.
        private int? _databaseTypeValue;

        public int DatabaseTypeValue
        {
            get
            {
                if (_databaseTypeValue == null)
                {
                    _databaseTypeValue = DatabaseType.GetDatabaseType(DatabaseTypeName);
                }

                return _databaseTypeValue.GetValueOrDefault();
            }
        }

        public DatabaseSettings Database { get; set; }

        public SqlServerDatabaseSettings SqlServerDatabase { get; set; }

        public PostgreSqlDatabaseSettings PostgreSqlDatabase { get; set; }

        public GoogleReCaptchaSettings GoogleReCaptcha { get; set; }
    }
}
