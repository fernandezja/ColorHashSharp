using Fernandezja.ColorHashSharp.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using Fernandezja.ColorHashSharp.Interfaces;

namespace Fernandezja.ColorHashSharp
{
    public class ColorHash : IColorHash, IColorHashAlias
    {
        private const int HueModulo = 359;
        private const int HueResolution = 727;

        private readonly (int Min, int Max)[] _hueRanges;
        private readonly double[] _saturation;
        private readonly double[] _lightness;
        private readonly bool _useJavaScriptCompatibility;
        private readonly IHashFunction _hashGenerator;
        private readonly ColorToHex _hexConverter;

        public ColorHash() : this(new Options())
        {
        }

        /// <summary>
        /// Creates a color generator with a copy of the supplied palettes and hue ranges.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="options"/> is null.</exception>
        /// <exception cref="ArgumentException">A palette or hue range is invalid.</exception>
        public ColorHash(Options options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            _saturation = CopyPalette(options.S, nameof(Options.S), nameof(options));
            _lightness = CopyPalette(options.L, nameof(Options.L), nameof(options));

            var ranges = options.HueRanges
                ?? throw new ArgumentException("HueRanges cannot be null.", nameof(options));
            _hueRanges = new (int Min, int Max)[ranges.Count];
            for (int index = 0; index < _hueRanges.Length; index++)
            {
                var range = ranges[index]
                    ?? throw new ArgumentException($"HueRanges[{index}] cannot be null.", nameof(options));
                int min = range.Min;
                int max = range.Max;
                if (min < 0 || max > 360 || min > max)
                {
                    throw new ArgumentException(
                        $"HueRanges[{index}] must satisfy 0 <= Min <= Max <= 360.", nameof(options));
                }

                _hueRanges[index] = (min, max);
            }

            _hashGenerator = options.HashFunction ?? new Sha256Hash();
            _useJavaScriptCompatibility = options.UseJavaScriptCompatibility;
            _hexConverter = new ColorToHex();
        }

        private static double[] CopyPalette(List<double> palette, string propertyName, string paramName)
        {
            if (palette == null || palette.Count == 0)
            {
                throw new ArgumentException($"{propertyName} must contain at least one value.", paramName);
            }

            var values = palette.ToArray();
            for (int index = 0; index < values.Length; index++)
            {
                double value = values[index];
                if (double.IsNaN(value) || value < 0 || value > 1)
                {
                    throw new ArgumentException(
                        $"{propertyName}[{index}] must be a finite value between 0 and 1.", paramName);
                }
            }

            return values;
        }

        #region IColorHash

        public string Build(string value)
        {
            return BuildToHex(value);
        }

        public Color BuildToColor(string value)
        {
            var hsl = BuildToHsl(value);
            var rounding = _useJavaScriptCompatibility ? MidpointRounding.AwayFromZero : MidpointRounding.ToEven;
            return ColorToRgb.ToRgb(hsl.H, hsl.S, hsl.L, rounding);
        }

        public string BuildToHex(string value)
        {
            var color = BuildToColor(value);
            return _hexConverter.BuildToHex(color, _useJavaScriptCompatibility);
        }

        public Hsl BuildToHsl(string value)
        {
            var hash = _hashGenerator.Generate(value ?? string.Empty);
            return _useJavaScriptCompatibility ? BuildJavaScriptHsl(hash) : BuildPortHsl(hash);
        }

        private Hsl BuildPortHsl(ulong hash)
        {
            double hue;

            if (_hueRanges.Length > 0)
            {
                var rangeCount = (ulong)_hueRanges.Length;
                var range = _hueRanges[(int)(hash % rangeCount)];
                hue = ((hash / rangeCount) % HueResolution)
                    * (ulong)(range.Max - range.Min) / HueResolution + (ulong)range.Min;
            }
            else
            {
                hue = hash % HueModulo;
            }

            hash /= 360;
            double saturation = _saturation[(int)(hash % (ulong)_saturation.Length)];

            hash /= (ulong)_saturation.Length;
            double lightness = _lightness[(int)(hash % (ulong)_lightness.Length)];

            return new Hsl(hue, saturation, lightness);
        }

        private Hsl BuildJavaScriptHsl(double hash)
        {
            double hue = hash % HueModulo;
            if (_hueRanges.Length > 0)
            {
                int rangeCount = _hueRanges.Length;
                var range = _hueRanges[(int)(hash % rangeCount)];
                hue = ((hash / rangeCount) % HueResolution)
                    * (range.Max - range.Min) / HueResolution + range.Min;
            }

            hash = Math.Ceiling(hash / 360);
            double saturation = _saturation[(int)(hash % _saturation.Length)];

            hash = Math.Ceiling(hash / _saturation.Length);
            double lightness = _lightness[(int)(hash % _lightness.Length)];

            return new Hsl(hue, saturation, lightness);
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
