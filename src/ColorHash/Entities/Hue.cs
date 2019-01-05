using System;
using System.Collections.Generic;
using System.Text;

namespace Fernandezja.ColorHashSharp.Entities
{
    /// <summary>
    /// Hue is one of the main properties (called color appearance parameters) of a color.
    /// Defined technically (in the CIECAM02 model)
    /// https://en.wikipedia.org/wiki/Hue
    /// </summary>
    [Serializable]
    public class Hue
    {
        public Hue()
        {
            Min = 0;
            Max = 360;
        }

        public int Min { get; set; }
        public int Max { get; set; }

        
    }
}
