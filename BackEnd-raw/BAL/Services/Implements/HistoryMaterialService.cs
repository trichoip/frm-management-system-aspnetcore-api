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
    public class HistoryMaterialService: IHistoryMaterialService
    {
        private IHistoryMaterialRepository _historyMaterialRepository;
        private IUnitOfWork _unitOfWork;

        public HistoryMaterialService(IHistoryMaterialRepository historyMaterialRepository, IUnitOfWork unitOfWork)
        {
            _historyMaterialRepository = historyMaterialRepository;
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
