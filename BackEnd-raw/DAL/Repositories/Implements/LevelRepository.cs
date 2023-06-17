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
    public class LevelRepository : RepositoryBase<Level>, ILevelRepository
    {
        private FRMDbContext _dbContext;
        public LevelRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            _dbContext = dbFactory.Init();
        }
        public Level GetById(long Id)
        {
            return _dbSet.FirstOrDefault(x=>x.Id==Id);
        }
        public List<Level> GetAll()
        {
            return this._dbSet.ToList();
        }

    }
}
