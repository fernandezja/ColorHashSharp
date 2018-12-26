using Fernandezja.ColorHash.Entities;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Drawing;

namespace Fernandezja.ColorHash
{
    public class ColorHash
    {

        private Options _options;

        public ColorHash()
        {
             _options = new Options();
        }


        public string Build(string value) {
            return BuildToHex(value);
        }

        public Color BuildToColor(string value)
        {
            var hsl = BuildToHsl(value);
            var color = (new ColorToRgb()).ToRgb(hsl);
            return color;
        }

        public string BuildToHex(string value)
        {
            var color = BuildToColor(value);
            var hex = (new ColorToHex()).BuildToHex(color);
            return hex;
        }

        public Hsl BuildToHsl(string value) {
            double h, s, l;

            var hashGenerator = new BKDRHash();

            var hash = hashGenerator.Generate(value);

            if (_options.HueRanges.Count > 0)
            {
                var rangeIndex = hash % Convert.ToUInt64(_options.HueRanges.Count);
                var hueValue = (Hue)_options.HueRanges[(int)rangeIndex]; //TODO: Convert int? prevent error

                var hueResolution = Convert.ToUInt64(727); 
                h = ((hash / Convert.ToUInt64(_options.HueRanges.Count)) % hueResolution) 
                    * (Convert.ToUInt64(hueValue.Max) - Convert.ToUInt64(hueValue.Min)) / hueResolution + Convert.ToUInt64(hueValue.Min);

            }
            else
            {
                h = hash % 359; // note that 359 is a prime
            }

            hash = (uint)(hash / 360);
            var sIndex = (int)(hash % Convert.ToUInt64(_options.S.Count));
            s = (double)_options.S[sIndex];

            hash = (uint)(hash / Convert.ToUInt64(_options.S.Count));
            var lIndex = (int)(hash % Convert.ToUInt64(_options.L.Count));
            l = (double)_options.L[lIndex];

            var hslResult = new Hsl(h, s, l);

            return hslResult;
        }
    }
}
