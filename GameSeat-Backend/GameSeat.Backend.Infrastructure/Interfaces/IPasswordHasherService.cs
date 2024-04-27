using GameSeat.Backend.Infrastructure.Data.DTOs;
using GameSeat.Backend.Infrastructure.Data.Models;

namespace GameSeat.Backend.Infrastructure.Interfaces
{
    public interface IPasswordHasherService
    {
        string HashPassword(UserModel user);
        ServiceResultDTO VerifyPassword(UserModel user, string providedPassword);
    }
}
