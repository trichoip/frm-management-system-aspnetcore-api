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
    public class SyllabusTrainerRepository : RepositoryBase<SyllabusTrainer>, ISyllabusTrainerRepository
    {
        public SyllabusTrainerRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
