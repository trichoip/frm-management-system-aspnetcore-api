using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BAL.Models;
using DAL.Entities;

namespace BAL.AutoMapperProfile
{
    public class HistorySyllabusProfile : Profile
    {
        // Team6
        public HistorySyllabusProfile()
    {
            CreateMap<HistorySyllabus, HistorySyllabusViewModel>().ReverseMap();
        }
        // Team6
        
    }
}