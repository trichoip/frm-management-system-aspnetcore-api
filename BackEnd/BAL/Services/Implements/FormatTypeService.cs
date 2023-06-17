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
    public class FormatTypeService: IFormatTypeService
    {
        private IFormatTypeRepository _formatTypeRepository;
        private IUnitOfWork _unitOfWork;

        public FormatTypeService(IFormatTypeRepository formatTypeRepository, IUnitOfWork unitOfWork)
        {
            _formatTypeRepository = formatTypeRepository;
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
