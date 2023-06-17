using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BAL.Models;
using DAL.Entities;

namespace BAL.AutoMapperProfile
{
    public class TrainingProgramProfile : Profile
    {
        public TrainingProgramProfile()
        {
            CreateMap<TrainingProgram, ClassTrainingProgamViewModel>().ReverseMap();
            CreateMap<TrainingProgram, TrainingProgramViewModel>().ReverseMap();
            CreateMap<TrainingProgram, TrainingProgramDetailViewModel>().ReverseMap();

        }
    }
}