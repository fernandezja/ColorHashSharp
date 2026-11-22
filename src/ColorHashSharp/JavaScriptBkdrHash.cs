using Fernandezja.ColorHashSharp.Interfaces;

namespace Fernandezja.ColorHashSharp
{
    /// <summary>
    /// Computes the optional BKDR hash from color-hash 2.x over UTF-16 code units.
    /// </summary>
    public sealed class JavaScriptBkdrHash : IHashFunction
    {
        private const ulong Seed = 131;
        private const ulong ReductionDivisor = 137;
        private const ulong ReductionThreshold = 9007199254740991 / ReductionDivisor;
        private const char Padding = 'x';

        public ulong Generate(string value)
        {
            ulong hash = 0;
            foreach (var codeUnit in value ?? string.Empty)
            {
                hash = Append(hash, codeUnit);
            }

            return Append(hash, Padding);
        }

        private static ulong Append(ulong hash, char codeUnit)
        {
            if (hash > ReductionThreshold)
            {
                hash /= ReductionDivisor;
            }

            return hash * Seed + codeUnit;
        }
    }
}
