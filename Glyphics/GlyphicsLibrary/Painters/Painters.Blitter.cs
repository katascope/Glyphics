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
        //Basic Blitter for one grid to another at XYZ location
        public void Blit(IByteGridContext bgc, IGrid pal, int x, int y, int z)
        {
            if (bgc == null || pal == null) return;

            IGrid grid = bgc.Grid;

            for (int bz = 0; bz < pal.SizeZ; bz++)
                for (int by = 0; by < pal.SizeY; by++)
                    for (int bx = 0; bx < pal.SizeX; bx++)
                        grid.Plot(bx + x, by + y, bz + z, pal.GetRgba(bx, by, bz));
        }
        
        //Blit one grid's rectangle portion into another
        public void RectBlit(IByteGridContext bgc, IGrid pal, int x1, int y1, int z1, int x2, int y2, int z2)
        {
            if (bgc == null || pal == null) return;

            MinMax(ref x1, ref x2);
            MinMax(ref y1, ref y2);
            MinMax(ref z1, ref z2);
            if (x2 - x1 == 0) x2++;
            if (y2 - y1 == 0) y2++;
            if (z2 - z1 == 0) z2++;
            double scaleX = (double)pal.SizeX / (x2 - x1);
            double scaleY = (double)pal.SizeY / (y2 - y1);
            double scaleZ = (double)pal.SizeZ / (z2 - z1);

            for (int z = z1; z < z2; z++)
            {
                for (int y = y1; y < y2; y++)
                {
                    for (int x = x1; x < x2; x++)
                    {
                        double scaledx = (x - x1) * scaleX;
                        double scaledy = (y - y1) * scaleY;
                        double scaledz = (z - z1) * scaleZ;
                        ulong b = pal.GetRgba((int)scaledx, (int)scaledy, (int)scaledz);
                        if (b != 0)
                            bgc.Grid.Plot(x, y, z, b);
                    }
                }
            }
        }
    }
}
