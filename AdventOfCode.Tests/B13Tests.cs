using Xunit;

namespace AdventOfCode.Tests
{
    public class B13Tests
    {
        [Fact]
        public void AoCB13_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var b13 = new B13();

            // Act
            var result = b13.AoCB13();

            // Assert
            Assert.Equal(906332393333683, result);
        }

        [Fact]
        public void LCM_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var b13 = new B13();

            // Act
            // Assert
            Assert.Equal(3382932, b13.LCM(1564, 49749));
            Assert.Equal(0, b13.LCM(0, 0));
            Assert.NotEqual(-1, b13.LCM(2, 1));
        }
    }
}