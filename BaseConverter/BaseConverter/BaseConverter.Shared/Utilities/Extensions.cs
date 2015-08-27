using System;
using System.Collections.Generic;
using System.Text;

namespace BaseConverter.Utilities
{
    public static class Extensions
    {
        /// <summary>
        /// Extension method to check if the specified string is in Hex format
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsHex(this string str)
        {
            foreach (char c in str)
            {
                if (!((c >= '0' && c <= '9') ||
                     (c >= 'a' && c <= 'f') ||
                     (c >= 'A' && c <= 'F')))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Extension method to check if the specified string contains numbers only
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsDigit(this string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }
    }
}
