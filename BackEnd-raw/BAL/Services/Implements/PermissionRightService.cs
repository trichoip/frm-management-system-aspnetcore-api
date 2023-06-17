using AutoMapper;
using BAL.Models;
using BAL.Services.Interfaces;
using DAL.Entities;
using DAL.Infrastructure;
using DAL.Repositories.Implements;
using DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Rewrite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Implements
{
    public class PermissionRightService : IPermissionRightService
    {
        private IPermissionRightRepository _permissionRightRepository;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public PermissionRightService(IPermissionRightRepository permissionRightRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _permissionRightRepository = permissionRightRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #region Group 5 - Authentication & Authorization
        public IEnumerable<PermissionRight> GetPermissionRightsByRoleId(long roleId)
        {
            return _permissionRightRepository.GetPermissionsRightsByRoleId(roleId);
        }
        #endregion
        public async Task<PermissionViewModel> SetPermission(long idRight, long idRole, long idPermission)
        {
            var result = _mapper.Map<PermissionViewModel>(await _permissionRightRepository.SetPermission(idRight, idRole, idPermission));
            await SaveAsync();
            return result;

        }
        public void Save()
        {
            _unitOfWork.Commit();
        }

        public async Task SaveAsync()
        {
            await _unitOfWork.commitAsync();
        }

        public async Task<List<PermissionViewModel>> GetAllRole()
        {
            var result = _mapper.Map<List<PermissionViewModel>>(_permissionRightRepository.GetAllRole());
            return result;
        }
    }
}
