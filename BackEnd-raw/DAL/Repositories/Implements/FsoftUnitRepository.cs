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
    public class FsoftUnitRepository : RepositoryBase<FsoftUnit>, IFsoftUnitRepository
    {
        private readonly FRMDbContext _dbContext;
        public FsoftUnitRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            _dbContext = dbFactory.Init();
        }

        public void CreateFSUForImport(FsoftUnit fsoftUnit)
        {
            _dbSet.Add(fsoftUnit);
            _dbContext.SaveChanges();
            return;
        }

        public List<FsoftUnit> GetAll()
        {
            return this._dbSet.ToList();
        }

        public  async Task<List<FsoftUnit>> GetFSUs()
        {
            return await this._dbSet.ToListAsync();
        }

        public FsoftUnit GetById(long? id)
        {
            return _dbSet.FirstOrDefault(s => s.Id == id);
        }

    }
}
