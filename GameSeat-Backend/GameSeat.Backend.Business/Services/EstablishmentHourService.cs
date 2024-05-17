
using GameSeat.Backend.Infrastructure.Data.Models;
using GameSeat.Backend.Infrastructure.Interfaces;

namespace GameSeat.Backend.Business.Services
{
    public class EstablishmentHourService : IEstablishmentHourService
    {
        private readonly IEstablishmentHourRepository _repository;

        public EstablishmentHourService(IEstablishmentHourRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<EstablishmentHourModel>> GetAllHoursAsync()
        {
            return await _repository.GetAllHoursAsync();
        }

        public async Task<EstablishmentHourModel> GetHoursByIdAsync(int establishmentId)
        {
            return await _repository.GetHoursByIdAsync(establishmentId);
        }

        public async Task UpdateHoursAsync(EstablishmentHourModel hours)
        {
            await _repository.UpdateHoursAsync(hours);
        }
    }
}
