using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BAL.Models;
using DAL.Entities;

namespace BAL.AutoMapperProfile
{
    public class ClassAdminProfile : Profile
    {
        public ClassAdminProfile()
        {
            CreateMap<ClassAdminViewModel, ClassAdmin>().ReverseMap();
            CreateMap<ClassAdmin, AdminViewModel>().ForMember(dpt => dpt.ID, opt => opt.MapFrom(src => src.User.ID))
                                                   .ForMember(dpt => dpt.FullName, opt => opt.MapFrom(src => src.User.FullName));
        }
    }
}