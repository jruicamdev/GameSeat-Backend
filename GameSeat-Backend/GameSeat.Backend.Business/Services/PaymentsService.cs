using GameSeat.Backend.Infrastructure.Data.Models;
using GameSeat.Backend.Infrastructure.Interfaces;

namespace GameSeat.Backend.Business.Services
{
    public class PaymentsService : IPaymentsService
    {
        private readonly IPaymentsRepository _paymentRepository;

        public PaymentsService(IPaymentsRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<IEnumerable<PaymentModel>> GetAllPaymentsAsync()
        {
            return await _paymentRepository.GetAllPaymentsAsync();
        }

        public async Task<PaymentModel> GetPaymentByIdAsync(int id)
        {
            return await _paymentRepository.GetPaymentByIdAsync(id);
        }

        public async Task AddPaymentAsync(PaymentModel payment)
        {
            await _paymentRepository.AddPaymentAsync(payment);
        }

        public async Task UpdatePaymentAsync(PaymentModel payment)
        {
            await _paymentRepository.UpdatePaymentAsync(payment);
        }

        public async Task DeletePaymentAsync(int id)
        {
            await _paymentRepository.DeletePaymentAsync(id);
        }
    }
}
