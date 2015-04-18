#region Copyright
/*Copyright (c) 2015, Katascope
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/
#endregion
using GlyphicsLibrary.ByteGrid;
using GlyphicsLibrary.Painters;

namespace GlyphicsLibrary.Renderers
{
    //Partial class to render rects
    internal partial class Renderer
    {
        //Render a set of rects into a grid as little filled 3d rectangles
        public void RenderRectsToGrid(IRectList rects, IGrid grid)
        {
            if (rects == null || grid == null) return;

            IByteGridContext bgc = new CByteGridContext(grid);
            IPainter painter = new CPainter();

            //Draw background
            painter.DrawFastFillRect(bgc, 0, 0, 0, bgc.Grid.SizeX, bgc.Grid.SizeY, bgc.Grid.SizeZ);

            //Then draw each triangle, adjusting for inclusive numbering
            const int inclusiveOffset = 1;
            foreach (IRect rect in rects)
            {
                bgc.Pen.SetColor(rect.Properties.Rgba);
                painter.DrawFastFillRect(bgc, (int)rect.Pt1.X, (int)rect.Pt1.Y, (int)rect.Pt1.Z, (int)rect.Pt2.X - inclusiveOffset, (int)rect.Pt2.Y - inclusiveOffset, (int)rect.Pt2.Z - inclusiveOffset);
            }
        }
    }
}
