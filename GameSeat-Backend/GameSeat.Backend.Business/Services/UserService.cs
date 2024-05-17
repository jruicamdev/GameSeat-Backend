using GameSeat.Backend.Infrastructure.Data.DTOs;
using GameSeat.Backend.Infrastructure.Data.Models;
using GameSeat.Backend.Infrastructure.Interfaces;


namespace GameSeat.Backend.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ServiceResultDTO> DeleteUserAsync(int id)
        {
            try
            {
                await _userRepository.DeleteAsync(id);
                return new ServiceResultDTO(true, "user.deleted");
            }
            catch (Exception)
            {
                return new ServiceResultDTO(false, "user.failed_deleted");
            }
        }

        public async Task<IEnumerable<UserModel>> GetAllUsersAsync()
        {
            IEnumerable<UserModel> users  = await _userRepository.GetAllAsync();
            return users;
        }

        public async Task<UserModel> GetUserByEmailAsync(string email)
        {
            UserModel user = await _userRepository.GetByEmailAsync(email);
            return user;
        }

        public async Task<ServiceResultDTO> RegisterAsync(UserDto userDto, bool? isAdmin)
        {

            UserModel user = new UserModel
            {
                Username = userDto.Username!,
                Email = userDto.Email!,
                Admin = isAdmin ?? false,
                Enable = true,
                Id = 0,
            };

            try
            {
                await _userRepository.CreateAsync(user);
                return new ServiceResultDTO(true, "user.created");
            }
            catch (Exception ex)
            {
                return new ServiceResultDTO(false, "user.failed_created" + ex);
            }
        }

        public Task<ServiceResultDTO> UpdateUserAsync(int id, UserDto userDto)
        {
            throw new NotImplementedException();
        }

    }
}
