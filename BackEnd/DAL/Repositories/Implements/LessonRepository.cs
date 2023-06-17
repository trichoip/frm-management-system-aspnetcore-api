using DAL.Entities;
using DAL.Infrastructure;
using DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace DAL.Repositories.Implements
{
    public class LessonRepository : RepositoryBase<Lesson>, ILessonRepository
    {
        // Team6
        private readonly FRMDbContext _dbContext;
        public LessonRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            _dbContext = dbFactory.Init();
        }

        public List<Lesson> GetAllUnitLessons(long unitId)
        {
            return _dbSet.Where(x=>x.IdUnit==unitId&& x.Status!=3).ToList();
        }
        public Lesson Create(Lesson lesson)
        {
            _dbSet.Add(lesson);
            return lesson;
        }

        public void UpdateLesson(Lesson lesson)
        {
            _dbSet.Update(lesson);
        }

        public Lesson GetById(long id)
        {
            return _dbSet.FirstOrDefault(x => x.Id == id && x.Status != 3);
        }
        public List<Lesson> GetLessons(long id)
        {
            return _dbSet.Where(src => src.IdUnit == id && src.Status == 1).ToList();
        }

        public List<Lesson> GetAllLessons()
        {
            return this._dbSet.ToList();
        }


        // team 01
        public void Deactivate(long id)
        {
            var result = _dbSet.FirstOrDefault(x => x.Id == id);
            if (result != null && result.Status != 3)
            {
                result.Status = 0;
                _dbSet.Update(result);

                //Save Changes by UnitOfWork Commit()
            }
            else
            {
                throw new Exception("No lesson with that id");
            }
        }
        public void Activate(long id)
        {
            var result = _dbSet.FirstOrDefault(x => x.Id == id);
            if (result != null && result.Status != 3)
            {
                result.Status = 1;
                _dbSet.Update(result);

                //Save Changes by UnitOfWork Commit()
            }
            else
            {
                throw new Exception("No lesson with that id");
            }
        }

        public void DeleteLesson(long id)
        {
            var result = _dbSet.FirstOrDefault(x => x.Id == id);
            if (result != null && result.Status != 3)
            {
                result.Status = 3;
                _dbSet.Update(result);
            }
            else
            {
                throw new Exception("No lesson with that id");
            }
        }
        public List<Lesson> GetUnitLessons(long idUnit)
        {
            return _dbContext.Lessons.Where(S => S.IdUnit == idUnit).Select(s => s).ToList();
        }

    }
}

