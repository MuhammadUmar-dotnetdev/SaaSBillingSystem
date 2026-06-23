using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SaaSBillingSystem.Application.Features.Auth.LoginUser;
using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Shared.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SaaSBillingSystem.Infrastructure.Services
{
    public class JwtService: IJwtService
    {
        private readonly IConfiguration _configuration;
        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public JwtDTO GenerateToken(AuthContext authContext)
        {
            var jwtSettings = _configuration.GetSection("Jwt");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, authContext.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, authContext.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, authContext.Email),
                new Claim("organization_id", authContext.OrganizationId.ToString()),
                new Claim(ClaimTypes.Role, authContext.Role.ToString()),
                new Claim("organization_name", authContext.OrganizationName)
            };

            var token = new JwtSecurityToken(
                    issuer: jwtSettings["Issuer"],
                    audience: jwtSettings["Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["ExpiresMinutes"]!)),
                    signingCredentials: credentials
                );

            var generatedToken = new JwtSecurityTokenHandler().WriteToken(token);
            return new JwtDTO
            {
                AccessToken = generatedToken,
                ExpiresAtUtc = token.ValidTo
            };
        }
    }
}
