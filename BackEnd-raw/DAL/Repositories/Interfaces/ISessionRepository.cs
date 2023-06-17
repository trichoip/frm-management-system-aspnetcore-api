using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface ISessionRepository
    {
        //team 01
        List<Session> GetSyllabusSession(long syllabusId);
        void UpdateIndex(long id, int newIndex);
        Session GetById(long id);
        Session Create(Session session);
        void Update(Session session);
        void DeleteSession(long id);
        void Deactivate(long id);
        void Activate(long id);
        //team 01


        // Team6
        List<Session> GetSessions(long id);
        Session GetSession(long id);
        // Team6

        // Team4
        List<Session> GetAllSessions();
    }
}
