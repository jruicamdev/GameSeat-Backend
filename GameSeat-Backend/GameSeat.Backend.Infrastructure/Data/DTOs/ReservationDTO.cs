
using GameSeat.Backend.Infrastructure.Data.Models;

namespace GameSeat.Backend.Infrastructure.Data.DTOs
{
    public class ReservationDTO
    {
        public int? Id { get; set; }
        public int UserId { get; set; }
        public int ChairId { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime{ get; set; }
        public DateOnly Date{ get; set; }
        public required string Status { get; set; }
    }
}
