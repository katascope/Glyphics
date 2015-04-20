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
    //Implementation of IDouble3, see for usage
    internal class CDouble3 : IDouble3
    {
        //Actual XYZ values
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        //Default constructor
        public CDouble3() { }
        
        //Assignment constructor
        public CDouble3(double nx, double ny, double nz)
        {
            X = nx; Y = ny; Z = nz;
        }

        //Duplicate object
        public IDouble3 Clone()
        {
            return new CDouble3(X, Y, Z);
        }

        //Readable description
        public override string ToString()
        {
            return "(" + (int)X + "," + (int)Y + "," + (int)Z + ")";
        }

        //Reset XYZ to 0
        public void Identity()
        {
            X = Y = Z = 0;
        }

        //Absorb from src
        public void CopyFrom(IDouble3 src)
        {
            if (src == null) return;

            X = src.X;
            Y = src.Y;
            Z = src.Z;
        }

        //Interpolate from ptA to ptB by mux (0 to 1)
        public void Lerp(double mux, IDouble3 ptA, IDouble3 ptB)
        {
            if (ptA == null || ptB == null) return;

            X = Lerper.Lerp1D(mux, ptA.X, ptB.X);
            Y = Lerper.Lerp1D(mux, ptA.Y, ptB.Y);
            Z = Lerper.Lerp1D(mux, ptA.Z, ptB.Z);
        }

        //True if same
        public bool CompareTo(IDouble3 d)
        {
            if (  (Compare.CompareDoubleAreEqual(d.X, X) == false)
               || (Compare.CompareDoubleAreEqual(d.Y, Y) == false)
               || (Compare.CompareDoubleAreEqual(d.Z, Z) == false) )
            {
                return false;
            }
            return true;
        }
    }
}
