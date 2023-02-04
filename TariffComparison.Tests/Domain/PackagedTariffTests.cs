using TariffComparison.Core.Domain.Base.Exceptions;
using TariffComparison.Core.Domain.Entities;
using TariffComparison.Core.Domain.ValueObjects;
using Xunit;

namespace TariffComparison.Tests.Domain
{
    public class PackagedTariffTests
    {
        [Fact]
        public void PackagedTariff_Can_Be_Created_Successfully()
        {
            // Arrange
            var name = "Test Tariff";
            var baseCost = Money.CreateIfNotEmpty(10);
            var additionalCostPerKWh = Money.CreateIfNotEmpty(2);
            var threshold = 100;

            // Act
            var packagedTariff = PackagedTariff.Create(name, baseCost, additionalCostPerKWh, threshold);

            // Assert
            Assert.NotEqual(Guid.Empty, packagedTariff.Id);
            Assert.Equal(name, packagedTariff.Name);
            Assert.Equal(baseCost, packagedTariff.BaseCost);
            Assert.Equal(additionalCostPerKWh, packagedTariff.AdditionalCostPerKWh);
            Assert.Equal(threshold, packagedTariff.Threshold);
        }

        [Fact]
        public void PackagedTariff_Throws_Exception_If_Name_Is_Null_Or_Empty()
        {
            // Arrange
            var name = string.Empty;
            var baseCost = Money.CreateIfNotEmpty(10);
            var additionalCostPerKWh = Money.CreateIfNotEmpty(2);
            var threshold = 100;

            // Act
            var ex = Assert.Throws<DomainStateException>(() =>
                PackagedTariff.Create(name, baseCost, additionalCostPerKWh, threshold));

            // Assert
            Assert.Equal("Name cannot be null or empty.", ex.Message);
        }

        [Fact]
        public void PackagedTariff_Throws_Exception_If_BaseCost_Is_Null()
        {
            // Arrange
            var name = "Test Tariff";
            Money baseCost = null;
            var additionalCostPerKWh = Money.CreateIfNotEmpty(2);
            var threshold = 100;

            // Act
            var ex = Assert.Throws<DomainStateException>(() =>
                PackagedTariff.Create(name, baseCost, additionalCostPerKWh, threshold));

            // Assert
            Assert.Equal("baseCost cannot be null", ex.Message);
        }

        [Fact]
        public void PackagedTariff_Throws_Exception_If_AdditionalCostPerKWh_Is_Null()
        {
            // Arrange
            var name = "Test Tariff";
            var baseCost = Money.CreateIfNotEmpty(10);
            Money additionalCostPerKWh = null;
            var threshold = 100;

            // Act
            var ex = Assert.Throws<DomainStateException>(() =>
                PackagedTariff.Create(name, baseCost, additionalCostPerKWh, threshold));

            // Assert
            Assert.Equal("additional Cost Per KWh cannot be null", ex.Message);
        }

        [Fact]
        public void PackagedTariff_Throws_Exception_If_Threshold_Is_Less_Than_Or_Equal_To_Zero()
        {
            // Arrange
            var name = "Test Tariff";
            var baseCost = Money.CreateIfNotEmpty(10);
            var additionalCostPerKWh = Money.CreateIfNotEmpty(2);
            var threshold = -100;
            // Act
            var ex = Assert.Throws<DomainStateException>(() =>
                PackagedTariff.Create(name, baseCost, additionalCostPerKWh, threshold));

            // Assert
            Assert.Equal("Threshold must be greater than zero.", ex.Message);
        }

        [Fact]
        public void Calculate_Cost_Below_Threshold()
        {
            // Arrange
            var name = "Test Tariff";
            var baseCost = Money.CreateIfNotEmpty(10);
            var additionalCostPerKWh = Money.CreateIfNotEmpty(2);
            var threshold = 100;
            var consumption = 50;
            var packagedTariff = PackagedTariff.Create(name, baseCost, additionalCostPerKWh, threshold);

            // Act
            var cost = packagedTariff.GetAnnualCost(consumption);

            // Assert
            Assert.Equal(baseCost.Value, cost);
        }
        [Fact]
        public void Calculate_Cost_Above_Threshold()
        {
            // Arrange
            var name = "Test Tariff";
            var baseCost = Money.CreateIfNotEmpty(10);
            var additionalCostPerKWh = Money.CreateIfNotEmpty(2);
            var threshold = 100;
            var consumption = 150;
            var packagedTariff = PackagedTariff.Create(name, baseCost, additionalCostPerKWh, threshold);

            // Act
            var cost = packagedTariff.GetAnnualCost(consumption);

            // Assert
            var expectedCost = baseCost.Value + (consumption - threshold) * additionalCostPerKWh.Value;
            Assert.Equal(expectedCost, cost);
        }
    }
}
