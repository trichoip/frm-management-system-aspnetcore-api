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
    public class ClassStatusRepository : RepositoryBase<ClassStatus>, IClassStatusRepository
    {
        private readonly FRMDbContext _dbContext;

        public ClassStatusRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            _dbContext = dbFactory.Init();
        }

        public void CreateClassStatusForImport(ClassStatus classStatus)
        {
            _dbSet.Add(classStatus);
            _dbContext.SaveChanges();
            return;
        }

        public List<ClassStatus> GetAll()
        {
            return this._dbSet.ToList();
        }

        public ClassStatus GetClassStatusByName(string nameClassStatus)
        {
            return _dbContext.ClassStatuses.FirstOrDefault(x => x.Name.Equals(nameClassStatus));
        }
    }
}
