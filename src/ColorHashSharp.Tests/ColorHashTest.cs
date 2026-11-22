using System.Collections.Generic;
using System.Linq;
using Fernandezja.ColorHashSharp;
using Fernandezja.ColorHashSharp.Entities;
using Xunit;

namespace ColorHashSharp.Tests
{
    // Behavioral cases from color-hash 2.0.2's color-hash.test.ts.
    public class ColorHashTest
    {
        private static readonly string[] Inputs = Enumerable.Range(0, 100)
            .Select(index => $"00000000-0000-4000-8000-{index:D12}")
            .ToArray();

        [Fact]
        public void Hsl_DefaultHueIsBelow359()
        {
            var colorHash = new ColorHash();

            foreach (var input in Inputs)
            {
                Assert.InRange(colorHash.Hsl(input).H, 0, 358);
            }
        }

        [Fact]
        public void Hsl_UsesFixedHue()
        {
            var colorHash = new ColorHash(new Options { HueRanges = new List<Hue> { new Hue(10, 10) } });

            foreach (var input in Inputs)
            {
                Assert.Equal(10, colorHash.Hsl(input).H);
            }
        }

        [Fact]
        public void Hsl_UsesSingleHueRange()
        {
            for (int min = 0; min < 360; min += 60)
            {
                for (int max = min + 1; max <= 360; max += 60)
                {
                    var colorHash = new ColorHash(new Options { HueRanges = new List<Hue> { new Hue(min, max) } });

                    foreach (var input in Inputs)
                    {
                        double hue = colorHash.Hsl(input).H;
                        Assert.True(hue >= min && hue < max, $"Hue {hue} is outside [{min}, {max}).");
                    }
                }
            }
        }

        [Fact]
        public void Hsl_UsesOneOfTheHueRanges()
        {
            var ranges = new List<Hue> { new Hue(30, 90), new Hue(180, 210), new Hue(270, 285) };
            var colorHash = new ColorHash(new Options { HueRanges = ranges });

            foreach (var input in Inputs)
            {
                double hue = colorHash.Hsl(input).H;
                Assert.Contains(ranges, range => hue >= range.Min && hue < range.Max);
            }
        }

        [Fact]
        public void Hsl_UsesFixedSaturationAndLightness()
        {
            var colorHash = new ColorHash(new Options
            {
                S = new List<double> { 0.5 },
                L = new List<double> { 0.5 }
            });

            var hsl = colorHash.Hsl(Inputs[0]);

            Assert.Equal((0.5, 0.5), (hsl.S, hsl.L));
        }

        [Fact]
        public void Hsl_UsesConfiguredSaturationAndLightnessPalettes()
        {
            var colorHash = new ColorHash(new Options
            {
                S = new List<double> { 0.9, 1 },
                L = new List<double> { 0.9, 1 }
            });

            var hsl = colorHash.Hsl(Inputs[0]);

            Assert.Contains(hsl.S, new[] { 0.9, 1 });
            Assert.Contains(hsl.L, new[] { 0.9, 1 });
        }
    }
}
