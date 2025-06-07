using CentralSecurityService.Common.Configuration;
using CentralSecurityService.Common.DataAccess.CentralSecurityService.Entities;
using Eadent.Common.DataAccess.EntityFramework.Databases;
using Microsoft.Data.SqlClient;
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

        public void AddReference(ReferenceEntity entity)
        {
            using (var databaseConnection = Database.GetDbConnection())
            {
                databaseConnection.Open();

                using (var command = databaseConnection.CreateCommand())
                {
                    command.CommandText = @"
                        INSERT INTO [Reference] (
                            UniqueReferenceId,
                            SubReferenceId,
                            ReferenceTypeId,
                            ThumbnailRelativeFileName,
                            ReferenceName,
                            SubjectNames,
                            Categorisations,
                            CreatedDateTimeUtc,
                            LastUpdatedDateTimeUtc
                        ) VALUES (
                            @UniqueReferenceId,
                            @SubReferenceId,
                            @ReferenceTypeId,
                            @ThumbnailRelativeFileName,
                            @ReferenceName,
                            @SubjectNames,
                            @Categorisations,
                            @CreatedDateTimeUtc,
                            @LastUpdatedDateTimeUtc
                        );
                    ";

                command.Parameters.Add(new SqlParameter("@UniqueReferenceId", entity.UniqueReferenceId));
                command.Parameters.Add(new SqlParameter("@SubReferenceId", entity.SubReferenceId));
                command.Parameters.Add(new SqlParameter("@ReferenceTypeId", (short)entity.ReferenceTypeId));
                command.Parameters.Add(new SqlParameter("@ThumbnailRelativeFileName", (object?)entity.ThumbnailRelativeFileName ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@ReferenceName", (object?)entity.ReferenceName ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@SubjectNames", (object?)entity.SubjectNames ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@Categorisations", (object?)entity.Categorisations ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@CreatedDateTimeUtc", entity.CreatedDateTimeUtc));
                command.Parameters.Add(new SqlParameter("@LastUpdatedDateTimeUtc", (object?)entity.LastUpdatedDateTimeUtc ?? DBNull.Value));

                command.ExecuteNonQuery();
            }
        }
    }
    }
}
