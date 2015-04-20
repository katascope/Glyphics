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
    //Implementation of IPen, see for usage
    internal class CPen : IPen
    {
        public bool IsUnit { get; set; }

        public ulong Rgba { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }
        public int Depth { get; set; }

        public int StartX { get; set; }
        public int StartY { get; set; }
        public int StartZ { get; set; }
        public int StopX { get; set; }
        public int StopY { get; set; }
        public int StopZ { get; set; }

        public int HatchX { get; set; }
        public int HatchY { get; set; }
        public int HatchZ { get; set; }

        public byte ShapeByte { get; set; }
        public byte TextureByte { get; set; }
        public byte AnimByte { get; set; }
        public byte PhysicsByte { get; set; }

        public CPen() { HatchX = HatchY = HatchZ = Width = Height = Depth = 1; CalculateBorders(); }
        public void SetSize(int w, int h, int d) { Width = w; Height = h; Depth = d; CalculateBorders(); }

        public void SetHatch(int x, int y, int z) { HatchX = x; HatchY = y; HatchZ = z; CalculateBorders(); }

        public void SetColor(byte val) { Rgba = val; }
        public void SetColor(byte val1, byte val2) { Rgba = val1 | ((ulong)val2 << 8); }
        public void SetColor(byte r, byte g, byte b) { SetColor(r, g, b, ((r+g+b)==0) ? (byte)0 : (byte)255); }
        public void SetColor(byte r, byte g, byte b, byte a) { Rgba = r | ((ulong)g << 8) | ((ulong)b << 16) | ((ulong)a << 24); }
        public void SetColor(ulong val) { Rgba = val; }

        public void SetShape(byte val) { ShapeByte = val; }
        public void SetTexture(byte val){TextureByte = val;}
        public void SetAnimation(byte val){AnimByte = val;}
        public void SetPhysics(byte val){PhysicsByte = val;}

        //Calculate out the dimensions of the pen
        public void CalculateBorders()
        {
            if ((Width == 1) && (Height == 1) && (Depth == 1)) IsUnit = true;
            else IsUnit = false;

            //Get dimensions
            int sizeX = Math.Max(1, Width / 2);
            int sizeY = Math.Max(1, Height / 2);
            int sizeZ = Math.Max(1, Depth / 2);

            //Get borders
            StartX = -sizeX; StopX = sizeX;
            StartY = -sizeY; StopY = sizeY;
            StartZ = -sizeZ; StopZ = sizeZ;

            //Make sure evenly divisible by 2 are offset by 1
            if (Width % 2 == 0) StartX++;
            if (Height % 2 == 0) StartY++;
            if (Depth % 2 == 0) StartZ++;

            //Make sure unit sizes are correct
            if (Width == 1) { StartX = 0; StopX = 0; }
            if (Height == 1) { StartY = 0; StopY = 0; }
            if (Depth == 1) { StartZ = 0; StopZ = 0; }
        }

        //Readable description
        public override string ToString()
        {
            return "[" + Width + "," + Height + "," + Depth + "]";
        }
    }
}
