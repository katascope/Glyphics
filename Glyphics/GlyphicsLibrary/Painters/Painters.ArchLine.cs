#region Copyright
/*Copyright (c) 2015, Katascope
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/
#endregion
using System;
using GlyphicsLibrary.ByteGrid;

namespace GlyphicsLibrary.Painters
{
    internal partial class CPainter
    {
        //Copy an archetype into context
        public void CopyInto(IByteGridContext bgc, int x, int y, int z, int archtype)
        {
            IGrid grid = ArchetypeLibrary.GridList.GetEntry(archtype);
            if (grid == null) return;
            CopyInto(bgc, x, y, z, grid);
        }

        //Copy a grid into context
        private static void CopyInto(IByteGridContext bgc, int x, int y, int z, IGrid grid)
        {
            if (bgc == null || grid == null) return;

            int width = grid.SizeX;
            int height = grid.SizeY;
            int depth = grid.SizeZ;

            IPainter painter = new CPainter();
            painter.RectBlit(bgc, grid, x, y, z, x + width, y + height, z + depth);
        }

        //Draw Archetype (shape) to Grid line
        public void ArchLine(IByteGridContext bgc, int x1, int y1, int z1, int x2, int y2, int z2, int shape, int rotation, int spacing)
        {
            int xd, yd, zd;
            int dx = x2 - x1;
            int dy = y2 - y1;
            int dz = z2 - z1;
            int ax = Math.Abs(dx) << 1;
            int ay = Math.Abs(dy) << 1;
            int az = Math.Abs(dz) << 1;
            int sx = Zsgn(dx);
            int sy = Zsgn(dy);
            int sz = Zsgn(dz);
            int x = x1;
            int y = y1;
            int z = z1;
            int count = 0;
            bool firstTime = true;

            IGrid grid = ArchetypeLibrary.GridList.GetEntry(shape);
            if (grid == null)
                return;

            if (ax >= Math.Max(ay, az))            /* x dominant */
            {
                yd = ay - (ax >> 1); zd = az - (ax >> 1);
                for (; ; )
                {
                    if ((++count > spacing) || (firstTime))
                    {
                        CopyInto(bgc, x, y, z, grid);
                        count = 0;
                        firstTime = false;
                    }

                    if (x == x2) return;

                    if (yd >= 0) { y += sy; yd -= ax; }
                    if (zd >= 0) { z += sz; zd -= ax; }
                    x += sx; yd += ay; zd += az;
                }
            }
            if (ay >= Math.Max(ax, az))            /* y dominant */
            {
                xd = ax - (ay >> 1); zd = az - (ay >> 1);
                for (; ; )
                {
                    if ((++count > spacing) || (firstTime))
                    {
                        CopyInto(bgc, x, y, z, grid);
                        count = 0;
                        firstTime = false;
                    }
                    if (y == y2) return;

                    if (xd >= 0) { x += sx; xd -= ay; }
                    if (zd >= 0) { z += sz; zd -= ay; }
                    y += sy; xd += ax; zd += az;
                }
            }
            if (az >= Math.Max(ax, ay))            /* z dominant */
            {
                xd = ax - (az >> 1); yd = ay - (az >> 1);
                for (; ; )
                {
                    if ((++count > spacing) || (firstTime))
                    {
                        CopyInto(bgc, x, y, z, grid);
                        count = 0;
                        firstTime = false;
                    }
                    if (z == z2) return;

                    if (xd >= 0) { x += sx; xd -= az; }
                    if (yd >= 0) { y += sy; yd -= az; }
                    z += sz; xd += ax; yd += ay;
                }
            }
        }
    }
}
