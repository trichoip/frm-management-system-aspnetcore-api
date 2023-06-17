using DAL.Entities;
using DAL.Infrastructure;
using DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Implements
{
    public class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
        private readonly FRMDbContext _dbContext;

        public RoleRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            _dbContext = dbFactory.Init();
        }
        public Role GetRoleById(long roleId)
        {
            return _dbSet.FirstOrDefault(r => r.Id == roleId);
        }

        public Role GetByID(long id)
        {
            return _dbContext.Roles.FirstOrDefault(x => x.Id == id);
        }

        #region Group 5 - Authentication & Authorization
        public Role GetRole(long roleId)
        {
            return this._dbSet.FirstOrDefault(x => x.Id == roleId);
        }
        #endregion

        public async Task<Role> Create(string name)
        {
            var role = new Role {  Name= name};
            await _dbContext.AddAsync(role);
            await _dbContext.SaveChangesAsync();
            return role;
        }

       
    }      
}

