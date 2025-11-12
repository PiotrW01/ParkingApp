using ApiBackendParkingApp.Interfaces;
using ApiBackendParkingApp.Models.DAO;
using ApiBackendParkingApp.Repositories.Interfaces;
using Microsoft.AspNetCore.Components.Web;


namespace ApiBackendParkingApp.Repositories
{
    public class ParkingLotRepository : IParkingLotRepository
    {
        private IDbInteractions _db;

        public ParkingLotRepository(IDbInteractions db)
        {
            _db = db;
        }

        private bool _disposed;
        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }
            _db = null;
            _disposed = true;
        }
        public async Task<IEnumerable<PlaceModelDao>> GetAllPlacesAsync()
        {
            string sql = @"Select * 
                            From Places";

            var result = await _db.QueryAsync<PlaceModelDao>(sql, null);
            return result;
        }

        public async Task<IEnumerable<SectorModelDAO>> GetAllSectorsAsync()
        {
            string sql = @"Select *
                                From Sectors";
            var result = await _db.QueryAsync<SectorModelDAO>(sql, null);
            return result;
        }

        public async Task<IEnumerable<ParkingLotModelDao>> GetAllReservationAsync()
        {
            string sql = @"SELECT * 
                            FROM Parking_Lot";
            var result = await _db.QueryAsync<ParkingLotModelDao>(sql, null);
            return result;
        }

        public async Task<int> AddReservationAsync(ParkingLotModelDao modelDao)
        {
            string checkAvailabilitySql = @"
                                             SELECT COUNT(1) 
                                             FROM Parking_Lot
                                             WHERE Place_Number = @Place_Number 
                                                 AND ((Start_Time <= @End_Time AND End_Time >= @Start_Time))";

            string sql = @"INSERT INTO Parking_Lot (License_Plate,Start_Time,End_Time,Place_Number) 
                                                    VALUES (@License_Plate,@Start_Time,@End_Time,@Place_Number)";

            try
            {

                var isAvailable = await _db.QueryFirstOrDefaultAsync<int>(checkAvailabilitySql, new
                {
                    Place_Number = modelDao.Place_Number,
                    Start_Time = modelDao.Start_Time,
                    End_Time = modelDao.End_Time
                });

                if (isAvailable > 0)
                {
                  throw new InvalidOperationException($"Miejsce {modelDao.Place_Number} jest już zarezewowane w tym przedziale czasowym.");
                }

                            

                var result = await _db.ExecuteAsync(sql, new
                {
                    License_Plate = modelDao.License_Plate,
                    Start_Time = modelDao.Start_Time,
                    End_Time = modelDao.End_Time,
                    Place_Number = modelDao.Place_Number,
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> CancelReservationAsync(ParkingLotModelDao reservationToCancel)
        {
            var sql = @"DELETE FROM Parking_Lot 
                        WHERE Parking_Lot_ID = @Parking_Lot_ID 
                        AND License_Plate = @License_Plate";


            try
            {
                var result = await _db.ExecuteAsync(sql,new {
                    Parking_Lot_ID = reservationToCancel.Parking_Lot_ID,
                    License_Plate = reservationToCancel.License_Plate
                });

                return result;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Błąd podczas anulowania rezerwacji");
            }
        }
        
    }
}
