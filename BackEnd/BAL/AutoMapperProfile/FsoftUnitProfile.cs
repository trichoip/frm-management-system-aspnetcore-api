using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BAL.Models;
using DAL.Entities;

namespace BAL.AutoMapperProfile
{
    public class FsoftUnitProfile: Profile
    {
        public FsoftUnitProfile()
        {
            CreateMap<FsoftUnit, FSUViewModel>();
        }
        
    }
}