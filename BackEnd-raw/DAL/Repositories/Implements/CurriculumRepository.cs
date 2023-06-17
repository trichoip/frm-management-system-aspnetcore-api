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
  public class CurriculumRepository : RepositoryBase<Curriculum>, ICurriculumRepository
  {
    private readonly FRMDbContext _dbContext;
    public CurriculumRepository(IDbFactory dbFactory) : base(dbFactory)
    {
      this._dbContext = dbFactory.Init();
    }


    public void DeleteCurriculum(Curriculum curriculum)
    {
      _dbSet.Remove(curriculum);
      // _dbContext.SaveChanges();
    }

    public List<Curriculum> GetCurriculum(long idProgram)
    {
      return _dbSet.Where(x => x.IdProgram == idProgram).ToList();
    }

    public void CreateCurriculum(Curriculum curriculum)
    {
      _dbSet.Add(curriculum);
    }

    // Team6
    public IQueryable<Curriculum> GetCurriculumsQuery(long id)
    {
      return _dbSet.Where(s => s.IdProgram == id);
    }
    // Team6

    public List<Curriculum> GetCurriculums(long id)
    {
      var curriculums = _dbSet.Where(s => s.IdProgram == id).ToList();
      return curriculums;
    }
  }
}