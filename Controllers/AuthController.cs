using authenticationJWT.Dtos;
using authenticationJWT.Models;
using authenticationJWT.Services;
using AuthenticationJWT.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace authenticationJWT.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IUserRepository _repository;
        private readonly JwtService _jwtService;
        public AuthController(
            IUserRepository repository,
            JwtService jwtService)
        {
            _repository = repository;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterDTO dto)
        {
            var userAlreadyExists = _repository.GetByEmail(dto.Email);

            if (userAlreadyExists != null) return BadRequest(new { message = "User with email already exist" });

            var user = new User(dto.Name, dto.Email, BCrypt.Net.BCrypt.HashPassword(dto.Password));

            return Created("success", _repository.Create(user));
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDTO dto)
        {
            var user = _repository.GetByEmail(dto.Email);

            if (user == null)
                return BadRequest(new { message = "Users with email not exists" });

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
                return BadRequest(new { message = "Invalid credentials" });

            var jwt = _jwtService.Generate(user.Id);

            Response.Cookies.Append("jwt", jwt, new CookieOptions { HttpOnly = true });

            return Ok(new { message = "Success" });
        }

        [HttpGet("get-user")]
        public IActionResult GetUser()
        {
            var jwt = Request.Cookies["jwt"];
            var token = _jwtService.Verify(jwt);
            int userId = int.Parse(token.Issuer);
            var user = _repository.GetById(userId);

            return Ok(user);
        }

        [HttpGet("user-list")]
        public IActionResult UserList()
        {
            var users = _repository.GetAll();

            return Ok(users);
        }

        [HttpPost("logout")]
        public IActionResult LogOut()
        {
            Response.Cookies.Delete("jwt");

            return Ok(new { message = "Success" });
        }
    }
}