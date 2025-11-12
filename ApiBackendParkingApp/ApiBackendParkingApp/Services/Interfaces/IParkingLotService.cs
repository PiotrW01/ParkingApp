using ApiBackendParkingApp.Models.DAO;
using ApiBackendParkingApp.Models.DTO;

namespace ApiBackendParkingApp.Services.Interfaces
{
    public interface IParkingLotService:IDisposable
    {
        public Task<IEnumerable<PlaceModelDTO>> GetAllPlacesAsync();
        public Task<IEnumerable<SectorModelDTO>> GetAllSectorsAsync();
        public Task<IEnumerable<ParkingLotModelDTO>> GetAllReservationAsync();
        public Task<int> AddReservationAsync(ParkingLotModelDTO parkingLotModelDTO);

        public Task<int> CancelReservationAsync(ParkingLotModelDTO reservationToCancelDTO);
        public Task<bool> SendConfirmEmail(ParkingLotModelDTO parkingLotModelDTO);
    }
}
