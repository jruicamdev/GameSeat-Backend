using GameSeat.Backend.Infrastructure.Data.Models;

namespace GameSeat.Backend.Infrastructure.Interfaces
{
    public interface IChairRepository
    {
        Task<IEnumerable<ChairModel>> GetAllChairsAsync();
        Task<ChairModel> GetChairByIdAsync(int id);
        Task CreateChairAsync(ChairModel chair);
        Task UpdateChairAsync(ChairModel chair);
        Task DeleteChairAsync(int id);
    }
}
