using System;
using System.Collections.Generic;
using System.Text;

namespace Fernandezja.ColorHashSharp.Entities
{
    /// <summary>
    /// HSL (hue, saturation, lightness)
    /// SL (hue, saturation, lightness) and HSV (hue, saturation, value) 
    /// are alternative representations of the RGB color model
    /// https://en.wikipedia.org/wiki/HSL_and_HSV
    /// </summary>
    [Serializable]
    public class Hsl
    {
        public Hsl()
        {
         
        }

        public Hsl(double h, double s, double l)
        {
            H = h;
            S = s;
            L = l;
        }


        public double H { get; set; }
        public double S { get; set; }
        public double L { get; set; }
    }
}
