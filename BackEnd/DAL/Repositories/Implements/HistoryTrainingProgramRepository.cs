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
    public class HistoryTrainingProgramRepository : RepositoryBase<HistoryTrainingProgram>, IHistoryTrainingProgramRepository
    {
        private readonly FRMDbContext _dbContext;
        public HistoryTrainingProgramRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            _dbContext = dbFactory.Init();
        }

        public void CreateHistoryTrainingProgram(HistoryTrainingProgram historyTrainingProgram)
        {
            _dbSet.Add(historyTrainingProgram);
          
        }

        public void GetHistoryTrainingProgramsById(long proId)
        {
            var list = _dbSet.Find(proId);
            
        }

        // Team6
        public IQueryable<HistoryTrainingProgram> GetHistoryTrainingQuery(long id)
        {
            return _dbSet.Where(h => h.IdProgram == id);
        }
        // Team6
    }
}
