using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace StoreCenter.Infrastructure.Extensions
{
    public static class JwtExtensions
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, 
            IConfiguration configuration, string _key, string _issuer, string _audience, string _expiryInMinutes)
        {

            var key = Encoding.UTF8.GetBytes(_key);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _issuer,
                    ValidAudience = _audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    
                    //Clock skew compensates for server time drift. We recommend 5 minutes or less. It means that the token is valid for 5 minutes after the expiry time.
                    //The following code means that the token is valid for 5 minutes after the expiry time.
                    ClockSkew = TimeSpan.FromMinutes(Convert.ToDouble(_expiryInMinutes)) 
                };
            });
            return services;
        }
    }
}
