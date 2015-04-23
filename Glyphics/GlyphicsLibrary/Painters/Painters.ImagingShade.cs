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
        //Shade all pixels in Grid
        public void Shade(IGridContext bgc, int axis, byte r1, byte g1, byte b1, byte r2, byte g2, byte b2)
        {
            if (bgc == null) return;

            IGrid grid = bgc.Grid;
            for (int z = 0; z < grid.SizeZ; z++)
            {
                for (int y = 0; y < grid.SizeY; y++)
                {
                    for (int x = 0; x < grid.SizeX; x++)
                    {
                        double factor = 0.0f;
                        if (axis == 0) factor = (double)x / grid.SizeX;
                        else if (axis == 1) factor = (double)y / grid.SizeY;
                        else if (axis == 2) factor = (double)z / grid.SizeZ;

                        ulong u = grid.GetRgba(x, y, z);
                        if (u > 0)
                        {
                            byte r, g, b, a;
                            Converter.Ulong2Rgba(u, out r, out g, out b, out a);
                            r = (byte)((Lerper.Lerp1D(factor, r1, r2)) / 1);
                            g = (byte)((Lerper.Lerp1D(factor, g1, g2)) / 1);
                            b = (byte)((Lerper.Lerp1D(factor, b1, b2)) / 1);
                            u = Converter.Rgba2Ulong(r, g, b, a);
                        }

                        grid.Plot(x, y, z, u, 0, (byte)grid.GetProperty(x, y, z).ShapeId, (byte)grid.GetProperty(x, y, z).TextureId, 0);
                    }
                }
            }
        }
    }
}
