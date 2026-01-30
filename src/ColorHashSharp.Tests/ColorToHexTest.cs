using Fernandezja.ColorHashSharp;
using System.Drawing;
using Xunit;

namespace ColorHashSharp.Tests
{
    public class ColorToHexTest
    {
        [Fact(DisplayName = "BuildToHex_RedColor_ShouldReturnFF0000")]
        public void BuildToHex_RedColor_ShouldReturnFF0000()
        {
            var colorToHex = new ColorToHex();
            var color = Color.FromArgb(255, 0, 0);
            
            var result = colorToHex.BuildToHex(color);

            Assert.Equal("FF0000", result);
        }

        [Fact(DisplayName = "BuildToHex_GreenColor_ShouldReturn00FF00")]
        public void BuildToHex_GreenColor_ShouldReturn00FF00()
        {
            var colorToHex = new ColorToHex();
            var color = Color.FromArgb(0, 255, 0);
            
            var result = colorToHex.BuildToHex(color);

            Assert.Equal("00FF00", result);
        }

        [Fact(DisplayName = "BuildToHex_BlueColor_ShouldReturn0000FF")]
        public void BuildToHex_BlueColor_ShouldReturn0000FF()
        {
            var colorToHex = new ColorToHex();
            var color = Color.FromArgb(0, 0, 255);
            
            var result = colorToHex.BuildToHex(color);

            Assert.Equal("0000FF", result);
        }

        [Fact(DisplayName = "BuildToHex_BlackColor_ShouldReturn000000")]
        public void BuildToHex_BlackColor_ShouldReturn000000()
        {
            var colorToHex = new ColorToHex();
            var color = Color.FromArgb(0, 0, 0);
            
            var result = colorToHex.BuildToHex(color);

            Assert.Equal("000000", result);
        }

        [Fact(DisplayName = "BuildToHex_WhiteColor_ShouldReturnFFFFFF")]
        public void BuildToHex_WhiteColor_ShouldReturnFFFFFF()
        {
            var colorToHex = new ColorToHex();
            var color = Color.FromArgb(255, 255, 255);
            
            var result = colorToHex.BuildToHex(color);

            Assert.Equal("FFFFFF", result);
        }

        [Fact(DisplayName = "BuildToHex_CustomColor_ShouldReturnCorrectHex")]
        public void BuildToHex_CustomColor_ShouldReturnCorrectHex()
        {
            var colorToHex = new ColorToHex();
            var color = Color.FromArgb(135, 150, 197);
            
            var result = colorToHex.BuildToHex(color);

            Assert.Equal("8796C5", result);
        }

        [Fact(DisplayName = "BuildToHex_LowValues_ShouldPadWithZeros")]
        public void BuildToHex_LowValues_ShouldPadWithZeros()
        {
            var colorToHex = new ColorToHex();
            var color = Color.FromArgb(1, 15, 16);
            
            var result = colorToHex.BuildToHex(color);

            Assert.Equal("010F10", result);
        }
    }
}
