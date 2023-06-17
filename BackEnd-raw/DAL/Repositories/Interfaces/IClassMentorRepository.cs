using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Repositories.Interfaces
{
    public interface IClassMentorRepository
    {
        void AddMentor(ClassMentor classMentor);
        void DeleteMentor(ClassMentor classMentor);

        List<ClassMentor> GetCLassMentor(long idClass);
        ClassMentor GetById(long? id);
        IQueryable<ClassMentor> GetMentorClassesQuery(long mentorId);
        //team4
        void AddMentorForImport(ClassMentor classMentor);
    }
}
