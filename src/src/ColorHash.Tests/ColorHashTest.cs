using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Fernandezja.ColorHash;
using Xunit;

namespace ColorHash.Tests
{
    public class ColorHashTest
    {
        [Fact(DisplayName = "BuildToHsl_ShouldCreateAColorWithTextYodaInHSL")]
        public void BuildToHsl_ShouldCreateAColorWithHSL()
        {
            var colorHash = new Fernandezja.ColorHash.ColorHash();

            var result = colorHash.BuildToHsl("yoda");

            Assert.NotNull(result);
            Assert.Equal(76, result.H);
            Assert.Equal(0.35, result.S);
            Assert.Equal(0.35, result.L);
        }



        [Theory(DisplayName = "BuildToHsl_ShouldCreateAColorInHSL")]
        [InlineData("yoda", 76, 0.35, 0.35)]
        [InlineData("Yoda", 314, 0.5, 0.65)]
        [InlineData("starwars", 339, 0.65, 0.5)]
        [InlineData("StarWars", 135, 0.35, 0.35)]
        public void BuildToHsl_ShouldCreateAColorInHSL(
            string phrase, double hExpected, double sExpected, double lExpected)
        {
            var colorHash = new Fernandezja.ColorHash.ColorHash();

            var result = colorHash.BuildToHsl(phrase);

            Assert.NotNull(result);
            Assert.Equal(hExpected, result.H);
            Assert.Equal(sExpected, result.S);
            Assert.Equal(lExpected, result.L);
        }

        [Theory(DisplayName = "BuildToHex_ShouldCreateAColorInHex")]
        [InlineData("yoda", "51593A")]
        [InlineData("Yoda", "A65392")]
        [InlineData("starwars", "802D4A")]
        [InlineData("StarWars", "3A5942")]
        public void BuildToHex_ShouldCreateAColorInHex(string phrase, string hexExpected)
        {
            var colorHash = new Fernandezja.ColorHash.ColorHash();

            var result = colorHash.BuildToHex(phrase);

            Assert.NotNull(result);
            Assert.Equal(hexExpected, result);
        }

        [Fact(DisplayName = "BuildToColor_ShouldCreateAColorObject")]
        public void BuildToColor_ShouldCreateAColorObject()
        {
            var colorHash = new Fernandezja.ColorHash.ColorHash();

            var result = colorHash.BuildToColor("yoda");
            
            Assert.Equal(Color.FromArgb(alpha:255,red:81,green:89, blue:58), 
                        result);
        }

        [Fact(DisplayName = "BuildToHsl_HelloWorldString")]
        public void BuildToHsl_HelloWorldString()
        {
            var colorHash = new Fernandezja.ColorHash.ColorHash();

            var result = colorHash.BuildToHsl("Hello World");

            Assert.NotNull(result);
            Assert.Equal(225, result.H);
            Assert.Equal(0.65, result.S);
            Assert.Equal(0.35, result.L);
        }

        [Fact(DisplayName = "Build_HelloWorldString")]
        public void Build_HelloWorldString()
        {
            var ColorHash = new Fernandezja.ColorHash.ColorHash();

            var resultHsl = ColorHash.BuildToHsl("Hello World");
            var resultColor = ColorHash.BuildToColor("Hello World");
            var resultHex = ColorHash.BuildToHex("Hello World");

            Assert.Equal(31, resultColor.R);
            Assert.Equal(46, resultColor.G);
            Assert.Equal(89, resultColor.B);

            Assert.Equal(225, resultHsl.H);
            Assert.Equal(0.65, resultHsl.S);
            Assert.Equal(0.35, resultHsl.L);

            Assert.Equal("1F2E59", resultHex);


        }



    }
}
