using Fernandezja.ColorHashSharp;
using Fernandezja.ColorHashSharp.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Xunit;

namespace ColorHashSharp.Tests
{
    public class ColorToRgbTest
    {

        [Fact(DisplayName = "ToRgb_HslToRgb")]
        public void ToRgb_HslToRgb()
        {
            //HSL from ""Hello World" 255, 0.35, 0.65
            //double hue, double saturation, double lightness
            var result = ColorToRgb.ToRgb(225, 0.35, 0.65);

            Assert.Equal(135, result.R);
            Assert.Equal(150, result.G);
            Assert.Equal(197, result.B);
        }

        [Fact(DisplayName = "ToRgb_WithHslObject_ShouldConvert")]
        public void ToRgb_WithHslObject_ShouldConvert()
        {
            var colorToRgb = new ColorToRgb();
            var hsl = new Hsl(225, 0.35, 0.65);

            var result = colorToRgb.ToRgb(hsl);

            Assert.Equal(135, result.R);
            Assert.Equal(150, result.G);
            Assert.Equal(197, result.B);
        }

        [Fact(DisplayName = "ToRgb1_Method_ShouldConvertHslToRgb")]
        public void ToRgb1_Method_ShouldConvertHslToRgb()
        {
            var result = ColorToRgb.ToRgb1(0, 1, 0.5);

            // ToRgb1 uses HSV algorithm, not HSL
            Assert.Equal(128, result.R);
            Assert.Equal(0, result.G);
            Assert.Equal(0, result.B);
        }

        [Fact(DisplayName = "ToRgb1_BlueColor_ShouldConvert")]
        public void ToRgb1_BlueColor_ShouldConvert()
        {
            var result = ColorToRgb.ToRgb1(240, 1, 0.5);

            Assert.Equal(0, result.R);
            Assert.Equal(0, result.G);
            Assert.Equal(128, result.B);
        }

        [Fact(DisplayName = "ToRgb1_GreenColor_ShouldConvert")]
        public void ToRgb1_GreenColor_ShouldConvert()
        {
            var result = ColorToRgb.ToRgb1(120, 1, 0.5);

            Assert.Equal(0, result.R);
            Assert.Equal(128, result.G);
            Assert.Equal(0, result.B);
        }

        [Theory(DisplayName = "ToRgb1_AllSectors_ShouldConvert")]
        [InlineData(0)]   // hi = 0
        [InlineData(60)]  // hi = 1
        [InlineData(120)] // hi = 2
        [InlineData(180)] // hi = 3
        [InlineData(240)] // hi = 4
        [InlineData(300)] // hi = 5
        public void ToRgb1_AllSectors_ShouldConvert(double hue)
        {
            var result = ColorToRgb.ToRgb1(hue, 1, 0.5);

            Assert.NotNull(result);
            Assert.Equal(255, result.A);
        }

        [Fact(DisplayName = "ToRgb2_RedColor_ShouldConvert")]
        public void ToRgb2_RedColor_ShouldConvert()
        {
            var result = ColorToRgb.ToRgb2(0, 1, 0.5);

            Assert.Equal(255, result.A);
            Assert.Equal(255, result.R);
        }

        [Fact(DisplayName = "ToRgb2_BlackColor_ShouldConvert")]
        public void ToRgb2_BlackColor_ShouldConvert()
        {
            var result = ColorToRgb.ToRgb2(0, 0, 0);

            Assert.Equal(0, result.R);
            Assert.Equal(0, result.G);
            Assert.Equal(0, result.B);
        }

        [Fact(DisplayName = "ToRgb2_WhiteColor_ShouldConvert")]
        public void ToRgb2_WhiteColor_ShouldConvert()
        {
            var result = ColorToRgb.ToRgb2(0, 0, 1);

            Assert.Equal(255, result.R);
            Assert.Equal(255, result.G);
            Assert.Equal(255, result.B);
        }
    }
}
