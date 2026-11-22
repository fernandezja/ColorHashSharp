using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Fernandezja.ColorHashSharp
{
    public class ColorToHex
    {
        public string BuildToHex(Color color)
        {
            return BuildToHex(color, cssFormat: false);
        }

        internal string BuildToHex(Color color, bool cssFormat)
        {
            var prefix = cssFormat ? "#" : string.Empty;
            var format = cssFormat ? "x2" : "X2";
            return prefix + color.R.ToString(format) + color.G.ToString(format) + color.B.ToString(format);
        }
    }
}
