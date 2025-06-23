using CentralSecurityService.Common.DataAccess.CentralSecurityService.Databases;
using CentralSecurityService.Common.DataAccess.CentralSecurityService.Entities;
using Eadent.Common.DataAccess.EntityFramework.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CentralSecurityService.Common.DataAccess.CentralSecurityService.Repositories
{
    public class ReferencesRepository : BaseRepository<ICentralSecurityServiceDatabase, ReferenceEntity, long>, IReferencesRepository
    {
        public ReferencesRepository(ICentralSecurityServiceDatabase database) : base(database)
        {
        }

        public Task<bool> ReferenceExistsAsync(string sourceReferenceName, CancellationToken cancellationToken = default)
        {
            return Database.References
                .AnyAsync(referenceEntity => referenceEntity.ReferenceName == sourceReferenceName, cancellationToken);
        }

        public Task<bool> ReferenceExistsIgnoringUniqueReferenceIdPrefixAsync(string sourceReferenceName, CancellationToken cancellationToken = default)
        {
            // This will be translated to T-SQL SUBSTRING in SQL Server.
            return Database.References
                .AnyAsync(referenceEntity => referenceEntity.ReferenceName.Length > 17 &&
                     referenceEntity.ReferenceName.Substring(17, sourceReferenceName.Length + 17) == sourceReferenceName, cancellationToken);
        }
    }
}
