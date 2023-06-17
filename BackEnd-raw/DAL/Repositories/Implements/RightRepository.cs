using DAL.Entities;
using DAL.Infrastructure;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Implements
{
    public class RightRepository : RepositoryBase<Right>, IRightRepository
    {
        public RightRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        #region Group 5 - Authentication & Authorization
        public Right GetRight(long rightId)
        {
            return this._dbSet.FirstOrDefault(x => x.Id == rightId);
        }
        public IEnumerable<Right> GetAll()
        {
            return this._dbSet.ToList();
        }
        #endregion
    }
}
