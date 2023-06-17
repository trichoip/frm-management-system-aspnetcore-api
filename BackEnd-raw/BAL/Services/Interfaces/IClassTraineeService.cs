using BAL.Models;
using DAL.Entities;

namespace BAL.Services.Interfaces
{
    public interface IClassTraineeService
    {
        ClassTrainee GetById(long id);
        Task<List<Class>> GetClassesById(long traineeId);
        Task<List<ClassTraineeViewModel>> GetTraineeClasses(long traineeId);
        void Save();
        void SaveAsync();
    }
}
