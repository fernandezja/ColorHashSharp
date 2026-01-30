using Fernandezja.ColorHashSharp;
using Xunit;

namespace ColorHashSharp.Tests
{
    public class UtilitiesTest
    {
        [Fact(DisplayName = "StringToNumber_SimpleString_ShouldConvert")]
        public void StringToNumber_SimpleString_ShouldConvert()
        {
            var utilities = new Utilities();
            var result = utilities.StringToNumber("abc");

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            // 'a' = 97, 'b' = 98, 'c' = 99
            Assert.Equal("979899", result);
        }

        [Fact(DisplayName = "StringToNumber_SingleChar_ShouldConvert")]
        public void StringToNumber_SingleChar_ShouldConvert()
        {
            var utilities = new Utilities();
            var result = utilities.StringToNumber("A");

            Assert.NotNull(result);
            // 'A' = 65
            Assert.Equal("65", result);
        }

        [Fact(DisplayName = "StringToNumber_EmptyString_ShouldReturnEmpty")]
        public void StringToNumber_EmptyString_ShouldReturnEmpty()
        {
            var utilities = new Utilities();
            var result = utilities.StringToNumber("");

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact(DisplayName = "StringToNumber_SpecialChars_ShouldConvert")]
        public void StringToNumber_SpecialChars_ShouldConvert()
        {
            var utilities = new Utilities();
            var result = utilities.StringToNumber(" !@");

            Assert.NotNull(result);
            // ' ' = 32, '!' = 33, '@' = 64
            Assert.Equal("323364", result);
        }

        [Fact(DisplayName = "StringToNumber_Numbers_ShouldConvert")]
        public void StringToNumber_Numbers_ShouldConvert()
        {
            var utilities = new Utilities();
            var result = utilities.StringToNumber("123");

            Assert.NotNull(result);
            // '1' = 49, '2' = 50, '3' = 51
            Assert.Equal("495051", result);
        }
    }
}
