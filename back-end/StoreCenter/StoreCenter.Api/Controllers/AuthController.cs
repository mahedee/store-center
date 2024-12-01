using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StoreCenter.Application.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StoreCenter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        ITokenGeneratorService _tokenGeneratorService;
        public AuthController(ITokenGeneratorService tokenGenerator)
        {
            _tokenGeneratorService = tokenGenerator;
        }

        // POST api/<AuthController>
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            // Dummy validation
            if (login.Username != "user" || login.Password != "password")
            {
                return Unauthorized();
            }

            var token = _tokenGeneratorService.GetJWTToken((login.Username, login.Username, new List<string> { "User" }));
            return Ok(token);
        }

        // PUT api/<AuthController>/5

    }

    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
