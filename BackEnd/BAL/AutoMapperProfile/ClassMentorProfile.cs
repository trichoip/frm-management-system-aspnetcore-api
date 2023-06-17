using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BAL.Models;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace BAL.AutoMapperProfile
{
    public class ClassMentorProfile: Profile
    {
        public ClassMentorProfile()
        {
            CreateMap<ClassMentor, ClassMentorViewModel>().ForMember(dept => dept.FullName, opt => opt.MapFrom(src => src.User.FullName))
                                                          .ForMember(dept => dept.Email, opt => opt.MapFrom(src => src.User.Email))
                                                          .ForMember(dept => dept.Phone, opt => opt.MapFrom(src => src.User.Phone))
                                                          .ForMember(dept => dept.ClassCode, opt => opt.MapFrom(src => src.Class.ClassCode))
                                                          .ForMember(dept => dept.StartTimeLearning, opt => opt.MapFrom(src => src.Class.StartTimeLearning))
                                                          .ForMember(dept => dept.EndTimeLearing, opt => opt.MapFrom(src => src.Class.EndTimeLearing))
                                                          .ForMember(dept => dept.CurrentSession, opt => opt.MapFrom(src => src.Class.CurrentSession))
                                                          .ForMember(dept => dept.CurrentUnit, opt => opt.MapFrom(src => src.Class.CurrentUnit))
                                                          .ForMember(dept => dept.StartDate, opt => opt.MapFrom(src => src.Class.StartDate))
                                                          .ForMember(dept => dept.EndDate, opt => opt.MapFrom(src => src.Class.EndDate));
            CreateMap<ClassMentor, TrainerViewModel>().ForMember(dept => dept.ID, opt => opt.MapFrom(src => src.User.ID))
                                                      .ForMember(dept => dept.FullName, opt => opt.MapFrom(src => src.User.FullName));
        }
        
    }
}