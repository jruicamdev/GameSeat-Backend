using GameSeat.Backend.Infrastructure.Data;
using GameSeat.Backend.Infrastructure.Data.Models;
using GameSeat.Backend.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameSeat.Backend.Infrastructure.Repositories
{
    public class ReservationRepository : IReservationRepository
    {

        private readonly DataContext _context;

        public ReservationRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ReservationModel> GetReservationAsync(int id)
        {
            return await _context.Reservations.FindAsync(id);
        }

        public async Task<IEnumerable<ReservationModel>> GetAllReservationsAsync()
        {
            return await _context.Reservations
                .Include(p => p.User)
                .Include(p => p.Payments)
                .ToListAsync();
        }

        public async Task<ReservationModel> AddReservationAsync(ReservationModel reservation)
        {
            try {
                IEnumerable<ReservationModel> chairsReserved = await GetReservationsByDate(reservation.Date.ToString(), reservation.StartTime, reservation.EndTime);

                foreach (var chair in chairsReserved)
                {
                    if (reservation.ChairId == chair.ChairId)
                    {
                        throw new InvalidOperationException("chair.reserved");
                    }
                }
                _context.Reservations.Add(reservation);
                await _context.SaveChangesAsync();
                return reservation;
            } catch (InvalidOperationException ex) when (ex.Message.Contains("chair.reserved"))
            {
                throw new ApplicationException("chair.reserved.", ex);
            }
        }

            public async Task<ReservationModel> UpdateReservationAsync(ReservationModel reservation)
        {
            _context.Entry(reservation).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return reservation;
        }

        public async Task DeleteReservationAsync(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<ReservationModel>> GetReservationsByDate(string date, TimeOnly startTime, TimeOnly endTime)
        {
            var targetDate = DateOnly.Parse(date); // Assuming the date string is in a valid format.
            var startDateTime = targetDate.ToDateTime(startTime);
            var endDateTime = targetDate.ToDateTime(endTime);

            var reservations = await _context.Reservations
                .Where(r => r.Date == targetDate // Reservations that are on the specified date
                            && ((r.StartTime <= endTime && r.StartTime >= startTime) // Starts within the requested period
                                || (r.EndTime >= startTime && r.EndTime <= endTime) // Ends within the requested period
                                || (r.StartTime <= startTime && r.EndTime >= endTime))) // Encompasses the entire requested period
                .ToListAsync();

            return reservations!;
        }

        public async Task<IEnumerable<ReservationModel>> GetReservationsByUserIdAsync(int userId)
        {
            return await _context.Reservations
                   .Where(r => r.UserId == userId)
                   .OrderByDescending(r => r.CreatedAt) // Ordenar por StartTime en orden descendente
                   .ToListAsync();
        }

        public async Task<ReservationModel> GetReservationById(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                throw new ApplicationException("reserrvation.notFound");
            }

            // Map Entity to Model
            return reservation;
        }

        public async Task<bool> CancelOrConfirmReservationAsync(int id, int status)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return false;
            }
            string statusString;
            if(status == 1)
            {
                statusString = "Canceled";
            }else if(status == 2)
            {
                statusString = "Confirmed";
            }
            else
            {
                throw new ApplicationException("status.notValid");
            }

            reservation.Status = statusString;
            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ReservationModel> GetReservationByIdAsync(int id)
        {
            return await _context.Reservations.FindAsync(id);
        }
    }
}
