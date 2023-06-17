using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface ISyllabusRepository
    {
        List<Syllabus> GetSyllabi(long idClass);

        //team01
        Syllabus GetById(long id);
        void CreateSyllabus(Syllabus syllabus);
        void UpdateSyllabus(Syllabus syllabus);
        //team01
        List<Syllabus> GetAll();
        // Team6
        Syllabus GetDetailById(long id);
        IQueryable<Syllabus> SearchSyllabusByNameQuery(string name);
        // Team6

        //team4
        List<Syllabus> SearchAndFilterSyllabus(List<string> keywords, DateTime? from, DateTime? to);
        void CreateSyllabusForImport(Syllabus syllabus);

    }
}
