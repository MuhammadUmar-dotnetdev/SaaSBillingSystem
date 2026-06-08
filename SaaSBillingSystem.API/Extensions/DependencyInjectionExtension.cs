using SaaSBillingSystem.Application.Interfaces;
using SaaSBillingSystem.Infrastructure.Repositories;
using SaaSBillingSystem.Infrastructure.Services;

namespace SaaSBillingSystem.API.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IOrganizationRepository, OrganizationRepository>();
            services.AddScoped<IPlanRepository, PlanRepository>();
            services.AddScoped<IPlanFeatureRepository, PlanFeatureRepository>();
            services.AddScoped<IInvitationRepository, InvitationRepository>();
            services.AddScoped<IOrganizationMembershipRepository, OrganizationMembershipRepository>();
            return services;
        }
    }
}
