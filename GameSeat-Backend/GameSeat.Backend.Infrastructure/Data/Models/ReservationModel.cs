

namespace GameSeat.Backend.Infrastructure.Data.Models
{
    public class ReservationModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ChairId { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public required string Status { get; set; }
        public double TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateOnly Date {  get; set; }
        public required virtual UserModel User { get; set; }
        public required virtual ChairModel Chair { get; set; }
        public virtual ICollection<PaymentModel>? Payments { get; set; }

    }
}
