using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Repositories.Interfaces
{
  public interface IClassUpdateHistoryRepository
  {
    void CreateClassHistoy(ClassUpdateHistory ClassHistory);
  }
}
