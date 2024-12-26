using StoreCenter.Application.Interfaces;
using StoreCenter.Infrastructure.Interfaces;

namespace StoreCenter.Application.Services
{
    public class TokenGeneratorService : ITokenGeneratorService
    {
        ITokenGenerator _tokenGenerator;
        public TokenGeneratorService(ITokenGenerator tokenGenerator) 
        {
            _tokenGenerator = tokenGenerator;
        }
        public string GetJWTToken((string userId, string userName, string email, IList<string> roles, IList<string> permissions) userDetails)
        {
            return _tokenGenerator.GenerateJWTToken(userDetails);
        }
    }
}
