using BAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interfaces
{
    public interface ILocationService
    {
        Task<List<LocationViewModel>> GetLocationByKeyword(string keyword);
        void Save();
        void SaveAsync();
    }
}
