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
    public class HistorySyllabusRepository : RepositoryBase<HistorySyllabus>, IHistorySyllabusRepository
    {
        // Team6
        private readonly FRMDbContext _dbContext;
        public HistorySyllabusRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            _dbContext = dbFactory.Init();
        }

        public void CreateHistorySyllabus(HistorySyllabus historySyllabus)
        {
            _dbSet.Add(historySyllabus);
        }

        public void AddHistorySyllabusForImport(HistorySyllabus historySyllabus)
        {
            _dbSet.Add(historySyllabus);
            _dbContext.SaveChanges();
        }

        public IQueryable<HistorySyllabus> GetHistorySyllabus()
        {
            return _dbSet;
        }

        // Team6
        //Team4
        public List<HistorySyllabus> GetAllHistorySyllabus()
        {
            return this._dbSet.ToList();
        }
        public HistorySyllabus GetCreatedOnHistorySyllabi(long id)
        {
            DateTime a = _dbContext.HistorySyllabi.Where(s => s.IdSyllabus.Equals(id)).Min(s => s.ModifiedOn);

            return _dbContext.HistorySyllabi.FirstOrDefault(s => s.IdSyllabus.Equals(id) && s.ModifiedOn == a);
        }
    }
}