using CentralSecurityService.Common.DataAccess.CentralSecurityService.Entities;
using Eadent.Common.DataAccess.EntityFramework.Repositories;

namespace CentralSecurityService.Common.DataAccess.CentralSecurityService.Repositories
{
    public interface IReferencesRepository : IBaseRepository<ReferenceEntity, long>
    {
        bool ReferenceExists(string sourceReferenceName);

        bool ReferenceExistsIgnoringUniqueReferenceIdPrefix(string sourceReferenceName);
    }
}
