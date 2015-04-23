#region Copyright
/*Copyright (c) 2015, Katascope
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/
#endregion
using GlyphicsLibrary.Atomics;

namespace GlyphicsLibrary.Painters
{
    internal partial class CPainter
    {
        //Color edges an RGBA
        public void EdgeColor(IGridContext bgc, int axis, byte ri, byte gi, byte bi, byte ai)
        {
            if (bgc == null) return;
            IGrid grid = bgc.Grid;
            //Inhibit because it would affect ALL drawn pixels
            grid.InhibitCodeTracking();
            for (int z = 0; z < grid.SizeZ; z++)
            {
                for (int y = 0; y < grid.SizeY; y++)
                {
                    for (int x = 0; x < grid.SizeX; x++)
                    {
                        ulong u = grid.GetRgba(x, y, z);
                        if (u > 0)
                        {
                            if (axis == 0)
                                if (((grid.GetRgba(x + 1, y, z) == 0) && (grid.GetRgba(x - 1, y, z) != 0)) || ((grid.GetRgba(x + 1, y, z) != 0) && (grid.GetRgba(x - 1, y, z) == 0)))
                                    u = Converter.Rgba2Ulong(ri, gi, bi, ai);
                            if (axis == 1)
                                if (((grid.GetRgba(x, y + 1, z) == 0) && (grid.GetRgba(x, y - 1, z) != 0)) || ((grid.GetRgba(x, y + 1, z) != 0) && (grid.GetRgba(x, y - 1, z) == 0)))
                                    u = Converter.Rgba2Ulong(ri, gi, bi, ai);
                            if (axis == 2)
                                if (((grid.GetRgba(x, y, z + 1) == 0) && (grid.GetRgba(x, y, z - 1) != 0)) || ((grid.GetRgba(x, y, z + 1) != 0) && (grid.GetRgba(x, y, z - 1) == 0)))
                                    u = Converter.Rgba2Ulong(ri, gi, bi, ai);
                            grid.Plot(x, y, z, u, 0, (byte)grid.GetProperty(x, y, z).ShapeId, (byte)grid.GetProperty(x, y, z).TextureId, 0);//bgc.Pen.shape_byte, bgc.Pen.texture_byte);
                        }
                    }
                }
            }
            grid.AllowCodeTracking();
        }

        //Add rgba blocks above edge neighbors
        public void UpV(IGridContext bgc, int reps, byte ri, byte gi, byte bi, byte ai)
        {
            if (bgc == null) return;
            IGrid grid = bgc.Grid;

            grid.InhibitCodeTracking();
            for (int z = 0; z < grid.SizeZ; z++)
            {
                for (int y = grid.SizeY - 1; y >= 0; y--)
                {
                    for (int x = 0; x < grid.SizeX; x++)
                    {
                        ulong u = grid.GetRgba(x, y, z);
                        ulong rgba = Converter.Rgba2Ulong(ri, gi, bi, ai);
                        if (u != 0)
                        {
                            int foo;
                            foo = 1;
                        }
                        if (rgba == u)
                        {
                            for (int i = 0; i < reps; i++)
                            {
                                if (grid.GetRgba(x, y + i, z) == 0)
                                    grid.Plot(x, y + i, z, u);
                            }
                        }
                    }
                }
            }
            grid.AllowCodeTracking();
        }
    }
}
