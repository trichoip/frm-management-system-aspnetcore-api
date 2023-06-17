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
  public class ClassSiteRepository : RepositoryBase<ClassSite>, IClassSiteRepository
  {
    private readonly FRMDbContext _dbContext;

    public ClassSiteRepository(IDbFactory dbFactory) : base(dbFactory)
    {
      _dbContext = dbFactory.Init();
    }

    public ClassSite GetById(long? id)
    {
      return _dbSet.FirstOrDefault(x => x.Id == id);
    }
        public List<ClassSite> GetAll()
        {
            return this._dbSet.ToList();
        }

        public void CreateClassSiteForImport(ClassSite classSite)
        {
            _dbSet.Add(classSite);
            _dbContext.SaveChanges();
            return;
        }
    }
}
