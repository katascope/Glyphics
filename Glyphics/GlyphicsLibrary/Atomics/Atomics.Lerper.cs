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
    //Interpolation utility class, a to b gradations by mux 0=fully a, 1=fully b
    internal static class Lerper
    {
        //Interpolate two BYTE
        public static byte Lerp1D(double mux, byte a, byte b)
        {
            return (byte)(a * (1.0 - mux) + b * mux);
        }
        
        //Interpolate two DOUBLE
        public static double Lerp1D(double mux, double v1, double v2)
        {
            return ((1 - mux) * v1 + mux * v2);
        }
        
        //Interpolate two INT
        public static int Lerp1D(double mux, int a, int b)
        {
            return (int)(a * (1.0 - mux) + b * mux);
        }
        
        //Treshold instead of interpolate two INT
        public static int ThresholdAb(double mux, int a, int b)
        {
            return (mux < 0.5) ? a : b;
        }

        //Interpolate two RGBA values encoded as ulongs
        public static ulong LerpRgba(double mux, ulong v1, ulong v2)
        {
            byte r1, r2;
            byte g1, g2;
            byte b1, b2;
            byte a1, a2;
            Converter.Ulong2Rgba(v1, out r1, out g1, out b1, out a1);
            Converter.Ulong2Rgba(v2, out r2, out g2, out b2, out a2);

            byte r = Lerp1D(mux, r1, r2);
            byte g = Lerp1D(mux, g1, g2);
            byte b = Lerp1D(mux, b1, b2);
            byte a = Lerp1D(mux, a1, a2);

            return Converter.Rgba2Ulong(r, g, b, a);
        }
    }
}
