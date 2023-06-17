using BAL.Services.Interfaces;
using DAL.Entities;
using DAL.Infrastructure;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Implements
{
    public class HistorySyllabusService: IHistorySyllabusService
    {
        private IHistorySyllabusRepository _historySyllabusRepository;
        private IUnitOfWork _unitOfWork;
        
        public HistorySyllabusService(IHistorySyllabusRepository historySyllabusRepository, IUnitOfWork unitOfWork)
        {
            _historySyllabusRepository = historySyllabusRepository;
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
