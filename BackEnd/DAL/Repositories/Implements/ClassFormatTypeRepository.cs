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
    public class ClassFormatTypeRepository : RepositoryBase<ClassFormatType>, IClassFormatTypeRepository
    {
        private readonly FRMDbContext _dbContext;
        public ClassFormatTypeRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            _dbContext = dbFactory.Init();
        }

        public void CreateFormatTypeForImport(ClassFormatType classFormatType)
        {
            _dbSet.Add(classFormatType);
            _dbContext.SaveChanges();
            return;
        }

        public List<ClassFormatType> GetAll()
        {
            return this._dbSet.ToList();
        }
    }
}
