using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BAL.Models;
using DAL.Entities;

namespace BAL.AutoMapperProfile
{
    public class SessionProfile : Profile
    {
        // Team6
        public SessionProfile()
        {
            CreateMap<Session, SessionViewModel>().ReverseMap();
            CreateMap<SessionViewModel, Session>().ReverseMap();
        }
        // Team6
    }
 
}