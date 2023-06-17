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
    public class PermissionRightRepository : RepositoryBase<PermissionRight>, IPermissionRightRepository
    {
        private readonly FRMDbContext _dbContext;

        public PermissionRightRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            _dbContext = dbFactory.Init();
        }


        public List<PermissionRight> GetAllRole()
        {
            return _dbSet.OrderBy(x => x.IdRight).ToList();
        }

        public async Task<PermissionRight> SetPermission(long idRight, long idRole, long idPermission)
        {
            var exist = await _dbSet.FirstOrDefaultAsync(x => x.IdRight == idRight && x.IdRole == idRole);
            if (exist != null)
            {
                exist.IdPermission = idPermission;
                _dbSet.Update(exist);
                //Save Changes by UnitOfWork Commit()
            }
            else
            {
                throw new Exception("No User with that id");

            }
            return exist;
        }

        #region Group 5 - Authentication & Authorization
        public IEnumerable<PermissionRight> GetPermissionsRightsByRoleId(long roleId)
        {
            return this._dbSet.Where(x => x.IdRole == roleId);
        }
        #endregion
    }
}
