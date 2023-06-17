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
    public class ClassProgramCodeService: IClassProgramCodeService
    {
        private IClassProgramCodeRepository _classProgramCodeRepository;
        private IUnitOfWork _unitOfWork;

        public ClassProgramCodeService(IClassProgramCodeRepository classProgramCodeRepository, IUnitOfWork unitOfWork)
        {
            _classProgramCodeRepository = classProgramCodeRepository;
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
