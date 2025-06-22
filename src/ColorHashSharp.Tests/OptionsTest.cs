using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Fernandezja.ColorHashSharp;
using Xunit;

namespace ColorHashSharp.Tests
{
    public class OptionsTest
    {

        [Fact(DisplayName = "GetLS_OneValue")] 
        public void GetLS_OneValue()
        {
            var options = new Fernandezja.ColorHashSharp.Options();
            var result = options.GetLS(0.35);
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(0.35, result[0]);
        }

        [Fact(DisplayName = "GetLS_DefaultValues")]
        public void GetLS_DefaultValues()
        {
            var options = new Fernandezja.ColorHashSharp.Options();
            var result = options.GetLS();
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
            Assert.Equal(0.35, result[0]);
            Assert.Equal(0.5, result[1]);
            Assert.Equal(0.65, result[2]);
        }

        [Fact(DisplayName = "GetLS_ArrayValues")]
        public void GetLS_ArrayValues()
        {
            var options = new Fernandezja.ColorHashSharp.Options();
            var result = options.GetLS(new ArrayList() { 0.35, 0.35, 0.35});
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
            Assert.Equal(0.35, result[0]);
            Assert.Equal(0.35, result[1]);
            Assert.Equal(0.35, result[2]);
        }


        [Theory(DisplayName = "SetHue_SimpleValue")]
        [InlineData(90)]
        [InlineData(0.35)]
        public void SetHue_SimpleValue(int value)
        {
            var options = new Fernandezja.ColorHashSharp.Options();
            options.SetHue(value);

            Assert.NotNull(options.HueRanges);
            Assert.Single(options.HueRanges);
            Assert.Equal(value, options.HueRanges[0].Min);
            Assert.Equal(value, options.HueRanges[0].Max);
        }


        [Fact(DisplayName = "SetHue_ListValues")]     
        public void SetHue_ListValues()
        {
            var options = new Fernandezja.ColorHashSharp.Options();

            var hueValues = new List<(int Min, int Max)>();
            hueValues.Add((90, 270));

            options.SetHue(hueValues);

            Assert.NotNull(options.HueRanges);
            Assert.Single(options.HueRanges);
            Assert.Equal(90, options.HueRanges[0].Min);
            Assert.Equal(270, options.HueRanges[0].Max);
        }

        [Fact(DisplayName = "SetHue_ListValues2")]
        public void SetHue_ListValues2()
        {
            var options = new Fernandezja.ColorHashSharp.Options();

            var hueValues = new List<(int Min, int Max)>();
            hueValues.Add((30, 90));
            hueValues.Add((180, 210));
            hueValues.Add((270, 285));

            options.SetHue(hueValues);

            Assert.NotNull(options.HueRanges);
            Assert.Equal(3, options.HueRanges.Count);

            Assert.Equal(30, options.HueRanges[0].Min);
            Assert.Equal(90, options.HueRanges[0].Max);

            Assert.Equal(180, options.HueRanges[1].Min);
            Assert.Equal(210, options.HueRanges[1].Max);

            Assert.Equal(270, options.HueRanges[2].Min);
            Assert.Equal(285, options.HueRanges[2].Max);
        }

    }
}
