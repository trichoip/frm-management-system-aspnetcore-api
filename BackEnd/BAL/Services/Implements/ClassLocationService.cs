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
    public class ClassLocationService: IClassLocationService
    {
        private IClassLocationRepository _classLocationRepository;
        private IUnitOfWork _unitOfWork;

        public ClassLocationService(IClassLocationRepository classLocationRepository, IUnitOfWork unitOfWork)
        {
            _classLocationRepository = classLocationRepository;
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
