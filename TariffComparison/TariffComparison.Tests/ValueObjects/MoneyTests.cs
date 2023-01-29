using TariffComparison.Core.Domain.Base.Exceptions;
using TariffComparison.Core.Domain.ValueObjects;
using Xunit;

namespace TariffComparison.Tests.ValueObjects
{
    public class MoneyTests
    {
        [Fact]
        public void Money_CreateWithNegativeAmount_ShouldThrowException()
        {
            // Arrange
            decimal negativeAmount = -100;

            // Act
            Action act = () => new Money(negativeAmount);

            // Assert
            var exception = Assert.Throws<InvalidValueObjectStateException>(act);
            Assert.Equal("The amount cannot be negative", exception.Message);
        }

     
        [Fact]
        public void Money_AdditionOperator_ShouldReturnNewInstanceWithSumAmount()
        {
            // Arrange
            Money money1 = new Money(100);
            Money money2 = new Money(50);

            // Act
            Money sumMoney = money1 + money2;

            // Assert
            Assert.NotEqual(money1, sumMoney);
            Assert.NotEqual(money2, sumMoney);
            Assert.Equal(150, sumMoney.Value);
        }

        [Fact]
        public void Money_SubtractionOperator_ShouldReturnNewInstanceWithSubtractedAmount()
        {
            // Arrange
            Money money1 = new Money(110);
            Money money2 = new Money(50);

            // Act
            Money subtractedMoney = money1 - money2;

            // Assert
            Assert.NotEqual(money1, subtractedMoney);
            Assert.NotEqual(money2, subtractedMoney);
            Assert.Equal(60, subtractedMoney.Value);
        }
     
        [Fact]
        public void Test_Addition_Operator_Returns_Expected_Result()
        {
            // Arrange
            Money money1 = new Money(20);
            Money money2 = new Money(10);

            // Act
            Money result = money1 + money2;

            // Assert
            Assert.Equal(30, result.Value);
        }

        [Fact]
        public void Test_Subtraction_Operator_Returns_Expected_Result()
        {
            // Arrange
            Money money1 = new Money(20);
            Money money2 = new Money(10);

            // Act
            Money result = money1 - money2;

            // Assert
            Assert.Equal(10, result.Value);
        }

        [Fact]
        public void Test_Less_Than_Operator_Returns_Expected_Result()
        {
            // Arrange
            Money money1 = new Money(10);
            Money money2 = new Money(20);

            // Act
            bool result = money1 < money2;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Test_Greater_Than_Operator_Returns_Expected_Result()
        {
            // Arrange
            Money money1 = new Money(20);
            Money money2 = new Money(10);

            // Act
            bool result = money1 > money2;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Test_Less_Than_Or_Equal_Operator_Returns_Expected_Result()
        {
            // Arrange
            Money money1 = new Money(10);
            Money money2 = new Money(20);

            // Act
            bool result = money1 <= money2;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Test_Greater_Than_Or_Equal_Operator_Returns_Expected_Result()
        {
            // Arrange
            Money money1 = new Money(20);
            Money money2 = new Money(20);

            // Act
            bool result = money1 >= money2;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Test_CreateIfNotEmpty_Method_Returns_Expected_Result()
        {
            // Arrange
            decimal? value = 10;

            // Act
            Money result = Money.CreateIfNotEmpty(value);

            // Assert
            Assert.Equal(10, result.Value);
        }
        [Fact]
        public void Test_CreateIfNotEmpty_Method_Returns_Null_For_Empty_Value()
        {
            // Arrange
            decimal? value = null;

            // Act
            Money result = Money.CreateIfNotEmpty(value);

            // Assert
            Assert.Null(result);
        }
    }
}

