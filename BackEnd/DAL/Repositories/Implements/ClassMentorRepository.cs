using DAL.Entities;
using DAL.Infrastructure;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories.Implements
{
    public class ClassMentorRepository : RepositoryBase<ClassMentor>, IClassMentorRepository
    {
        private readonly FRMDbContext _dbContext;
        public ClassMentorRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            this._dbContext = dbFactory.Init();
        }

        public void AddMentor(ClassMentor _classMentor)
        {
            _dbSet.Add(_classMentor);
        }
        public void DeleteMentor(ClassMentor classMentor)
        {
            _dbSet.Remove(classMentor);
        }

        public List<ClassMentor> GetCLassMentor(long idClass)
        {
            return _dbSet.Where(x => x.IdClass == idClass).ToList();
        }
        public ClassMentor GetById(long? id)
        {
            return _dbContext.classMentors.FirstOrDefault(x => x.IdUser == id);
        }
        public List<ClassMentor> GetClassMentorsById(long mentorId)
        {
            return _dbSet.Where(cm => cm.IdUser == mentorId).ToList();
        }

        #region Group 5 - GetMentorClasses
        public IQueryable<ClassMentor> GetMentorClassesQuery(long mentorId)
        {
            return _dbSet.Where(x => x.IdUser == mentorId);
        }

        public void AddMentorForImport(ClassMentor classMentor)
        {
            _dbSet.Add(classMentor);
            _dbContext.SaveChanges();
            return;
        }
        #endregion
    }
}
