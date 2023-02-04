using Moq;
using TariffComparison.Core.Application.Services;
using TariffComparison.Core.Domain.Base;
using TariffComparison.Core.Domain.Contracts.Repository;
using TariffComparison.Core.Domain.Entities;
using TariffComparison.Core.Domain.ValueObjects;
using Xunit;

namespace TariffComparison.Tests.Services
{
    public class CompareTariffsServiceTests
    {
        [Fact]
        public void CompareTariffs_ShouldReturnCorrectComparisonResults()
        {
            // Arrange
            decimal consumption = 3500;
            var products = new List<IProduct>
            {
                BasicTariff.Create("basic electricity tariff",new Money(5),new Money(new decimal(0.22))),
                PackagedTariff.Create("Packaged tariff",new Money(800),new Money(new decimal(0.30)),4000)
            };
            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(products);

            var compareTariffsService = new CompareTariffsService();

            // Act
            var result = compareTariffsService.CompareTariffs(products, consumption).ToList();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Equal("basic electricity tariff", result[0].TariffName);
            Assert.Equal(830, result[0].AnnnualCost);
            Assert.Equal("Packaged tariff", result[1].TariffName);
            Assert.Equal(800, result[1].AnnnualCost);
        }
        [Fact]
        public void CompareTariffs_ShouldReturnEmptyList_WhenNoProductsFound()
        {
            // Arrange
            decimal consumption = 3500;
            var products = new List<IProduct>();
            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(products);

            var compareTariffsService = new CompareTariffsService();

            // Act
            var result = compareTariffsService.CompareTariffs(products, consumption).ToList();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void CompareTariffs_ShouldThrowException_WhenConsumptionIsLessThanZero()
        {
            // Arrange
            decimal consumption = -3500;
            var products = new List<IProduct>
            {
                BasicTariff.Create("basic electricity tariff",new Money(5),new Money(new decimal(0.22))),
                PackagedTariff.Create("Packaged tariff",new Money(800),new Money(new decimal(0.30)),4000)
            };
            var mockProductRepository = new Mock<IProductRepository>();
            mockProductRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(products);

            var compareTariffsService = new CompareTariffsService();

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => compareTariffsService.CompareTariffs(products, consumption).ToList());
        }

    }
}

