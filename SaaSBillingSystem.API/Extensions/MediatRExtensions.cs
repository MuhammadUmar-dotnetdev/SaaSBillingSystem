using System.Reflection;

namespace SaaSBillingSystem.API.Extensions
{
    public static class MediatRExtensions
    {
        public static IServiceCollection AddMediatRConfig(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.Load("SaaSBillingSystem.Application"));
            });
            return services;
        }
    }
}
