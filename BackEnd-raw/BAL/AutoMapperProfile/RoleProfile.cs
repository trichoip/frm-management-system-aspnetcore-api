using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DAL.Entities;
using BAL.Models;

namespace BAL.AutoMapperProfile
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleViewModel, Role>().ReverseMap();
        }
    }
}