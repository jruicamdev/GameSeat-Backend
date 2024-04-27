
using GameSeat.Backend.Infrastructure.Data.Models;

namespace GameSeat.Backend.Infrastructure.Interfaces
{
    public interface ITokenService
    {
        public string CreateToken(UserModel user);
    }
}
