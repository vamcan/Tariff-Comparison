using TariffComparison.Core.Domain.Base;
using TariffComparison.Core.Domain.ValueObjects;

namespace TariffComparison.Core.Domain.Entities
{
    public class PackagedTariff : IBaseEntity, IProduct, IAggregateRoot
    {
        private PackagedTariff()
        {

        }
        public Guid Id { get; private init; }
        public string Name { get; private init; }
        public Money BaseCost { get; set; }
        public Money AdditionalCostPerKWh { get; set; }
        public int Threshold { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public decimal GetAnnualCost(decimal consumption)
        {
            if (consumption <= Threshold)
                return BaseCost.Value;
            else
                return BaseCost.Value + (consumption - Threshold) * AdditionalCostPerKWh.Value;

        }
        public static PackagedTariff Create(string name, Money baseCost, Money additionalCostPerKWh, int threshold)
        {
            var packagedTariff = new PackagedTariff
            {
                Id = Guid.NewGuid(),
                Name = name,
                BaseCost = baseCost,
                AdditionalCostPerKWh = additionalCostPerKWh,
                Threshold = threshold
            };
            return packagedTariff;
        }
    }
}
