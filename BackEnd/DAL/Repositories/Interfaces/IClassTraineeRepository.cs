using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Repositories.Interfaces
{
    public interface IClassTraineeRepository
    {
        void AddTrainee(ClassTrainee classTrainee);

        void DeleteTrainee(ClassTrainee classtrainee);

        List<ClassTrainee> GetCLassTrainee(long idClass);
        ClassTrainee GetById(long id);
        IQueryable<ClassTrainee> GetClassTraineesQuery(long traineeId);
    }
}
