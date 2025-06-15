using CentralSecurityService.Common.DataAccess.CentralSecurityService.Databases;
using CentralSecurityService.Common.DataAccess.CentralSecurityService.Entities;
using Eadent.Common.DataAccess.EntityFramework.Repositories;

namespace CentralSecurityService.Common.DataAccess.CentralSecurityService.Repositories
{
    public class ReferencesRepository : BaseRepository<ICentralSecurityServiceDatabase, ReferenceEntity, long>, IReferencesRepository
    {
        public ReferencesRepository(ICentralSecurityServiceDatabase database) : base(database)
        {
        }

        public bool ReferenceExists(string sourceReferenceName)
        {
            return Database.References
                .Any(referenceEntity => referenceEntity.ReferenceName == sourceReferenceName);
        }

        public bool ReferenceExistsIgnoringUniqueReferenceIdPrefix(string sourceReferenceName)
        {
            // This will be translated to T-SQL SUBSTRING in SQL Server.
            return Database.References
                .Any(referenceEntity => referenceEntity.ReferenceName.Length > 17 &&
                          referenceEntity.ReferenceName.Substring(17, sourceReferenceName.Length + 17) == sourceReferenceName);
        }
    }
}
