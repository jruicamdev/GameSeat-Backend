using GameSeat.Backend.Infrastructure.Data.Models;

namespace GameSeat.Backend.Infrastructure.Interfaces
{
    public interface IReservationRepository
    {
        Task<ReservationModel> GetReservationAsync(int id);
        Task<IEnumerable<ReservationModel>> GetReservationsByDate(string date, TimeOnly startTime, TimeOnly endTime);
        Task<IEnumerable<ReservationModel>> GetAllReservationsAsync();
        Task<ReservationModel> AddReservationAsync(ReservationModel reservation);
        Task<ReservationModel> UpdateReservationAsync(ReservationModel reservation);
        Task DeleteReservationAsync(int id);
    }

}
