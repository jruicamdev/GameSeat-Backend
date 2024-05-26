using GameSeat.Backend.Infrastructure.Data.Models;

namespace GameSeat.Backend.Infrastructure.Interfaces
{
    public interface IPaymentsRepository
    {
        Task<IEnumerable<PaymentModel>> GetAllPaymentsAsync();
        Task<PaymentModel> GetPaymentByIdAsync(int id);
        Task AddPaymentAsync(PaymentModel payment);
        Task UpdatePaymentAsync(PaymentModel payment);
        Task DeletePaymentAsync(int id);
    }
}
