
using GameSeat.Backend.Infrastructure.Data.DTOs;
using GameSeat.Backend.Infrastructure.Data.Models;

namespace GameSeat.Backend.Infrastructure.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResultDTO> RegisterAsync(UserDto userDto, bool? isAdmin);
        Task<string> AuthenticateAsync(string username, string password);
        Task<IEnumerable<UserModel>> GetAllUsersAsync();
        Task<UserModel> GetUserByIdAsync(int id);
        Task<ServiceResultDTO> UpdateUserAsync(int id, UserDto userDto);
        Task<ServiceResultDTO> DeleteUserAsync(int id);
    }
}
