using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Repositories.Interfaces
{
    public interface IClassAdminReporitory
    {
        void AddAdmin(ClassAdmin classAdmin);
        void DeleteAdmin(ClassAdmin classAdmin);

        List<ClassAdmin> GetCLassAdmin(long idClass);
        ClassAdmin GetById(long id);
        IQueryable<ClassAdmin> GetClassAdminsById(long adminId);

        //team4
        void AddAdminForImport(ClassAdmin classAdmin);
    }
}
