using QC4Common.Classes.HatchColor;
using System;
using Xunit;

namespace QC4Test.HatchColorTests
{
    public class ColorPresetExceptionTests
    {
        [Fact]
        public void Constructor_WithMessage_SetsErrorMessage()
        {
            // Arrange
            string expectedMessage = "Test error message";

            // Act
            var exception = new ColorPresetException(expectedMessage);

            // Assert
            Assert.Equal(expectedMessage, exception.Message);
        }

        [Fact]
        public void Constructor_WithMessageAndInnerException_SetsErrorMessageAndInnerException()
        {
            // Arrange
            string expectedMessage = "Test error message";
            var innerException = new Exception("Inner exception message");

            // Act
            var exception = new ColorPresetException(expectedMessage, innerException);

            // Assert
            Assert.Equal(expectedMessage, exception.Message);
            Assert.Same(innerException, exception.InnerException);
        }
    }
}
