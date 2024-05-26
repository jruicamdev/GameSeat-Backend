using GameSeat.Backend.Infrastructure.Data.Models;

namespace GameSeat.Backend.Infrastructure.Interfaces
{
    public interface IEstablishmentHourRepository
    {
        Task<IEnumerable<EstablishmentHourModel>> GetAllHoursAsync();
        Task UpdateHoursAsync(EstablishmentHourModel hours);
        Task<EstablishmentHourModel> GetHoursByIdAsync(int establishmentID);
        Task<EstablishmentHourModel> GetByDayOfWeekAsync(string dayOfWeek);
        Task UpdateAsync(EstablishmentHourModel establishmentHour);
    }
}
