using DAL.Entities;
using BAL.Models;

namespace BAL.Services.Interfaces
{
    public interface IClassMentorService
    {
        Task<List<Class>> GetClassesById(long mentorId);
        Task<List<ClassMentorViewModel>> GetMentorClasses(long mentorId);
        ClassMentor GetById(long id);
        void Save();
        void SaveAsync();
    }
}
