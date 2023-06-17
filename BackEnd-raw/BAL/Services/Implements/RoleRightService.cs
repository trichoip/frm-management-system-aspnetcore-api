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
    public class RoleRightService: IRoleRightService
    {
        private IRoleRightRepository _roleRightRepository;
        private IUnitOfWork _unitOfWork;

        public RoleRightService(IRoleRightRepository roleRightRepository, IUnitOfWork unitOfWork)
        {
            _roleRightRepository = roleRightRepository;
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
