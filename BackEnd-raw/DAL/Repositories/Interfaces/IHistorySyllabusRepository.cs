using DAL.Entities;
using DAL.Repositories.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IHistorySyllabusRepository
    {
        void CreateHistorySyllabus(HistorySyllabus historySyllabus);
        // Team6
        IQueryable<HistorySyllabus> GetHistorySyllabus();
        

            // Team6
            //Team 4
            List<HistorySyllabus> GetAllHistorySyllabus();
        void AddHistorySyllabusForImport(HistorySyllabus history);
        HistorySyllabus GetCreatedOnHistorySyllabi(long id);


    }
}
