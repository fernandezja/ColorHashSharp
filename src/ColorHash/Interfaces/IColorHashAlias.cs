using System.Drawing;
using Fernandezja.ColorHashSharp.Entities;

namespace Fernandezja.ColorHashSharp.Interfaces
{
    public interface IColorHashAlias { 
    
        /// <summary>
        /// Alias of BuildToColor method
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        Color Rgb(string value);

        /// <summary>
        /// Alias of BuildToHex method
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        string Hex(string value);


        /// <summary>
        /// Alias of BuildToHsl method
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        Hsl Hsl(string value);
    }
}