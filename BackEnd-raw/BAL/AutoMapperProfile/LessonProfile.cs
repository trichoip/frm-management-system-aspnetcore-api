using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BAL.Models;
using DAL.Entities;

namespace BAL.AutoMapperProfile
{
    public class LessonProfile : Profile
    {
        public LessonProfile()
        {
            CreateMap<LessonViewModel, Lesson>().ReverseMap();
            CreateMap<Lesson, LessonViewModel>()
                .ForMember(x => x.DeliveryType, x => x.MapFrom(x => x.DeliveryType.Name))
                .ForMember(x => x.FormatType, x => x.MapFrom(x => x.FormatType.Name))
                .ForMember(x => x.OutputStandard, x => x.MapFrom(x => x.OutputStandard.Name));
        }
        
    }
}