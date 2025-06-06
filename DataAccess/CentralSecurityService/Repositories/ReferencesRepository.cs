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
    }
}
