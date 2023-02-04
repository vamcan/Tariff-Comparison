using TariffComparison.Core.Domain.Base.Exceptions;
using TariffComparison.Core.Domain.Entities;
using TariffComparison.Core.Domain.ValueObjects;
using Xunit;

namespace TariffComparison.Tests.Domain
{
    public class BasicTariffTests
    {
        [Fact]
        public void BasicTariff_Create_ShouldCreateInstance_WithValidInput()
        {
            // Arrange
            string name = "basic electricity tariff";
            Money baseCostPerMonth = new Money(5);
            Money consumptionCostPerKWh = new Money(new decimal(0.22));

            // Act
            var basicTariff = BasicTariff.Create(name, baseCostPerMonth, consumptionCostPerKWh);

            // Assert
            Assert.NotNull(basicTariff);
            Assert.NotEqual(Guid.Empty, basicTariff.Id);
            Assert.Equal(name, basicTariff.Name);
            Assert.Equal(baseCostPerMonth, basicTariff.BaseCostPerMonth);
            Assert.Equal(consumptionCostPerKWh, basicTariff.ConsumptionCostPerKWh);
        }
        [Theory]
        [InlineData(null, 0, 0)]
        [InlineData("", 0, 0)]
      public void BasicTariff_Create_ShouldThrowException_WithInvalidName(string name, decimal baseCostPerMonth, decimal consumptionCostPerKWh)
        {
            // Arrange
            Money baseCost = new Money(baseCostPerMonth);
            Money consumptionCost = new Money(consumptionCostPerKWh);

            // Act & Assert
            Assert.Throws<DomainStateException>(() => BasicTariff.Create(name, baseCost, consumptionCost));
        }
        [Theory]
        [InlineData(0, 0)]
        [InlineData(0, 0.22)]
        [InlineData(5, 0)]
        public void BasicTariff_Create_ShouldThrowException_WithInvalidCosts(decimal baseCostPerMonth, decimal consumptionCostPerKWh)
        {
            // Arrange
            string name = "basic electricity tariff";
            Money baseCost = baseCostPerMonth == 0 ? null : new Money(baseCostPerMonth);
            Money consumptionCost = consumptionCostPerKWh == 0 ? null : new Money(consumptionCostPerKWh);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => BasicTariff.Create(name, baseCost, consumptionCost));
        }

        [Fact]
        public void BasicTariff_GetAnnualCost_ShouldReturnCorrectAnnualCost()
        {
            // Arrange
            decimal consumption = 3500;
            string name = "basic electricity tariff";
            Money baseCostPerMonth = new Money(5);
            Money consumptionCostPerKWh = new Money(new decimal(0.22));
            var basicTariff = BasicTariff.Create(name, baseCostPerMonth, consumptionCostPerKWh);

            // Act
            var annualCost = basicTariff.GetAnnualCost(consumption);

            // Assert
            Assert.Equal(830, annualCost);
        }
    }
}
