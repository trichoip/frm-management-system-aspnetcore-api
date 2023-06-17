using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Repositories.Interfaces
{
  public interface IClassSiteRepository
  {
    ClassSite GetById(long? id);
        List<ClassSite> GetAll();
        void CreateClassSiteForImport(ClassSite classSite);
    }
}
