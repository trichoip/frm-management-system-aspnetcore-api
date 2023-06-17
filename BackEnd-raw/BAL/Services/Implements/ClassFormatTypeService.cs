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
    public class ClassFormatTypeService: IClassFormatTypeService
    {
        private IClassAdminReporitory _classAdminReporitory;
        private IUnitOfWork _unitOfWork;

        public ClassFormatTypeService(IClassAdminReporitory classAdminReporitory, IUnitOfWork unitOfWork)
        {
            _classAdminReporitory = classAdminReporitory;
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
