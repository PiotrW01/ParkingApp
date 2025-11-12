using ApiBackendParkingApp.Models.DAO;
using ApiBackendParkingApp.Models.DTO;
using ApiBackendParkingApp.Repositories.Interfaces;
using ApiBackendParkingApp.Services.Interfaces;
using AutoMapper;
using System.Net;
using System.Net.Mail;

namespace ApiBackendParkingApp.Services
{
    public class ParkingLotService : IParkingLotService
    {
        private IParkingLotRepository _parkingLotRepository;
        private IMapper _mapper;

        public ParkingLotService(IParkingLotRepository parkingLotRepository, IMapper mapper)
        {
            _parkingLotRepository = parkingLotRepository;
            _mapper = mapper;
        }

        private bool _isDisposed;

        public void Dispose()
        {
            if (_isDisposed)
                return;

            _parkingLotRepository.Dispose();
            _parkingLotRepository = null;
            _mapper = null;
            _isDisposed = true;
        }

        public async Task<IEnumerable<PlaceModelDTO>> GetAllPlacesAsync()
        {
            try
            {
                var dao = await _parkingLotRepository.GetAllPlacesAsync();

                var result = new List<PlaceModelDTO>();

                foreach (var place in dao) 
                {
                    result.Add(_mapper.Map<PlaceModelDTO>(place));
                }
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
                
            }
        }

        public async Task<IEnumerable<SectorModelDTO>> GetAllSectorsAsync()
        {
            try
            {
                var dao = await _parkingLotRepository.GetAllSectorsAsync();

                var result = new List<SectorModelDTO>();

                foreach (var sectors in dao)
                {
                    result.Add(_mapper.Map<SectorModelDTO>(sectors));
                }

                return result;

            }
            catch (Exception ex)
            {
                 Console.WriteLine(ex.Message);
                throw ex;

            }
        }
        public async Task<IEnumerable<ParkingLotModelDTO>> GetAllReservationAsync()
        {
            try
            {
                var dao = await _parkingLotRepository.GetAllReservationAsync();

                var result = new List<ParkingLotModelDTO>();

                foreach (var reservation in dao)
                {
                    Console.WriteLine(reservation);
                    result.Add(_mapper.Map<ParkingLotModelDTO>(reservation));
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> AddReservationAsync(ParkingLotModelDTO parkingLotModelDTO)
        {
            try
            {
                var dao = _mapper.Map<ParkingLotModelDao>(parkingLotModelDTO);
                var result = await _parkingLotRepository.AddReservationAsync(dao);
               
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<int> CancelReservationAsync(ParkingLotModelDTO reservationToCancelDTO)
        {
            try
            {
                var dao = _mapper.Map<ParkingLotModelDao>(reservationToCancelDTO);
                return await _parkingLotRepository.CancelReservationAsync(dao);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> SendConfirmEmail(ParkingLotModelDTO parkingLotModelDTO)
        {

            var allReservation = await GetAllReservationAsync();
            try
            {
                var findReservationToSendEmail = allReservation.FirstOrDefault(r => r.License_Plate == parkingLotModelDTO.License_Plate
                                                                                      && r.Start_Time == parkingLotModelDTO.Start_Time
                                                                                       && r.End_Time == parkingLotModelDTO.End_Time
                                                                                       && r.Place_Number == parkingLotModelDTO.Place_Number);
                if (findReservationToSendEmail != null)
                {
                    Console.WriteLine(findReservationToSendEmail.Parking_Lot_ID);

                    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                    {
                        Port = 587,
                        Credentials = new NetworkCredential("kuba24481pl@gmail.com", "zaps rlag pvuy tnwp"),
                        EnableSsl = true
                    };

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress("kuba24481pl@gmail.com", "Reservation Service"), // Adres nadawcy
                        Subject = "Reservation Confirmation",
                        Body = $"Thank you for your reservation! Your reservation ID is: {findReservationToSendEmail.Parking_Lot_ID}.",
                        IsBodyHtml = false,
                    };

                    mailMessage.To.Add(parkingLotModelDTO.ClientEmail);
                    await smtpClient.SendMailAsync(mailMessage);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

           
        }
    }
}
