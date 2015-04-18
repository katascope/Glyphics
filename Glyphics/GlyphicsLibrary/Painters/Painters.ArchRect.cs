#region Copyright
/*Copyright (c) 2015, Katascope
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/
#endregion

using GlyphicsLibrary.ByteGrid;

namespace GlyphicsLibrary.Painters
{
    internal partial class CPainter
    {
        //Draw archetype (shape) to Grid rectangle
        public void ArchRect(IByteGridContext bgc, int x1, int y1, int z1, int x2, int y2, int z2, int shape, int rotation, int spacing)
        {
            MinMax(ref x1, ref x2);
            MinMax(ref y1, ref y2);
            MinMax(ref z1, ref z2);

            IGrid grid = ArchetypeLibrary.GridList.GetEntry(shape);
            if (grid == null)
                return;

            int spacingX = grid.SizeX - 1;
            int spacingY = grid.SizeY - 1;
            int spacingZ = grid.SizeZ - 1;

            for (int z = z1; z < z2; z++)
            {
                for (int y = y1; y < y2; y++)
                {
                    for (int x = x1; x < x2; x++)
                    {
                        if (((x - x1) % spacingX == 0) && ((y - y1) % spacingY == 0) && ((z - z1) % spacingZ == 0))
                            CopyInto(bgc, x, y, z, grid);
                    }
                }
            }
        }
    }
}
