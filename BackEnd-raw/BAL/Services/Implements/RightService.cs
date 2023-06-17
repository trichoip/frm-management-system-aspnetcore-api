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
    public class RightService: IRightService
    {
        private IRightRepository _rightRepository;
        private IUnitOfWork _unitOfWork;

        public RightService(IRightRepository rightRepository, IUnitOfWork unitOfWork)
        {
            _rightRepository = rightRepository;
            _unitOfWork = unitOfWork;
        }

        #region Group 5 - Authentication & Authorization
        public IEnumerable<Right> GetAll()
        {
            return _rightRepository.GetAll();
        }

        public Right GetRight(long rightId)
        {
            return _rightRepository.GetRight(rightId);
        }
        #endregion
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
