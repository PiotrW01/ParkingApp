namespace ApiBackendParkingApp.Models.DTO
{
    public class ParkingLotModelDTO
    {
        public int Parking_Lot_ID { get; set; }
        public string License_Plate { get; set; } = string.Empty;
        public DateTime Start_Time { get; set; }
        public DateTime End_Time { get; set; }
        public int Place_Number { get; set; }
        public string ClientEmail { get; set; } = string.Empty;
    }
}
