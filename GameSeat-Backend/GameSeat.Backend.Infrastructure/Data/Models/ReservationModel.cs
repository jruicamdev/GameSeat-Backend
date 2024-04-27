using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GameSeat.Backend.Infrastructure.Data.Models
{
    public class ReservationModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ChairId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public required string Status { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public required virtual UserModel User { get; set; }
        public required virtual ChairModel Chair { get; set; }
        public virtual ICollection<PaymentModel> Payments { get; set; }
    }
}
