using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BAL.Models;
using DAL.Entities;
namespace BAL.AutoMapperProfile
{
  public class CurriculumProfile : Profile
  {
    public CurriculumProfile()
    {
    //   CreateMap<Curriculum, CurriculumViewModel>().ForMember(dpt => dpt.idSyllabus, opt => opt.MapFrom(src => src.IdSyllabus));
    }
  }
}