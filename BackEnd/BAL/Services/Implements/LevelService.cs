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
    public class LevelService: ILevelService
    {
        private ILevelRepository _levelRepository;
        private IUnitOfWork _unitOfWork;

        public LevelService(ILevelRepository levelRepository, IUnitOfWork unitOfWork)
        {
            _levelRepository = levelRepository;
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
