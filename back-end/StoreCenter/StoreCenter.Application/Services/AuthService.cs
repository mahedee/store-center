using StoreCenter.Application.Dtos;
using StoreCenter.Application.Helper;
using StoreCenter.Application.Interfaces;
using StoreCenter.Domain.Entities;
using StoreCenter.Infrastructure.Interfaces;

namespace StoreCenter.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly ITokenGeneratorService _tokenGeneratorService;

        public AuthService(IUserRepository userRepository,
            IAuthenticationManager authenticationManager,
            ITokenGeneratorService tokenGeneratorService)
        {
            _userRepository = userRepository;
            _authenticationManager = authenticationManager;
            _tokenGeneratorService = tokenGeneratorService;
        }

        public async Task<string?> LoginAsync(LoginDto loginDto)
        {
            bool result = await _authenticationManager.SigninUserAsync(loginDto.UserName, PasswordHasher.HashPassword(loginDto.Password));

            // If user is not authenticated return null
            // To do: send proper response
            if (!result)
            {
                return null;
            }

            // If user is authenticated, generate token
            var user = await _userRepository.GetUserByUserNameAsync(loginDto.UserName);
            string token = _tokenGeneratorService.GetJWTToken((user.Id.ToString(), user.UserName, new List<string> { "User" }));
            return token;
        }

        public async Task<(bool Success, List<string> Errors)> SignUpAsync(SignUpDto signUpDto)
        {
            if (signUpDto.Password != signUpDto.ConfirmPassword)
            {
                return (false, new List<string> { "Passwords do not match" });
            }

            var existingUser = await _userRepository.GetUserByUserNameAsync(signUpDto.Username);
            if (existingUser != null)
            {
                // match email if it exists add to error list
                return (false, new List<string> { "User already exists" });
            }

            var user = new User
            {
                UserName = signUpDto.Username,
                Email = signUpDto.Email,
                PasswordHash = PasswordHasher.HashPassword(signUpDto.Password)
            };

            await _userRepository.AddUserAsync(user);
            await _userRepository.SaveChangesAsync();


            return (true, new List<string>() { "User created successfully" });
        }
    }
}
