using System;
using System.Collections.Generic;
using System.Text;

namespace Base64Universal.Utilities
{
    public static class Extensions
    {
        /// <summary>
        /// Extension method to check if the specified char is in Hex format
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsHex(this char c)
        {
            return ((c >= '0' && c <= '9') ||
                     (c >= 'a' && c <= 'f') ||
                     (c >= 'A' && c <= 'F'));
        }
    }
}
