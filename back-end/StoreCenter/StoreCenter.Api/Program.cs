using Microsoft.OpenApi.Models;
using StoreCenter.Application.Extensions;
using StoreCenter.Application.Interfaces;
using StoreCenter.Application.Services;
using StoreCenter.Infrastructure.Extensions;

namespace StoreCenter.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

   
            var _key = builder.Configuration["Jwt:Key"] ?? throw new ArgumentNullException("Jwt:Key");
            var _issuer = builder.Configuration["Jwt:Issuer"] ?? throw new ArgumentNullException("Jwt:Issuer");
            var _audience = builder.Configuration["Jwt:Audience"] ?? throw new ArgumentNullException("Jwt:Audience");
            var _expiryInMinutes = builder.Configuration["Jwt:ExpiryInMinutes"] ?? throw new ArgumentNullException("Jwt:ExpiryInMinutes");


            // Add DbContext
            builder.Services.AddDbContext(builder.Configuration);
            // Add Jwt Authentication
            builder.Services.AddJwtAuthentication(builder.Configuration, _key, _issuer, _audience, _expiryInMinutes);

            // Add Dependency Injection for Application
            builder.Services.AddApplication(builder.Configuration);

            // Add Dependency Injection for Infrastructure
            builder.Services.AddInfrastructure(builder.Configuration, _key, _issuer, _audience, _expiryInMinutes);
           
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Test API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Configure the HTTP request pipeline.
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
