using Eadent.Common.DataAccess.EntityFramework.Databases;

namespace CentralSecurityService.Common.DataAccess.CentralSecurityService.Databases
{
    public interface ICentralSecurityServiceDatabase : IBaseDatabase
    {
        long GetNextUniqueReferenceId();
    }
}
