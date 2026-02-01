
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ETicketing.CA.Application.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
namespace ETicketing.CA.Infrastructure.Services
{
    public sealed class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly JwtSecurityTokenHandler _handler = new();

        public TokenService(IConfiguration config)
        {
            _config = config;
        }

        public string CreateAccessToken(string userId)
        {
            return CreateToken(userId, minutes: 15);
        }

        public string CreateRefreshToken(string userId)
        {
            return CreateToken(userId, minutes: 60 * 24 * 7); // 7 días
        }

        private string CreateToken(string userId, int minutes)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]!)
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(minutes),
                signingCredentials: creds
            );

            return _handler.WriteToken(token);
        }

        public string ValidateRefreshToken(string refreshToken)
        {
            try
            {
                var key = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_config["Jwt:Key"]!)
                );

                var parameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _config["Jwt:Issuer"],
                    ValidAudience = _config["Jwt:Audience"],
                    IssuerSigningKey = key
                };

                var principal = _handler.ValidateToken(refreshToken, parameters, out _);

                return principal.FindFirstValue(JwtRegisteredClaimNames.Sub);
            }
            catch
            {
                return null;
            }
        }
    }
}
