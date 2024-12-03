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

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<string?> LoginAsync(LoginDto loginDto)
        {
            throw new NotImplementedException();
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
