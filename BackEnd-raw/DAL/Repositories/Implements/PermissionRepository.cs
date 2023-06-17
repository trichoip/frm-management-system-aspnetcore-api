using DAL.Entities;
using DAL.Infrastructure;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Implements
{
    public class PermissionRepository : RepositoryBase<Permission>, IPermissionRepository
    {
        public PermissionRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
       

        #region Group 5 - Authentication & Authorization
        public IEnumerable<Permission> GetAll()
        {
            return this._dbSet.ToList();
        }

        public Permission GetPermission(long permissionId)
        {
            return this._dbSet.FirstOrDefault(x => x.Id == permissionId);
        }
        #endregion
    }
}
