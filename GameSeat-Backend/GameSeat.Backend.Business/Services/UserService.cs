using GameSeat.Backend.Infrastructure.Data.DTOs;
using GameSeat.Backend.Infrastructure.Data.Models;
using GameSeat.Backend.Infrastructure.Interfaces;


namespace GameSeat.Backend.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasherService _passwordHasherService;
        private readonly ITokenService _tokenService;
        public UserService(IUserRepository userRepository, IPasswordHasherService passwordHasherService, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _passwordHasherService = passwordHasherService;
            _tokenService = tokenService;
        }
        public async Task<string> AuthenticateAsync(string username, string password)
        {
            try
            {
                var user = await _userRepository.AuthenticateAsync(username, password);
                var token = string.Empty;
                if(user.Id != 0)
                {
                    token = _tokenService.CreateToken(user);
                }
                return token;
            }
            catch (Exception)
            {
                return "token.failed";
            }
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

        public async Task<UserModel> GetUserByIdAsync(int id)
        {
            UserModel user = await _userRepository.GetByIdAsync(id);
            return user;
        }

        public async Task<ServiceResultDTO> RegisterAsync(UserDto userDto, bool? isAdmin)
        {

            UserModel user = new UserModel
            {
                Username = userDto.Username,
                Email = userDto.Email,
                PasswordHash = userDto.Password,
                Admin = isAdmin ?? false,
                Enable = true,
                Id = 0,
            };
            user.PasswordHash = _passwordHasherService.HashPassword(user);

            try
            {
                await _userRepository.CreateAsync(user);
                return new ServiceResultDTO(true, "user.created");
            }
            catch (Exception)
            {
                return new ServiceResultDTO(false, "user.failed_created");
            }
        }

        public Task<ServiceResultDTO> UpdateUserAsync(int id, UserDto userDto)
        {
            throw new NotImplementedException();
        }

    }
}
