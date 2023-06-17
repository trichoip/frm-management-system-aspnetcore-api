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
    public class FsoftUnitService: IFsoftUnitService
    {
        private IFsoftUnitRepository _fsoftUnitRepository;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public FsoftUnitService(IFsoftUnitRepository fsoftUnitRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _fsoftUnitRepository = fsoftUnitRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<FSUViewModel>> GetFSUsAsync()
        {
            return _mapper.Map<List<FSUViewModel>>(await _fsoftUnitRepository.GetFSUs());
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
