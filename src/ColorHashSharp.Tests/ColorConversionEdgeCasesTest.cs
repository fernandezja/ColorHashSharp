using Fernandezja.ColorHashSharp;
using Fernandezja.ColorHashSharp.Entities;
using System.Drawing;
using Xunit;

namespace ColorHashSharp.Tests
{
    public class ColorConversionEdgeCasesTest
    {
        #region HSL Edge Cases

        [Theory(DisplayName = "HsLtoRgb_VariousLightnessValues")]
        [InlineData(0, 1, 0.0)] // Black
        [InlineData(0, 1, 0.25)]
        [InlineData(0, 1, 0.5)]
        [InlineData(0, 1, 0.75)]
        [InlineData(0, 1, 1.0)] // White
        public void HsLtoRgb_VariousLightnessValues(double h, double s, double l)
        {
            var result = SimpleColorTransforms.HsLtoRgb(h, s, l);

            Assert.InRange(result.R, 0, 255);
            Assert.InRange(result.G, 0, 255);
            Assert.InRange(result.B, 0, 255);
        }

        [Theory(DisplayName = "HsLtoRgb_VariousSaturationValues")]
        [InlineData(120, 0.0, 0.5)]
        [InlineData(120, 0.25, 0.5)]
        [InlineData(120, 0.5, 0.5)]
        [InlineData(120, 0.75, 0.5)]
        [InlineData(120, 1.0, 0.5)]
        public void HsLtoRgb_VariousSaturationValues(double h, double s, double l)
        {
            var result = SimpleColorTransforms.HsLtoRgb(h, s, l);

            Assert.InRange(result.R, 0, 255);
            Assert.InRange(result.G, 0, 255);
            Assert.InRange(result.B, 0, 255);
        }

        [Theory(DisplayName = "HsLtoRgb_EdgeHueValues")]
        [InlineData(-10, 1, 0.5)]  // Negative, should clamp to 0
        [InlineData(370, 1, 0.5)]  // Over 360, should clamp to 360
        public void HsLtoRgb_EdgeHueValues(double h, double s, double l)
        {
            var result = SimpleColorTransforms.HsLtoRgb(h, s, l);

            Assert.InRange(result.R, 0, 255);
            Assert.InRange(result.G, 0, 255);
            Assert.InRange(result.B, 0, 255);
        }

        [Fact(DisplayName = "HsLtoRgb_LowLuminance_ShouldBeDark")]
        public void HsLtoRgb_LowLuminance_ShouldBeDark()
        {
            var result = SimpleColorTransforms.HsLtoRgb(120, 1, 0.1);

            // Low luminance should produce dark colors
            Assert.True(result.R < 100 && result.G < 100 && result.B < 100);
        }

        [Fact(DisplayName = "HsLtoRgb_HighLuminance_ShouldBeBright")]
        public void HsLtoRgb_HighLuminance_ShouldBeBright()
        {
            var result = SimpleColorTransforms.HsLtoRgb(120, 1, 0.9);

            // High luminance should produce bright colors
            Assert.True(result.R > 150 || result.G > 150 || result.B > 150);
        }

        #endregion

        #region HSB Edge Cases

        [Theory(DisplayName = "HsBtoRgb_VariousBrightnessValues")]
        [InlineData(120, 1, 0.0)]
        [InlineData(120, 1, 0.25)]
        [InlineData(120, 1, 0.5)]
        [InlineData(120, 1, 0.75)]
        [InlineData(120, 1, 1.0)]
        public void HsBtoRgb_VariousBrightnessValues(double h, double s, double b)
        {
            var result = SimpleColorTransforms.HsBtoRgb(h, s, b);

            Assert.InRange(result.R, 0, 255);
            Assert.InRange(result.G, 0, 255);
            Assert.InRange(result.B, 0, 255);
        }

