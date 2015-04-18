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
        //Draw two points to Grid
        private static void Dualplot(IByteGridContext bgc, int x1, int y, int x2, int zc)
        {
            IGrid grid = bgc.Grid;
            ulong color = bgc.Pen.Rgba;

            grid.Plot(x1, y, zc, color);
            grid.Plot(x2, y, zc, color);
        }

        //Draw set points to Grid
        private static void Fastcpoints(IByteGridContext bgc, int x, int y, int xc, int yc, int zc)
        {
            Dualplot(bgc, -x + xc, y + yc, x + xc, zc);
            Dualplot(bgc, -y + xc, x + yc, y + xc, zc);
            Dualplot(bgc, -x + xc, -y + yc, x + xc, zc);
            Dualplot(bgc, -y + xc, -x + yc, y + xc, zc);
        }

        //Draw an 8-bit star to Grid
        public void DrawStar(IByteGridContext bgc, int xc, int yc, int zc, int radius)
        {
            int x = 0;
            int y = radius;
            int d = 1 - radius;

            Fastcpoints(bgc, x, y, xc, yc, zc);
            while (y > x)
            {
                if (d >= 0)
                {
                    d += ((2 * (x - y)) + 5);
                    x++;
                }
                else
                {
                    x++;
                    y--;
                    d += ((2 * x) + 3);
                }
                Fastcpoints(bgc, x, y, xc, yc, zc);
            }
        }
    }
}
