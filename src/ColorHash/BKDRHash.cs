using System;
using System.Diagnostics;
using System.Numerics;
using System.Text;

namespace Fernandezja.ColorHashSharp
{
    public class BKDRHash
    {
        private const char PADDING_CHAR = 'x';

        /// <summary>
        /// The Number.MAX_SAFE_INTEGER constant represents the maximum safe integer in JavaScript  
        /// https://developer.mozilla.org/es/docs/Web/JavaScript/Referencia/Objetos_globales/Number/MAX_SAFE_INTEGER
        /// </summary>
        private const ulong JAVASCRIPT_MAX_SAFE_INTEGER = 9007199254740991;

        private const ulong SEED = 131;

        private const ulong SEED2 = 137;


        public ulong Generate(string value) {
            return GenerateVersion3(value);
        }



        public ulong GenerateVersion2(string value)
        {
            long hash = 0;

            // Make hash more sensitive for short string like 'a', 'b', 'c'
            var valueWithPadding = $"{value}{PADDING_CHAR}";

            var valueUtf8 = ToUTF8(valueWithPadding);

            var max = (Int64.MaxValue / (long)SEED);

            for (int i = 0; i < valueUtf8.Length; i++)
            {
                if (hash > max)
                {
                    Debug.WriteLine($"hash > max");
                    Debug.WriteLine($"  hash = {hash}");
                    Debug.WriteLine($"  max = {max} >> ToUInt64(max) = {Convert.ToUInt64(max)}");
                    Debug.WriteLine($"  seed2 = {SEED2}");

                    hash = (hash / (long)SEED2);

                    Debug.WriteLine($"  new hash = {hash}");
                    Debug.WriteLine($" ");
                }

                var bytes = ToUTF8Bytes(valueUtf8[i].ToString());

                Debug.WriteLine($"{valueUtf8[i].ToString()} > {bytes[0]}");

                hash = (hash * (long)SEED) + bytes[0];

                Debug.WriteLine($"{valueUtf8[i].ToString()} > {bytes[0]} > hash = {hash}");
            }

            return (ulong)hash;
        }



        /// <summary>
        /// BKDR Hash (modified version). Idem  original code.
        /// https://github.com/zenozeng/color-hash/blob/master/lib/bkdr-hash.js
        /// Example values nodejs https://repl.it/@Jose_AA/BKDR-Hash
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ulong GenerateVersion3(string value)
        {
            ulong hash = 0;

            // Make hash more sensitive for short string like 'a', 'b', 'c'
            var valueWithPadding = $"{value}{PADDING_CHAR}";

            var valueUtf8 = ToUTF8(valueWithPadding);
            
            var max = (JAVASCRIPT_MAX_SAFE_INTEGER / SEED);

            for (int i = 0; i < valueUtf8.Length; i++)
            {
                if (hash > max)
                {
                    Debug.WriteLine($"hash > max");
                    Debug.WriteLine($"  hash = {hash}");
                    Debug.WriteLine($"  max = {max} >> ToUInt64(max) = {Convert.ToUInt64(max)}");
                    Debug.WriteLine($"  seed2 = {SEED2}");

                    hash = (hash / SEED2);

                    Debug.WriteLine($"  new hash = {hash}");
                    Debug.WriteLine($" ");
                }

                var bytes = ToUTF8Bytes(valueUtf8[i].ToString());

                Debug.WriteLine($"{valueUtf8[i].ToString()} > {bytes[0]}");

                hash = (hash * SEED) + bytes[0];

                Debug.WriteLine($"{valueUtf8[i].ToString()} > {bytes[0]} > hash = {hash}");
            }

            return hash;
        }

        private string ToUTF8(string value)
        {
            var bytes = Encoding.Default.GetBytes(value);
            return Encoding.UTF8.GetString(bytes);
        }

        private byte[] ToUTF8Bytes(string value)
        {
            var bytes = Encoding.UTF8.GetBytes(value);
            return bytes;
        }
        


    }
}
