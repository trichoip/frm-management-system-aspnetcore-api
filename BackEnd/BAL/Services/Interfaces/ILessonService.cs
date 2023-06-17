using BAL.Models;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interfaces
{
    public interface ILessonService
    {
        // Team6
        List<LessonViewModel> GetLessons(long id);
        // Team6
        void Save();
        void SaveAsync();
    }
}
