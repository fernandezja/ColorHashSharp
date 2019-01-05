using System.Drawing;
using Fernandezja.ColorHashSharp.Entities;

namespace Fernandezja.ColorHashSharp.Interfaces
{
    public interface IColorHash
    {
        string Build(string value);
        Color BuildToColor(string value);
        string BuildToHex(string value);
        Hsl BuildToHsl(string value);
    }
}