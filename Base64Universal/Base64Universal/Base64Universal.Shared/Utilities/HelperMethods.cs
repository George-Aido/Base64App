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
            return "Not valid input";
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
            return "Not valid input";
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
            return "Not valid input";
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

        #region Number
        /// <summary>
        /// Converts the input parameter from string number to decimal number.
        /// </summary>
        /// <param name="plainText">If string is a number its int value must not be longer than <see cref="long"/></param>
        /// <param name="fromBase">The base of the input value, which must be 2, 8, 10, or 16.</param>
        /// <param name="toBase">The base of the return value, which must be 2, 8, 10, or 16.</param>
        /// <returns>The decimal output</returns>
        public static string StringNumberFromBaseToBase(string plainText, int fromBase, int toBase)
        {
            if (!string.IsNullOrEmpty(plainText) && (plainText.IsDigit() || plainText.IsHex()))
            {
                try
                {
                    long binaryNumber = Convert.ToInt64(plainText, fromBase);
                    return Convert.ToString(binaryNumber, toBase);
                }
                catch (Exception)
                {
                    //Do nothing
                }
            }
            return "Not valid input";
        }
        #endregion
    }
}
