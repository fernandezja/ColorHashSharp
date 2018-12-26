using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Fernandezja.ColorHash;
using Xunit;

namespace ColorHash.Tests
{
    public class OptionsTest
    {

        [Fact(DisplayName = "GetLS_OneValue")] 
        public void GetLS_OneValue()
        {
            var options = new Fernandezja.ColorHash.Options();
            var result = options.GetLS(0.35);
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(0.35, result[0]);
        }

        [Fact(DisplayName = "GetLS_DefaultValues")]
        public void GetLS_DefaultValues()
        {
            var options = new Fernandezja.ColorHash.Options();
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
            var options = new Fernandezja.ColorHash.Options();
            var result = options.GetLS(new ArrayList() { 0.35, 0.35, 0.35});
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
            Assert.Equal(0.35, result[0]);
            Assert.Equal(0.35, result[1]);
            Assert.Equal(0.35, result[2]);
        }


    }
}
