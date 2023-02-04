﻿using TariffComparison.Core.Application.Base.Common;
using Xunit;

namespace TariffComparison.Tests.Base
{
    public class OperationResultTests
    {
        [Fact]
        public void Test_SuccessResult()
        {
            // Arrange
            var expectedResult = "test result";

            // Act
            var result = OperationResult<string>.SuccessResult(expectedResult);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(expectedResult, result.Result);
            Assert.Null(result.ErrorMessage);
            Assert.False(result.IsException);
            Assert.False(result.IsNotFound);
        }

        [Fact]
        public void Test_FailureResult()
        {
            // Arrange
            var expectedErrorMessage = "test error message";
           

            // Act
            var result = OperationResult<string>.FailureResult(expectedErrorMessage);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(expectedErrorMessage, result.ErrorMessage);
            Assert.True(result.IsException);
            Assert.False(result.IsNotFound);
        }

        [Fact]
        public void Test_NotFoundResult()
        {
            // Arrange
            var expectedErrorMessage = "test error message";

            // Act
            var result = OperationResult<string>.NotFoundResult(expectedErrorMessage);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(expectedErrorMessage, result.ErrorMessage);
            Assert.Equal(default, result.Result);
            Assert.True(result.IsException);
            Assert.True(result.IsNotFound);
        }
    }
}
