using Fernandezja.ColorHashSharp;
using Fernandezja.ColorHashSharp.Interfaces;
using Xunit;

namespace ColorHashSharp.Tests
{
    public class CustomHashFunctionTest
    {
        [Theory]
        [InlineData("abc", 294, 0.5, 0.5, 178, 64, 191, "#b240bf")]
        [InlineData("Hello World", 334, 0.35, 0.5, 172, 83, 122, "#ac537a")]
        public void CustomCharCodeHash_MatchesUpstream(
            string input, double h, double s, double l, int red, int green, int blue, string hex)
        {
            var colorHash = new ColorHash(new Options { HashFunction = new CharCodeHash() });

            var hsl = colorHash.Hsl(input);
            var rgb = colorHash.Rgb(input);

            Assert.Equal((h, s, l), (hsl.H, hsl.S, hsl.L));
            Assert.Equal((red, green, blue), ((int)rgb.R, (int)rgb.G, (int)rgb.B));
            Assert.Equal(hex, colorHash.Hex(input));
        }

        private sealed class CharCodeHash : IHashFunction
        {
            public ulong Generate(string value)
            {
                ulong hash = 0;
                foreach (char codeUnit in value)
                {
                    hash += codeUnit;
                }

                return hash;
            }
        }
    }
}
