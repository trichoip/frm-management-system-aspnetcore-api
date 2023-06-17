using BAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interfaces
{
    public interface IFsoftUnitService
    {
        Task<List<FSUViewModel>> GetFSUsAsync();
        void Save();
        void SaveAsync();
    }
}
