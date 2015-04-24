#region Copyright
/*Copyright (c) 2015, Katascope
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/
#endregion
using System;

namespace GlyphicsLibrary.Atomics
{
    //Comparisong utility class
    internal class Compare
    {
        private Compare() { }

        //MathCompare two doubles
        public static bool CompareFloatAreEqual(float v1, float v2)
        {
            const float threshold = 0.01f;
            float diff = v2 - v1;
            return (Math.Abs(diff) < threshold);
        }

        //MathCompare two doubles
        public static bool CompareDoubleAreEqual(double v1, double v2)
        {
            const double threshold = 0.01f;
            double diff = v2 - v1;
            return (Math.Abs(diff) < threshold);
        }

        //MathCompare two byte arrays, return true if equal/false if not.
        public static bool CompareBytes(byte[] result, byte[] expectedResult)
        {
            if (result.Length != expectedResult.Length) return false;

            for (int i = 0; i < result.Length;i++ )
                if (result[i] != expectedResult[i]) return false;
            return true;
        }
    }
}