using Fernandezja.ColorHashSharp.Entities;
using Xunit;

namespace ColorHashSharp.Tests
{
    public class HueTest
    {
        [Fact(DisplayName = "Hue_DefaultConstructor_ShouldInitialize")]
        public void Hue_DefaultConstructor_ShouldInitialize()
        {
            var hue = new Hue();

            Assert.NotNull(hue);
            Assert.Equal(0, hue.Min);
            Assert.Equal(360, hue.Max);
        }

        [Fact(DisplayName = "Hue_ParameterizedConstructor_ShouldSetValues")]
        public void Hue_ParameterizedConstructor_ShouldSetValues()
        {
            var hue = new Hue(30, 120);

            Assert.NotNull(hue);
            Assert.Equal(30, hue.Min);
            Assert.Equal(120, hue.Max);
        }

        [Fact(DisplayName = "Hue_Properties_CanBeModified")]
        public void Hue_Properties_CanBeModified()
        {
            var hue = new Hue();
            
            hue.Min = 90;
            hue.Max = 270;

            Assert.Equal(90, hue.Min);
            Assert.Equal(270, hue.Max);
        }
    }
}