        [Theory(DisplayName = "HsBtoRgb_AllSectorsDetailed")]
        [InlineData(15)]   // Sector 0 (0-60)
        [InlineData(45)]   // Sector 0
        [InlineData(75)]   // Sector 1 (60-120)
        [InlineData(105)]  // Sector 1
        [InlineData(135)]  // Sector 2 (120-180)
        [InlineData(165)]  // Sector 2
        [InlineData(195)]  // Sector 3 (180-240)
        [InlineData(225)]  // Sector 3
        [InlineData(255)]  // Sector 4 (240-300)
        [InlineData(285)]  // Sector 4
        [InlineData(315)]  // Sector 5 (300-360)
        [InlineData(345)]  // Sector 5
        public void HsBtoRgb_AllSectorsDetailed(double hue)
        {
            var result = SimpleColorTransforms.HsBtoRgb(hue, 1, 1);

            Assert.Equal(255, result.A);
            Assert.InRange(result.R, 0, 255);
            Assert.InRange(result.G, 0, 255);
            Assert.InRange(result.B, 0, 255);
        }

        #endregion

        #region Transform Edge Cases

        [Fact(DisplayName = "TransformBrightness_ExtremeIncrease")]
        public void TransformBrightness_ExtremeIncrease()
        {
            var color = Color.FromArgb(50, 50, 50);
            var result = SimpleColorTransforms.TransformBrightness(
                color, 
                SimpleColorTransforms.ColorTransformMode.Hsl, 
                5.0);

            Assert.NotEqual(color, result);
        }

        [Fact(DisplayName = "TransformSaturationAndBrightness_ExtremeValues")]
        public void TransformSaturationAndBrightness_ExtremeValues()
        {
            var color = Color.FromArgb(100, 150, 200);
            var result = SimpleColorTransforms.TransformSaturationAndBrightness(
                color, 
                SimpleColorTransforms.ColorTransformMode.Hsb, 
                5.0, 
                5.0);

            Assert.NotNull(result);
        }

        #endregion

        #region RgBtoHsl Special Cases

        [Fact(DisplayName = "RgBtoHsl_MaxIsRed_GreenGreaterThanBlue")]
        public void RgBtoHsl_MaxIsRed_GreenGreaterThanBlue()
        {
            var color = Color.FromArgb(255, 100, 50);
            var result = SimpleColorTransforms.RgBtoHsl(color);

            Assert.InRange(result[0], 0, 360);
        }

        [Fact(DisplayName = "RgBtoHsl_MaxIsRed_GreenLessThanBlue")]
        public void RgBtoHsl_MaxIsRed_GreenLessThanBlue()
        {
            var color = Color.FromArgb(255, 50, 100);
            var result = SimpleColorTransforms.RgBtoHsl(color);

            Assert.InRange(result[0], 0, 360);
        }

        [Fact(DisplayName = "RgBtoHsl_LuminanceLowSaturationNonZero")]
        public void RgBtoHsl_LuminanceLowSaturationNonZero()
        {
            var color = Color.FromArgb(30, 20, 10);
            var result = SimpleColorTransforms.RgBtoHsl(color);

            Assert.InRange(result[1], 0, 1); // Saturation
            Assert.InRange(result[2], 0, 0.5); // Low luminance
        }

        [Fact(DisplayName = "RgBtoHsl_LuminanceHighSaturationNonZero")]
        public void RgBtoHsl_LuminanceHighSaturationNonZero()
        {
            var color = Color.FromArgb(255, 200, 150);
            var result = SimpleColorTransforms.RgBtoHsl(color);

            Assert.InRange(result[1], 0, 1); // Saturation
            Assert.InRange(result[2], 0.5, 1); // High luminance
        }

        #endregion

        #region RgBtoHsb Special Cases

        [Fact(DisplayName = "RgBtoHsb_MaxIsRed_GreenGreaterThanBlue")]
        public void RgBtoHsb_MaxIsRed_GreenGreaterThanBlue()
        {
            var color = Color.FromArgb(255, 100, 50);
            var result = SimpleColorTransforms.RgBtoHsb(color);

            Assert.InRange(result[0], 0, 60); // Hue in first sector
        }

        [Fact(DisplayName = "RgBtoHsb_MaxIsRed_GreenLessThanBlue")]
        public void RgBtoHsb_MaxIsRed_GreenLessThanBlue()
        {
            var color = Color.FromArgb(255, 50, 100);
            var result = SimpleColorTransforms.RgBtoHsb(color);

            Assert.InRange(result[0], 300, 360); // Hue wraps around
        }

        #endregion
    }
}
