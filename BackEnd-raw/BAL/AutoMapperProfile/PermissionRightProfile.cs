using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BAL.Models;
using DAL.Entities;

namespace BAL.AutoMapperProfile
{
    public class PermissionRightProfile : Profile
    {
        public PermissionRightProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<PermissionViewModel, PermissionRight>().ReverseMap();
        }

    }
    
}