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
    public class FormatTypeRepository: RepositoryBase<FormatType>, IFormatTypeRepository
    {
        public FormatTypeRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public List<FormatType> GetAll()
        {
            return this._dbSet.ToList();
        }
    }
}
