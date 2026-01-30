using Fernandezja.ColorHashSharp;
using System.Collections;
using Xunit;

namespace ColorHashSharp.Tests
{
    public class OptionsExtendedTest
    {
        [Fact(DisplayName = "Options_DefaultConstructor_ShouldInitializeWithDefaults")]
        public void Options_DefaultConstructor_ShouldInitializeWithDefaults()
        {
            var options = new Options();

            Assert.NotNull(options.S);
            Assert.NotNull(options.L);
            Assert.NotNull(options.HueRanges);
            Assert.Equal(3, options.S.Count);
            Assert.Equal(3, options.L.Count);
            Assert.Empty(options.HueRanges);
        }

        [Fact(DisplayName = "GetLS_WithNullParam_ShouldReturnDefaults")]
        public void GetLS_WithNullParam_ShouldReturnDefaults()
        {
            var options = new Options();
            var result = options.GetLS((ArrayList)null);

            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
            Assert.Equal(0.35, result[0]);
            Assert.Equal(0.5, result[1]);
            Assert.Equal(0.65, result[2]);
        }

        [Theory(DisplayName = "SetHue_DifferentValues_ShouldSetCorrectly")]
        [InlineData(0)]
        [InlineData(180)]
        [InlineData(360)]
        public void SetHue_DifferentValues_ShouldSetCorrectly(int value)
        {
            var options = new Options();
            options.SetHue(value);

            Assert.Single(options.HueRanges);
            Assert.Equal(value, options.HueRanges[0].Min);
            Assert.Equal(value, options.HueRanges[0].Max);
        }

        [Fact(DisplayName = "Options_SaturationAndLightness_DefaultValues")]
        public void Options_SaturationAndLightness_DefaultValues()
        {
            var options = new Options();

            // Test that S and L contain the expected default values
            Assert.Contains(0.35, options.S.ToArray());
            Assert.Contains(0.5, options.S.ToArray());
            Assert.Contains(0.65, options.S.ToArray());
            
            Assert.Contains(0.35, options.L.ToArray());
            Assert.Contains(0.5, options.L.ToArray());
            Assert.Contains(0.65, options.L.ToArray());
        }
    }
}
