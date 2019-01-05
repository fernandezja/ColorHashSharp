using System;
using System.Collections.Generic;
using System.Text;

namespace Fernandezja.ColorHashSharp
{
    public class Utilities
    {
        internal protected string StringToNumber(string value)
        {
            var result = new StringBuilder();
            for (var i = 0; i < value.Length; i++)
            {
                result.Append(((int)value[i]).ToString());
            }
            return result.ToString();
        }

    }
}
