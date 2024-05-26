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
        Task<IEnumerable<ReservationModel>> GetReservationsByUserIdAsync(int userId);
        Task<bool> CancelOrConfirmReservationAsync(int id, int status);
        Task<ReservationModel> GetReservationByIdAsync(int reservationId);


    }
}
