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
    public class ClassTechnicalGroupRepository : RepositoryBase<ClassTechnicalGroup>, IClassTechnicalGroupRepository
    {
        private readonly FRMDbContext _dbContext;
        public ClassTechnicalGroupRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            _dbContext = dbFactory.Init();
        }

        public void CreateTecniGroupForImport(ClassTechnicalGroup classTechnicalGroup)
        {
            _dbSet.Add(classTechnicalGroup);
            _dbContext.SaveChanges();
            return;
        }

        public List<ClassTechnicalGroup> GetAll()
        {
            return this._dbSet.ToList();
        }
    }
}
