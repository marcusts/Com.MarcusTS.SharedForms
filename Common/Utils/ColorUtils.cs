// *********************************************************************************
// Copyright @2021 Marcus Technical Services, Inc.
// <copyright
// file=ColorUtils.cs
// company="Marcus Technical Services, Inc.">
// </copyright>
// 
// MIT License
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// *********************************************************************************

namespace Com.MarcusTS.SharedForms.Common.Utils
{
    using System.Globalization;
    using System.Linq;
    using Xamarin.Forms;

    public static class ColorUtils
    {
        private const           int   ARGB_LENGTH             = 8;
        private const           int   HEX_FRAGMENT_LEN        = 2;
        private const           char  HEX_PREFIX              = '#';
        private const           int   MAX_ARGB_BYTES          = 4;
        private const           int   RGB_LENGTH              = 6;
        private static readonly int[] LEGAL_ARG_PARSE_LENGTHS = { RGB_LENGTH, ARGB_LENGTH };

        public static bool CanBeParsedFromHex(this string hex, out Color retColor)
        {
            // Not an actual result; default can be anything
            retColor = Color.Default;
            hex      = hex.TrimStart(HEX_PREFIX);

            if (!LEGAL_ARG_PARSE_LENGTHS.Contains(hex.Length))
            {
                return false;
            }

            var argbBytes     = new byte[MAX_ARGB_BYTES];
            var nextArgbIndex = 0;

            for (var parseIdx = 0; parseIdx < hex.Length; parseIdx += HEX_FRAGMENT_LEN)
            {
                if (byte.TryParse(hex.Substring(parseIdx, HEX_FRAGMENT_LEN), NumberStyles.HexNumber, default,
                    out var hexByte))
                {
                    // Insert and increment
                    argbBytes[nextArgbIndex++] = hexByte;
                }
                else
                {
                    return false;
                }
            }

            if (hex.Length == RGB_LENGTH)
            {
                // Hard-code opaque as the last arg value
                argbBytes[MAX_ARGB_BYTES - 1] = byte.MaxValue;
            }

            retColor = Color.FromRgba(argbBytes[0], argbBytes[1], argbBytes[2], argbBytes[3]);

            return true;
        }
    }
}