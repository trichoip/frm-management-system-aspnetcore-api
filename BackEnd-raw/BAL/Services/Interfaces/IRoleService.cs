using BAL.Models;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAL.Models;

namespace BAL.Services.Interfaces
{
    public interface IRoleService
    {
        RoleViewModel GetByID(long id);
        #region Group 5 - Authentication & Authorization
        public Role GetRole(long roleId);
        #endregion
        RoleViewModel GetRoleById(long roleId);
        void Save();
        void SaveAsync();
        Task<List<PermissionViewModel>> GetAllRole();
        Task<Role> AddNewRoleAsync(string name);

    }
}
