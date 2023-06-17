using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BAL.Models;
using DAL.Entities;

namespace BAL.AutoMapperProfile
{
    public class SyllabusProfile : Profile
    {
        // Team1
        public SyllabusProfile()
        {
            CreateMap<SyllabusViewModel, Syllabus>().ReverseMap();
            CreateMap<Syllabus, SyllabusViewModel>()
                .ForMember(x => x.HistorySyllabus, x => x.MapFrom(x => x.HistorySyllabi.OrderByDescending(x => x.ModifiedOn).FirstOrDefault()));

            CreateMap<Syllabus, SearchSyllabusViewModel>()
           .ForMember(des => des.SyllabusName, act => act.MapFrom(src => src.Name)).ReverseMap(); 
            
        }
        // Team1
    }
}