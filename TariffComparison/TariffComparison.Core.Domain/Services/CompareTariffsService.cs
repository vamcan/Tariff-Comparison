using TariffComparison.Core.Domain.Base;
using TariffComparison.Core.Domain.Dto;

namespace TariffComparison.Core.Domain.Services
{
    public interface ICompareTariffsService
    {
        IEnumerable<TariffComparisonDto> CompareTariffs(IEnumerable<IProduct> products, decimal consumption);
    }

    public class CompareTariffsService : ICompareTariffsService
    {
        public IEnumerable<TariffComparisonDto> CompareTariffs(IEnumerable<IProduct> products, decimal consumption)
        {
            return products.Select(tariff => new TariffComparisonDto
            {
                TariffName = tariff.Name,
                AnnnualCost = tariff.GetAnnualCost(consumption)
            });
        }
    }
}
