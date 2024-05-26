using GameSeat.Backend.Infrastructure.Data.Models;

namespace GameSeat.Backend.Infrastructure.Interfaces
{
    public interface IPdfService
    {
        byte[] GeneratePdf(List<PaymentModel> payments);

    }
}
