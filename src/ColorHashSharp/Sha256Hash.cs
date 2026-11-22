using System.Security.Cryptography;
using System.Text;
using Fernandezja.ColorHashSharp.Interfaces;

namespace Fernandezja.ColorHashSharp
{
    /// <summary>
    /// Reads the first four bytes of a SHA-256 digest as an unsigned big-endian integer.
    /// </summary>
    public sealed class Sha256Hash : IHashFunction
    {
        private static readonly Encoding Utf8 = new UTF8Encoding(false, true);

        public ulong Generate(string value)
        {
            using (var sha256 = SHA256.Create())
            {
                var digest = sha256.ComputeHash(Encode(value ?? string.Empty));
                return ((ulong)digest[0] << 24) | ((ulong)digest[1] << 16)
                    | ((ulong)digest[2] << 8) | digest[3];
            }
        }

        private static byte[] Encode(string value)
        {
            try
            {
                return Utf8.GetBytes(value);
            }
            catch (EncoderFallbackException)
            {
                // color-hash 2.0.2 combines any surrogate with the next code unit,
                // using zero for the missing code unit at the end of a string.
                var normalized = new StringBuilder(value.Length);
                for (int index = 0; index < value.Length; index++)
                {
                    char codeUnit = value[index];
                    if (char.IsSurrogate(codeUnit))
                    {
                        int next = ++index < value.Length ? value[index] : 0;
                        int scalar = 0x10000 + (((codeUnit & 0x3FF) << 10) | (next & 0x3FF));
                        normalized.Append(char.ConvertFromUtf32(scalar));
                    }
                    else
                    {
                        normalized.Append(codeUnit);
                    }
                }

                return Utf8.GetBytes(normalized.ToString());
            }
        }
    }
}
