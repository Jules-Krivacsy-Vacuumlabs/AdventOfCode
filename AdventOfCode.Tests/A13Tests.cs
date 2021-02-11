using AdventOfCode;
using System;
using Xunit;

namespace AdventOfCode.Tests
{
    public class A13Tests
    {
        [Fact]
        public void Main_Should()
        {
            // Arrange
            var expected = 2382;
            // Act
            var result = A13.Main();
            // Assert
            Assert.Equal(expected, result);
        }
    }
}
