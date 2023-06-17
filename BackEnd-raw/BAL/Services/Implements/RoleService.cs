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
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using BAL.Models;
using DAL.Entities;
using AutoMapper;

namespace BAL.Services.Implements
{
    public class RoleService: IRoleService
    {
        private IRoleRepository _roleRepository;
        private IPermissionRightRepository _permissionRightRepository;
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoleService(IRoleRepository roleRepository, IPermissionRightRepository permissionRightRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _permissionRightRepository = permissionRightRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public RoleViewModel GetRoleById(long roleId)
        {
            return _mapper.Map<RoleViewModel>(_roleRepository.GetRoleById(roleId));
        }

        public RoleViewModel GetByID(long id)
        {
            return _mapper.Map<RoleViewModel>(_roleRepository.GetByID(id));
        }
        #region Group 5 - Authentication & Authorization
        public Role GetRole(long roleId)
        {
            return _roleRepository.GetRole(roleId);
        }
        #endregion

        public async Task<List<PermissionViewModel>> GetAllRole()
        {
            var result = _mapper.Map<List<PermissionViewModel>>(_permissionRightRepository.GetAllRole());
            return result;
        }
        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void SaveAsync()
        {
            _unitOfWork.commitAsync();
        }
        public async Task<Role> AddNewRoleAsync(string name)
        {
            var result = await _roleRepository.Create(name);
            return result;
        }
    }
}
