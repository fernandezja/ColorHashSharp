using Fernandezja.ColorHashSharp.Entities;
using Xunit;

namespace ColorHashSharp.Tests
{
    public class HslTest
    {
        [Fact(DisplayName = "Hsl_DefaultConstructor_ShouldInitialize")]
        public void Hsl_DefaultConstructor_ShouldInitialize()
        {
            var hsl = new Hsl();

            Assert.NotNull(hsl);
            Assert.Equal(0, hsl.H);
            Assert.Equal(0, hsl.S);
            Assert.Equal(0, hsl.L);
        }

        [Fact(DisplayName = "Hsl_ParameterizedConstructor_ShouldSetValues")]
        public void Hsl_ParameterizedConstructor_ShouldSetValues()
        {
            var hsl = new Hsl(120.5, 0.75, 0.5);

            Assert.NotNull(hsl);
            Assert.Equal(120.5, hsl.H);
            Assert.Equal(0.75, hsl.S);
            Assert.Equal(0.5, hsl.L);
        }

        [Fact(DisplayName = "Hsl_Properties_CanBeModified")]
        public void Hsl_Properties_CanBeModified()
        {
            var hsl = new Hsl();
            
            hsl.H = 240;
            hsl.S = 0.8;
            hsl.L = 0.6;

            Assert.Equal(240, hsl.H);
            Assert.Equal(0.8, hsl.S);
            Assert.Equal(0.6, hsl.L);
        }
    }
}
