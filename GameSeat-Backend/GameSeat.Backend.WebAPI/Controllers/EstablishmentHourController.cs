using GameSeat.Backend.Infrastructure.Data.Models;
using GameSeat.Backend.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GameSeat.Backend.WebAPI.Controllers
{
    [ApiController]
    [Route("establishmentHour")]
    public class EstablishmentHourController : Controller
    {
        private readonly IEstablishmentHourService _establishmentHoursService;

        public EstablishmentHourController(IEstablishmentHourService establishmentHoursService)
        {
            _establishmentHoursService = establishmentHoursService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstablishmentHourModel>>> GetAll()
        {
            var hours = await _establishmentHoursService.GetAllHoursAsync();
            if (hours == null || !hours.Any()) return NotFound();
            return Ok(hours);
        }

        [HttpPut("{establishmentId}")]
        public async Task<IActionResult> Put([FromQuery]int establishmentId, [FromBody] EstablishmentHourModel hours)
        {
            if (establishmentId != hours.Id)
                return BadRequest("Mismatched Establishment ID");

            await _establishmentHoursService.UpdateHoursAsync(hours);
            return NoContent();
        }

        [HttpGet("{establishmentId}")]
        public async Task<ActionResult<EstablishmentHourModel>> Get(int establishmentId)
        {
            var hours = await _establishmentHoursService.GetHoursByIdAsync(establishmentId);
            if (hours == null) return NotFound();
            return Ok(hours);
        }
    }
}

