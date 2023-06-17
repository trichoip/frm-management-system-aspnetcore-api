using AutoMapper;
using BAL.Models;
using BAL.Services.Interfaces;
using DAL.Infrastructure;
using DAL.Repositories.Interfaces;

namespace BAL.Services.Implements
{
    public class UnitService : IUnitService
    {
        private IUnitRepository _unitRepository;
        private IUnitOfWork _unitOfWork;

        // Team6
        private readonly IMapper _mapperUnit;

        public UnitService(IUnitRepository unitRepository, IUnitOfWork unitOfWork, IMapper mapperUnit)
        {
            _unitRepository = unitRepository;
            _unitOfWork = unitOfWork;
            _mapperUnit = mapperUnit;
        }


        public List<UnitViewModel> GetUnits(long id)
        {
            List<UnitViewModel> viewModel = new List<UnitViewModel>();
            viewModel = _mapperUnit.Map<List<UnitViewModel>>(_unitRepository.GetUnits(id));
            return viewModel;
        }
        // Team6

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
