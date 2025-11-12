using ApiBackendParkingApp.Models.DAO;
using ApiBackendParkingApp.Models.DTO;
using ApiBackendParkingApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiBackendParkingApp.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class ParkingLotController:ControllerBase
    {
        private IParkingLotService _parkingLotService;

        public ParkingLotController(IParkingLotService parkingLotService)
        {
            _parkingLotService = parkingLotService;
        }
        private void Dispose()
        {
            _parkingLotService.Dispose();
            _parkingLotService = null;
        }
        [HttpGet("AllParkingSpaces")]
        public async Task<IActionResult> GetAllParkingSpaces()
        {
            try
            {
                var ParkingSpaces = await _parkingLotService.GetAllPlacesAsync();
                return Ok(ParkingSpaces);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
             
            }
            finally
            {
                Dispose();
            }

        }
        [HttpGet("AllSectors")]
        public async Task<IActionResult> GetSectors()
        {
            try
            {
                var Sectors = await _parkingLotService.GetAllSectorsAsync();
                return Ok(Sectors);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
            finally
            {
                Dispose();
            }
        }

        [HttpGet("AllReservation")]
        public async Task<IActionResult> GetAllReservation()
        {
            try
            {
                var reservation = await _parkingLotService.GetAllReservationAsync();
                return Ok(reservation);
            }
            catch (Exception)
            {

                return NotFound();
            }
            finally { Dispose(); }
        }

        [HttpPost("AddNewReservation")]
        public async Task<IActionResult> AddReservation([FromBody] ParkingLotModelDTO newReservation) 
        {
            try
            {
                var Reservations = await _parkingLotService.AddReservationAsync(newReservation);
                if (Reservations > 0)
                {
                    var FindReservation = await _parkingLotService.SendConfirmEmail(newReservation);
                    return Ok(new { Message = "Rezerwacja została zrobiona poprawnie" });

                }
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(500, new {Message = "Wystąpił błąd podczas dodawania" });
            }
            finally
            {
                Dispose();
            }
            
        }

        [HttpDelete("CancelResevation")]

        public async Task<IActionResult> DeleteReservation([FromBody] ParkingLotModelDTO resevationToCancel)
        {
            try
            {
                var reservation = await _parkingLotService.CancelReservationAsync(resevationToCancel);

                if (reservation > 0)
                {
                    return Ok("Rezerwacja została anulowana poprawnie");
                }
                return BadRequest();
            }
            catch (Exception)
            {

                return StatusCode(500, new { Message = "Wystąpił błąd podczas anulowania rezerwacji" });  
            }
            finally 
            {
                Dispose();
            }
        }

    }
}
