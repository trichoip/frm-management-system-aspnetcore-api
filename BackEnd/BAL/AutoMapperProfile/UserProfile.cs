using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using AutoMapper;
using DAL.Entities;
using BAL.Models;

namespace BAL.AutoMapperProfile
{
    public class UserProfile : Profile
    {
        // Team6
        public UserProfile()
        {
            CreateMap<User, TrainerViewModel>().ReverseMap();
            CreateMap<UserViewModel, User>().ReverseMap();
            CreateMap<User, AccountViewModel>().ReverseMap();
            CreateMap<User, UserAccountViewModel>().ReverseMap();
        }
        // Team6
    }
}