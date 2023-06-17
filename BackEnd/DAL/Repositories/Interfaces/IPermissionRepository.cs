using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IPermissionRepository
    {
        #region Group 5 - Authentication & Authorization
        public Permission GetPermission(long permissionId);
        public IEnumerable<Permission> GetAll();
        #endregion
    }
}
