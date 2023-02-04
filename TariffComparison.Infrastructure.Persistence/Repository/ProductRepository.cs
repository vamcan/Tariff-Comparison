using TariffComparison.Core.Domain.Base;
using TariffComparison.Core.Domain.Contracts.Repository;
using TariffComparison.Core.Domain.Entities;
using TariffComparison.Core.Domain.ValueObjects;

namespace TariffComparison.Infrastructure.Persistence.Repository
{
    public class ProductRepository : IProductRepository
    {
        public Task<List<IProduct>> GetAllAsync()
        {
            var productList = new List<IProduct>()
            {
                BasicTariff.Create("basic electricity tariff",new Money(5),new Money(new decimal(0.22))),
                PackagedTariff.Create("Packaged tariff",new Money(800),new Money(new decimal(0.30)),4000)
            };
            return Task.FromResult(productList);
        }
    }
}
