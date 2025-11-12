using ApiBackendParkingApp.Models.DAO;
using ApiBackendParkingApp.Models.DTO;
using AutoMapper;

namespace ApiBackendParkingApp.Models.Mapping
{
    public class SectorMappingProfile:Profile
    {
        public SectorMappingProfile()
        {
            CreateMap<SectorModelDAO, SectorModelDTO>()
                .ForMember(dest => dest.Sector_ID, opt => opt.MapFrom(src => src.Sector_ID))
                .ForMember(dest => dest.Sector_Name, opt => opt.MapFrom(src => src.Sector_Name))
                .ReverseMap();


        }
    }
}
