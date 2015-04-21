#region Copyright
/*Copyright (c) 2015, Katascope
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/
#endregion

namespace GlyphicsLibrary
{
    //Interface for the main Grid class
    public interface IGrid
    {
        //Dimensions XYZ
        int SizeX { get; }
        int SizeY { get; }
        int SizeZ { get; }

        //Bits-Per-Pixel
        int Bpp { get; }
        
        //Total size
        int Size { get; }
        
        //Duplicate Object
        IGrid Clone();
        
        //Plot an rgba to xyz
        void Plot(int x, int y, int z, ulong b);

        //Plot properties to xyz
        void Plot(int x, int y, int z, ulong b, byte physics, byte shape, byte texture, byte anim);

        //Get the RGBA value at xyz
        ulong GetRgba(int x, int y, int z);

        //Get the property at xyz
        ICellProperties GetProperty(int x, int y, int z);

        //Create a duplicate of the raw bytes of the grid
        byte[] CloneData();

        //Copy the values from src to local
        void CopyFrom(IGrid src);

        //Set the rectangle for scissoring
        void SetScissor(IRect nScissor);

        //Disable scissoring
        void NoScissor();

        //Set the value for tracking code lines
        void SetTracker(byte tracker);

        //Disable code line tracking
        void InhibitCodeTracking();

        //Enable code line tracking
        void AllowCodeTracking();

        //True if xyz is in range of the grid's boundaries
        bool InRange(int x, int y, int z);

        //Return true if the raw bytes = expectedResult bytes
        bool CompareBytes(byte[] expectedResult);

        //Return true if the raw bytes = expectedResult bytes
        bool IsEqualTo(IGrid grid);
        
        //Returns the count of non-zero cells
        ulong CountNonZero();
    }
}
