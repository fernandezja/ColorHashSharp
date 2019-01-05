using System.Drawing;
using Fernandezja.ColorHash.Entities;

namespace Fernandezja.ColorHash.Interfaces
{
    public interface IColorHash
    {
        string Build(string value);
        Color BuildToColor(string value);
        string BuildToHex(string value);
        Hsl BuildToHsl(string value);
    }
}