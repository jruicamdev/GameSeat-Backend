
namespace GameSeat.Backend.Infrastructure.Data.Models
{
    public class EstablishmentHourModel
    {
        public int Id { get; set; }
        public required string  DayOfWeek { get; set; }
        public TimeOnly OpeningTime { get; set; }
        public TimeOnly ClosingTime { get; set; }
    }
}
