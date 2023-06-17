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
    public class SyllabusTrainerService: ISyllabusTrainerService
    {
        private ISyllabusTrainerRepository _syllabusTrainerRepository;
        private IUnitOfWork _unitOfWork;

        public SyllabusTrainerService(ISyllabusTrainerRepository syllabusTrainerRepository, IUnitOfWork unitOfWork)
        {
            _syllabusTrainerRepository = syllabusTrainerRepository;
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
