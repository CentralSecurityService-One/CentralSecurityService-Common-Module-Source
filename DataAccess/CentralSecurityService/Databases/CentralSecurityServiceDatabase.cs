using CentralSecurityService.Common.Configuration;
using CentralSecurityService.Common.DataAccess.CentralSecurityService.Entities;
using Eadent.Common.Configuration;
using Eadent.Common.DataAccess.EntityFramework.Databases;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace CentralSecurityService.Common.DataAccess.CentralSecurityService.Databases
{
    public class CentralSecurityServiceDatabase : BaseDatabase, ICentralSecurityServiceDatabase
    {
        public virtual DbSet<ReferenceEntity> References { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            int databaseTypeValue = CentralSecurityServiceCommonSettings.Instance.DatabaseTypeValue;

            if (databaseTypeValue == DatabaseType.SqlServer)
            {
                modelBuilder.HasDefaultSchema(CentralSecurityServiceCommonSettings.Instance.SqlServerDatabase.DatabaseSchema);
            }
            else if (databaseTypeValue == DatabaseType.PostgreSql)
            {
                modelBuilder.HasDefaultSchema(CentralSecurityServiceCommonSettings.Instance.PostgreSqlDatabase.DatabaseSchema);
            }
            else
            {
                throw new InvalidOperationException($"Unsupported Database Type Value: {databaseTypeValue}");
            }

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public CentralSecurityServiceDatabase(DbContextOptions<CentralSecurityServiceDatabase> options) : base(options)
        {
            int databaseTypeValue = CentralSecurityServiceCommonSettings.Instance.DatabaseTypeValue;

            if (databaseTypeValue == DatabaseType.SqlServer)
            {
                DatabaseName = CentralSecurityServiceCommonSettings.Instance.SqlServerDatabase.DatabaseName;
                DatabaseSchema = CentralSecurityServiceCommonSettings.Instance.SqlServerDatabase.DatabaseSchema;
            }
            else if (databaseTypeValue == DatabaseType.PostgreSql)
            {
                DatabaseName = CentralSecurityServiceCommonSettings.Instance.PostgreSqlDatabase.DatabaseName;
                DatabaseSchema = CentralSecurityServiceCommonSettings.Instance.PostgreSqlDatabase.DatabaseSchema;
            }
            else
            {
                throw new InvalidOperationException($"Unsupported Database Type Value: {databaseTypeValue}");
            }
        }

        public long GetNextUniqueReferenceIdPostgreSql()
        {
            long nextUniqueReferenceId;

            // WARNING: Be very careful about Connections and ConnectionString usage here. The original code killed the ConnectionString.
            var connectionString = CentralSecurityServiceCommonSettings.Instance.PostgreSqlDatabase.ConnectionString;

            using (var databaseConnection = new NpgsqlConnection(connectionString))
            {
                databaseConnection.Open();

                using (var sqlCommand = databaseConnection.CreateCommand())
                {
                    // PostgreSQL sequence next value
                    var schema = CentralSecurityServiceCommonSettings.Instance.PostgreSqlDatabase.DatabaseSchema;
                    sqlCommand.CommandText = $"SELECT nextval('\"{schema}\".\"UniqueReferenceId\"');";

                    nextUniqueReferenceId = (long)sqlCommand.ExecuteScalar();
                }

                databaseConnection.Close();
            }

            return nextUniqueReferenceId;
        }

        public long GetNextUniqueReferenceIdSqlServer()
        {
            long nextUniqueReferenceId;

            // WARNING: Be very careful about Connections and ConnectionString usage here. The original code killed the ConnectionString.
            var connectionString = CentralSecurityServiceCommonSettings.Instance.SqlServerDatabase.ConnectionString;

            using (var databaseConnection = new SqlConnection(connectionString))
            {
                databaseConnection.Open();

                using (var sqlCommand = databaseConnection.CreateCommand())
                {
                    sqlCommand.CommandText = $"SELECT NEXT VALUE FOR {CentralSecurityServiceCommonSettings.Instance.SqlServerDatabase.DatabaseSchema}.UniqueReferenceId;";

                    nextUniqueReferenceId = (long)sqlCommand.ExecuteScalar();
                }

                databaseConnection.Close();
            }

            return nextUniqueReferenceId;
        }

        public long GetNextUniqueReferenceId()
        {
            int databaseTypeValue = CentralSecurityServiceCommonSettings.Instance.DatabaseTypeValue;

            if (databaseTypeValue == DatabaseType.SqlServer)
            {
                return GetNextUniqueReferenceIdSqlServer();
            }
            else if (databaseTypeValue == DatabaseType.PostgreSql)
            {
                return GetNextUniqueReferenceIdPostgreSql();
            }
            else
            {
                throw new InvalidOperationException($"Unsupported Database Type Value: {databaseTypeValue}");
            }
        }
    }
}
