namespace GameSeat.Backend.Infrastructure.Data.Models
{
    public class UpdateChairStatusModel
    {
        public int[] Ids { get; set; }
        public bool IsMaintenance { get; set; }
    }
}
