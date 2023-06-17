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
    public class DeliveryTypeRepository : RepositoryBase<DeliveryType>, IDeliveryTypeRepository
    {
        public DeliveryTypeRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
        public List<DeliveryType> GetAll()
        {
            return this._dbSet.ToList();
        }

    }
}
