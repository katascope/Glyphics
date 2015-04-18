#region Copyright
/*Copyright (c) 2015, Katascope
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/
#endregion
using System;

namespace GlyphicsLibrary.Painters
{
    //Bitmasks for defining faces
    [Flags]
    enum CubeFaceMask
    {
        Bottom = 1,
        Top = 2,
        Front = 4,
        Back = 8,
        Right = 16,
        Left = 32,
    }

    internal partial class CPainter
    {
        //Draw a facemask-mitigated filled rect
        public void DrawMaskFillRect(IByteGridContext bgc, int x1, int y1, int z1, int x2, int y2, int z2, byte bitmask)
        {
            if ((bitmask & (int)CubeFaceMask.Left) != 0)   DrawFillRect(bgc, x1, y1, z1, x1, y2, z2);
            if ((bitmask & (int)CubeFaceMask.Right) != 0)  DrawFillRect(bgc, x2, y1, z1, x2, y2, z2);
            if ((bitmask & (int)CubeFaceMask.Back) != 0)   DrawFillRect(bgc, x1, y1, z1, x2, y2, z1);
            if ((bitmask & (int)CubeFaceMask.Front) != 0)  DrawFillRect(bgc, x1, y1, z2, x2, y2, z2);
            if ((bitmask & (int)CubeFaceMask.Top) != 0)    DrawFillRect(bgc, x1, y2, z1, x2, y2, z2);
            if ((bitmask & (int)CubeFaceMask.Bottom) != 0) DrawFillRect(bgc, x1, y1, z1, x2, y1, z2);
        }

        //Draw a facemask-mitigated hollow rect
        public void DrawMaskHollowRect(IByteGridContext bgc, int x1, int y1, int z1, int x2, int y2, int z2, byte bitmask)
        {
            if ((bitmask & (int)CubeFaceMask.Left) != 0) DrawHollowRect(bgc, x1, y1, z1, x1, y2, z2);
            if ((bitmask & (int)CubeFaceMask.Right) != 0) DrawHollowRect(bgc, x2, y1, z1, x2, y2, z2);
            if ((bitmask & (int)CubeFaceMask.Back) != 0) DrawHollowRect(bgc, x1, y1, z1, x2, y2, z1);
            if ((bitmask & (int)CubeFaceMask.Front) != 0) DrawHollowRect(bgc, x1, y1, z2, x2, y2, z2);
            if ((bitmask & (int)CubeFaceMask.Top) != 0) DrawHollowRect(bgc, x1, y2, z1, x2, y2, z2);
            if ((bitmask & (int)CubeFaceMask.Bottom) != 0) DrawHollowRect(bgc, x1, y1, z1, x2, y1, z2);
        }
    }
}
