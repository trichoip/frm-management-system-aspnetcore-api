using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IRightRepository
    {
        #region Group 5 - Authentication & Authorization
        public Right GetRight(long rightId);
        public IEnumerable<Right> GetAll();
        #endregion
    }
}
