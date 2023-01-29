using TariffComparison.Core.Domain.Base;
using TariffComparison.Core.Domain.Dto;

namespace TariffComparison.Core.Application.Services
{
    public interface ICompareTariffsService
    {
        IEnumerable<TariffComparisonDto> CompareTariffs(IEnumerable<IProduct> products, decimal consumption);
    }

    public class CompareTariffsService : ICompareTariffsService
    {
        public IEnumerable<TariffComparisonDto> CompareTariffs(IEnumerable<IProduct> products, decimal consumption)
        {
            if (consumption<0)
            {
                throw new ArgumentOutOfRangeException("consumption cannot be Negative");
            }
            return products.Select(tariff => new TariffComparisonDto
            {
                TariffName = tariff.Name,
                AnnnualCost = tariff.GetAnnualCost(consumption)
            });
        }
    }
}
