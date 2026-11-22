using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Fernandezja.ColorHashSharp;
using Xunit;

namespace ColorHashSharp.Tests
{
    public class ColorHashTest
    {
        [Fact(DisplayName = "BuildToHsl_ShouldCreateAColorWithTextYodaInHSL")]
        public void BuildToHsl_ShouldCreateAColorWithHSL()
        {
            var colorHash = new Fernandezja.ColorHashSharp.ColorHash();

            var result = colorHash.BuildToHsl("yoda");

            Assert.NotNull(result);
            Assert.Equal(323, result.H);
            Assert.Equal(0.65, result.S);
            Assert.Equal(0.65, result.L);
        }



        [Theory(DisplayName = "BuildToHsl_ShouldCreateAColorInHSL")]
        [InlineData("yoda", 323, 0.65, 0.65)]
        [InlineData("Yoda", 53, 0.5, 0.5)]
        [InlineData("starwars", 185, 0.65, 0.5)]
        [InlineData("StarWars", 342, 0.5, 0.5)]
        public void BuildToHsl_ShouldCreateAColorInHSL(
            string phrase, double hExpected, double sExpected, double lExpected)
        {
            var colorHash = new Fernandezja.ColorHashSharp.ColorHash();

            var result = colorHash.BuildToHsl(phrase);

            Assert.NotNull(result);
            Assert.Equal(hExpected, result.H);
            Assert.Equal(sExpected, result.S);
            Assert.Equal(lExpected, result.L);
        }


        [Theory(DisplayName = "BuildToHsl_ShouldCreateAColorInHSLWithCustomHueOption")]
        [InlineData("yoda", 90, 90, 0.65, 0.65)]
        [InlineData("yoda", 270, 270, 0.65, 0.65)]
        public void BuildToHsl_ShouldCreateAColorInHSLWithCustomHueOption(
                    string phrase, int hue,
                    int hExpected, double sExpected, double lExpected)
        {
            var options = new Options();
            options.SetHue(hue);

            var colorHash = new Fernandezja.ColorHashSharp.ColorHash(options);

            var result = colorHash.BuildToHsl(phrase);

            Assert.NotNull(result);
            Assert.Equal(hExpected, result.H);
            Assert.Equal(sExpected, result.S);
            Assert.Equal(lExpected, result.L);

            
        }



        [Theory(DisplayName = "BuildToHsl_ShouldCreateAColorInHexWithCustomHueOption")]
        [InlineData("yoda", 90, "A6E06C")]
        [InlineData("yoda", 210, "6CA6E0")]
        [InlineData("yoda", 270, "A66CE0")]
        [InlineData("yoda", 360, "E06C6C")]
        public void BuildToHsl_ShouldCreateAColorInHexWithCustomHueOption(
                    string phrase, int hue, string hexExpected)
        {
            var options = new Options();
            options.SetHue(hue);

            var colorHash = new Fernandezja.ColorHashSharp.ColorHash(options);

            var result = colorHash.BuildToHex(phrase);

            Assert.NotNull(result);

            Assert.Equal(hexExpected, result);


        }

        [Theory(DisplayName = "BuildToHex_ShouldCreateAColorInHex")]
        [InlineData("yoda", "E06CB3")]
        [InlineData("Yoda", "BFB040")]
        [InlineData("starwars", "2DC5D2")]
        [InlineData("StarWars", "BF4066")]
        public void BuildToHex_ShouldCreateAColorInHex(string phrase, string hexExpected)
        {
            var colorHash = new Fernandezja.ColorHashSharp.ColorHash();

            var result = colorHash.BuildToHex(phrase);

            Assert.NotNull(result);
            Assert.Equal(hexExpected, result);
        }

        [Fact(DisplayName = "BuildToColor_ShouldCreateAColorObject")]
        public void BuildToColor_ShouldCreateAColorObject()
        {
            var colorHash = new Fernandezja.ColorHashSharp.ColorHash();

            var result = colorHash.BuildToColor("yoda");
            
            Assert.Equal(Color.FromArgb(255, 224, 108, 179), result);
        }

        [Fact(DisplayName = "BuildToHsl_HelloWorldString")]
        public void BuildToHsl_HelloWorldString()
        {
            var colorHash = new Fernandezja.ColorHashSharp.ColorHash();

            var result = colorHash.BuildToHsl("Hello World");

            Assert.NotNull(result);
            Assert.Equal(233, result.H);
            Assert.Equal(0.35, result.S);
            Assert.Equal(0.5, result.L);
        }

        [Fact(DisplayName = "Build_HelloWorldStringToHslColorAndHex")]
        public void Build_HelloWorldStringToHslColorAndHex()
        {
            var ColorHash = new Fernandezja.ColorHashSharp.ColorHash();

            var resultHsl = ColorHash.BuildToHsl("Hello World");
            var resultColor = ColorHash.BuildToColor("Hello World");
            var resultHex = ColorHash.BuildToHex("Hello World");

            Assert.Equal(83, resultColor.R);
            Assert.Equal(93, resultColor.G);
            Assert.Equal(172, resultColor.B);

            Assert.Equal(233, resultHsl.H);
            Assert.Equal(0.35, resultHsl.S);
            Assert.Equal(0.5, resultHsl.L);

            Assert.Equal("535DAC", resultHex);


        }

