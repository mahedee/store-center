using Microsoft.AspNetCore.Mvc;
using StoreCenter.Api.Helpers;
using StoreCenter.Application.Dtos;
using StoreCenter.Application.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StoreCenter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        ITokenGeneratorService _tokenGeneratorService;
        IAuthService _authService;
        public AuthController(ITokenGeneratorService tokenGenerator, IAuthService authService)
        {
            _tokenGeneratorService = tokenGenerator;
            _authService = authService;
        }

        [HttpPost("signup")]
        [ProducesDefaultResponseType(typeof(SignUpDto))]
        public async Task<IActionResult> SignUp([FromBody] SignUpDto signUpDto)
        {
            var result = await _authService.SignUpAsync(signUpDto);

            if (!result.Success)
            {
                return ApiResponseHelper.ValidationError(result.Errors);
            }

            return ApiResponseHelper.Success(null, "User created successfully");
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
