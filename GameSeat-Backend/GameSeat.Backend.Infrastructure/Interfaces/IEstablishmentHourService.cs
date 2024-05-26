using GameSeat.Backend.Infrastructure.Data.Models;

namespace GameSeat.Backend.Infrastructure.Interfaces
{
    public interface IEstablishmentHourService
    {
        Task<IEnumerable<EstablishmentHourModel>> GetAllHoursAsync();
        Task<EstablishmentHourModel> GetHoursByIdAsync(int establishmentId);
        Task UpdateHoursAsync(EstablishmentHourModel hours);
        Task UpdatePriceForDayAsync(string dayOfWeek, int price);

    }
}
