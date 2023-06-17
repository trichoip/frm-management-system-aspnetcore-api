using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Infrastructure
{
    public class RepositoryBase<T> where T : class
    {
        private FRMDbContext _dbContext;
        protected DbSet<T> _dbSet { get; private set; }

        private IDbFactory DbFactory { get; }

        private FRMDbContext DbContext
        {
            get
            {
                if (_dbContext == null)
                {
                    _dbContext = DbFactory.Init();
                }
                return _dbContext;
            }
        }

        protected RepositoryBase(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
                        
            _dbSet = DbContext.Set<T>();
        }
    }
}
