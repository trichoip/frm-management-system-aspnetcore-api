using AutoMapper;
using BAL.Models;
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
    public class FsucontactPointService: IFsucontactPointService
    {
        private readonly IFSUContactPointRepository _fSUContactPointRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FsucontactPointService(IFSUContactPointRepository fSUContactPointRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _fSUContactPointRepository = fSUContactPointRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public FSUViewModel GetFSUsAll()
        {
            return _mapper.Map<FSUViewModel>(_fSUContactPointRepository.GetFSUsAll());
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
