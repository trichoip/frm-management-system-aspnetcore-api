using DAL.Entities;
using DAL.Infrastructure;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Implements
{
    public class SessionRepository : RepositoryBase<Session>, ISessionRepository
    {
        // Team6
        private readonly FRMDbContext _dbContext;
        public SessionRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            _dbContext = dbFactory.Init();
        }

        public List<Session> GetSyllabusSession(long syllabusId)
        {
            return _dbSet.Where(s => s.IdSyllabus == syllabusId
             && s.Status != 3).ToList();
        }

        public void UpdateIndex(long id, int newIndex)
        {
            var result = _dbSet.FirstOrDefault(x => x.Id == id && x.Status!=3);
            if(result != null)
            {
                result.Index = newIndex;
                _dbSet.Update(result);

                //Save change in Unit Of Work Commit()
            }
        }
        public Session Create(Session session)
        {
            _dbSet.Add(session);
            return session;
        }
        public void Delete(long id)
        {
            var result = _dbSet.FirstOrDefault(x => x.Id == id);
            _dbSet.Remove(result);
        }
        //team 01
        public void Deactivate(long id)
        {
            var result = _dbSet.FirstOrDefault(x => x.Id == id);
            if (result != null && result.Status != 3)
            {
                result.Status = 0;
                _dbSet.Update(result);

                //Save Changes by UnitOfWork Commit()
            }
            else
            {
                throw new Exception("No session with that id");
            }
        }
        public void Activate(long id)
        {
            var result = _dbSet.FirstOrDefault(x => x.Id == id);
            if (result != null && result.Status != 3)
            {
                result.Status = 1;
                _dbSet.Update(result);

                //Save Changes by UnitOfWork Commit()
            }
            else
            {
                throw new Exception("No session with that id");
            }
        }

        public void DeleteSession(long id)
        {
            var result = _dbSet.FirstOrDefault(x => x.Id == id);
            if (result != null && result.Status != 3)
            {
                result.Status = 3;
                _dbSet.Update(result);
            }
            else
            {
                throw new Exception("No session with that id");
            }
        }

        public Session GetById(long id)
        {
            return _dbSet.FirstOrDefault(x => x.Id == id && x.Status != 3);
        }

        public void Update(Session session)
        {
            _dbSet.Update(session);
        }

        public List<Session> GetSessions(long id)
        {

            var sessions = _dbSet.Where(s => s.IdSyllabus == id && s.Status != 3).ToList();
            return sessions;
        }

        public Session GetSession(long id)
        {
           return _dbSet.FirstOrDefault(s => s.IdSyllabus == id);
        }
        // Team6

        public List<Session> GetAllSessions()
        {
            return this._dbSet.ToList();
        }
                    
    }
}
