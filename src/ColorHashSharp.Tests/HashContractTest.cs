using System.Collections.Generic;
using System.Linq;
using Fernandezja.ColorHashSharp;
using Fernandezja.ColorHashSharp.Entities;
using Fernandezja.ColorHashSharp.Interfaces;
using Xunit;

namespace ColorHashSharp.Tests
{
    public class HashContractTest
    {
        // Raw hash values from color-hash 2.0.2.
        [Theory]
        [InlineData("Hello World", 2777786068UL, 294020464607729UL)]
        [InlineData("é", 1251562878UL, 30643UL)]
        [InlineData("😀", 4031003188UL, 957426589UL)]
        [InlineData("", 3820012610UL, 120UL)]
        [InlineData("\0", 1848904604UL, 120UL)]
        public void HashFunctions_MatchJavaScript(string input, ulong sha256, ulong bkdr)
        {
            Assert.Equal(sha256, new Sha256Hash().Generate(input));
            Assert.Equal(bkdr, new JavaScriptBkdrHash().Generate(input));
        }

        [Theory]
        [InlineData("2018-06-14T17", 2080245275UL, "2018-06-15T09", 1833461466UL)]
        [InlineData("myView1", 170744141UL, "myView2", 551785129UL)]
        public void Sha256_MatchesUpstreamCollisionRegressionCases(
            string first, ulong firstHash, string second, ulong secondHash)
        {
            var hash = new Sha256Hash();

            Assert.Equal(firstHash, hash.Generate(first));
            Assert.Equal(secondHash, hash.Generate(second));
        }

        [Fact]
        public void JavaScriptBkdr_MatchesUpstreamShortAndLongInputs()
        {
            var hash = new JavaScriptBkdrHash();
            string longInput = string.Concat(Enumerable.Repeat("Hello World.", 10000));

            Assert.Equal(219759694UL, hash.Generate("abc"));
            Assert.Equal(235617375UL, hash.Generate("hij"));
            Assert.Equal(117111301690285UL, hash.Generate(longInput));
            Assert.Equal(122475178103641UL, hash.Generate(longInput.Substring(0, longInput.Length - 1)));
        }

        [Fact]
        public void Sha256_MatchesJavaScriptForUnpairedSurrogates()
        {
            var hash = new Sha256Hash();

            Assert.Equal(824408855UL, hash.Generate("\uD800"));
            Assert.Equal(3879378172UL, hash.Generate("\uDC00A"));
        }

        [Fact]
        public void Sha256_PreservesNullAsEmpty()
        {
            Assert.Equal(3820012610UL, new Sha256Hash().Generate(null));
        }

        [Theory]
        [InlineData("Hello World", 233, 0.35, 0.5, 83, 93, 172, "535DAC")]
        [InlineData("é", 205, 0.5, 0.65, 121, 173, 210, "79ADD2")]
        [InlineData("😀", 49, 0.5, 0.65, 210, 194, 121, "D2C279")]
        [InlineData("", 233, 0.65, 0.35, 31, 45, 147, "1F2D93")]
        public void CompatibilityDisabled_PreservesPortColorsWithSha256(
            string input, double h, double s, double l, int r, int g, int b, string hex)
        {
            var port = new ColorHash(new Options { UseJavaScriptCompatibility = false });
            AssertColor(port, input, (h, s, l), (r, g, b), hex);
        }

        [Fact]
        public void LongInput_HasStableHashesAndHistoricalColor()
        {
            var input = new string('a', 10000);

            Assert.Equal(668802913UL, new Sha256Hash().Generate(input));
            Assert.Equal(574486312506502UL, new JavaScriptBkdrHash().Generate(input));
            Assert.Equal(574486312470084UL, new BKDRHash().Generate(input));
            var port = new ColorHash(new Options { UseJavaScriptCompatibility = false });
            AssertColor(port, input, (273, 0.65, 0.5), (136, 45, 210), "882DD2");
        }

        [Fact]
        public void CompatibilityDisabled_PreservesIntegerHueInterpolation()
        {
            var options = new Options
            {
                UseJavaScriptCompatibility = false,
                HueRanges = new List<Hue> { new Hue(90, 91), new Hue(210, 211) }
            };

            AssertColor(new ColorHash(options), "Hello World", (90, 0.35, 0.5), (128, 172, 83), "80AC53");
        }

        [Theory]
        [InlineData("Hello World", 225, 0.35, 0.65, 135, 150, 197, "8796C5")]
        [InlineData("é", 157, 0.35, 0.65, 135, 197, 173, "87C5AD")]
        [InlineData("😀", 224, 0.65, 0.35, 31, 62, 147, "1F3E93")]
        [InlineData("", 120, 0.35, 0.35, 58, 120, 58, "3A783A")]
        public void HistoricalBkdr_PreservesPortColors(
            string input, double h, double s, double l, int r, int g, int b, string hex)
        {
            var colorHash = new ColorHash(new Options
            {
                UseJavaScriptCompatibility = false,
                HashFunction = new BKDRHash()
            });

            AssertColor(colorHash, input, (h, s, l), (r, g, b), hex);
        }

