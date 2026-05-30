using Microsoft.EntityFrameworkCore;
using SaaSBillingSystem.Infrastructure.Persistence;
using System.Runtime.CompilerServices;

namespace SaaSBillingSystem.API.Extensions
{
    public static class PersistenceExtension
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")).UseSnakeCaseNamingConvention();
            });
            return services;
        }
    }
}
