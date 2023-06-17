using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Repositories.Interfaces
{
  public interface IClassProgramCodeRepository
  {
    ClassProgramCode GetById(long? id);
        List<ClassProgramCode> GetAll();
        void CreateProgramCodeForImport(ClassProgramCode classProgramCode);
    }
}
