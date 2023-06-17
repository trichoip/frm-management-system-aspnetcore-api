using BAL.Models;
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
    public class LessonService: ILessonService
    {
        private ILessonRepository _lessonRepository;
        private IUnitOfWork _unitOfWork;
        //team6
        private readonly IMapper _mapper;
        public LessonService(ILessonRepository lessonRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _lessonRepository = lessonRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public List<LessonViewModel> GetLessons(long id)
        {
            List<LessonViewModel> viewModel = null;
            viewModel = _mapper.Map<List<LessonViewModel>>(_lessonRepository.GetLessons(id));
            return viewModel;
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
