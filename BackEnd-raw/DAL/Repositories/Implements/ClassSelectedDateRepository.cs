using DAL.Entities;
using DAL.Infrastructure;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Implements
{
    public class ClassSelectedDateRepository : RepositoryBase<ClassSelectedDate>, IClassSelectedDateRepository
    {
        private readonly FRMDbContext _dbContext;

        public ClassSelectedDateRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            _dbContext = dbFactory.Init();
        }

        public ClassSelectedDate GetByIdClass(long idClass)
        {
            return _dbContext.ClassSelectedDates.FirstOrDefault(x => x.IdClass == idClass);
        }

        public ClassSelectedDate GetByIdClass(long idClass, DateTime? date)
        {
            ClassSelectedDate classSelectedDate;

            if (!date.HasValue)
            {
                classSelectedDate = _dbContext.ClassSelectedDates.FirstOrDefault(x => x.IdClass == idClass);
            }
            else
            {
                classSelectedDate = _dbContext.ClassSelectedDates.FirstOrDefault(x => x.IdClass == idClass && x.ActiveDate == date);
            }

            return classSelectedDate;
        }

        public ClassSelectedDate GetByIdClassFilter(long idClass, long idClassFilter, DateTime? date)
        {
            ClassSelectedDate classSelectedDate;

            if (!date.HasValue)
            {
                classSelectedDate = _dbContext.ClassSelectedDates.FirstOrDefault(x => x.IdClass == idClass && x.IdClass == idClassFilter);
            }
            else
            {
                classSelectedDate = _dbContext.ClassSelectedDates.FirstOrDefault(x => x.IdClass == idClass && x.IdClass == idClassFilter && x.ActiveDate == date);
            }

            return classSelectedDate;
        }

        public IEnumerable<ClassSelectedDate> GetClassSelectedDateAll(DateTime? date)
        {
            IEnumerable<ClassSelectedDate> classSelectedDateList;

            if (!date.HasValue)
            {
                classSelectedDateList = _dbContext.ClassSelectedDates.ToList();
            }
            else
            {
                classSelectedDateList = _dbContext.ClassSelectedDates.Where(c => c.ActiveDate == date).ToList();
            }

            return classSelectedDateList;
        }

        public IEnumerable<ClassSelectedDate> GetClassSelectedDateByID(long idClass, DateTime? date)
        {
            IEnumerable<ClassSelectedDate> classSelectedDateList;

            if (!date.HasValue)
            {
                classSelectedDateList = _dbContext.ClassSelectedDates.Where(c => c.IdClass == idClass).ToList();
            }
            else
            {
                classSelectedDateList = _dbContext.ClassSelectedDates.Where(c => c.IdClass == idClass && c.ActiveDate == date).ToList();
            }

            return classSelectedDateList;
        }
        public void AddSeletedDate(ClassSelectedDate _classSelectedDate)
        {
            _dbSet.Add(_classSelectedDate);
        }

        public List<ClassSelectedDate> GetSeletedDateListById(long ClassId)
        {
            var list = _dbSet.Where(x => x.IdClass == ClassId && x.Status == 1).ToList();
            return list;
        }

        public async Task<List<ClassSelectedDate>> GetSelectedDatesQueryAsync()
        {
            return await _dbSet.Include(x => x.Class)
                            .ThenInclude(x => x.ClassMentors).ThenInclude(x => x.User)
                            .Include(x => x.Class).ThenInclude(x => x.ClassAdmins).ThenInclude(x => x.User)
                            .Include(x => x.Class).ThenInclude(x => x.Locations).ThenInclude(x => x.Location)
                            .Include(x => x.Class).ThenInclude(x => x.ClassTrainees)
                            .Include(x => x.Class).ThenInclude(x => x.ClassStatus)
                            .Include(x => x.Class).ThenInclude(x => x.AttendeeType)
                            .Include(x => x.Class).ThenInclude(x => x.ClassMentors)
                            .ToListAsync();
        }
    }
}
