#region Copyright
/*Copyright (c) 2015, Katascope
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/
#endregion

namespace GlyphicsLibrary.Painters
{
    internal partial class CPainter
    {
        //Plots a thicker point to a grid without using penshape
        private void Thickplot(IGrid grid, int x, int y, int z, ulong color)
        {
            int height = grid.SizeY;
            grid.Plot(x, height-y, z, color);
            grid.Plot(x + 1, height - y, z, color);
            grid.Plot(x - 1, height - y, z, color);
        }

        //Draw alien's right eye
        private void alien_right_eye(IGridContext bgc, int xc, int yc, int zc, int radius, ulong color)
        {
            int x = 0;
            int y = radius;
            int d = 3 - (2 * radius);

            Thickplot(bgc.Grid, xc + x, yc + y, zc, color);
            Thickplot(bgc.Grid, xc + y + radius / 2, yc - x - radius / 2, zc, color);
            Thickplot(bgc.Grid, xc - x, yc + y, zc, color);
            Thickplot(bgc.Grid, xc - y, yc + x, zc, color);
            Thickplot(bgc.Grid, xc - x + radius / 2, yc - y - radius / 2, zc, color);

            while (y > x)
            {
                if (d < 0) d += (4 * x) + 6;
                else d += 4 * (x - y--) + 10;
                x++;
                Thickplot(bgc.Grid, xc + x, yc + y, zc, color);
                Thickplot(bgc.Grid, xc - y, yc - x, zc, color);
                Thickplot(bgc.Grid, xc - x, yc + y, zc, color);
                Thickplot(bgc.Grid, xc - y, yc + x, zc, color);
            }
            int lx = x;
            int ly = y;
            x = 0;
            y = radius;
            d = 3 - (2 * radius);
            while (y > x)
            {
                if (d < 0) d += (4 * x) + 6;
                else d += 4 * (x - y--) + 10;
                x++;
                Thickplot(bgc.Grid, xc + x + radius / 2, yc - y - radius / 2, zc, color);
                Thickplot(bgc.Grid, xc + y + radius / 2, yc + x - radius / 2, zc, color);
                Thickplot(bgc.Grid, xc + y + radius / 2, yc - x - radius / 2, zc, color);
                Thickplot(bgc.Grid, xc - x + radius / 2, yc - y - radius / 2, zc, color);
            }
            int height = bgc.Grid.SizeY;
            DrawLine2D(bgc, xc - lx, height-(yc - ly), xc - x + radius / 2, height-(yc - y - radius / 2), zc);
            DrawLine2D(bgc, xc + lx, height-(yc + ly), xc + x + radius / 2, height-(yc + y - radius / 2), zc);
        }

        //Draw alien's left eye
        private void alien_left_eye(IGridContext bgc, int xc, int yc, int zc, int radius, ulong color)
        {
            int x = 0;
            int y = radius;
            int d = 3 - (2 * radius);

            Thickplot(bgc.Grid, xc - x, yc + y, zc, color);
            Thickplot(bgc.Grid, xc - y - radius / 2, yc - x - radius / 2, zc, color);
            Thickplot(bgc.Grid, xc + x, yc + y, zc, color);
            Thickplot(bgc.Grid, xc + y, yc + x, zc, color);
            Thickplot(bgc.Grid, xc + x - radius / 2, yc - y - radius / 2, zc, color);
            while (y > x)
            {
                if (d < 0) d += (4 * x) + 6;
                else d += 4 * (x - y--) + 10;
                x++;
                Thickplot(bgc.Grid, xc - x, yc + y, zc, color);
                Thickplot(bgc.Grid, xc + y, yc - x, zc, color);
                Thickplot(bgc.Grid, xc + x, yc + y, zc, color);
                Thickplot(bgc.Grid, xc + y, yc + x, zc, color);
            }
            int lx = x;
            int ly = y;
            x = 0;
            y = radius;
            d = 3 - (2 * radius);
            while (y > x)
            {
                if (d < 0) d += (4 * x) + 6;
                else d += 4 * (x - y--) + 10;
                x++;
                Thickplot(bgc.Grid, xc - x - radius / 2, yc - y - radius / 2, zc, color);
                Thickplot(bgc.Grid, xc - y - radius / 2, yc + x - radius / 2, zc, color);
                Thickplot(bgc.Grid, xc - y - radius / 2, yc - x - radius / 2, zc, color);
                Thickplot(bgc.Grid, xc + x - radius / 2, yc - y - radius / 2, zc, color);
            }
            int height = bgc.Grid.SizeY;
            DrawLine2D(bgc, xc + lx, height - (yc - ly), xc + x - radius / 2, height - (yc - y - radius / 2), zc);
            DrawLine2D(bgc, xc - lx, height - (yc + ly), xc - x - radius / 2, height - (yc + y - radius / 2), zc);
        }

        //Draw alien
        public void DrawAlien(IGridContext bgc, int xc, int yc, int zc, int radius)
        {
            if (bgc == null) return;

            int x = 0;
            int y = radius;
            int d = 3 - (2 * radius);
            IGrid grid = bgc.Grid;

            ulong color = bgc.Pen.Rgba;

            //	 fast_circle(xc,yc,radius,color);
            Thickplot(grid, xc + y - x, yc + 2 * x - 1, zc, color);
            Thickplot(grid, xc + y - x, yc + 2 * x, zc, color);
            Thickplot(grid, xc - y + x, yc + 2 * x - 1, zc, color);
            Thickplot(grid, xc - y + x, yc + 2 * x, zc, color);
            Thickplot(grid, xc + x, yc - y, zc, color);
            Thickplot(grid, xc - x, yc - y, zc, color);
            Thickplot(grid, xc + y, yc - x, zc, color);
            Thickplot(grid, xc - y, yc - x, zc, color);

            while (y > x)
            {
                if (d < 0)
                {
                    d = d + (4 * x) + 6;
                }
                else
                {
                    d = d + 4 * (x - y) + 10;
                    y--;
                }
                x++;
                Thickplot(grid, xc + y - x, yc + (2 * x - 1) * 3 / 4, zc, color);
                Thickplot(grid, xc + y - x, yc + (2 * x) * 3 / 4, zc, color);
                Thickplot(grid, xc - y + x, yc + (2 * x - 1) * 3 / 4, zc, color);
                Thickplot(grid, xc - y + x, yc + (2 * x) * 3 / 4, zc, color);
                Thickplot(grid, xc + x, yc - y, zc, color);
                Thickplot(grid, xc - x, yc - y, zc, color);
                Thickplot(grid, xc + y, yc - x, zc, color);
                Thickplot(grid, xc - y, yc - x, zc, color);
            }
            alien_right_eye(bgc, xc + x / 4 * 3, yc - (y / 3) / 2, zc, radius / 4, color);
            alien_left_eye(bgc, xc - x / 4 * 3, yc - (y / 3) / 2, zc, radius / 4, color);
        }
    }
}
