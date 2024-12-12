using Microsoft.IdentityModel.Tokens;
using Minimal_API.Domain.Interfaces;
using Minimal_API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace Minimal_API.Domain.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
            var key = _configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("A chave secreta JWT ('Jwt:Key') não foi configurada.");

            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        }
        public string CreateToken(Administrador administrador)
        {
            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, administrador.Email),
                new Claim("Perfil", administrador.Perfil),
                new Claim(ClaimTypes.Role, administrador.Perfil)
            };
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
