using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Minimal_API.Application.Interfaces;
using Minimal_API.Application.Mappings;
using Minimal_API.Application.Services;
using Minimal_API.Domain.Account;
using Minimal_API.Domain.Interfaces;
using Minimal_API.Domain.Interfeces;
using Minimal_API.Infra.Data.Context;
using Minimal_API.Infra.Data.Identity;
using Minimal_API.Infra.Data.Repositories;
using System.Text;

namespace Minimal_API.infra.Ioc
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("sqlserver"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
            });

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                var secretKey = configuration["Jwt:Key"];
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateLifetime = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddAutoMapper(typeof(EntitiesToDTOProfile));

            // Repositories
            services.AddScoped<IVeiculoRepository, VeiculoRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            // Services
            services.AddScoped<IVeiculoService, VeiculoService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IAuthenticate, AuthenticateService>();
            

            return services;
        }
    }
}
