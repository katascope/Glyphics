#region Copyright
/*Copyright (c) 2015, Katascope
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/
#endregion
using GlyphicsLibrary.Atomics;
using GlyphicsLibrary.ByteGrid;
using GlyphicsLibrary.Painters;

namespace GlyphicsLibrary.Renderers
{
    //Partial class to render oblique cells
    internal partial class Renderer
    {
        //Render a cell at x,y coords
        public static void RenderCell(IGridContext bgc, int x, int y)
        {
            IPainter painter = new CPainter();
            const int cellSize = 12;
            ulong color = bgc.Pen.Rgba;
            const int subSize = 8;
            const int tinSize = (cellSize - subSize);
            byte r, g, b, a;
            Atomics.Converter.Ulong2Rgba(color, out r, out g, out b, out a);

            bgc.Pen.Rgba = Atomics.Converter.Rgba2Ulong(16, 16, 16, 255);
            painter.DrawHollowRect(bgc, x + tinSize, y, 0, x + cellSize - 1, y + subSize - 1, 0);

            int ir = r;
            int ig = g;
            int ib = b;
            //Top side
            bgc.Pen.Rgba = Atomics.Converter.Rgba2Ulong((byte)ir, (byte)ig, (byte)ib, 255);
            painter.DrawLine2D(bgc, x + tinSize, y + 1, x + subSize + 1, y + 1, 0);
            painter.DrawLine2D(bgc, x + tinSize - 1, y + 2, x + subSize, y + 2, 0);
            painter.DrawLine2D(bgc, x + tinSize - 2, y + 3, x + subSize - 1, y + 3, 0);

            //Front side
            ir = r;
            ig = g;
            ib = b;
            bgc.Pen.Rgba = Atomics.Converter.Rgba2Ulong((byte)ir, (byte)ig, (byte)ib, 255);
            painter.DrawFastFillRect(bgc, x + 1, y + tinSize + 1, 0, x + subSize, y + subSize + 2, 0);

            //Right side
            ir = r / 2;
            ig = g / 2;
            ib = b / 2;
            bgc.Pen.Rgba = Atomics.Converter.Rgba2Ulong((byte)ir, (byte)ig, (byte)ib, 255);
            painter.DrawLine2D(bgc, x + cellSize - 2, y + 2, x + cellSize - 2, y + subSize, 0);
            painter.DrawLine2D(bgc, x + cellSize - 3, y + 2, x + cellSize - 3, y + subSize, 0);
            painter.DrawLine2D(bgc, x + cellSize - 4, y + 3, x + cellSize - 4, y + subSize + 1, 0);

            //Front rect
            bgc.Pen.Rgba = Atomics.Converter.Rgba2Ulong(32, 32, 32, 255);
            painter.DrawHollowRect(bgc, x, y + tinSize, 0, x + subSize - 1, y + cellSize - 1, 0);

            //Lines
            painter.DrawLine2D(bgc, x, y + tinSize, x + tinSize, y, 0);
            painter.DrawLine2D(bgc, x + subSize - 1, y + tinSize, x + cellSize - 1, y, 0);
            painter.DrawLine2D(bgc, x + subSize - 1, y + cellSize - 1, x + cellSize - 1, y + subSize - 1, 0);
        }

        //Render a grid into another grid with oblique perspective and iconic cells
        public static void RenderObliqueCellsSet(IGrid gridSrc, IGrid gridDst)
        {
            IGridContext bgc = new CGridContext(gridDst);

            const int cellSize = 7;
            const int tinSize = 4;

            for (int y = 0; y < gridSrc.SizeY; y++)
            {
                for (int z = gridSrc.SizeZ; z >= 0; z--)
                {
                    for (int x = 0; x < gridSrc.SizeX; x++)
                    {
                        ulong u = gridSrc.GetRgba(x, y, z);
                        if (u != 0)
                        {
                         //   ulong val = gridSrc.GetRgba(x, y, z);
                            bgc.Pen.SetColor(u);

                            int sx = x * cellSize + z * tinSize;
                            int sy = gridDst.SizeY - (z + 1) * 4 - cellSize - 1 - y * cellSize;

                            RenderCell(bgc, sx, sy);
                        }
                    }
                }
            }
        }

        //Render obliquely and return
        public IGrid RenderObliqueCells(IGrid grid)
        {
            if (grid == null) return null;

            const int cellSize = 12;
            int ix = grid.SizeX * cellSize;
            int iy = grid.SizeY * cellSize;
            IGrid grid2 = new CGrid(ix, iy, 1, grid.Bpp);

            IGridContext bgc = new CGridContext(grid2);
            bgc.Pen.Rgba = Atomics.Converter.Rgba2Ulong(255, 255, 255, 255);

            IPainter painter = new CPainter();
            painter.DrawFastFillRect(bgc, 0, 0, 0, grid2.SizeX, grid2.SizeY, 1);

            RenderObliqueCellsSet(grid, grid2);
            return grid2;
        }

        //Render a grid into another grid with oblique perspective and iconic cells
        public static void RenderObliqueCellsSetRects(IRectList rects, IGrid gridDst)
        {
            IGridContext bgc = new CGridContext(gridDst);

            const int cellSize = 7;
            const int tinSize = 4;

            foreach (IRect rect in rects)
            {
                ulong u = rect.Properties.Rgba;
                bgc.Pen.SetColor(u);
                if (u != 0)
                {
                    var x1 = (int)rect.Pt1.X;
                    var y1 = (int)rect.Pt1.Y;
                    var z1 = (int)rect.Pt1.Z;

                    int sx1 = x1 * cellSize + z1 * tinSize;
                    int sy1 = gridDst.SizeY - (z1 + 1) * 4 - cellSize - 1 - y1 * cellSize;

                    var x2 = (int)rect.Pt2.X;
                    var y2 = (int)rect.Pt2.Y;
                    var z2 = (int)rect.Pt2.Z;

                    int sx2 = x2 * cellSize + z2 * tinSize;
                    int sy2 = gridDst.SizeY - (z2 + 1) * 4 - cellSize - 1 - y2 * cellSize;

                    bgc.Pen.SetColor(u);
                    RenderCell(bgc, sx1, sy1);
                    bgc.Pen.SetColor(u);
                    RenderCell(bgc, sx2, sy2);
                }
            }
        }

        //Render obliquely and return
        public IGrid RenderObliqueCellsRects(IRectList rects)
        {
            if (rects == null) return null;

            const int cellSize = 12;
            int ix = rects.SizeX * cellSize;
            int iy = rects.SizeY * cellSize;
            IGrid grid2 = new CGrid(ix, iy, 1, 4);

            IGridContext bgc = new CGridContext(grid2);
            bgc.Pen.Rgba = Atomics.Converter.Rgba2Ulong(255, 255, 255, 255);

            IPainter painter = new CPainter();
            painter.DrawFastFillRect(bgc, 0, 0, 0, grid2.SizeX, grid2.SizeY, 1);

            RenderObliqueCellsSetRects(rects, grid2);
            return grid2;
        }
    }
}
