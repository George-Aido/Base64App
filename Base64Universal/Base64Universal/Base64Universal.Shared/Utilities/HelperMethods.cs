using System;
using System.Collections.Generic;
using System.Text;
using Windows.Security.Cryptography;
using Windows.Storage.Streams;

namespace Base64Universal
{
    public class HelperMethods
    {
        /// <summary>
        /// Encode the input parameter to base64 and return it as a string.
        /// </summary>
        /// <param name="plainText">The string parameter that needs encoding</param>
        /// <returns></returns>
        public static string Base64Encode(string plainText)
        {
            if (!string.IsNullOrEmpty(plainText))
            {
                var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
                return Convert.ToBase64String(plainTextBytes);
            }
            return string.Empty;
        }

        /// <summary>
        /// Decode the input parameter from base64 and return it as a string.
        /// </summary>
        /// <param name="base64EncodedData">The string parameter that needs decoding</param>
        /// <returns></returns>
        public static string Base64Decode(string base64EncodedData)
        {
            if (!string.IsNullOrEmpty(base64EncodedData))
            {
                var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
                return Encoding.UTF8.GetString(base64EncodedBytes, 0, base64EncodedBytes.Length);
            }
            return string.Empty;
        }


        /// <summary>
        /// Converts the input parameter from string to Hex
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns>The encoded output</returns>
        public static string ToHex(string plainText)
        {
            if (!string.IsNullOrEmpty(plainText))
            {
                var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
                // remove dashes from hex string
                return BitConverter.ToString(plainTextBytes).Replace("-", "");
            }
            return string.Empty;
        }

        /// <summary>
        /// Converts the input parameter from hex to string. Input can have dashes (20-3a-ff etc...)
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns>The decoded output</returns>
        public static string FromHex(string plainText)
        {
            // null checking and verify if valid hex
            if (!string.IsNullOrEmpty(plainText))
            {
                plainText = plainText.Replace("-", "");
                if (IsHex(plainText.ToCharArray()))
                {
                    // remeber that each byte is two digits in hex
                    byte[] plainTextBytes = new byte[plainText.Length / 2];
                    for (int i = 0; i < plainTextBytes.Length; i++)
                    {
                        // convert to byte
                        plainTextBytes[i] = Convert.ToByte(plainText.Substring(i * 2, 2), 16);
                    }
                    return Encoding.UTF8.GetString(plainTextBytes, 0, plainTextBytes.Length);
                }
            }
            return "Not valid input";
        }

        /// <summary>
        /// Checks if the input parameter is valid hex
        /// </summary>
        /// <param name="chars"></param>
        /// <returns></returns>
        private static bool IsHex(IEnumerable<char> chars)
        {
            bool isHex;
            foreach (var c in chars)
            {
                isHex = ((c >= '0' && c <= '9') ||
                         (c >= 'a' && c <= 'f') ||
                         (c >= 'A' && c <= 'F'));

                if (!isHex)
                    return false;
            }
            return true;
        }
    }
}
