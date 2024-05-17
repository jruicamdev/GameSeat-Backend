
using GameSeat.Backend.Infrastructure.Interfaces;
using GameSeat.Backend.Infrastructure.Data.Models;

namespace GameSeat.Backend.Business.Services
{
    public class ChairService : IChairService
    {
        private readonly IChairRepository _chairRepository;

        public ChairService(IChairRepository chairRepository)
        {
            _chairRepository = chairRepository;
        }

        public async Task<IEnumerable<ChairModel>> GetAllChairsAsync()
        {
            return await _chairRepository.GetAllChairsAsync();
        }

        public async Task<ChairModel> GetChairByIdAsync(int id)
        {
            return await _chairRepository.GetChairByIdAsync(id);
        }

        public async Task CreateChairAsync(ChairModel chair)
        {
            await _chairRepository.CreateChairAsync(chair);
        }

        public async Task UpdateChairAsync(ChairModel chair, int chairId)
        {
            await _chairRepository.UpdateChairAsync(chair, chairId);
        }

        public async Task DeleteChairAsync(int id)
        {
            await _chairRepository.DeleteChairAsync(id);
        }
    }
}


