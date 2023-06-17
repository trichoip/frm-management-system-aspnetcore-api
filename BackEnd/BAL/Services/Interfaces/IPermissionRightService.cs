using BAL.Models;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interfaces
{
    public interface IPermissionRightService
    {
        #region Group 5 - Authentication & Authorization
        public IEnumerable<PermissionRight> GetPermissionRightsByRoleId(long roleId);
        #endregion
        void Save();
        Task SaveAsync();
        Task<List<PermissionViewModel>> GetAllRole();

        Task<PermissionViewModel> SetPermission(long idRight, long idRole, long idPermission);
    }
}
