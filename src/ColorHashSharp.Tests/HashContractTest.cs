using System.Collections.Generic;
using Fernandezja.ColorHashSharp;
using Fernandezja.ColorHashSharp.Entities;
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
        public void DefaultColors_MatchPortContract(
            string input, double h, double s, double l, int r, int g, int b, string hex)
        {
            AssertColor(new ColorHash(), input, (h, s, l), (r, g, b), hex);
        }

        [Fact]
        public void LongInput_HasStableHashesAndColor()
        {
            var input = new string('a', 10000);

            Assert.Equal(668802913UL, new Sha256Hash().Generate(input));
            Assert.Equal(574486312506502UL, new JavaScriptBkdrHash().Generate(input));
            Assert.Equal(574486312470084UL, new BKDRHash().Generate(input));
            AssertColor(new ColorHash(), input, (273, 0.65, 0.5), (136, 45, 210), "882DD2");
        }

        [Fact]
        public void NarrowHueRanges_HaveStableColor()
        {
            var options = new Options
            {
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
            var colorHash = new ColorHash(new Options { HashFunction = new BKDRHash() });

            AssertColor(colorHash, input, (h, s, l), (r, g, b), hex);
        }

        private static void AssertColor(ColorHash colorHash, string input,
            (double H, double S, double L) expectedHsl, (int R, int G, int B) expectedRgb, string expectedHex)
        {
            var hsl = colorHash.Hsl(input);
            var rgb = colorHash.Rgb(input);

            Assert.Equal(expectedHsl, (hsl.H, hsl.S, hsl.L));
            Assert.Equal(expectedRgb, ((int)rgb.R, (int)rgb.G, (int)rgb.B));
            Assert.Equal(expectedHex, colorHash.Hex(input));
        }
    }
}
