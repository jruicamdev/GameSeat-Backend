

using GameSeat.Backend.Infrastructure.Data;
using GameSeat.Backend.Infrastructure.Data.Models;
using GameSeat.Backend.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameSeat.Backend.Infrastructure.Repositories
{
    public class PaymentsRepository : IPaymentsRepository
    {
        private readonly DataContext _context;

        public PaymentsRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PaymentModel>> GetAllPaymentsAsync()
        {
            return await _context.Payments
                                 .Include(p => p.Reservation)
                                 .ThenInclude(r => r.User)
                                 .Include(p => p.Reservation!.Chair)  // Incluir la información de la silla
                                 .ToListAsync();
        }

        public async Task<PaymentModel> GetPaymentByIdAsync(int id)
        {
            return await _context.Payments.FindAsync(id);
        }

        public async Task AddPaymentAsync(PaymentModel payment)
        {
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePaymentAsync(PaymentModel payment)
        {
            _context.Entry(payment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeletePaymentAsync(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
                await _context.SaveChangesAsync();
            }
        }
    }
}
