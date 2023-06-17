using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IClassStatusRepository
    {
        ClassStatus GetClassStatusByName(string nameClassStatus);

        List<ClassStatus> GetAll();
        void CreateClassStatusForImport(ClassStatus classStatus);
    }
}
