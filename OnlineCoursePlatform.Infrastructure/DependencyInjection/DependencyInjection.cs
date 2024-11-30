using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineCoursePlatform.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using OnlineCoursePlatform.Application.Interfaces;
using OnlineCoursePlatform.Infrastructure.Services;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using OnlineCoursePlatform.Application.Features.UserManagement.Queries.GetUsers;

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
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetUsersQueryHandler).Assembly));

            // Register application services
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ICookieService, CookieService>();

            return services;
        }
    }
}
