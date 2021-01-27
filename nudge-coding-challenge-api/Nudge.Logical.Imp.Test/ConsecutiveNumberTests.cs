using System;
using Xunit;

namespace Nudge.Logical.Imp.Test
{
    public class ConsecutiveNumberTests
    {
        [Theory]
        [InlineData("a123456789")]
        [InlineData("a12345asd6789")]
        [InlineData("a123456789ert")]
        public void IsConsecutive_ReturnException_WhenGivenStringContainsCharacters(string series)
        {
            Assert.Throws<ArgumentException>(() => new ConsecutiveNumber().IsConsecutive(series));
        }

        [Theory]
        [InlineData("123456789")]
        [InlineData("121314151617")]
        [InlineData("123124125")]
        [InlineData("245672456824569")]
        [InlineData("235678235679")]
        public void IsConsecutive_ReturnTrue_WhenGivenStringHasSetOfConsecutiveNumbers(string series)
        {
            Assert.True(new ConsecutiveNumber().IsConsecutive(series));
        }

        [Theory]
        [InlineData("121418")]
        [InlineData("121314191617")]
        [InlineData("123129125")]
        [InlineData("2456245724592459")]
        [InlineData("235678235601235680235681")]
        public void IsConsecutive_ReturnFalse_WhenGivenStringHasNoSetOfConsecutiveNumbers(string series)
        {
            Assert.False(new ConsecutiveNumber().IsConsecutive(series));
        }
    }
}
