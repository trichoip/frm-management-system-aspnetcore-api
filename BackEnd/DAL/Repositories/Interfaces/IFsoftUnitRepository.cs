using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IFsoftUnitRepository
    {
        Task<List<FsoftUnit>> GetFSUs();

        List<FsoftUnit> GetAll();
        void CreateFSUForImport(FsoftUnit fsoftUnit);
        FsoftUnit GetById(long? id);
    }
}
