using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Role GetByID(long id);
        #region Group 5 - Authentication & Authorization
        public Role GetRole(long roleId);
        #endregion
      
        Task<Role> Create(string name);
        Role GetRoleById(long roleId);
    }
}
