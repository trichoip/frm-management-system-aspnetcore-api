using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IOutputStandardRepository
    {
        //Team4
        OutputStandard GetOutputStandard(long id);
        List<OutputStandard> GetAll();
    }
    
}
