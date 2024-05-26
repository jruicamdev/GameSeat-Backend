using GameSeat.Backend.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GameSeat.Backend.Infrastructure.Interfaces
{
    public interface IReservationRepository
    {
        Task<ReservationModel> GetReservationAsync(int id);
        Task<IEnumerable<ReservationModel>> GetReservationsByDate(string date, TimeOnly startTime, TimeOnly endTime);
        Task<IEnumerable<ReservationModel>> GetAllReservationsAsync();
        Task<ReservationModel> GetReservationById(int id);
        Task<ReservationModel> AddReservationAsync(ReservationModel reservation);
        Task<ReservationModel> UpdateReservationAsync(ReservationModel reservation);
        Task DeleteReservationAsync(int id);
        Task<IEnumerable<ReservationModel>> GetReservationsByUserIdAsync(int userId);
        Task<bool> CancelOrConfirmReservationAsync(int id, int status);
        Task<ReservationModel> GetReservationByIdAsync(int reservationId);


    }

}
