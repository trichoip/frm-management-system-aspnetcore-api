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
    public class AttendeeTypeService: IAttendeeTypeService
    {
        private IAttendeeTypeRepository _attendeeTypeRepository;
        private IUnitOfWork _unitOfWork;

        public AttendeeTypeService(IAttendeeTypeRepository attendeeTypeRepository, IUnitOfWork unitOfWork)
        {
            _attendeeTypeRepository = attendeeTypeRepository;
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
