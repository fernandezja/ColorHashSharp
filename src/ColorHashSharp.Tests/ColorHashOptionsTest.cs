using System;
using System.Collections.Generic;
using Fernandezja.ColorHashSharp;
using Fernandezja.ColorHashSharp.Entities;
using Fernandezja.ColorHashSharp.Interfaces;
using Xunit;

namespace ColorHashSharp.Tests
{
    public class ColorHashOptionsTest
    {
        [Fact]
        public void Constructor_RejectsNullOptions()
        {
            Assert.Throws<ArgumentNullException>("options", () => new ColorHash(null));
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Constructor_RejectsMissingOrEmptyPalettes(bool saturation)
        {
            AssertInvalidPalette(saturation, null);
            AssertInvalidPalette(saturation, new List<double>());
        }

        [Theory]
        [InlineData(-0.01)]
        [InlineData(1.01)]
        [InlineData(double.NaN)]
        [InlineData(double.NegativeInfinity)]
        [InlineData(double.PositiveInfinity)]
        public void Constructor_RejectsInvalidPaletteEntries(double value)
        {
            var palette = new List<double> { 0.5, value };

            AssertInvalidPalette(true, palette);
            AssertInvalidPalette(false, palette);
        }

        [Fact]
        public void Constructor_RejectsNullHueListAndEntries()
        {
            var missing = Assert.Throws<ArgumentException>("options",
                () => new ColorHash(new Options { HueRanges = null }));
            var withNull = Assert.Throws<ArgumentException>("options",
                () => new ColorHash(new Options { HueRanges = new List<Hue> { new Hue(), null } }));

            Assert.Contains("HueRanges", missing.Message);
            Assert.Contains("HueRanges[1]", withNull.Message);
        }

        [Theory]
        [InlineData(-1, 90)]
        [InlineData(180, 90)]
        [InlineData(0, 361)]
        public void Constructor_RejectsInvalidHueBounds(int min, int max)
        {
            var options = new Options { HueRanges = new List<Hue> { new Hue(), new Hue(min, max) } };

            var error = Assert.Throws<ArgumentException>("options", () => new ColorHash(options));

            Assert.Contains("HueRanges[1]", error.Message);
        }

        [Theory]
        [InlineData(0, 0, 0, "#000000")]
        [InlineData(90, 1, 1, "#ffffff")]
        [InlineData(360, 1, 0.5, "#ff0000")]
        public void Constructor_AcceptsPaletteEndpointsAndFixedHues(
            int hue, double saturation, double lightness, string expectedHex)
        {
            var options = new Options
            {
                S = new List<double> { saturation },
                L = new List<double> { lightness },
                HueRanges = new List<Hue> { new Hue(hue, hue) }
            };
            var generator = new ColorHash(options);

            var hsl = generator.Hsl("test");

            Assert.Equal(((double)hue, saturation, lightness), (hsl.H, hsl.S, hsl.L));
            Assert.Equal(expectedHex, generator.Hex("test"));
        }

        [Fact]
        public void Constructor_CopiesPalettesAndHueBounds()
        {
            var range = new Hue(90, 90);
            var options = new Options
            {
                S = new List<double> { 0.35 },
                L = new List<double> { 0.65 },
                HueRanges = new List<Hue> { range }
            };
            var original = new ColorHash(options);

            options.S[0] = 0.5;
            options.L[0] = 0.35;
            range.Min = 180;
            range.Max = 180;

            var before = original.Hsl("test");
            var updated = new ColorHash(options).Hsl("test");

            Assert.Equal((90.0, 0.35, 0.65), (before.H, before.S, before.L));
            Assert.Equal((180.0, 0.5, 0.35), (updated.H, updated.S, updated.L));

            options.S.Clear();
            options.L.Clear();
            options.HueRanges.Clear();

            var after = original.Hsl("test");
            Assert.Equal((before.H, before.S, before.L), (after.H, after.S, after.L));
        }

        [Fact]
        public void NullInput_IsPassedToTheSelectedHashAsAnEmptyString()
        {
            var defaultGenerator = new ColorHash();
            var customHash = new RecordingHash();
            var customGenerator = new ColorHash(new Options { HashFunction = customHash });

            Assert.Equal(defaultGenerator.Hex(string.Empty), defaultGenerator.Hex(null));

            customGenerator.Hex(null);
            Assert.Equal(string.Empty, customHash.LastInput);

            customGenerator.Hex("test");
            Assert.Equal("test", customHash.LastInput);
        }

        private static void AssertInvalidPalette(bool saturation, List<double> palette)
        {
            var options = new Options();
            if (saturation)
            {
                options.S = palette;
            }
            else
            {
                options.L = palette;
            }

            var error = Assert.Throws<ArgumentException>("options", () => new ColorHash(options));
            Assert.StartsWith(saturation ? nameof(Options.S) : nameof(Options.L), error.Message);
        }

        private sealed class RecordingHash : IHashFunction
        {
            public string LastInput { get; private set; }

            public ulong Generate(string value)
            {
                LastInput = value;
                return 0;
            }
        }
    }
}
