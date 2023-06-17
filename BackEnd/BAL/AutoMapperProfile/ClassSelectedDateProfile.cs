using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BAL.Models;
using DAL.Entities;

namespace BAL.AutoMapperProfile
{
  public class ClassSelectedDateProfile : Profile
  {
    public ClassSelectedDateProfile()
    {
      CreateMap<ClassSelectedDate, ClassCalenderViewModel>().ForMember(dept => dept.Id, opt => opt.MapFrom(src => src.IdClass))
                                                            .ForMember(dept => dept.ClassCode, opt => opt.MapFrom(src => src.Class.ClassCode))
                                                            .ForMember(dept => dept.CurrentSession, opt => opt.MapFrom(src => src.Class.CurrentSession))
                                                            .ForMember(dept => dept.StartTimeLearning, opt => opt.MapFrom(src => src.Class.StartTimeLearning))
                                                            .ForMember(dept => dept.EndTimeLearing, opt => opt.MapFrom(src => src.Class.EndTimeLearing))
                                                            .ForMember(dept => dept.ActiveDate, opt => opt.MapFrom(src => src.ActiveDate));

        }
  }
}