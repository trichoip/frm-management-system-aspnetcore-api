using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Interfaces
{
  public interface ICurriculumRepository
  {

    void DeleteCurriculum(Curriculum curriculum);
    List<Curriculum> GetCurriculum(long IdProgram);
    // Team6
    List<Curriculum> GetCurriculums(long id);
    void CreateCurriculum(Curriculum curriculum);
    

    IQueryable<Curriculum> GetCurriculumsQuery(long id);
    // Team6
    }
}
