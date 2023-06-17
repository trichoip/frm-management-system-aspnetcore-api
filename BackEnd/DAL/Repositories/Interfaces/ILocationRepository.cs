using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
  public interface ILocationRepository
  {
    Location GetLocationByName(string nameLocation);
    Task<List<Location>> GetLocationByKeyword(string keyword);
    Location GetById(long id);
  }
}
