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
        //Adjust hue of Grid
        public void HueShift(IGridContext bgc, double hue)
        {
            if (bgc == null) return;

            IGrid grid = bgc.Grid;
            for (int z = 0; z < grid.SizeZ; z++)
            {
                for (int y = 0; y < grid.SizeY; y++)
                {
                    for (int x = 0; x < grid.SizeX; x++)
                    {
                        ulong u = grid.GetRgba(x, y, z);
                        byte r, g, b, a;
                        Converter.Ulong2Rgba(u, out r, out g, out b, out a);
                        double h, s, l;
                        Converter.Rgb2Hsl(r, g, b, out h, out s, out l);
                        h += hue;
                        if (h > 360.0f) h -= 360.0f;
                        u = Converter.Hsl2Rgb(h, s, l);
                        u = Converter.SetAlpha(u, a);
                        grid.Plot(x, y, z, u);
                    }
                }
            }
        }

        //Adjust brightness of Grid
        public void Brightness(IGridContext bgc, double factor)
        {
            if (bgc == null) return;

            IGrid grid = bgc.Grid;
            for (int z = 0; z < grid.SizeZ; z++)
            {
                for (int y = 0; y < grid.SizeY; y++)
                {
                    for (int x = 0; x < grid.SizeX; x++)
                    {
                        ulong u = grid.GetRgba(x, y, z);
                        Converter.Brightness(factor, ref u);
                        grid.Plot(x, y, z, u);
                    }
                }
            }
        }

        //Adjust saturation of Grid
        public void Saturation(IGridContext bgc, double factor)
        {
            if (bgc == null) return;

            IGrid grid = bgc.Grid;
            for (int z = 0; z < grid.SizeZ; z++)
            {
                for (int y = 0; y < grid.SizeY; y++)
                {
                    for (int x = 0; x < grid.SizeX; x++)
                    {
                        ulong u = grid.GetRgba(x, y, z);
                        Converter.Saturation(factor, ref u);
                        grid.Plot(x, y, z, u);
                    }
                }
            }
        }
    }
}
