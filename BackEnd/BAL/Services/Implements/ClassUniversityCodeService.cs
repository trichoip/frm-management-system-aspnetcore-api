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
    public class ClassUniversityCodeService: IClassUniversityCodeService
    {
        private IClassUniversityCodeRepository _classUniversityCodeRepository;
        private IUnitOfWork _unitOfWork;

        public ClassUniversityCodeService(IClassUniversityCodeRepository classUniversityCodeRepository, IUnitOfWork unitOfWork)
        {
            _classUniversityCodeRepository = classUniversityCodeRepository;
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
