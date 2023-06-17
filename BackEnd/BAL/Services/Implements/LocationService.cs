using AutoMapper;
using BAL.Models;
using BAL.Services.Interfaces;
using DAL.Entities;
using DAL.Infrastructure;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Implements
{
    public class LocationService: ILocationService
    {
        private ILocationRepository _locationRepository;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public LocationService(ILocationRepository locationRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _locationRepository = locationRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<LocationViewModel>> GetLocationByKeyword(string keyword)
        {
            List<Location> locations = await _locationRepository.GetLocationByKeyword(keyword);
            return _mapper.Map<List<LocationViewModel>>(locations);
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
