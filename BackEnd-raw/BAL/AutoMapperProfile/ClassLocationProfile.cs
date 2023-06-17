using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BAL.Models;
using DAL.Entities;

namespace BAL.AutoMapperProfile
{
    public class ClassLocationProfile: Profile
    {
        public ClassLocationProfile()
        {
            CreateMap<ClassLocation, ClassLocationViewModel>().ForMember(dpt => dpt.Id, opt => opt.MapFrom(src => src.Location.Id))
                                                              .ForMember(dpt => dpt.Name, opt => opt.MapFrom(src => src.Location.Name));
        }
        
    }
}