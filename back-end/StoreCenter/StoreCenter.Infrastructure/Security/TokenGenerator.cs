using StoreCenter.Infrastructure.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StoreCenter.Infrastructure.Security
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly string _key;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly string _expiryMinutes;

        public TokenGenerator(string key, string issuer, string audience, string expiryMinutes)
        {
            _key = key;
            _issuer = issuer;
            _audience = audience;
            _expiryMinutes = expiryMinutes;
        }

        public string GenerateJWTToken((string userId, string userName, IList<string> roles) userDetails)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Deconstruct the tuple
            // Deconstructing a tuple is a feature that allows you to break a tuple into its individual parts.
            var (userId, userName, roles) = userDetails;

            // Claims is a collection of key-value pairs that represent the subject of the token.
            // Claims are used to store information about the user, such as the user's name, email, and roles.
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, userName), // Subject of the token. For example, the user's name.
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // Unique identifier for the token. For example, a GUID.
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()), // Time the token was issued.Iat means "issued at".
                new Claim(ClaimTypes.NameIdentifier, userId), // Unique identifier for the user. For example, the user's ID.
                new Claim(ClaimTypes.Name, userName) // Name of the user. For example, the user's Full name.
            };

            // Add roles to the claims
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_expiryMinutes)),
                signingCredentials: signingCredentials
            );

            var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodedToken;
        }
    }
}
