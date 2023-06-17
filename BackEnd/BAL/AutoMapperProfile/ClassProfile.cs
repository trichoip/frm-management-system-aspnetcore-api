using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BAL.Models;
using DAL.Entities;

namespace BAL.AutoMapperProfile
{
  public class ClassProfile : Profile
  {
    public ClassProfile()
    {
      CreateMap<ClassViewModel, Class>().ReverseMap();
      CreateMap<UpdateClassViewModel, Class>().ReverseMap();
      CreateMap<StudentClassViewModel, Class>().ReverseMap();
      CreateMap<Class, ClassCalenderViewModel>().ForMember(dpt => dpt.Locations, opt => opt.MapFrom(src => src.Locations));
    }
  }
}
