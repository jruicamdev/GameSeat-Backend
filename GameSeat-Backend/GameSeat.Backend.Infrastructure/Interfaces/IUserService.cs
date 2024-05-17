
using GameSeat.Backend.Infrastructure.Data.DTOs;
using GameSeat.Backend.Infrastructure.Data.Models;

namespace GameSeat.Backend.Infrastructure.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResultDTO> RegisterAsync(UserDto userDto, bool? isAdmin);
        Task<IEnumerable<UserModel>> GetAllUsersAsync();
        Task<UserModel> GetUserByEmailAsync(string email);
        Task<ServiceResultDTO> UpdateUserAsync(int id, UserDto userDto);
        Task<ServiceResultDTO> DeleteUserAsync(int id);
    }
}
