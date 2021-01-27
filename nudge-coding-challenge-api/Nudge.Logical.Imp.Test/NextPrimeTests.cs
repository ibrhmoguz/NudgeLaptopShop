using System;
using Xunit;

namespace Nudge.Logical.Imp.Test
{
    public class NextPrimeTests
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(-10)]
        [InlineData(-123)]
        public void NextPrimeFinder_ReturnException_WhenGivenNotNaturalNumber(int number)
        {
            Assert.Throws<ArgumentException>(() => new NextPrime().NextPrimeFinder(number));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void NextPrimeFinder_Return2_WhenGivenNaturalNumberLessThan2(int number)
        {
            Assert.Equal(2, new NextPrime().NextPrimeFinder(number));
        }

        [Theory]
        [InlineData(3)]
        [InlineData(11)]
        [InlineData(29)]
        [InlineData(41)]
        [InlineData(59)]
        [InlineData(97)]
        public void NextPrimeFinder_ReturnItself_WhenGivenPrimeNumber(int number)
        {
            Assert.Equal(number, new NextPrime().NextPrimeFinder(number));
        }

        [Theory]
        [InlineData(12, 13)]
        [InlineData(24, 29)]
        [InlineData(54, 59)]
        [InlineData(72, 73)]
        [InlineData(85, 89)]
        [InlineData(93, 97)]
        public void NextPrimeFinder_ReturnNextPrimeNumber_WhenGivenNotPrimeNumber(int number, int nextPrimeNumber)
        {
            Assert.Equal(nextPrimeNumber, new NextPrime().NextPrimeFinder(number));
        }
    }
}
