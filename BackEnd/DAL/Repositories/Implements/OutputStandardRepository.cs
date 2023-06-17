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
    public class OutputStandardRepository : RepositoryBase<OutputStandard>, IOutputStandardRepository
    {
        private readonly FRMDbContext _dbContext;
        public OutputStandardRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            _dbContext = dbFactory.Init();
        }

        public List<OutputStandard> GetAll()
        {
            return this._dbSet.ToList();
        }

        public OutputStandard GetOutputStandard(long id)
        {
            return _dbContext.OutputStandards.First(s => s.Id == id);
        }

    }
}
