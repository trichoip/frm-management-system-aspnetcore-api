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
  public class ClassUpdateHistoryRepository : RepositoryBase<ClassUpdateHistory>, IClassUpdateHistoryRepository
  {
    public ClassUpdateHistoryRepository(IDbFactory dbFactory) : base(dbFactory)
    {
    }

    public void CreateClassHistoy(ClassUpdateHistory ClassHistory)
    {

      _dbSet.Add(ClassHistory);

    }
  }
}
