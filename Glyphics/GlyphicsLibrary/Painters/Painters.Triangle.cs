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
        //Draw hollow triangle in 2D to Grid
        public void DrawTriangle2D(IGridContext bgc, int x1, int y1, int x2, int y2, int x3, int y3, int z)
        {
            DrawLine2D(bgc, x1, y1, x2, y2, z);
            DrawLine2D(bgc, x2, y2, x3, y3, z);
            DrawLine2D(bgc, x3, y3, x1, y1, z);
        }

        //Draw hollow triangle in 3D to Grid
        public void DrawTriangle3D(IGridContext bgc, int x1, int y1, int z1, int x2, int y2, int z2, int x3, int y3, int z3)
        {
            DrawLine3D(bgc, x1, y1, z1, x2, y2, z2);
            DrawLine3D(bgc, x2, y2, z2, x3, y3, z3);
            DrawLine3D(bgc, x3, y3, z3, x1, y1, z1);
        }

        private bool QuickTriangle2D(IGridContext bgc, int x1, int y1, int x2, int y2, int x3, int y3)
        {
            if ((x1 == x2) && (y1 == y2) && (x1 == x3) && (y1 == y3))
            {
                DrawPen(bgc, x1, y1, 0);
                return true;
            }
            if ((x1 == x2) && (y1 == y2))
            {
                DrawLine2D(bgc, x1, y1, x3, y3, 0);
                return true;
            }
            if ((x1 == x3) && (y1 == y3))
            {
                DrawLine2D(bgc, x1, y1, x2, y2, 0);
                return true;
            }
            if ((x2 == x3) && (y2 == y3))
            {
                DrawLine2D(bgc, x1, y1, x3, y3, 0);
                return true;
            }
            return false;
        }

        //Draw filled triangle 2d to Grid
        public void DrawFillTriangle2D(IGridContext bgc, int x1, int y1, int x2, int y2, int x3, int y3)
        {
            var vtx = new IDouble3[3];
            var order = new int[3];

            if (QuickTriangle2D(bgc,x1,y1,x2,y2,x3,y3)) 
                return;


            vtx[0] = new CDouble3(x1, y1, 0);
            vtx[1] = new CDouble3(x2, y2, 0);
            vtx[2] = new CDouble3(x3, y3, 0);

            // find the lowest Y
            if (vtx[0].Y < vtx[1].Y)
            {
                if (vtx[0].Y < vtx[2].Y) order[0] = 0;
                else order[0] = 2;
            }
            else
            {
                if (vtx[1].Y < vtx[2].Y) order[0] = 1;
                else order[0] = 2;
            }

            // find the highest Y
            if (vtx[0].Y > vtx[1].Y)
            {
                if (vtx[0].Y > vtx[2].Y) order[2] = 0;
                else order[2] = 2;
            }
            else
            {
                if (vtx[1].Y > vtx[2].Y) order[2] = 1;
                else order[2] = 2;
            }

            // and the middle one is a matter of deduction
            order[1] = 3 - (order[0] + order[2]);

            if (order[0] == -1) return;
            if (order[1] == -1) return;
            if (order[2] == -1) return;

            double d0 = (vtx[order[1]].X - vtx[order[0]].X) / (vtx[order[1]].Y - vtx[order[0]].Y);
            double d1 = (vtx[order[2]].X - vtx[order[0]].X) / (vtx[order[2]].Y - vtx[order[0]].Y);

            IDouble3 pt1 = new CDouble3(vtx[order[0]].X, vtx[order[0]].Y, 0);
            IDouble3 pt2 = new CDouble3(vtx[order[0]].X, vtx[order[0]].Y, 0);
            bool done = false;

            if (Math.Abs((vtx[order[1]].Y - vtx[order[0]].Y)) < 0.01) return;

            //if ( (pt1.x != pt2.x) && (pt1.y != pt2.y) && (pt1.z != pt2.z) )
            {
                while (!done)
                {
                    DrawLine2D(bgc, (int)pt1.X, (int)pt1.Y, (int)pt2.X, (int)pt2.Y, 0);
                    pt1.X += d0;
                    pt1.Y++;
                    pt2.X += d1;
                    pt2.Y++;
                    if (pt1.X < 0)
                        return;
                    if (pt1.Y >= vtx[order[1]].Y)
                        done = true;
                }
            }

            d0 = (vtx[order[2]].X - vtx[order[1]].X) / (vtx[order[2]].Y - vtx[order[1]].Y);

            if (Math.Abs((vtx[order[2]].Y - vtx[order[1]].Y)) < 0.01) return;

            done = false;
            while (!done)
            {
                DrawLine2D(bgc, (int)pt1.X, (int)pt1.Y, (int)pt2.X, (int)pt2.Y, 0);
                pt1.X += d0;
                pt1.Y++;
                pt2.X += d1;
                pt2.Y++;
                if (pt1.X < 0)
                    done = true;
                if (pt1.Y >= vtx[order[2]].Y)
                    done = true;
            }
        }

        //Tries to draw a true 3d triangle, really lazily
        public void DrawFillTriangle3D(IGridContext bgc, int x1, int y1, int z1, int x2, int y2, int z2, int x3, int y3, int z3)
        {
            if (bgc == null) return;

            bgc.Pen.SetSize(1, 1, 1);
            BresenhamCallingBresnhamLine3D(bgc, x1, y1, z1, x2, y2, z2, x3, y3, z3);
            //BresenhamCallingBresnhamLine3D(bgc, x1, y1, z1, x3, y3, z3, x2, y2, z2);
            BresenhamCallingBresnhamLine3D(bgc, x3, y3, z3, x1, y1, z1, x2, y2, z2);
        }

        //This function is so lazy and bad I'm too lazy to explain it
        private void BresenhamCallingBresnhamLine3D(IGridContext bgc, int x1, int y1, int z1, int x2, int y2, int z2, int originX, int originY, int originZ)
        {
            MinMax(ref x1, ref x2);
            MinMax(ref y1, ref y2);
            MinMax(ref z1, ref z2);

            bgc.Grid.Plot(x1, y1, z1, bgc.Pen.Rgba);
            bgc.Grid.Plot(x2, y2, z2, bgc.Pen.Rgba);
            bgc.Grid.Plot(originX, originY, originZ, bgc.Pen.Rgba);

            int dx = x2 - x1;
            int dy = y2 - y1;
            int dz = z2 - z1;

            int ax = Math.Abs(dx) * 2;
            int ay = Math.Abs(dy) * 2;
            int az = Math.Abs(dz) * 2;

            int sx = Math.Sign(dx);
            int sy = Math.Sign(dy);
            int sz = Math.Sign(dz);

            int x = x1;
            int y = y1;
            int z = z1;
            int idx = 1;

            if (ax >= Math.Max(ay, az))			//% x dominant
            {
                int yd = ay - ax / 2;
                int zd = az - ax / 2;

                while (true)
                {
                    DrawLine3D(bgc, originX, originY, originZ, x, y, z);
                    idx = idx + 1;

                    if (x == x2)
                    { 		//% end
                        break;
                    }

                    if (yd >= 0)
                    {//		% move along y
                        y = y + sy;
                        yd = yd - ax;
                    }

                    if (zd >= 0)
                    {//		% move along z
                        z = z + sz;
                        zd = zd - ax;
                    }

                    x = x + sx;		//% move along x
                    yd = yd + ay;
                    zd = zd + az;
                }
            }
            else if (ay >= Math.Max(ax, az))
            {	//% y dominant
                int xd = ax - ay / 2;
                int zd = az - ay / 2;

                while (true)
                {
                    DrawLine3D(bgc, originX, originY, originZ, x, y, z);
                    idx = idx + 1;

                    if (y == y2)
                    {//		% end
                        break;
                    }

                    if (xd >= 0)
                    {//		% move along x
                        x = x + sx;
                        xd = xd - ay;
                    }

                    if (zd >= 0)
                    {//		% move along z
                        z = z + sz;
                        zd = zd - ay;
                    }

                    y = y + sy;		//% move along y
                    xd = xd + ax;
                    zd = zd + az;
                }
            }
            else if (az >= Math.Max(ax, ay))
            {//		% z dominant
                int xd = ax - az / 2;
                int yd = ay - az / 2;

                while (true)
                {
                    DrawLine3D(bgc, originX, originY, originZ, x, y, z);
                    idx = idx + 1;

                    if (z == z2)
                    {//	% end
                        break;
                    }

                    if (xd >= 0)
                    {		//% move along x
                        x = x + sx;
                        xd = xd - az;
                    }

                    if (yd >= 0)
                    {//	% move along y
                        y = y + sy;
                        yd = yd - az;
                    }

                    z = z + sz;	//	% move along z
                    xd = xd + ax;
                    yd = yd + ay;
                }
            }
        }
    }
}
