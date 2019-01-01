using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Fernandezja.ColorHash
{
    public class ColorToHex
    {
        public string BuildToHex(Color color) {
            string hex = color.R.ToString("X2") 
                    + color.G.ToString("X2") 
                    + color.B.ToString("X2");
            return hex;
        }
    }
}
