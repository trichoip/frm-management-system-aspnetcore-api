using BAL.Models;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interfaces
{
    public interface ISessionService
    {

        // Team6
        List<SessionViewModel> GetSessions(long id);
        // Team6
        void Save();
        void SaveAsync();
    }
}
