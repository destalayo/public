using ETicketing.CA.Application.Abstractions;
using ETicketing.CA.Infrastructure.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ETicketing.CA.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddController(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<ISessionService, SessionApiService>();
            services.AddScoped<IAuditScope, AuditScope>();
            services.AddCors(options => { options.AddDefaultPolicy(policy => { policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); }); });
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!)
                        )
                    };
                });

            services.AddAuthorization();
            return services;
        }
    }
}
