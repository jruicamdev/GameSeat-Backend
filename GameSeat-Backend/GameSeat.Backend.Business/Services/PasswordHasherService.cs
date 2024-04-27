
using GameSeat.Backend.Infrastructure.Data.DTOs;
using GameSeat.Backend.Infrastructure.Data.Models;
using GameSeat.Backend.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace GameSeat.Backend.Business.Services
{
    public class PasswordHasherService : IPasswordHasherService
    {
        private readonly PasswordHasher<UserModel> _passwordHasher;

        public PasswordHasherService()
        {
            _passwordHasher = new PasswordHasher<UserModel>();
        }

        public string HashPassword(UserModel user )
        {
            return _passwordHasher.HashPassword(user, user.PasswordHash);
        }

        public ServiceResultDTO VerifyPassword(UserModel user,string providedPassword)
        {
            try
            {
                if(user== null)
                {
                    var failed = ServiceResultDTO.Failure("Error en el HasherPasswordService");
                    return failed;
                }
                else 
                {
                    var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, providedPassword);
                    if (result == PasswordVerificationResult.Failed)
                    {
                        var failed = ServiceResultDTO.Failure("Contraseña no valida", new UserModel
                        {
                            Email = user.Email,
                            PasswordHash = providedPassword
                        });
                        return failed;

                    }
                    else
                    {
                        var successed = ServiceResultDTO.Successful("Contraseña valida", new UserModel
                        {
                            Email = user.Email,
                            PasswordHash = providedPassword
                        });
                        return successed;

                    }
                }
                
            } 
            catch (Exception ex)
            {
                var failed = ServiceResultDTO.Failure("Error en el HasherPasswordService",ex);
                return failed;
            }
        }
    }
}
