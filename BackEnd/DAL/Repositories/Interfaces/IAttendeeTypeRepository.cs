using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
  public interface IAttendeeTypeRepository
  {
    AttendeeType GetAttendeeTypeByName(string nameAttendeeType);
    AttendeeType GetById(long? id);
        List<AttendeeType> GetAll();
        void CreateAttendeeTypeForImport(AttendeeType attendeeType);
    }
}
