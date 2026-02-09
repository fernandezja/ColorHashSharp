using System;

namespace Fernandezja.ColorHashSharp.Interfaces
{
    /// <summary>
    /// Interface for custom hash functions
    /// </summary>
    public interface IHashFunction
    {
        /// <summary>
        /// Generate hash from string value
        /// </summary>
        /// <param name="value">Input string</param>
        /// <returns>Hash as unsigned long</returns>
        ulong Generate(string value);
    }
}
