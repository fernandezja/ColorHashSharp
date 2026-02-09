using Fernandezja.ColorHashSharp.Entities;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Drawing;
using Fernandezja.ColorHashSharp.Interfaces;

namespace Fernandezja.ColorHashSharp
{
    public class ColorHash : IColorHash, IColorHashAlias
    {
        private readonly Options _options;
        private readonly IHashFunction _hashGenerator;
        private readonly ColorToHex _hexConverter;

        public ColorHash()
        {
             _options = new Options();
             _hashGenerator = new BKDRHash();
             _hexConverter = new ColorToHex();
        }

        public ColorHash(Options options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            // Use custom hash function if provided, otherwise use default BKDRHash
            _hashGenerator = _options.HashFunction ?? new BKDRHash();
            _hexConverter = new ColorToHex();
        }

        #region IColorHash

        public string Build(string value)
        {
            return BuildToHex(value);
        }

        public Color BuildToColor(string value)
        {
            var hsl = BuildToHsl(value);
            var color = ColorToRgb.ToRgb(hsl.H, hsl.S, hsl.L);
            return color;
        }

        public string BuildToHex(string value)
        {
            var color = BuildToColor(value);
            var hex = _hexConverter.BuildToHex(color);
            return hex;
        }

        public Hsl BuildToHsl(string value)
        {
            double h, s, l;

            var hash = _hashGenerator.Generate(value);

            if (_options.HueRanges.Count > 0)
            {
                var rangeIndex = hash % Convert.ToUInt64(_options.HueRanges.Count);

                var hueValue = _options.HueRanges[(int)rangeIndex];

                var hueResolution = Convert.ToUInt64(727);
                h = ((hash / Convert.ToUInt64(_options.HueRanges.Count)) % hueResolution)
                    * (Convert.ToUInt64(hueValue.Max) - Convert.ToUInt64(hueValue.Min)) / hueResolution + Convert.ToUInt64(hueValue.Min);

            }
            else
            {
                h = hash % 359; // note that 359 is a prime
            }

            hash = (hash / 360);
            var sIndex = (hash % (ulong)_options.S.Count);
            s = _options.S[(int)sIndex];

            hash = (hash / (ulong)_options.S.Count);
            var lIndex = (hash % (ulong)_options.L.Count);
            l = _options.L[(int)lIndex];

            var hslResult = new Hsl(h, s, l);

            return hslResult;
        }
        #endregion

        #region IColorHashAlias

        /// <summary>
        /// Alias of BuildToColor method
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Color Rgb(string value) {
            return BuildToColor(value);
        }

        /// <summary>
        /// Alias of BuildToHex method
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Hex(string value)
        {
            return BuildToHex(value);
        }


        /// <summary>
        /// Alias of BuildToHsl method
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Hsl Hsl(string value)
        {
            return BuildToHsl(value);
        }

        #endregion
    }
}
