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

        public SqlServerDatabaseSettings SqlServerDatabase { get; set; }

        public PostgreSqlDatabaseSettings PostgreSqlDatabase { get; set; }

        public GoogleReCaptchaSettings GoogleReCaptcha { get; set; }
    }
}
