#region Copyright
/*Copyright (c) 2015, Katascope
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/
#endregion

namespace GlyphicsLibrary.Atomics
{
    internal partial class Converter
    {        
        //Convert r/g/b/a to a ulong
        public static ulong Rgba2Ulong(byte r, byte g, byte b, byte a)
        {
            ulong val = 0;
            val |= ((ulong)r << 0);
            val |= ((ulong)g << 8);
            val |= ((ulong)b << 16);
            val |= ((ulong)a << 24);
            return val;
        }

        //Convert a ulong to r/g/b/a
        public static void Ulong2Rgba(ulong val, out byte r, out byte g, out byte b, out byte a)
        {
            r = (byte)((val >> 0) & 255);
            g = (byte)((val >> 8) & 255);
            b = (byte)((val >> 16) & 255);
            a = (byte)((val >> 24) & 255);
        }

        //Set just the alpha component of a ulong RGBA value
        public static ulong SetAlpha(ulong val, byte a)
        {
            val &= 0xFFFFFF;
            val |= ((ulong)a << 24);
            return val;
        }
    }
}
