using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;
using TariffComparison.Core.Domain.Services;

namespace TariffComparison.Core.Application.ServiceConfiguration
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICompareTariffsService, CompareTariffsService>();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
