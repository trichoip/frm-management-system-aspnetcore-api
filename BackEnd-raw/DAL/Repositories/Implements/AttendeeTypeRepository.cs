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
  public class AttendeeTypeRepository : RepositoryBase<AttendeeType>, IAttendeeTypeRepository
  {
    private readonly FRMDbContext _dbContext;

    public AttendeeTypeRepository(IDbFactory dbFactory) : base(dbFactory)
    {
      _dbContext = dbFactory.Init();
    }

    public AttendeeType GetAttendeeTypeByName(string nameAttendeeType)
    {
      return _dbContext.AttendeeTypes.FirstOrDefault(x => x.Name.Equals(nameAttendeeType));
    }

    public AttendeeType GetById(long? id)
    {
      return _dbSet.FirstOrDefault(x => x.Id == id);
    }
        public List<AttendeeType> GetAll()
        {
            return this._dbSet.ToList();
        }

        public void CreateAttendeeTypeForImport(AttendeeType attendeeType)
        {
            _dbSet.Add(attendeeType);
            _dbContext.SaveChanges();
            return;
        }
    }
}
