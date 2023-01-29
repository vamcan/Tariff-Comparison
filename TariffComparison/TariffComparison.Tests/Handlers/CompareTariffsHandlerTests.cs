using Moq;
using TariffComparison.Core.Application.Services;
using TariffComparison.Core.Application.Tariff.CompareTariffs;
using TariffComparison.Core.Domain.Base;
using TariffComparison.Core.Domain.Contracts.Repository;
using TariffComparison.Core.Domain.Dto;
using TariffComparison.Core.Domain.Entities;
using TariffComparison.Core.Domain.ValueObjects;
using Xunit;

namespace TariffComparison.Tests.Handlers
{
    public class CompareTariffsHandlerTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<ICompareTariffsService> _compareTariffsServiceMock;
        private readonly CompareTariffsHandler _compareTariffsHandler;

        public CompareTariffsHandlerTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _compareTariffsServiceMock = new Mock<ICompareTariffsService>();
            _compareTariffsHandler = new CompareTariffsHandler(_productRepositoryMock.Object, _compareTariffsServiceMock.Object);
        }

        [Fact]
        public async Task Handle_Should_Return_SuccessResult_With_TariffComparisonDto_List()
        {
            // Arrange
            var request = new CompareTariffsRequest { Consumption = 3500 };
            var expectedTariffComparisonDtoList = new List<TariffComparisonDto>
            {
                new TariffComparisonDto { TariffName = "Tariff A", AnnnualCost = 830 },
                new TariffComparisonDto { TariffName = "Tariff B", AnnnualCost = 800 }
            };
            _productRepositoryMock.Setup(x => x.GetAllAsync())
            .ReturnsAsync(new List<IProduct>
                {
                    BasicTariff.Create("basic electricity tariff",new Money(5),new Money(new decimal(0.22))),
                    PackagedTariff.Create("Packaged tariff",new Money(800),new Money(new decimal(0.30)),4000)
                });
            _compareTariffsServiceMock.Setup(x => x.CompareTariffs(It.IsAny<IEnumerable<IProduct>>(), request.Consumption))
                .Returns(expectedTariffComparisonDtoList);

            // Act
            var result = await _compareTariffsHandler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(expectedTariffComparisonDtoList, result.Result);
        }

        [Fact]
        public async Task Handle_Should_Return_FailureResult_With_Exception_Message()
        {
            // Arrange
            var request = new CompareTariffsRequest { Consumption = 3500 };
            var expectedExceptionMessage = "Something went wrong";
            _productRepositoryMock.Setup(x => x.GetAllAsync())
                .ThrowsAsync(new Exception(expectedExceptionMessage));

            // Act
            var result = await _compareTariffsHandler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(expectedExceptionMessage, result.ErrorMessage);
        }
    }
}
