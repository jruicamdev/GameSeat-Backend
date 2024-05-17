using GameSeat.Backend.Infrastructure.Data;
using GameSeat.Backend.Infrastructure.Data.Models;
using GameSeat.Backend.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameSeat.Backend.Infrastructure.Repositories
{
    public class EstablishmentHourRepository : IEstablishmentHourRepository
    {
        private readonly DataContext _context;

        public EstablishmentHourRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EstablishmentHourModel>> GetAllHoursAsync()
        {
            return await _context.EstablishmentHours.ToListAsync();
        }

        public async Task UpdateHoursAsync(EstablishmentHourModel hours)
        {
            _context.EstablishmentHours.Update(hours);
            await _context.SaveChangesAsync();
        }

        public async Task<EstablishmentHourModel> GetHoursByIdAsync(int establishmentID)
        {
            return await _context.EstablishmentHours.FirstOrDefaultAsync(r => r.Id == establishmentID);
        }
    }
}
