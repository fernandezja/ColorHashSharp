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

        public Hsl _hsl { get; set; }
        public Hue _hue { get; set; }
        public ArrayList _hueList { get; set; }
        public ArrayList _ls { get; set; }
        public ArrayList _s { get; set; }
        public ArrayList _l { get; set; }

        public ColorHash()
        {
            _hsl = new Hsl();
            _hue = new Hue();
            _hueList = new ArrayList();
            _ls = new ArrayList() { 0.35, 0.5, 0.65 };

            _s = new ArrayList() { 0.35, 0.5, 0.65 };
            _l = new ArrayList() { 0.35, 0.5, 0.65 };
        }


        

        private string StringToNumber(string value) {
            var result = new StringBuilder();
            for (var i = 0; i < value.Length; i++)
            {
                result.Append(((int)value[i]).ToString());
            }
            return result.ToString();
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

            if (_hueList.Count > 0)
            {
                var rangeIndex = hash % Convert.ToUInt64(_hueList.Count);
                var hueValue = (Hue)_hueList[(int)rangeIndex]; //TODO: Convert int? prevent error

                var hueResolution = Convert.ToUInt64(727); 
                h = ((hash / Convert.ToUInt64(_hueList.Count)) % hueResolution) 
                    * (Convert.ToUInt64(hueValue.Max) - Convert.ToUInt64(hueValue.Min)) / hueResolution + Convert.ToUInt64(hueValue.Min);

            }
            else
            {
                h = hash % 359; // note that 359 is a prime
            }

            hash = (uint)(hash / 360);
            var sIndex = (int)(hash % Convert.ToUInt64(_s.Count));
            s = (double)_s[sIndex];

            hash = (uint)(hash / Convert.ToUInt64(_s.Count));
            var lIndex = (int)(hash % Convert.ToUInt64(_l.Count));
            l = (double)_l[lIndex];

            var hslResult = new Hsl(h, s, l);

            return hslResult;
        }
    }
}
