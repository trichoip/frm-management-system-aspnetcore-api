using AutoMapper;
using BAL.Models;
using DAL.Entities;

namespace BAL.AutoMapperProfile
{
    public class UnitProfile : Profile
    {
        public UnitProfile()
        {
            // team 06
            CreateMap<UnitViewModel, Unit>().ReverseMap();
            CreateMap<Unit, UnitViewModel>().ReverseMap();
            // team 06
        }
    }
}