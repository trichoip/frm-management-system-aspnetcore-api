using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IPermissionRightRepository
    {
        List<PermissionRight> GetAllRole();
        Task<PermissionRight> SetPermission(long idRight, long idRole, long idPermission);
        #region Group 5 - Authentication & Authorization
        public IEnumerable<PermissionRight> GetPermissionsRightsByRoleId(long roleId);
        #endregion
    }
}
