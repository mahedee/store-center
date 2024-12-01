﻿using Microsoft.AspNetCore.Mvc;
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

            /*
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Thisis@secreteKey&KeepitSecret&Dontdiscloseit"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, login.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var token = new JwtSecurityToken(
                issuer: "https://mahedee.net",
                audience: "https://mahedee.net",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            return Ok(new { Token = tokenHandler.WriteToken(token) });

            */
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
