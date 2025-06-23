using CentralSecurityService.Common.DataAccess.CentralSecurityService.Entities;
using Eadent.Common.DataAccess.EntityFramework.Repositories;

namespace CentralSecurityService.Common.DataAccess.CentralSecurityService.Repositories
{
    public interface IReferencesRepository : IBaseRepository<ReferenceEntity, long>
    {
        Task<bool> ReferenceExistsAsync(string sourceReferenceName, CancellationToken cancellationToken = default);

        Task<bool> ReferenceExistsIgnoringUniqueReferenceIdPrefixAsync(string sourceReferenceName, CancellationToken cancellationToken = default);
    }
}
