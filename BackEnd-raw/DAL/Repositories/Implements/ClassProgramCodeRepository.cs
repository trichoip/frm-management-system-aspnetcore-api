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
    public class ClassProgramCodeRepository : RepositoryBase<ClassProgramCode>, IClassProgramCodeRepository
    {
        private readonly FRMDbContext _dbContext;
        public ClassProgramCodeRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            _dbContext = dbFactory.Init();
        }

        public ClassProgramCode GetById(long? id)
        {
            return _dbSet.FirstOrDefault(x => x.Id == id);


        }
        public List<ClassProgramCode> GetAll()
        {
            return this._dbSet.ToList();
        }

        public void CreateProgramCodeForImport(ClassProgramCode classProgramCode)
        {
            _dbSet.Add(classProgramCode);
            _dbContext.SaveChanges();
            return;
        }
    }
}
