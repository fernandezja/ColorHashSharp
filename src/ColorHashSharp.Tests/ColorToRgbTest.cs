using Fernandezja.ColorHashSharp;
using System;
using System.Collections.Generic;
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
    }
}
