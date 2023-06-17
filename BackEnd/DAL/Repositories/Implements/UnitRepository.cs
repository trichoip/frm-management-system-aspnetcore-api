using DAL.Entities;
using DAL.Infrastructure;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories.Implements
{
    public class UnitRepository : RepositoryBase<Unit>, IUnitRepository
    {
        private readonly FRMDbContext _dbContext;
        public UnitRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            _dbContext = dbFactory.Init();
        }

        public List<Unit> GetAllSessionUnit(long sessionId)
        {
            return _dbSet.Where(x=>x.IdSession==sessionId&& x.Status!=3).ToList<Unit>();
        }

        public void UpdateIndex(long id, int newIndex)
        {
            var result = _dbSet.FirstOrDefault(x => x.Id == id);
            if (result != null)
            {
                result.Index = newIndex;
                _dbSet.Update(result);

                //Save change in Unit Of Work Commit()
            }
        }
        public Unit Create(Unit unit)
        {
            _dbSet.Add(unit);
            return unit;
        }

        public Unit GetById(long id)
        {
            return _dbSet.FirstOrDefault(x => x.Id == id && x.Status!=3);
        }

        public void Update(Unit entity)
        {
            _dbSet.Update(entity);
        }

        public List<Unit> GetUnits(long id)
        {
            var units = _dbSet.Where(u => u.IdSession == id).ToList();
            return units;
        }

        public List<Unit> GetAllUnits()
        {
            return this._dbSet.ToList();
        }
         // Team 01
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
                throw new Exception("No unit with that id");
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
                throw new Exception("No unit with that id");
            }
        }

        public void DeleteUnit(long id)
        {
            var result = _dbSet.FirstOrDefault(x => x.Id == id);
            if (result != null && result.Status != 3)
            {
                result.Status = 3;
                _dbSet.Update(result);
            }
            else
            {
                throw new Exception("No unit with that id");
            }
        }

        public List<Unit> GetSessionUnits(long IdSession)
        {
            return _dbContext.Units.Where(s => s.IdSession == IdSession).Select(s => s).ToList();
        }

    }
}
