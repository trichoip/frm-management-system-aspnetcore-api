using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface ILessonRepository
    {
        //team 01
        void Deactivate(long id);
        void Activate(long id);
        void DeleteLesson(long id);
        List<Lesson> GetAllUnitLessons(long unitId);
        void UpdateLesson(Lesson lesson);
        Lesson GetById(long id);
        Lesson Create(Lesson lesson);
        //team 6
        List<Lesson> GetLessons(long id);
        //team 6
        //team 4
        List<Lesson> GetAllLessons();
        List<Lesson> GetUnitLessons(long idUnit);
    }
}
