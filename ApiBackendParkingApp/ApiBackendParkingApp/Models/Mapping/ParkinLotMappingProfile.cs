using ApiBackendParkingApp.Models.DAO;
using ApiBackendParkingApp.Models.DTO;
using AutoMapper;



namespace ApiBackendParkingApp.Models.Mapping
{
    public class ParkinLotMappingProfile:Profile
    {
       public ParkinLotMappingProfile()
       {
            CreateMap<ParkingLotModelDao, ParkingLotModelDTO>()
                .ForMember(dest => dest.Parking_Lot_ID, opt => opt.MapFrom(src => src.Parking_Lot_ID))
                .ForMember(dest => dest.License_Plate, opt => opt.MapFrom(src => src.License_Plate))
                .ForMember(dest => dest.Start_Time, opt => opt.MapFrom(src => src.Start_Time))
                .ForMember(dest => dest.End_Time, opt => opt.MapFrom(src => src.End_Time))
                .ForMember(dest => dest.Place_Number, opt => opt.MapFrom(src => src.Place_Number))
                .ForMember(dest => dest.ClientEmail, opt => opt.MapFrom(src => src.ClientEmail))
                .ReverseMap();
       }
    }
}
