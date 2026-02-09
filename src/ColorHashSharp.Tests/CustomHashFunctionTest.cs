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
            Assert.NotNull(hsl);
            Assert.NotNull(rgb);
            Assert.NotNull(hex);
            Assert.True(hsl.H >= 0 && hsl.H < 360);
            Assert.True(hsl.S >= 0 && hsl.S <= 1);
            Assert.True(hsl.L >= 0 && hsl.L <= 1);
        }

        [Fact(DisplayName = "CustomHashFunction_ShouldProduceDifferentResults")]
        public void CustomHashFunction_ShouldProduceDifferentResults()
        {
            // Arrange - Default BKDRHash
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

        [Fact(DisplayName = "Options_WithoutCustomHash_ShouldUseDefaultBKDRHash")]
        public void Options_WithoutCustomHash_ShouldUseDefaultBKDRHash()
        {
            // Arrange
            var options = new Options(); // No custom hash
            var colorHash = new ColorHash(options);

            // Act
            var hex = colorHash.Hex("Hello World");

            // Assert - Should produce expected result from BKDRHash
            Assert.NotNull(hex);
            Assert.Equal(6, hex.Length);
        }
    }
}
