using Azure.Identity;
using GameSeat.Backend.Infrastructure.Data.DTOs;
using GameSeat.Backend.Infrastructure.Data.Models;
using GameSeat.Backend.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GameSeat.Backend.WebAPI.Controllers
{
    /// <summary>
    /// Se encarga de la obtención de tokens JWT de la aplicación
    /// </summary>
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto userDto, bool? isAdmin)
        {
            var result = await _userService.RegisterAsync(userDto, isAdmin);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("login")]
        public async Task<TokenResult> Login([FromBody]Credentials credentials)
        {
            var result = await _userService.AuthenticateAsync(credentials.Username, credentials.Password);
            TokenResult finalResult = new TokenResult { TokenString = string.Empty};
            if (result!= "token.failed")
            {
                 finalResult = new TokenResult{ TokenString = result };

            }
            return finalResult;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user != null)
                return Ok(user);
            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserDto userDto)
        {
            var result = await _userService.UpdateUserAsync(id, userDto);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

    }
}
