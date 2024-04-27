
namespace GameSeat.Backend.Infrastructure.Data.Models
{
    public class EstablishmentHourModel
    {
        public int Id { get; set; }
        public int DayOfWeek { get; set; }
        public DateTime OpeningTime { get; set; }
        public DateTime ClosingTime { get; set; }
    }
}
