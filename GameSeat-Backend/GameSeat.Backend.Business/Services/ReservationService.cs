using GameSeat.Backend.Infrastructure.Data.DTOs;
using GameSeat.Backend.Infrastructure.Data.Models;
using GameSeat.Backend.Infrastructure.Interfaces;
using System.Runtime.InteropServices;


namespace GameSeat.Backend.Business.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IChairRepository _chairRepository;
        private readonly IEstablishmentHourRepository _establishmentHourRepository;


        public ReservationService(IReservationRepository reservationRepository, IUserRepository userRepository, IChairRepository chairRepository, IEstablishmentHourRepository establishmentHourRepository)
        {
            _reservationRepository = reservationRepository;
            _userRepository = userRepository;
            _chairRepository = chairRepository;
            _establishmentHourRepository = establishmentHourRepository;
        }

        public async Task<ReservationModel> GetReservationAsync(int id)
        {
            return await _reservationRepository.GetReservationAsync(id);
        }

        public async Task<IEnumerable<ReservationModel>> GetAllReservationsAsync()
        {
            return await _reservationRepository.GetAllReservationsAsync();
        }

        public async Task<ReservationModel> CreateReservationAsync(ReservationDTO reservation)
        {
            try
            {
                double costPerHour = 0;
                DayOfWeek day = reservation.Date.DayOfWeek;
                int dayId = GetDatabaseDayId(day);

                if (day == DayOfWeek.Saturday || day == DayOfWeek.Sunday)
                {
                    costPerHour = 4.00;
                }
                else
                {
                    costPerHour = 3.00;
                }

                TimeSpan duration = reservation.EndTime - reservation.StartTime;
                double totalAmount = (double)duration.TotalHours * costPerHour;

                // Obtener el horario del establecimiento
                var establishmentHours = await _establishmentHourRepository.GetHoursByIdAsync(dayId);

                // Confirmar que las horas de la reserva están dentro del horario de operación
                if (reservation.StartTime < establishmentHours.OpeningTime || reservation.EndTime > establishmentHours.ClosingTime)
                {
                    // Lanzar una excepción o manejar el caso cuando los tiempos no son válidos
                    throw new InvalidOperationException("reservation.hoursOutOfSchedule");
                }

                var reservationModel = new ReservationModel
                {
                    Id = 0,
                    UserId = reservation.UserId,
                    ChairId = reservation.ChairId,
                    Date = reservation.Date,
                    StartTime = reservation.StartTime,
                    EndTime = reservation.EndTime,
                    Status = reservation.Status,
                    CreatedAt = DateTime.Now,
                    TotalAmount = totalAmount,
                    User = await _userRepository.GetUserByIdAsync(reservation.UserId),
                    Chair = await _chairRepository.GetChairByIdAsync(reservation.ChairId)
                };

                return await _reservationRepository.AddReservationAsync(reservationModel);
            }catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<ReservationModel> UpdateReservationAsync(ReservationModel reservation)
        {
            return await _reservationRepository.UpdateReservationAsync(reservation);
        }

        public async Task DeleteReservationAsync(int id)
        {
            await _reservationRepository.DeleteReservationAsync(id);
        }

        public async Task<IEnumerable<ReservationModel>> GetReservationsByDate(string date, TimeOnly startTime, TimeOnly endTime)
        {
            return await _reservationRepository.GetReservationsByDate(date, startTime, endTime);
        }

        private int GetDatabaseDayId(DayOfWeek dayOfWeek)
        {
            // En .NET, DayOfWeek enumera los días de la semana empezando por domingo (0) hasta sábado (6)
            // En tu base de datos, Lunes es 1 y Domingo es 7.

            // Si es domingo (0 en .NET), debe retornar 7.
            if (dayOfWeek == DayOfWeek.Sunday)
            {
                return 7;
            }
            else
            {
                return (int)dayOfWeek;
            }
        }
    }
}
