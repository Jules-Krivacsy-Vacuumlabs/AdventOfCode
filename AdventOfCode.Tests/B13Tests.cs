using Xunit;

namespace AdventOfCode.Tests
{
    public class B13Tests
    {
        [Fact]
        public void AoCB13_Should()
        {
            // Arrange
            var b13 = new B13();

            // Act
            var result = b13.AoCB13();

            // Assert
            Assert.Equal(906332393333683, result);
        }

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(1564, 49749, 3382932)]
        public void LCM_Should(long a, long b, long expected)
        {
            // Arrange
            var b13 = new B13();

            // Act
            var result = b13.LCM(a, b);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}