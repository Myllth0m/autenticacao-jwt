using authenticationJWT.Dtos;
using authenticationJWT.Models;
using AuthenticationJWT.Data;
using Microsoft.AspNetCore.Mvc;

namespace authenticationJWT.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IUserRepository _repository;
        public AuthController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterDTO dto)
        {
            var user = new User(dto.Name, dto.Email, BCrypt.Net.BCrypt.HashPassword(dto.Password));

            return Created("success", _repository.Create(user));
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDTO dto)
        {
            var user = _repository.GetByEmail(dto.Email);

            if (user == null) return BadRequest(new { message = "Users with email not exists" });

            if (BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
                return Ok(user);

            return BadRequest(new { message = "users not found" });
        }
    }
}