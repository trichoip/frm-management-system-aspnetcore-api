using AutoMapper;
using BAL.Models;
using BAL.Services.Interfaces;
using DAL.Entities;
using DAL.Infrastructure;
using DAL.Repositories.Interfaces;

namespace BAL.Services.Implements
{
    public class MaterialService : IMaterialService
    {
        // Team6
        private IMaterialRepository _materialRepository;
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MaterialService(IMaterialRepository materialRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _materialRepository = materialRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public List<MaterialViewModel> GetMaterials(long id)
        {
            return _mapper.Map<List<MaterialViewModel>>(_materialRepository.GetLessonMaterials(id));
        }

        public void DeleteMaterial(long id)
        {
            _materialRepository.DeleteMaterial(id);
        }
        // Team6

        // team 6 - viet
        public void UpdateMaterial(MaterialViewModel material)
        {
            //Update Material
            if (material.Id != null)
            {
                _materialRepository.Update(_mapper.Map<Material>(material));
            }
        }


        // team 6 - viet

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

