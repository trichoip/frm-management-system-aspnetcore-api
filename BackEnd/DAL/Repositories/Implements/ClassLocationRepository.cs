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
  public class ClassLocationRepository : RepositoryBase<ClassLocation>, IClassLocationRepository
  {
    private readonly FRMDbContext _dbContext;

    public ClassLocationRepository(IDbFactory dbFactory) : base(dbFactory)
    {
      _dbContext = dbFactory.Init();
    }

    public ClassLocation GetClassByIdLocation(long id)
    {
      return _dbContext.ClassLocations.FirstOrDefault(x => x.IdLocation == id);
    }


    public async Task<List<ClassLocation>> GetByClassId(long classId)
    {
      return await _dbSet.Where(x => x.IdClass == classId).Include(x => x.Location).Include(x => x.Class).ThenInclude(y => y.ClassAdmins).ThenInclude(z => z.User).ToListAsync();
    }
    public void AddLocation(ClassLocation classLocation)
    {
      _dbSet.Add(classLocation);
    }
    public void DeleteLocation(ClassLocation classLocation)
    {
      _dbSet.Remove(classLocation);
    }
        public void AddClassLocation(ClassLocation location)
        {
            _dbSet.Add(location);
            _dbContext.SaveChanges();
            return;
        }
    }
}
