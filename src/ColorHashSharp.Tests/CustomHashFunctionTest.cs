using Fernandezja.ColorHashSharp;
using Fernandezja.ColorHashSharp.Interfaces;
using System;
using Xunit;

namespace ColorHashSharp.Tests
{
    /// <summary>
    /// Custom simple hash function for testing
    /// </summary>
    public class SimpleCharCodeHash : IHashFunction
    {
        public ulong Generate(string value)
        {
            if (string.IsNullOrEmpty(value))
                return 0;

            ulong hash = 0;
            foreach (char c in value)
            {
                hash += (ulong)c;
            }
            return hash;
        }
    }

    public class CustomHashFunctionTest
    {
        [Fact(DisplayName = "CustomHashFunction_ShouldWork")]
        public void CustomHashFunction_ShouldWork()
        {
            // Arrange
            var customHash = new SimpleCharCodeHash();
            var options = new Options
            {
                HashFunction = customHash
            };
            var colorHash = new ColorHash(options);

            // Act
            var hsl = colorHash.Hsl("Hello World");
            var rgb = colorHash.Rgb("Hello World");
            var hex = colorHash.Hex("Hello World");

            // Assert
            Assert.Equal((334d, 0.65, 0.35), (hsl.H, hsl.S, hsl.L));
            Assert.Equal((147, 31, 82), ((int)rgb.R, (int)rgb.G, (int)rgb.B));
            Assert.Equal("931F52", hex);
        }

        [Fact(DisplayName = "CustomHashFunction_ShouldProduceDifferentResults")]
        public void CustomHashFunction_ShouldProduceDifferentResults()
        {
            // Arrange - Default SHA-256
            var defaultColorHash = new ColorHash();
            var defaultHex = defaultColorHash.Hex("Test");

            // Arrange - Custom simple hash
            var customHash = new SimpleCharCodeHash();
            var options = new Options
            {
                HashFunction = customHash
            };
            var customColorHash = new ColorHash(options);
            var customHex = customColorHash.Hex("Test");

            // Assert - Different hash functions should produce different results
            Assert.NotEqual(defaultHex, customHex);
        }

        [Fact(DisplayName = "CustomHashFunction_ShouldBeConsistent")]
        public void CustomHashFunction_ShouldBeConsistent()
        {
            // Arrange
            var customHash = new SimpleCharCodeHash();
            var options = new Options
            {
                HashFunction = customHash
            };
            var colorHash = new ColorHash(options);

            // Act
            var hex1 = colorHash.Hex("Consistent");
            var hex2 = colorHash.Hex("Consistent");

            // Assert - Same input should always produce same output
            Assert.Equal(hex1, hex2);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void Constructors_WithoutCustomHash_ShouldUseSha256(bool passOptions)
        {
            var colorHash = passOptions ? new ColorHash(new Options()) : new ColorHash();

            Assert.Equal("535DAC", colorHash.Hex("Hello World"));
        }

        [Fact]
        public void Options_CanSelectJavaScriptBkdr()
        {
            var colorHash = new ColorHash(new Options { HashFunction = new JavaScriptBkdrHash() });

            Assert.Equal("40BF51", colorHash.Hex("é"));
        }
    }
}
