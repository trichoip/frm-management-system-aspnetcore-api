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
    public class OutputStandardService: IOutputStandardService
    {
        private IOutputStandardRepository _outputStandardRepository;
        private IUnitOfWork _unitOfWork;

        public OutputStandardService(IOutputStandardRepository outputStandardRepository, IUnitOfWork unitOfWork)
        {
            _outputStandardRepository = outputStandardRepository;
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
