using CentralSecurityService.Common.Configuration;
using CentralSecurityService.Common.DataAccess.CentralSecurityService.Entities;
using Eadent.Common.DataAccess.EntityFramework.Databases;
using Microsoft.EntityFrameworkCore;

namespace CentralSecurityService.Common.DataAccess.CentralSecurityService.Databases
{
    public class CentralSecurityServiceDatabase : BaseDatabase, ICentralSecurityServiceDatabase
    {
        public virtual DbSet<ReferenceEntity> References { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(CentralSecurityServiceCommonSettings.Instance.Database.DatabaseSchema);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public CentralSecurityServiceDatabase(DbContextOptions<CentralSecurityServiceDatabase> options) : base(options)
        {
            DatabaseName = CentralSecurityServiceCommonSettings.Instance.Database.DatabaseName;
            DatabaseSchema = CentralSecurityServiceCommonSettings.Instance.Database.DatabaseSchema;
        }

        public long GetNextUniqueReferenceId()
        {
            long nextUniqueReferenceId;

            using (var databaseConnection = Database.GetDbConnection())
            {
                databaseConnection.Open();

                using (var sqlCommand = databaseConnection.CreateCommand())
                {
                    sqlCommand.CommandText = $"SELECT NEXT VALUE FOR {CentralSecurityServiceCommonSettings.Instance.Database.DatabaseSchema}.UniqueReferenceId;";

                    nextUniqueReferenceId = (long)sqlCommand.ExecuteScalar();
                }
            }

            return nextUniqueReferenceId;
        }
    }
}
