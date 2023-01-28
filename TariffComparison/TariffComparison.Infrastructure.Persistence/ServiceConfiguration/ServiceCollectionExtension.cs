using Microsoft.Extensions.DependencyInjection;
using TariffComparison.Core.Domain.Contracts.Repository;
using TariffComparison.Infrastructure.Persistence.Repository;

namespace TariffComparison.Infrastructure.Persistence.ServiceConfiguration
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            return services;
        }
    }
}
