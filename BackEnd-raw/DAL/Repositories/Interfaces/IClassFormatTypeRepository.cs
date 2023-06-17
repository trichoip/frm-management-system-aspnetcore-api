using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IClassFormatTypeRepository
    {
        List<ClassFormatType> GetAll();
        void CreateFormatTypeForImport(ClassFormatType classFormatType);
    }
}
