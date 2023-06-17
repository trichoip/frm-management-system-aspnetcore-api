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
    public class ClassUpdateHistoryService: IClassUpdateHistoryService
    {
        private IClassUpdateHistoryRepository _classUpdateHistoryRepository;
        private IUnitOfWork _unitOfWork;

        public ClassUpdateHistoryService(IClassUpdateHistoryRepository classUpdateHistoryRepository, IUnitOfWork unitOfWork)
        {
            _classUpdateHistoryRepository = classUpdateHistoryRepository;
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
