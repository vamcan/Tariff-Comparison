using TariffComparison.Core.Domain.Base;

namespace TariffComparison.Core.Domain.Contracts.Repository
{
    public interface IProductRepository
    {
        public Task<IList<IProduct>> GetAllAsync();
    }
}
