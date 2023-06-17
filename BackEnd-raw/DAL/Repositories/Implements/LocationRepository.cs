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
  public class LocationRepository : RepositoryBase<Location>, ILocationRepository
  {
    private readonly FRMDbContext _dbContext;

    public LocationRepository(IDbFactory dbFactory) : base(dbFactory)
    {
      _dbContext = dbFactory.Init();
    }

    public Location GetLocationByName(string nameLocation)
    {
      return _dbContext.Locations.FirstOrDefault(x => x.Name.Equals(nameLocation));
    }

    public async Task<List<Location>> GetLocationByKeyword(string keyword)
    {
      return await _dbSet.Where(l => l.Name.Contains(keyword)).ToListAsync();
    }

    public Location GetById(long id)
    {
      return _dbSet.Find(id);
    }
  }
}
