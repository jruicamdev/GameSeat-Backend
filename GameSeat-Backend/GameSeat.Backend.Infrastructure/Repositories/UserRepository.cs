
using GameSeat.Backend.Infrastructure.Data;
using GameSeat.Backend.Infrastructure.Data.DTOs;
using GameSeat.Backend.Infrastructure.Data.Models;
using GameSeat.Backend.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameSeat.Backend.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {

        public readonly DataContext _context;
        public readonly IPasswordHasherService _passwordHasherService;

        public UserRepository(DataContext context, IPasswordHasherService passwordHasherService)
        {
            _context = context;
            _passwordHasherService = passwordHasherService;
        }

        public async Task<IEnumerable<UserModel>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<UserModel> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<UserModel> CreateAsync(UserModel user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user; // Return the newly created user with the ID assigned by the database
        }

        public async Task<UserModel> UpdateAsync(UserModel user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return user; // Return the updated user
        }

        public async Task<ServiceResultDTO> DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            return new ServiceResultDTO(true, "user.delete_correct");
        }

        public async Task<UserModel> AuthenticateAsync(string email, string password)
        {
            UserModel user = await GetByUserNameAsync(email);

            // Verify the password hash matches
            ServiceResultDTO verified = _passwordHasherService.VerifyPassword(user, password);
            if (verified.Message.Equals("Contraseña valida"))
            {
                return user;
            }
            else
            {
                return new UserModel();
            }
        }
        public async Task<UserModel> GetByUserNameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

    }
}
