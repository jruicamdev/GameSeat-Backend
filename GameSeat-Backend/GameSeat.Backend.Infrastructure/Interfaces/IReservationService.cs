using GameSeat.Backend.Infrastructure.Data.DTOs;
using GameSeat.Backend.Infrastructure.Data.Models;

namespace GameSeat.Backend.Infrastructure.Interfaces
{
    public interface IReservationService
    {
        Task<ReservationModel> GetReservationAsync(int id);
        Task<IEnumerable<ReservationModel>> GetAllReservationsAsync();
        Task<ReservationModel> CreateReservationAsync(ReservationDTO reservation);
        Task<ReservationModel> UpdateReservationAsync(ReservationModel reservation);
        Task DeleteReservationAsync(int id);
        Task<IEnumerable<ReservationModel>> GetReservationsByDate(string date, TimeOnly startTime, TimeOnly endTime);
    }
}
