using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interfaces
{
    public interface IClassUpdateHistoryService
    {
        void Save();
        void SaveAsync();
    }
}
