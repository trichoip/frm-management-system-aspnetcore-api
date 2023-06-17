using DAL.Entities;
using DAL.Infrastructure;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Implements
{
    public class SyllabusRepository : RepositoryBase<Syllabus>, ISyllabusRepository
    {
        private readonly FRMDbContext _dbContext;
        public SyllabusRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            _dbContext = dbFactory.Init();
        }

        public void CreateSyllabus(Syllabus syllabus)
        {
            _dbSet.Add(syllabus);
        }

        public void UpdateSyllabus(Syllabus syllabus)
        {
            var result = _dbSet.FirstOrDefault(x => x.Id == syllabus.Id && x.Status != 3);
            if (result != null)
            {
                _dbSet.Update(syllabus);
            }
        }

        public Syllabus GetById(long id)
        {
            var result = _dbSet
                .Include(x => x.AssignmentSchema)
                .Include(x => x.Level)
                .Include(x => x.HistorySyllabi)
                .Include(x => x.Sessions.Where(x => x.Status != 3))
                                    .ThenInclude(x => x.Units.Where(x => x.Status != 3))
                                    .ThenInclude(x => x.Lessons.Where(x => x.Status != 3))
                                    .ThenInclude(x => x.Materials.Where(x => x.Status != 3))
                .Include(x => x.Sessions.Where(x => x.Status != 3))
                                    .ThenInclude(x => x.Units.Where(x => x.Status != 3))
                                    .ThenInclude(x => x.Lessons.Where(x => x.Status != 3))
                                    .ThenInclude(x => x.DeliveryType)
                .Include(x => x.Sessions.Where(x => x.Status != 3))
                                    .ThenInclude(x => x.Units.Where(x => x.Status != 3))
                                    .ThenInclude(x => x.Lessons.Where(x => x.Status != 3))
                                    .ThenInclude(x => x.FormatType)
                .Include(x => x.Sessions.Where(x => x.Status != 3))
                                    .ThenInclude(x => x.Units.Where(x => x.Status != 3))
                                    .ThenInclude(x => x.Lessons.Where(x => x.Status != 3))
                                    .ThenInclude(x => x.OutputStandard)
                .FirstOrDefault(x => x.Id == id && x.Status != 3);
            return result;
        }

        public List<Syllabus> GetSyllabi(long idClass)
        {
            return _dbSet.Where(x => x.Id == idClass).ToList();
        }

        public List<Syllabus> GetAll()
        {
            return this._dbSet.ToList();
        }

        public Syllabus GetDetailById(long id)
        {
            return _dbSet.FirstOrDefault(s => s.Id == id);
        }

        public IQueryable<Syllabus> SearchSyllabusByNameQuery(string name)
        {
            return _dbSet.Where(n => n.Name.Contains(name));
        }

        public List<Syllabus> SearchAndFilterSyllabus(List<string> keywords, DateTime? from, DateTime? to)
        {
            if (from == null) from = DateTime.Parse("1754-01-01");
            if (to == null) to = DateTime.Parse("9999-12-31");
            if (_dbContext.Syllabi == null) throw new Exception("Connection failed");
            List<Syllabus> syl = new List<Syllabus>();
            if (keywords != null && keywords.Count != 0)
            {
                for (int i = 0; i < keywords.Count; i++)
                {
                    if (keywords[i] == null) keywords[i] = "";
                    //if (keywords[i] != null)
                    //{
                    if (i == 0)
                    {
                        syl = _dbContext.Syllabi.Include("HistorySyllabi").Where(s => s.HistorySyllabi.Min(x => x.ModifiedOn) >= from && s.HistorySyllabi.Min(x => x.ModifiedOn) <= to).Where(s => s.Name.ToLower().Contains(keywords[i].ToLower()) || s.Code.ToLower().Contains(keywords[i].ToLower())).ToList();
                    }
                    else
                    {
                        syl = syl.Where(s => s.HistorySyllabi.Min(x => x.ModifiedOn) >= from && s.HistorySyllabi.Min(x => x.ModifiedOn) <= to).Where(s => s.Name.ToLower().Contains(keywords[i].ToLower()) || s.Code.ToLower().Contains(keywords[i].ToLower())).ToList();
                    }
                        //}
                }
            }
            else
            {
                syl = _dbContext.Syllabi.Include("HistorySyllabi").Where(s => s.HistorySyllabi.Min(x => x.ModifiedOn) >= from && s.HistorySyllabi.Min(x => x.ModifiedOn) <= to).ToList();
            }
            

                return syl;

        }
        public void CreateSyllabusForImport(Syllabus syllabus)
        {
            _dbSet.Add(syllabus);
            _dbContext.SaveChanges();
            return;
        }


    }
}
