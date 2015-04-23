#region Copyright
/*Copyright (c) 2015, Katascope
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/
#endregion
using System.Collections.Generic;

namespace GlyphicsLibrary
{
    //Set of painting functions to draw to an IGrid or IByteGridContext(which has an IGrid)
    public interface IPainter
    {
        void CopyInto(IGridContext bgc, int x, int y, int z, int archtype);
        void Blit(IGridContext bgc, IGrid pal, int x, int y, int z);
        void RectBlit(IGridContext bgc, IGrid pal, int x1, int y1, int z1, int x2, int y2, int z2);
        void PaletteBlit(IGridContext bgc, IGrid grid, IGrid palette);
        void BlendBlit(IGridContext bgc, IGrid pal, int x1, int y1, int z1, int x2, int y2, int z2, int blend);

        void DrawLine2D(IGridContext bgc, int x1, int y1, int x2, int y2, int z);
        void DrawLetter(IGridContext bgc, PenTwist twistType, int x, int y, int z, int letter, bool flip);
        void DrawHollowRect(IGridContext bgc, int x1, int y1, int z1, int x2, int y2, int z2);

        void DrawAlien(IGridContext bgc, int xc, int yc, int zc, int radius);
        void DrawStar(IGridContext bgc, int xc, int yc, int zc, int radius);
        void DrawFastFillRect(IGridContext bgc, int x1, int y1, int z1, int x2, int y2, int z2);

        void DrawTriangle2D(IGridContext bgc, int x1, int y1, int x2, int y2, int x3, int y3, int z);
        void DrawFillTriangle2D(IGridContext bgc, int x1, int y1, int x2, int y2, int x3, int y3);
        void DrawFillTriangle3D(IGridContext bgc, int x1, int y1, int z1, int x2, int y2, int z2, int x3, int y3, int z3);

        void DrawFillRect(IGridContext bgc, int x1, int y1, int z1, int x2, int y2, int z2);
        
        void DrawClear(IGridContext bgc);

        void DrawCircle2DAnyAxis(IGridContext bgc, PenTwist ptt, int x0, int y0, int z, int radius);
        void DrawFillCircle2D(IGridContext bgc, PenTwist ptt, int x1, int y1, int z, int radius);
        void DrawWallCube(IGridContext bgc, byte bitmask);
        void DrawSupports(IGridContext bgc);
        void DrawPen(IGridContext bgc, int x, int y, int z);
        void DrawLine3D(IGridContext bgc, int x1, int y1, int z1, int x2, int y2, int z2);
        void DrawArc(IGridContext bgc, PenTwist twistType, int x, int y, int z, int radius, int startAnglePercent, int stopAnglePercent);
        void DrawStairs(IGridContext bgc, int x1, int y1, int z1, int x2, int y2, int z2, int w, int h, int d);
        void DrawCircle3D(IGridContext bgc, int sx, int sy, int sz, int radius);
        void DrawMaskHollowRect(IGridContext bgc, int x1, int y1, int z1, int x2, int y2, int z2, byte bitmask);
        void DrawMaskFillRect(IGridContext bgc, int x1, int y1, int z1, int x2, int y2, int z2, byte bitmask);
        void DrawOval3D(IGridContext bgc, int sx, int sy, int sz, int width, int height, int depth);
        void Pyramid3D(IGridContext bgc, int x1, int y1, int z1, int x2, int y2, int z2);
        void Diamond2D(IGridContext bgc, int sx, int sy, int sz, int width, int height);
        void DrawTriangle3D(IGridContext bgc, int x1, int y1, int z1, int x2, int y2, int z2, int x3, int y3, int z3);

        void EdgeColor(IGridContext bgc, int axis, byte ri, byte gi, byte bi, byte ai);
        void UpV(IGridContext bgc, int reps, byte ri, byte gi, byte bi, byte ai);
        
        void Quad2D(IGridContext bgc, int x1, int y1, int x2, int y2, int z, int height);
        void DrawFillCircle3D(IGridContext bgc, int x1, int y1, int z, int radius);
        void ArchRect(IGridContext bgc, int x1, int y1, int z1, int x2, int y2, int z2, int shape, int rotation, int spacing);
        void ArchLine(IGridContext bgc, int x1, int y1, int z1, int x2, int y2, int z2, int shape, int rotation, int spacing);
        void DrawPolygon(IGridContext bgc, PenTwist twistType, int x, int y, int z, int radius, int sides);
        void Extrude(IGridContext bgc, int x1, int y1, int z1, int x2, int y2, int z2, int shape, int startScale, int stopScale);

        void ExtrudeX(IGridContext bgc, int startX, int startY, int startZ, int stopX, int shape, int startScale, int stopScale, int skips);
        void ExtrudeY(IGridContext bgc, int startX, int startY, int startZ, int stopX, int shape, int startScale, int stopScale, int skips);
        void ExtrudeZ(IGridContext bgc, int startX, int startY, int startZ, int stopX, int shape, int startScale, int stopScale, int skips);

        void FlipX(IGridContext bgc);
        void FlipY(IGridContext bgc);
        void FlipZ(IGridContext bgc);
        void MirrorX(IGridContext bgc);
        void MirrorY(IGridContext bgc);
        void MirrorZ(IGridContext bgc);
        void RotateX(IGridContext bgc);
        void RotateY(IGridContext bgc);
        void RotateZ(IGridContext bgc);

        void PalGen1D(IGrid pal, byte vs1, byte vs2, byte vs3, byte vs4, byte vd1, byte vd2, byte vd3, byte vd4);
        void PalGen1DBanded(IGrid pal, List<ulong> bandColors);
        void PalGen1DBand(int numBands, IGrid pal, ulong color1, ulong color2, int band);

        void HueShift(IGridContext bgc, double hue);
        void Brightness(IGridContext bgc, double factor);
        void Saturation(IGridContext bgc, double factor);
        void Invert(IGridContext bgc);
        void Grayscale(IGridContext bgc);
        void Colorize(IGridContext bgc, double hue, double saturation);
        void Palettize(IGridContext bgc, IGrid palette);
        void Shade(IGridContext bgc, int axis, byte r1, byte g1, byte b1, byte r2, byte g2, byte b2);

        void ApplyFilterBlur(IGridContext bgc);
        void ApplyFilterMotion(IGridContext bgc);
        void ApplyFilterEdgeHorizontal(IGridContext bgc);
        void ApplyFilterEdgeVertical(IGridContext bgc);
        void ApplyFilterEdgeAll(IGridContext bgc);
        void ApplyFilterSharpen(IGridContext bgc);
        void ApplyFilterEmboss(IGridContext bgc);
        void ApplyFilterSmooth(IGridContext bgc);

        void DrawCorners(IGridContext bgc, int x1, int y1, int z1, int x2, int y2, int z2);
        void DrawShadows(IGridContext bgc);

        void DrawMaze(IGridContext bgc, byte seed, int x1, int y, int z1, int x2, int z2);
    }
}
