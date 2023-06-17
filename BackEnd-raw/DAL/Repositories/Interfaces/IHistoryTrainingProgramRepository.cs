using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IHistoryTrainingProgramRepository
    {
        void GetHistoryTrainingProgramsById(long proId);
        void CreateHistoryTrainingProgram(HistoryTrainingProgram historyTrainingProgram);

        // Team6
        IQueryable<HistoryTrainingProgram> GetHistoryTrainingQuery(long id);
        // Team6
    }
}
