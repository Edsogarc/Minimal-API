using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Minimal_API.Domain.Account;
using Minimal_API.Infra.Data.Context;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace Minimal_API.Infra.Data.Identity
{
    public class AuthenticateService : IAuthenticate
    {
        private readonly ApplicationDbContext _context;
        private readonly SymmetricSecurityKey _key;
        private readonly IConfiguration _configuration;
        public AuthenticateService(IConfiguration configuration, ApplicationDbContext context)
        {
            _context = context;
            _configuration = configuration;
            var key = _configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("A chave secreta JWT ('Jwt:Key') não foi configurada.");

            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        }

        public bool Authenticate(string email, string password)
        {
            var usuario = _context.Usuario.Where(u => u.Email.ToLower() == email.ToLower()).FirstOrDefault();
            if (usuario == null) return false;

            using var hmac = new HMACSHA256(usuario.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != usuario.PasswordHash[i]) return false;
            }
            return true;
        }
        public bool UserExists(string email)
        {
            var usuario = _context.Usuario.Any(u => u.Email.ToLower() == email.ToLower());

            return usuario;
        }
        public string GenerateToken(int id, string email, string perfil = "Editor")
        {
            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim("Id", id.ToString()),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, perfil)
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
