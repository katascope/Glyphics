#region Copyright
/*Copyright (c) 2015, Katascope
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/
#endregion
using System.Collections.Generic;

namespace GlyphicsLibrary.Atomics
{
    //Utility to assist serialization and console display output
    internal class Transcode64
    {
        private Transcode64() { }

        private const string Lookup64 = @"0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ!@";
//        private static string notused = "`~[]{}|;:,./<>?-=_+\\\'\")#$%^&*(";
        public static string To64(int val)
        {
            return "" + Lookup64[val & 63];
        }
        public static byte From64(char val)
        {
            var index = (byte)Lookup64.IndexOf(val);
            return index;
        }

        //Rects
        public static string SerializeRectPointsToString(IRect rect, int inclusiveOffset)
        {
            string result = "";
            RectMath.MinMax(ref rect);
            result += To64((int)rect.Pt1.X);
            result += To64((int)rect.Pt1.Y);
            result += To64((int)rect.Pt1.Z);
            result += To64((int)rect.Pt2.X - inclusiveOffset);
            result += To64((int)rect.Pt2.Y - inclusiveOffset);
            result += To64((int)rect.Pt2.Z - inclusiveOffset);
            return result;
        }

        //Convert a hexadecimal character to byte value
        private static byte HexCharToByte(char v)
        {
            switch (v)
            {
                case '0': return 0;
                case '1': return 1;
                case '2': return 2;
                case '3': return 3;
                case '4': return 4;
                case '5': return 5;
                case '6': return 6;
                case '7': return 7;
                case '8': return 8;
                case '9': return 9;
                case 'a':
                case 'A': return 10;
                case 'b':
                case 'B': return 11;
                case 'c':
                case 'C': return 12;
                case 'd':
                case 'D': return 13;
                case 'e':
                case 'E': return 14;
                case 'f':
                case 'F': return 15;
            }
            return 0;
        }

        //Convert a string of hexadecimal data to a byte array
        public static byte[] HexDataToBytes(string data)
        {
            var bytes = new List<byte>();
            for (int i = 0; i < data.Length; i += 2)
            {
                int v1 = HexCharToByte(data[i]) * 16;
                v1 += HexCharToByte(data[i + 1]);
                bytes.Add((byte)v1);
            }
            return bytes.ToArray();
        }

        //Convert one ulong to 4 r/g/b/a 4BPP(32-bit) 
        public static void RecodeUlongtoRgba(ulong u, out byte r, out byte g, out byte b, out byte a)
        {
            Pixel.Ulong2Rgba(u, out r, out g, out b, out a);
            r = (byte)(r >> 2);
            g = (byte)(g >> 2);
            b = (byte)(b >> 2);
            a = (byte)(a >> 2);
        }

        //Convert 4 r/g/b/a 4BPP(32-bit) to one ulong value
        public static ulong RecodeRgbatoUlong(byte r, byte g, byte b, byte a)
        {
            r = (byte)(r << 2 | 7);
            g = (byte)(g << 2 | 7);
            b = (byte)(b << 2 | 7);
            a = (byte)(a << 2 | 7);
            return Pixel.Rgba2Ulong(r, g, b, a);
        }
    }
}
