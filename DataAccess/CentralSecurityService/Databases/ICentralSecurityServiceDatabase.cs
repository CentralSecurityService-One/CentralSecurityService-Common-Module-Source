using CentralSecurityService.Common.DataAccess.CentralSecurityService.Entities;
using Eadent.Common.DataAccess.EntityFramework.Databases;
using Microsoft.EntityFrameworkCore;

namespace CentralSecurityService.Common.DataAccess.CentralSecurityService.Databases
{
    public interface ICentralSecurityServiceDatabase : IBaseDatabase
    {
        DbSet<ReferenceEntity> References { get; set; }

        long GetNextUniqueReferenceId();
    }
}
