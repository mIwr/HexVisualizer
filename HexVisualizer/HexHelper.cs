using System;
using System.Text.RegularExpressions;

namespace HexVisualizer
{
    public static class HexHelper
    {
        static readonly Regex regexHex = new Regex(@"\A\b(0[xX]){0,1}[0-9a-fA-F]+\b\Z", RegexOptions.Compiled); //@"\A\b(0[xX])?[0-9a-fA-F]+\b\Z"
        public static byte[] ToByteArray(this string hex)
        {
            if(hex.Length % 2 == 1)
            {
                throw new ArgumentOutOfRangeException("hex", "The hex string cannot have an odd number of digits");
            }

            if(!regexHex.IsMatch(hex))
            {
                throw new ArgumentOutOfRangeException("hex", "The hex string contains non hex values");
            }

            if (hex.Substring(0,2).ToLower() == "0x")
            {
                hex = hex.Remove(0, 2);
            }

            byte[] arr = new byte[hex.Length >> 1];

            for(int i = 0; i < hex.Length >> 1; ++i)
            {
                arr[i] = (byte)((GetHexVal(hex[i << 1]) << 4) + (GetHexVal(hex[(i << 1) + 1])));
            }

            return arr;
        }

        public static string ToHexString(this byte[] data)
        {
            return BitConverter.ToString(data);
        }

        private static int GetHexVal(char hex)
        {
            var val = (int)hex;
            //For uppercase A-F letters:
            //return val - (val < 58 ? 48 : 55);

            //For lowercase a-f letters:
            //return val - (val < 58 ? 48 : 87);

            //Or the two combined, but a bit slower:
            return val - (val < 58 ? 48 : (val < 97 ? 55 : 87));
        }

    }
}
