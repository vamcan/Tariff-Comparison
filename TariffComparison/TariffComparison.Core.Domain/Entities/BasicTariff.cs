﻿using TariffComparison.Core.Domain.Base;
using TariffComparison.Core.Domain.Base.Exceptions;
using TariffComparison.Core.Domain.ValueObjects;

namespace TariffComparison.Core.Domain.Entities
{
    public class BasicTariff : IBaseEntity, IProduct, IAggregateRoot
    {
        private BasicTariff()
        {

        }
        public Guid Id { get; private init; }
        public string Name { get; private init; }
        public Money BaseCostPerMonth { get; private init; }
        public Money ConsumptionCostPerKWh { get; private init; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public decimal GetAnnualCost(decimal consumption)
        {
            return (BaseCostPerMonth.Value * 12) + (consumption * ConsumptionCostPerKWh.Value);
        }

        public static BasicTariff Create(string name, Money baseCostPerMonth, Money consumptionCostPerKWh)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new DomainStateException("Name cannot be null or empty.");
            }
            if (baseCostPerMonth== null)
            {
                throw new ArgumentException("baseCostPerMonth cannot be null");
            }
            if (consumptionCostPerKWh== null)
            {
                throw new ArgumentException("consumptionCostPerKWh cannot be null");
            }
            var basicTariff = new BasicTariff
            {
                Id = Guid.NewGuid(),
                Name = name,
                BaseCostPerMonth = baseCostPerMonth,
                ConsumptionCostPerKWh = consumptionCostPerKWh,
            };
            return basicTariff;
        }
    }
}
