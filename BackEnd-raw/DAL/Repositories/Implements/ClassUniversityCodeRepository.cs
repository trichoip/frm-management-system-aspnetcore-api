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
    public class ClassUniversityCodeRepository : RepositoryBase<ClassUniversityCode>, IClassUniversityCodeRepository
    {
        private readonly FRMDbContext _dbContext;
        public ClassUniversityCodeRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            _dbContext = dbFactory.Init();
        }

        public void CreateUniCodeForImport(ClassUniversityCode classUniversityCode)
        {
            _dbSet.Add(classUniversityCode);
            _dbContext.SaveChanges();
            return;
        }

        public List<ClassUniversityCode> GetAll()
        {
            return this._dbSet.ToList();
        }
    }
}
