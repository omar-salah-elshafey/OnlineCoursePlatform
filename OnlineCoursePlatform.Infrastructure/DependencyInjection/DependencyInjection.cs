using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineCoursePlatform.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using OnlineCoursePlatform.Application.Interfaces;
using OnlineCoursePlatform.Application.Services;
using OnlineCoursePlatform.Infrastructure.Services;

namespace OnlineCoursePlatform.Infrastructure.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Register DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnections")));

            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromMinutes(5); // Set token expiration to .... minutes
            });

            // Register application services, e.g., AuthService
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPasswordManagementService, PasswordManagementService>();
            services.AddScoped<IUserManagementService, UserManagementService>();
            services.AddScoped<ICookieService, CookieService>();

            return services;
        }
    }
}
