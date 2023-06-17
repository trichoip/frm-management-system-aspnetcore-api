using BAL.Services.Interfaces;
using DAL.Infrastructure;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Implements
{
    public class HistoryTrainingProgramService: IHistoryTrainingProgramService
    {
        private IHistoryTrainingProgramRepository _historyTrainingProgramRepository;
        private IUnitOfWork _unitOfWork;

        public HistoryTrainingProgramService(IHistoryTrainingProgramRepository historyTrainingProgramRepository, IUnitOfWork unitOfWork)
        {
            _historyTrainingProgramRepository = historyTrainingProgramRepository;
            _unitOfWork = unitOfWork;
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void SaveAsync()
        {
            _unitOfWork.commitAsync();
        }
    }
}
