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
    //Utility class for Rectangle math
    internal class RectMath
    {
        private RectMath() { }

        //Order v1,v2 by magnitude
        public static void MinMax(ref double v1, ref double v2)
        { 
            //if v1 <= v2, no problem. If v1 > v2, then swap them
            if (v1 > v2)
            {
                //Just swap them, no fancy tricks
                double temp = v1; v1 = v2; v2 = temp;
            }
        }

        //Order the points in a rectangle by magnitude
        public static void MinMax(ref IRect rect)
        {
            IDouble3 pt1 = rect.Pt1;
            IDouble3 pt2 = rect.Pt2;
            
            double v1 = pt1.X;
            double v2 = pt2.X; 
            MinMax(ref v1, ref v2); pt1.X = v1; pt2.X = v2;
            v1 = pt1.Y; v2 = pt2.Y; MinMax(ref v1, ref v2); pt1.Y = v1; pt2.Y = v2;
            v1 = pt1.Z; v2 = pt2.Z; MinMax(ref v1, ref v2); pt1.Z = v1; pt2.Z = v2;
            rect.Pt1 = pt1;
            rect.Pt2 = pt2;
        }

        //GetToken the smallest power of 2 that will fit V
        public static int SmallestPowerOfN(int v)
        {
            //Find smallest power of 2 max fits in
            int m = 0;
            if (v <= 1) m = 1;
            else if (v <= 2) m = 2;
            else if (v <= 4) m = 4;
            else if (v <= 8) m = 8;
            else if (v <= 16) m = 16;
            else if (v <= 32) m = 32;
            else if (v <= 64) m = 64;
            else if (v <= 128) m = 128;
            else if (v <= 256) m = 256;
            return m;
        }
    }
}