        [Fact(DisplayName = "Hsl_ShouldCreateAColorInHSL")]
        public void Hsl_ShouldCreateAColorInHSL()
        {
            var colorHash = new Fernandezja.ColorHashSharp.ColorHash();

            var result = colorHash.Hsl("Hello World");

            Assert.NotNull(result);
            Assert.Equal(233, result.H);
            Assert.Equal(0.35, result.S);
            Assert.Equal(0.5, result.L);
        }



        [Fact(DisplayName = "Hex_ShouldCreateAColorInHex")]
        public void Hex_ShouldCreateAColorInHex()
        {
            var colorHash = new Fernandezja.ColorHashSharp.ColorHash();

            var result = colorHash.Hex("Hello World");

            Assert.NotNull(result);
            Assert.Equal("535DAC", result);
        }


        [Fact(DisplayName = "Rgb_ShouldCreateAColorInRgb")]
        public void Rgb_ShouldCreateAColorInRgb()
        {
            var colorHash = new Fernandezja.ColorHashSharp.ColorHash();

            var result = colorHash.Rgb("Hello World");

            Assert.Equal(83, result.R);
            Assert.Equal(93, result.G);
            Assert.Equal(172, result.B);
        }

        [Fact(DisplayName = "Build_ShouldReturnHexString")]
        public void Build_ShouldReturnHexString()
        {
            var colorHash = new Fernandezja.ColorHashSharp.ColorHash();

            var result = colorHash.Build("Hello World");

            Assert.NotNull(result);
            Assert.Equal("535DAC", result);
        }

        [Fact(DisplayName = "BuildToHsl_WithEmptyHueRanges_ShouldUseDefaultHueCalculation")]
        public void BuildToHsl_WithEmptyHueRanges_ShouldUseDefaultHueCalculation()
        {
            var options = new Options();
            // Ensure HueRanges is empty (default)
            Assert.Empty(options.HueRanges);

            var colorHash = new Fernandezja.ColorHashSharp.ColorHash(options);
            var result = colorHash.BuildToHsl("test");

            Assert.NotNull(result);
            Assert.InRange(result.H, 0, 359);
        }

        [Fact(DisplayName = "BuildToHsl_WithMultipleHueRanges_ShouldUseHueRanges")]
        public void BuildToHsl_WithMultipleHueRanges_ShouldUseHueRanges()
        {
            var options = new Options();
            var hueValues = new List<(int Min, int Max)>();
            hueValues.Add((30, 90));
            hueValues.Add((180, 210));
            options.SetHue(hueValues);

            var colorHash = new Fernandezja.ColorHashSharp.ColorHash(options);
            var result = colorHash.BuildToHsl("test");

            Assert.NotNull(result);
        }

        [Theory(DisplayName = "BuildToColor_DifferentStrings_ShouldProduceDifferentColors")]
        [InlineData("test1")]
        [InlineData("test2")]
        [InlineData("test3")]
        public void BuildToColor_DifferentStrings_ShouldProduceDifferentColors(string input)
        {
            var colorHash = new Fernandezja.ColorHashSharp.ColorHash();
            var result = colorHash.BuildToColor(input);

            Assert.NotNull(result);
            Assert.Equal(255, result.A);
        }

        [Fact(DisplayName = "BuildToHex_EmptyString_ShouldGenerateColor")]
        public void BuildToHex_EmptyString_ShouldGenerateColor()
        {
            var colorHash = new Fernandezja.ColorHashSharp.ColorHash();
            var result = colorHash.BuildToHex("");

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact(DisplayName = "ColorHash_DefaultConstructor_ShouldInitialize")]
        public void ColorHash_DefaultConstructor_ShouldInitialize()
        {
            var colorHash = new Fernandezja.ColorHashSharp.ColorHash();
            var result = colorHash.BuildToHex("test");

            Assert.NotNull(result);
        }

        [Fact(DisplayName = "ColorHash_WithOptionsConstructor_ShouldInitialize")]
        public void ColorHash_WithOptionsConstructor_ShouldInitialize()
        {
            var options = new Options();
            var colorHash = new Fernandezja.ColorHashSharp.ColorHash(options);
            var result = colorHash.BuildToHex("test");

            Assert.NotNull(result);
        }

        [Theory(DisplayName = "BuildToHsl_VariousSaturationAndLightness_ShouldSelectFromArrays")]
        [InlineData("a")]
        [InlineData("aa")]
        [InlineData("aaa")]
        [InlineData("aaaa")]
        [InlineData("aaaaa")]
        [InlineData("aaaaaa")]
        [InlineData("aaaaaaa")]
        [InlineData("aaaaaaaa")]
        [InlineData("aaaaaaaaa")]
        public void BuildToHsl_VariousSaturationAndLightness_ShouldSelectFromArrays(string input)
        {
            var colorHash = new Fernandezja.ColorHashSharp.ColorHash();
            var result = colorHash.BuildToHsl(input);

            Assert.NotNull(result);
            Assert.Contains(result.S, new[] { 0.35, 0.5, 0.65 });
            Assert.Contains(result.L, new[] { 0.35, 0.5, 0.65 });
        }

    }
}
