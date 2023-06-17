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
    public class ClassTechnicalGroupService: IClassTechnicalGroupService
    {
        private IClassTechnicalGroupRepository _classTechnicalGroupRepository;
        private IUnitOfWork _unitOfWork;

        public ClassTechnicalGroupService(IClassTechnicalGroupRepository classTechnicalGroupRepository, IUnitOfWork unitOfWork)
        {
            _classTechnicalGroupRepository = classTechnicalGroupRepository;
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
