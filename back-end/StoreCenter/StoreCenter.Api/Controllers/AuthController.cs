using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreCenter.Api.Helpers;
using StoreCenter.Application.Dtos;
using StoreCenter.Application.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StoreCenter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize()]
    public class AuthController : ControllerBase
    {
        //ITokenGeneratorService _tokenGeneratorService;
        IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            //_tokenGeneratorService = tokenGenerator;
            _authService = authService;
        }

        [HttpPost("signup")]
        [ProducesDefaultResponseType(typeof(SignUpDto))]
        [AllowAnonymous]

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
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            var token = await _authService.LoginAsync(login);
            return Ok(token);
        }

        // PUT api/<AuthController>/5

    }
}
