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
    public class DeliveryTypeService: IDeliveryTypeService
    {
        private IDeliveryTypeRepository _deliveryTypeRepository;
        private IUnitOfWork _unitOfWork;

        public DeliveryTypeService(IDeliveryTypeRepository deliveryTypeRepository, IUnitOfWork unitOfWork)
        {
            _deliveryTypeRepository = deliveryTypeRepository;
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
