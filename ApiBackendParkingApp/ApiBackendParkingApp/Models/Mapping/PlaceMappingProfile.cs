using ApiBackendParkingApp.Models.DAO;
using ApiBackendParkingApp.Models.DTO;
using AutoMapper;

namespace ApiBackendParkingApp.Models.Mapping
{
    public class PlaceMappingProfile:Profile
    {
        public PlaceMappingProfile()
        {
            CreateMap<PlaceModelDao, PlaceModelDTO>()
                .ForMember(dest => dest.Place_Number, opt => opt.MapFrom(src => src.Place_Number))
                .ForMember(dest => dest.Sector_ID, opt => opt.MapFrom(src => src.Sector_ID))
                .ReverseMap();
        }
    }
}
