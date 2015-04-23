#region Copyright
/*Copyright (c) 2015, Katascope
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/
#endregion
using System;
using GlyphicsLibrary.Atomics;

namespace GlyphicsLibrary.Painters
{
    internal partial class CPainter
    {
        //Draw an arbitrary shape at XYZ
        public void DrawShape(IGridContext bgc, PenTwist twistType, int type, int x, int y, int z, int scale)
        {
            switch (type)
            {
                case 0: DrawPen(bgc, x, y, z); break;
                case 1: DrawLine2D(bgc, x - scale, y - scale, x + scale, y + scale, z); break;
                case 2: DrawFillRect(bgc, x - scale, y - scale, z - scale, x + scale, y + scale, z + scale); break;
                case 3: DrawPolygon(bgc, twistType, x, y, z, scale, 3); break;
                case 4: DrawPolygon(bgc, twistType, x, y, z, scale, 4); break;
                case 5: DrawPolygon(bgc, twistType, x, y, z, scale, 5); break;
                case 6: DrawPolygon(bgc, twistType, x, y, z, scale, 6); break;
                case 7: DrawPolygon(bgc, twistType, x, y, z, scale, 7); break;
                case 8: DrawPolygon(bgc, twistType, x, y, z, scale, 8); break;
                case 9: DrawCircle2DAnyAxis(bgc, twistType, x, y, z, scale); break;
            }
        }

        //Extrude shape along X-axis
        public void ExtrudeX(IGridContext bgc, int startX, int startY, int startZ, int stopX, int shape, int startScale, int stopScale, int skips)
        {
            for (int x=startX;x<stopX;x++)
            {
                double mux = (double)(x-startX)/(stopX-startX);
                int scale = Lerper.Lerp1D(mux, startScale, stopScale);
                if (startScale == stopScale) scale = startScale;
                DrawShape(bgc, PenTwist.YZaxis, shape, startY, startZ, x, scale);
            }
        }

        //Extrude shape along Y-axis
        public void ExtrudeY(IGridContext bgc, int startX, int startY, int startZ, int stopY, int shape, int startScale, int stopScale, int skips)
        {
            for (int y = startY; y < stopY; y++)
            {
                double mux = (double)(y - startY) / (stopY - startY);
                int scale = Lerper.Lerp1D(mux, startScale, stopScale);
                if (startScale == stopScale) scale = startScale;
                DrawShape(bgc, PenTwist.XZaxis, shape, startX, startZ, y, scale);
            }
        }

        //Extrude shape along Z-axis
        public void ExtrudeZ(IGridContext bgc, int startX, int startY, int startZ, int stopZ, int shape, int startScale, int stopScale, int skips)
        {
            for (int z = startZ; z < stopZ; z++)
            {
                double mux = (double)(z - startZ) / (stopZ - startZ);
                int scale = Lerper.Lerp1D(mux, startScale, stopScale);
                if (startScale == stopScale) scale = startScale;
                DrawShape(bgc, PenTwist.XYaxis, shape, startX, startY, z, scale);
            }
        }

        //Extrude shape along path line
        public void Extrude(IGridContext bgc, int x1, int y1, int z1, int x2, int y2, int z2, int shape, int startScale, int stopScale)
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

            int scale = startScale;

            if (ax >= Math.Max(ay, az))            /* x dominant */
            {
                yd = ay - (ax >> 1);zd = az - (ax >> 1);
                for (; ; )
                {
                    DrawShape(bgc, PenTwist.XYaxis, shape, x, y, z, scale);
                    if (x == x2) return;

                    if (yd >= 0) {y += sy;yd -= ax;}
                    if (zd >= 0) { z += sz; zd -= ax; }
                    x += sx;yd += ay;zd += az;
                }
            }
            if (ay >= Math.Max(ax, az))            /* y dominant */
            {
                xd = ax - (ay >> 1);zd = az - (ay >> 1);
                for (; ; )
                {
                    DrawShape(bgc, PenTwist.XYaxis, shape, x, y, z, scale);
                    if (y == y2) return;

                    if (xd >= 0){x += sx;xd -= ay;}
                    if (zd >= 0){z += sz;zd -= ay;}
                    y += sy;xd += ax;zd += az;
                }
            }
            if (az >= Math.Max(ax, ay))            /* z dominant */
            {
                xd = ax - (az >> 1);yd = ay - (az >> 1);
                for (; ; )
                {
                    DrawShape(bgc, PenTwist.XYaxis, shape, x, y, z, scale);
                    if (z == z2) return;

                    if (xd >= 0) {x += sx;xd -= az;}
                    if (yd >= 0) {y += sy;yd -= az;}
                    z += sz;xd += ax;yd += ay;
                }
            }
        }
    }
}

