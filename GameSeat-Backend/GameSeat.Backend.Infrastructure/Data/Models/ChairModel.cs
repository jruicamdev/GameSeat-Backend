using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSeat.Backend.Infrastructure.Data.Models
{
    public class ChairModel
    {
        public required int Id { get; set; }
        public required string Description { get; set; }
        public required string Status { get; set; }
        public required decimal PricePerHour { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual ICollection<ReservationModel> Reservations { get; set; }
    }
}
