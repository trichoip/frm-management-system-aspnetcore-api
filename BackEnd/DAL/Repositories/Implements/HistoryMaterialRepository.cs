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
    public class HistoryMaterialRepository : RepositoryBase<HistoryMaterial>, IHistoryMaterialRepository
    {
        private readonly FRMDbContext _dbContext;
        public HistoryMaterialRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            _dbContext = dbFactory.Init();
        }

        public void CreateHistoryMaterial(HistoryMaterial historyMaterial)
        {
            _dbSet.Add(historyMaterial);
        }
        

    }
}
