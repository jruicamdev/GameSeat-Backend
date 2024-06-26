﻿using GameSeat.Backend.Infrastructure.Data.Models;

namespace GameSeat.Backend.Infrastructure.Interfaces
{
    public interface IChairService
    {
        Task<IEnumerable<ChairModel>> GetAllChairsAsync();
        Task<ChairModel> GetChairByIdAsync(int id);
        Task CreateChairAsync(ChairModel chair);
        Task UpdateChairAsync(ChairModel chair, int chairId);
        Task DeleteChairAsync(int id);
        Task UpdateChairStatusAsync(int id, bool isMaintenance);

    }

}
