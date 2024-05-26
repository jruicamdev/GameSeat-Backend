using GameSeat.Backend.Business.Services;
using GameSeat.Backend.Infrastructure.Data.DTOs;
using GameSeat.Backend.Infrastructure.Data.Models;
using GameSeat.Backend.Infrastructure.Interfaces;
using GameSeat.Backend.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GameSeat.Backend.WebAPI.Controllers
{
    [ApiController]
    [Route("reservations")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        // GET: api/Reservations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationModel>>> GetReservations()
        {
            var reservations = await _reservationService.GetAllReservationsAsync();
            return Ok(reservations);
        }

        // GET: api/Reservations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationModel>> GetReservation(int id)
        {
            var reservation = await _reservationService.GetReservationAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            return Ok(reservation);
        }

        // POST: api/Reservations
        [HttpPost]
        public async Task<ActionResult<ReservationModel>> CreateReservation([FromBody]ReservationDTO reservation)
        {
            try
            {
                var newReservation = await _reservationService.CreateReservationAsync(reservation);
                return CreatedAtAction(nameof(GetReservation), new { id = newReservation.Id }, newReservation);

            }
            catch(ApplicationException ex ) when (ex.Message.Contains("chair.reserved"))
            {
                return StatusCode(409,"chair.reserved");
            }
        }

        // PUT: api/Reservations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReservation(int id, ReservationModel reservation)
        {
            var previousReservation = await _reservationService.GetReservationAsync(id);
            if (id != reservation.Id || previousReservation == null )
            {
                return BadRequest("put.reservation.badrequest");
            }

            try
            {
                await _reservationService.UpdateReservationAsync(reservation);
            }
            catch (Exception e)
            {
                return Ok("put.reservation.incorrect" + e);
            }

            return Ok("put.reservation.correct");
        }

        // DELETE: api/Reservations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var reservation = await _reservationService.GetReservationAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            await _reservationService.DeleteReservationAsync(id);
            return Ok();
        }

        // Get: api/Reservations/
        [HttpGet("date")]
        public async Task<ActionResult<IEnumerable<ReservationModel>>> GetReservationsByDate([FromQuery] DateOnly date, [FromQuery] TimeOnly startTime, [FromQuery] TimeOnly endTime)
        {
            try
            {
                // Convert DateOnly to string in "yyyy-MM-dd" format directly
                var parsedDate = date.ToString("yyyy-MM-dd");

                // Call the service with the converted date
                var reservations = await _reservationService.GetReservationsByDate(parsedDate, startTime, endTime);
                return Ok(reservations);
            }
            catch (Exception ex)
            {
                return BadRequest("Error processing request of date: " + ex.Message);
            }
        }

        [HttpGet("by/{userId}")]
        public async Task<ActionResult<IEnumerable<ReservationModel>>> GetReservationsByUserId([FromRoute]int userId)
        {
            var reservations = await _reservationService.GetReservationsByUserIdAsync(userId);
            if (reservations == null)
            {
                return NotFound();
            }
            return Ok(reservations);
        }

        [HttpPost("cancel-or-confirm/{id}")]
        public async Task<IActionResult> CancelOrConfirmReservation([FromRoute]int id, [FromBody] int status)
        {
            if (status != 1 && status != 2)
            {
                return BadRequest("Invalid status.");
            }

            var result = await _reservationService.CancelOrConfirmReservationAsync(id, status);
            if (!result)
            {
                return NotFound();
            }

            return Ok("reserva.updated");
        }


    }
}
