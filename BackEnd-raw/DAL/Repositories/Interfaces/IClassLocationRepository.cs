using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
  public interface IClassLocationRepository
  {
    public Task<List<ClassLocation>> GetByClassId(long classId);
    ClassLocation GetClassByIdLocation(long id);
    void AddLocation(ClassLocation classLocation);
    void DeleteLocation(ClassLocation classLocation);
        void AddClassLocation(ClassLocation location);
    }
}
