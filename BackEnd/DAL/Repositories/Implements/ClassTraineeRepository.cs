using DAL.Entities;
using DAL.Infrastructure;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories.Implements
{
    public class ClassTraineeRepository : RepositoryBase<ClassTrainee>, IClassTraineeRepository
    {
        private readonly FRMDbContext _dbContext;
        public ClassTraineeRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            this._dbContext = dbFactory.Init();
        }

        public void AddTrainee(ClassTrainee classTrainee)
        {

            _dbSet.Add(classTrainee);
        }
        public void DeleteTrainee(ClassTrainee classTrainee)
        {
            _dbSet.Remove(classTrainee);
        }

        public List<ClassTrainee> GetCLassTrainee(long idClass)
        {
            return _dbSet.Where(x => x.IdClass == idClass).ToList();
        }
        public ClassTrainee GetById(long id)
        {
            return _dbContext.ClassTrainees.FirstOrDefault(x => x.IdUser == id);
        }
        #region Group 5 - GetTraineeClasses
        public IQueryable<ClassTrainee> GetClassTraineesQuery(long traineeId)
        {
            return _dbSet.Where(ct => ct.IdUser == traineeId);
        }
        #endregion
    }
}
