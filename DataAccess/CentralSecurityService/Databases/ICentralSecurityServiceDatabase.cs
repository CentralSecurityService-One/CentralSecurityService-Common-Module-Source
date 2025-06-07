using CentralSecurityService.Common.DataAccess.CentralSecurityService.Entities;
using Eadent.Common.DataAccess.EntityFramework.Databases;

namespace CentralSecurityService.Common.DataAccess.CentralSecurityService.Databases
{
    public interface ICentralSecurityServiceDatabase : IBaseDatabase
    {
        long GetNextUniqueReferenceId();

        void AddReference(ReferenceEntity referenceEntity);
    }
}
