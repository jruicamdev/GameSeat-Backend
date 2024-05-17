
using GameSeat.Backend.Infrastructure.Data.Models;
using GameSeat.Backend.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GameSeat.Backend.WebAPI.Controllers
{
    [ApiController]
    [Route("chairs")]
    public class ChairsController : ControllerBase
    {
        private readonly IChairService _chairService;

        public ChairsController(IChairService chairService)
        {
            _chairService = chairService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllChairs()
        {
            var chairs = await _chairService.GetAllChairsAsync();
            return Ok(chairs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetChair(int id)
        {
            var chair = await _chairService.GetChairByIdAsync(id);
            if (chair == null)
            {
                return NotFound();
            }
            return Ok(chair);
        }

        [HttpPost]
        public async Task<IActionResult> CreateChair([FromBody] ChairModel chair)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _chairService.CreateChairAsync(chair);
            return CreatedAtAction(nameof(GetChair), new { id = chair.Id }, chair);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateChair(int id, [FromBody] ChairModel chair)
        {
            if (id != chair.Id)
            {
                return BadRequest();
            }

            await _chairService.UpdateChairAsync(chair, id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChair(int id)
        {
            var chair = await _chairService.GetChairByIdAsync(id);
            if (chair == null)
            {
                return NotFound();
            }

            await _chairService.DeleteChairAsync(id);
            return NoContent();
        }
    }
}
