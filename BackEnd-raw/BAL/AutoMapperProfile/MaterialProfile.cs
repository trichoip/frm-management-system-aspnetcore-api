using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DAL.Entities;
using BAL.Models;
using DAL.Entities;

namespace BAL.AutoMapperProfile
{
    public class MaterialProfile : Profile
    {
        // Team6
        public MaterialProfile()
        {
            CreateMap<Material, MaterialViewModel>().ReverseMap();
            CreateMap<MaterialViewModel, Material>().ReverseMap();
        }
        // Team6
    }
}