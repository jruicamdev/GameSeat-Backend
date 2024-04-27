
using GameSeat.Backend.Infrastructure.Data.DTOs;
using GameSeat.Backend.Infrastructure.Data.Models;

namespace GameSeat.Backend.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserModel>> GetAllAsync();
        Task<UserModel> GetByIdAsync(int id);
        Task<UserModel> CreateAsync(UserModel user);
        Task<UserModel> UpdateAsync(UserModel user);
        Task<ServiceResultDTO> DeleteAsync(int id);
        Task<UserModel> AuthenticateAsync(string username, string password);
        Task<UserModel> GetByUserNameAsync(string email);

    }
}
