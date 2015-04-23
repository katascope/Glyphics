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
        //Rotate Grid on X
        public void RotateX(IGridContext bgc)
        {
            if (bgc == null) return;

            //Unit size check
            if (bgc.Grid.SizeY != bgc.Grid.SizeZ) return;

            var grid2 = new CGrid(bgc.Grid.SizeX, bgc.Grid.SizeY, bgc.Grid.SizeZ, bgc.Grid.Bpp);
            grid2.CopyFrom(bgc.Grid);

            for (int z = 0; z < bgc.Grid.SizeZ; z++)
            {
                for (int y = 0; y < bgc.Grid.SizeY; y++)
                {
                    for (int x = 0; x < bgc.Grid.SizeX; x++)
                    {
                        ulong u = bgc.Grid.GetRgba(x, y, z);
                        grid2.Plot(x, z, y, u);
                    }
                }
            }
            bgc.Grid.CopyFrom(grid2);
        }

        //Rotate Grid on Y
        public void RotateY(IGridContext bgc)
        {
            if (bgc == null) return;

            //Unit size check
            if (bgc.Grid.SizeX != bgc.Grid.SizeZ) return;

            var grid2 = new CGrid(bgc.Grid.SizeX, bgc.Grid.SizeY, bgc.Grid.SizeZ, bgc.Grid.Bpp);
            grid2.CopyFrom(bgc.Grid);

            for (int z = 0; z < bgc.Grid.SizeZ; z++)
            {
                for (int y = 0; y < bgc.Grid.SizeY; y++)
                {
                    for (int x = 0; x < bgc.Grid.SizeX; x++)
                    {
                        ulong u = bgc.Grid.GetRgba(x, y, z);
                        grid2.Plot(z, y, x, u);
                    }
                }
            }
            bgc.Grid.CopyFrom(grid2);
        }

        //Rotate Grid on Z
        public void RotateZ(IGridContext bgc)
        {
            if (bgc == null) return;

            //Unit size check
            if (bgc.Grid.SizeX != bgc.Grid.SizeY) return;

            var grid2 = new CGrid(bgc.Grid.SizeX, bgc.Grid.SizeY, bgc.Grid.SizeZ, bgc.Grid.Bpp);
            //grid2.CopyFrom(bgc.Grid);

            for (int z = 0; z < bgc.Grid.SizeZ; z++)
            {
                for (int y = 0; y < bgc.Grid.SizeY; y++)
                {
                    for (int x = 0; x < bgc.Grid.SizeX; x++)
                    {
                        ulong u = bgc.Grid.GetRgba(x, y, z);
                        grid2.Plot(y, x, z, u);
                    }
                }
            }
            bgc.Grid.CopyFrom(grid2);
        }
   }
}