        // Color vectors from the published color-hash 2.0.2 npm package.
        [Theory]
        [InlineData("Hello World", 233, 0.5, 0.65, 121, 132, 210, "#7984d2")]
        [InlineData("é", 205, 0.65, 0.35, 31, 99, 147, "#1f6393")]
        [InlineData("😀", 49, 0.65, 0.35, 147, 126, 31, "#937e1f")]
        [InlineData("", 233, 0.35, 0.5, 83, 93, 172, "#535dac")]
        public void DefaultColors_MatchUpstream(
            string input, double h, double s, double l, int r, int g, int b, string hex)
        {
            var colorHash = new ColorHash();

            AssertColor(colorHash, input, (h, s, l), (r, g, b), hex);
            var hsl = colorHash.BuildToHsl(input);
            Assert.Equal((h, s, l), (hsl.H, hsl.S, hsl.L));
            Assert.Equal(colorHash.Rgb(input), colorHash.BuildToColor(input));
            Assert.Equal(hex, colorHash.BuildToHex(input));
            Assert.Equal(hex, colorHash.Build(input));
        }

        [Fact]
        public void DefaultOptions_MatchUpstream()
        {
            var options = new Options();

            Assert.True(options.UseJavaScriptCompatibility);
            Assert.Equal("#7984d2", new ColorHash(options).Hex("Hello World"));
        }

        [Fact]
        public void JavaScriptMode_MatchesUpstreamForLongInputs()
        {
            var input = new string('a', 10000);
            var sha256 = new ColorHash();
            var bkdr = new ColorHash(new Options { HashFunction = new JavaScriptBkdrHash() });

            AssertColor(sha256, input, (273, 0.35, 0.65), (169, 135, 197), "#a987c5");
            AssertColor(bkdr, input, (71, 0.65, 0.35), (126, 147, 31), "#7e931f");
        }

        [Fact]
        public void JavaScriptMode_MatchesUpstreamBkdrForUnicode()
        {
            var colorHash = new ColorHash(new Options
            {
                HashFunction = new JavaScriptBkdrHash()
            });

            AssertColor(colorHash, "é", (128, 0.65, 0.65), (108, 224, 123), "#6ce07b");
            AssertColor(colorHash, "😀", (155, 0.5, 0.5), (64, 191, 138), "#40bf8a");
        }

        [Fact]
        public void JavaScriptMode_InterpolatesHueWithFractionalDivision()
        {
            var colorHash = new ColorHash(new Options
            {
                HashFunction = new FixedHash(3),
                HueRanges = new List<Hue> { new Hue(0, 1), new Hue(10, 11) }
            });

            AssertColor(colorHash, "custom", (10.002063273727648, 0.5, 0.5), (191, 85, 64), "#bf5540");
        }

        [Theory]
        [InlineData(0UL, 0, 0.1, 0.2, 56, 46, 46, "#382e2e")]
        [InlineData(1UL, 1, 0.4, 0.5, 179, 78, 77, "#b34e4d")]
        [InlineData(360UL, 1, 0.4, 0.5, 179, 78, 77, "#b34e4d")]
        [InlineData(361UL, 2, 0.9, 0.5, 242, 20, 13, "#f2140d")]
        [InlineData(1080UL, 3, 0.1, 0.5, 140, 116, 115, "#8c7473")]
        [InlineData(1081UL, 4, 0.4, 0.7, 209, 152, 148, "#d19894")]
        [InlineData(ulong.MaxValue, 303, 0.1, 0.2, 56, 46, 56, "#382e38")]
        public void JavaScriptMode_RoundsUpBothPaletteDivisions(
            ulong hash, double h, double s, double l, int r, int g, int b, string hex)
        {
            var colorHash = new ColorHash(new Options
            {
                HashFunction = new FixedHash(hash),
                S = new List<double> { 0.1, 0.4, 0.9 },
                L = new List<double> { 0.2, 0.5, 0.7, 0.8 }
            });

            AssertColor(colorHash, "custom", (h, s, l), (r, g, b), hex);
        }

        [Theory]
        [InlineData(76.5 / 255, 77, "#4d4d4d", "4C4C4C")]
        [InlineData(1.0 / 255, 1, "#010101", "010101")]
        public void JavaScriptMode_UsesJavaScriptRoundingAndCssHex(
            double lightness, int channel, string cssHex, string portHex)
        {
            var options = new Options { S = new List<double> { 0 }, L = new List<double> { lightness } };
            var javaScript = new ColorHash(options);
            options.UseJavaScriptCompatibility = false;
            var port = new ColorHash(options);

            AssertColor(javaScript, "", (233, 0, lightness), (channel, channel, channel), cssHex);
            Assert.Equal(portHex, port.Hex(""));
        }

        private static void AssertColor(ColorHash colorHash, string input,
            (double H, double S, double L) expectedHsl, (int R, int G, int B) expectedRgb, string expectedHex)
        {
            var hsl = colorHash.Hsl(input);
            var rgb = colorHash.Rgb(input);

            Assert.Equal(expectedHsl, (hsl.H, hsl.S, hsl.L));
            Assert.Equal(255, rgb.A);
            Assert.Equal(expectedRgb, ((int)rgb.R, (int)rgb.G, (int)rgb.B));
            Assert.Equal(expectedHex, colorHash.Hex(input));
        }

        private sealed class FixedHash : IHashFunction
        {
            private readonly ulong _hash;

            public FixedHash(ulong hash)
            {
                _hash = hash;
            }

            public ulong Generate(string value) => _hash;
        }
    }
}
