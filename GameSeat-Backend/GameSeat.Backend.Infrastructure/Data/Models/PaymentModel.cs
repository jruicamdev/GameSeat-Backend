

namespace GameSeat.Backend.Infrastructure.Data.Models
{
    public class PaymentModel
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public decimal Amount { get; set; }
        public required string PaymentMethod { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual ReservationModel Reservation { get; set; }
    }
}
