
using GameSeat.Backend.Infrastructure.Data;
using GameSeat.Backend.Infrastructure.Data.Models;
using GameSeat.Backend.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameSeat.Backend.Infrastructure.Repositories
{
    public class ChairRepository : IChairRepository
    {
        private readonly DataContext _context;

        public ChairRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ChairModel>> GetAllChairsAsync()
        {
            return await _context.Chairs.ToListAsync();
        }

        public async Task<ChairModel> GetChairByIdAsync(int id)
        {
            return await _context.Chairs.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task CreateChairAsync(ChairModel chair)
        {
            await _context.Chairs.AddAsync(chair);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateChairAsync(ChairModel chair, int chairId)
        {
            _context.Chairs.Update(chair);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteChairAsync(int id)
        {
            var chair = await GetChairByIdAsync(id);
            if (chair != null)
            {
                _context.Chairs.Remove(chair);
                await _context.SaveChangesAsync();
            }
        }
    }
}
