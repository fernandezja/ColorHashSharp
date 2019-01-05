using Fernandezja.ColorHashSharp.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;

namespace Fernandezja.ColorHashSharp
{
    public class ColorToRgb
    {
        public Color ToRgb(Hsl hsl) {
            return ToRgb(hsl.H, hsl.S, hsl.L);
        }


        public static Color ToRgb(double hue, double saturation, double lightness) {
            return ToRgb2(hue, saturation, lightness);
        }



        /// <summary>
        /// From https://en.wikipedia.org/wiki/HSL_and_HSV
        /// </summary>
        /// <param name="hue"></param>
        /// <param name="saturation"></param>
        /// <param name="lightness"></param>
        /// <returns></returns>
        public static Color ToRgb1(double hue, double saturation, double lightness)
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



        /// <summary>
        /// Idem HSL2RGB from zenozeng/color-hash
        /// https://github.com/zenozeng/color-hash/blob/master/dist/color-hash.js
        /// </summary>
        /// <param name="hue"></param>
        /// <param name="saturation"></param>
        /// <param name="lightness"></param>
        /// <returns></returns>
        public static Color ToRgb2(double hue, double saturation, double lightness)
        {
            var h = hue / 360;
            Debug.WriteLine($"h = {h}");

            var q = lightness < 0.5 
                    ? lightness * (1 + saturation) 
                    : lightness + saturation - (lightness * saturation);


            var p = 2.0 * lightness - q;

            Debug.WriteLine($"q = {q}");
            Debug.WriteLine($"p = {p}");

            var r = GetColor(h + 1 / 3.0, q, p);
            var g = GetColor(h, q, p);
            var b = GetColor(h - 1 / 3.0, q, p);

            return Color.FromArgb(alpha: 255, red: r, green: g, blue: b);


        }

        private static int GetColor(double color, double q, double p) {

            Debug.WriteLine($" 1 color = {color}");

            if (color < 0)
            {
                color++;
            }

            if (color > 1)
            {
                color--;
            }

            if (color < (1.0/6.0))
            {
                color = p + (q - p) * 6.0 * color;
            }
            else if (color < 0.5)
            {
                color = q;
            }
            else if (color < (2.0/3.0))
            {
                color = p + (q - p) * 6.0 * ((2.0 / 3.0) - color);
            }
            else
            {
                color = p;
            }

            Debug.WriteLine($" 2 color = {color}");

            Debug.WriteLine($" ----------------------------");

            return (int)Math.Round(color * 255);

            
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
