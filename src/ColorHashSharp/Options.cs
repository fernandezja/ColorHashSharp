using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Fernandezja.ColorHashSharp
{
    public class Options
    {
        public ArrayList HueRanges { get; set; }

        /// <summary>
        /// Saturation
        /// </summary>
        public ArrayList S { get; set; }

        /// <summary>
        /// Lightness
        /// </summary>
        public ArrayList L { get; set; }

        public Options()
        {
            //TODO: Get from options param
            S = GetLS(new ArrayList() { 0.35, 0.5, 0.65 });
            L = GetLS(new ArrayList() { 0.35, 0.5, 0.65 });

            HueRanges = new ArrayList();
        }


        /// <summary>
        /// Get lightness or saturation
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        internal protected ArrayList GetLS(double param)
        {
            return new ArrayList() { param };
        }

        /// <summary>
        ///  Get lightness or saturation
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        internal protected ArrayList GetLS(ArrayList param)
        {
            if (param == null)
            {
                // note that 3 is a prime
                param = new ArrayList() { 0.35, 0.5, 0.65 };
            }

            return param;
        }

        internal protected ArrayList GetLS()
        {
            return GetLS(null);
        }


    }
}
