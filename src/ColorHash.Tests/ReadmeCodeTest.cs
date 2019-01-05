using System;
using System.Collections.Generic;
using System.Text;
using ColorHashSharp = Fernandezja.ColorHash;
using Xunit;
using System.Diagnostics;

namespace ColorHash.Tests
{
    public class ReadmeCodeTest
    {
        [Fact(DisplayName = "ReadmeCodeExample_ToCopyPaste")]
        public void ReadmeCodeExample_ToCopyPaste()
        {
            Debug.WriteLine($"ColorHash >  Hello World");

            var colorHash = new ColorHashSharp.ColorHash();

            //HSL (return HSL object)
            var hsl = colorHash.Hsl("Hello World");

            Debug.WriteLine($"  H = {hsl.H} | S = {hsl.S} | L = {hsl.L}");


            //RGB (return System.Drawing.Color object)
            var color = colorHash.Rgb("Hello World");

            Debug.WriteLine($"   R= {color.R} | G = {color.G} | B = {color.B}");
            

            //Hex (return string)
            var hex = colorHash.Hex("Hello World");

            Debug.WriteLine($"  hex = {hex}");
        }
    }
}
