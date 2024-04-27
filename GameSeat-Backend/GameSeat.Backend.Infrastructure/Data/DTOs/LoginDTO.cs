
namespace GameSeat.Backend.Infrastructure.Data.DTOs
{
    public class LoginDTO
    {
        public string? Email { get; set; }
        public string? Username { get; set; }

        public string Password { get; set; } = string.Empty;

        public bool IsValid()
        {
            // Comprobar que al menos uno de los dos campos esté lleno y que la contraseña no esté vacía
            return (string.IsNullOrEmpty(Username) != string.IsNullOrEmpty(Email)) && !string.IsNullOrEmpty(Password);
        }
    }
}
