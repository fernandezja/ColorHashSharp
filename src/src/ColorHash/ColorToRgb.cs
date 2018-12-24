using Fernandezja.ColorHash.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Fernandezja.ColorHash
{
    public class ColorToRgb
    {
        public Color ToRgb(Hsl hsl) {
            return ToRgb(hsl.H, hsl.S, hsl.L);
        }

        /// <summary>
        /// From https://en.wikipedia.org/wiki/HSL_and_HSV
        /// </summary>
        /// <param name="hue"></param>
        /// <param name="saturation"></param>
        /// <param name="lightness"></param>
        /// <returns></returns>
        public static Color ToRgb(double hue, double saturation, double lightness)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            lightness = lightness * 255;
            int v = Convert.ToInt32(lightness);
            int p = Convert.ToInt32(lightness * (1 - saturation));
            int q = Convert.ToInt32(lightness * (1 - f * saturation));
            int t = Convert.ToInt32(lightness * (1 - (1 - f) * saturation));

            if (hi == 0)
                return Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(255, t, p, v);
            else
                return Color.FromArgb(255, v, p, q);
        }

        //public Color ToRgb(double h, double s, double l)
        //{
        //    double r = 0, 
        //           g = 0, 
        //           b = 0;

        //    if (l != 0)
        //    {
        //        if (s == 0)
        //            r = g = b = l;
        //        else
        //        {
        //            double temp2;
        //            if (l < 0.5)
        //                temp2 = l * (1.0 + s);
        //            else
        //                temp2 = l + s - (l * s);

        //            double temp1 = 2.0 * l - temp2;

        //            r = GetColorComponent(temp1, temp2, h + 1.0 / 3.0);
        //            g = GetColorComponent(temp1, temp2, h);
        //            b = GetColorComponent(temp1, temp2, h - 1.0 / 3.0);
        //        }
        //    }
        //    return Color.FromArgb((int)(255 * r), (int)(255 * g), (int)(255 * b));

        //}

        //private static double GetColorComponent(
        //    double temp1, double temp2, double temp3)
        //{
        //    if (temp3 < 0.0)
        //        temp3 += 1.0;
        //    else if (temp3 > 1.0)
        //        temp3 -= 1.0;

        //    if (temp3 < 1.0 / 6.0)
        //        return temp1 + (temp2 - temp1) * 6.0 * temp3;
        //    else if (temp3 < 0.5)
        //        return temp2;
        //    else if (temp3 < 2.0 / 3.0)
        //        return temp1 + ((temp2 - temp1) * ((2.0 / 3.0) - temp3) * 6.0);
        //    else
        //        return temp1;
        //}
    }
}
