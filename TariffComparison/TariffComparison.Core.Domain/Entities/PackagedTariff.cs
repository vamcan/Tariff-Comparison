using TariffComparison.Core.Domain.Base;
using TariffComparison.Core.Domain.Base.Exceptions;
using TariffComparison.Core.Domain.ValueObjects;

namespace TariffComparison.Core.Domain.Entities
{
    public class PackagedTariff : IProduct, IAggregateRoot
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
            if (string.IsNullOrEmpty(name))
                throw new DomainStateException("Name cannot be null or empty.");

            if (baseCost == null)
                throw new DomainStateException("baseCost cannot be null");

            if (additionalCostPerKWh == null)
                throw new DomainStateException("additional Cost Per KWh cannot be null");

            if (threshold <= 0)
                throw new DomainStateException("Threshold must be greater than zero.");

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
