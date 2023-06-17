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
    public class ClassSiteService: IClassSiteService
    {
        private IClassSiteRepository _classSiteRepository;
        private IUnitOfWork _unitOfWork;

        public ClassSiteService(IClassSiteRepository classSiteRepository, IUnitOfWork unitOfWork)
        {
            _classSiteRepository = classSiteRepository;
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
