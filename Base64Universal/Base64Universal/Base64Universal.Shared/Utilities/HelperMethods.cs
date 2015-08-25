using System;
using System.Collections.Generic;
using System.Text;
using Windows.Security.Cryptography;
using Windows.Storage.Streams;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Linq;
using System.Globalization;

namespace Base64Universal.Utilities
{
    public class HelperMethods
    {
        #region base64
        /// <summary>
        /// Encode the input parameter to base64 and return it as a string.
        /// </summary>
        /// <param name="plainText">The string parameter that needs encoding</param>
        /// <returns></returns>
        public static string ToBase64(string plainText)
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
        public static string FromBase64(string base64EncodedData)
        {
            if (!string.IsNullOrEmpty(base64EncodedData))
            {
                var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
                return Encoding.UTF8.GetString(base64EncodedBytes, 0, base64EncodedBytes.Length);
            }
            return string.Empty;
        }
        #endregion

        #region hex Text
        /// <summary>
        /// Converts the input parameter from string to Hex. Use this for simple text.
        /// For numbers use <see cref="StringNumberToHex(string)"/>
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns>The encoded output</returns>
        public static string StringTextToHex(string plainText)
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
        /// Converts the input parameter from hex to string. Use this for simple text.
        /// For numbers use <see cref="StringNumberToHex(string)"/>
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns>The decoded output</returns>
        public static string StringTextFromHex(string plainText)
        {
            // null checking and verify if valid hex
            if (!string.IsNullOrEmpty(plainText))
            {
                //plainText = plainText.Replace("-", "");
                if (plainText.IsHex())
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

        #endregion

        #region hex Number

        /// <summary>
        /// Converts the input parameter from string number to Hex number.
        /// For simple text <see cref="StringTextToHex(string)"/>
        /// </summary>
        /// <param name="plainText">If string is a number its int value must not be longer than <see cref="long"/></param>
        /// <returns>The hex output</returns>
        public static string StringNumberToHex(string plainText)
        {
            if (!string.IsNullOrEmpty(plainText) && plainText.IsDigit())
            {
                long result;
                if (Int64.TryParse(plainText, out result))
                    return result.ToString("X");
            }
            return string.Empty;
        }

        /// <summary>
        /// Converts the input parameter from string number to Hex number.
        /// For simple text <see cref="StringTextToHex(string)"/>
        /// </summary>
        /// <param name="hexText">If string is a number its int value must not be longer than <see cref="long"/></param>
        /// <returns>The decimal output</returns>
        public static string StringNumberFromHex(string hexText)
        {
            if (!string.IsNullOrEmpty(hexText) && hexText.IsHex())
            {
                long result;
                if (Int64.TryParse(hexText, System.Globalization.NumberStyles.HexNumber, new CultureInfo("en-US"), out result))
                    return result.ToString();
            }
            return string.Empty;
        }

        #endregion
    }
}
