using DAL.Entities;
using DAL.Infrastructure;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Implements
{
    public class FSUContactPointRepository : RepositoryBase<FSUContactPoint>, IFSUContactPointRepository
    {
        private readonly FRMDbContext _dbContext;

        public FSUContactPointRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            _dbContext = dbFactory.Init();
        }

        public FSUContactPoint Get(long id)
        {
            return _dbContext.FSUContactPoints.FirstOrDefault(x => x.IdFSU == id);
        }

        public IEnumerable<FSUContactPoint> GetFSUsAll()
        {
            return _dbContext.FSUContactPoints.ToList();
        }
    }
    
}
