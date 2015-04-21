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
        void CopyInto(IByteGridContext bgc, int x, int y, int z, int archtype);
        void Blit(IByteGridContext bgc, IGrid pal, int x, int y, int z);
        void RectBlit(IByteGridContext bgc, IGrid pal, int x1, int y1, int z1, int x2, int y2, int z2);
        void PaletteBlit(IByteGridContext bgc, IGrid grid, IGrid palette);
        void BlendBlit(IByteGridContext bgc, IGrid pal, int x1, int y1, int z1, int x2, int y2, int z2, int blend);

        void DrawLine2D(IByteGridContext bgc, int x1, int y1, int x2, int y2, int z);
        void DrawLetter(IByteGridContext bgc, PenTwist twistType, int x, int y, int z, int letter, bool flip);
        void DrawHollowRect(IByteGridContext bgc, int x1, int y1, int z1, int x2, int y2, int z2);

        void DrawAlien(IByteGridContext bgc, int xc, int yc, int zc, int radius);
        void DrawStar(IByteGridContext bgc, int xc, int yc, int zc, int radius);
        void DrawFastFillRect(IByteGridContext bgc, int x1, int y1, int z1, int x2, int y2, int z2);

        void DrawTriangle2D(IByteGridContext bgc, int x1, int y1, int x2, int y2, int x3, int y3, int z);
        void DrawFillTriangle2D(IByteGridContext bgc, int x1, int y1, int x2, int y2, int x3, int y3);
        void DrawFillTriangle3D(IByteGridContext bgc, int x1, int y1, int z1, int x2, int y2, int z2, int x3, int y3, int z3);

        void DrawFillRect(IByteGridContext bgc, int x1, int y1, int z1, int x2, int y2, int z2);
        
        void DrawClear(IByteGridContext bgc);

        void DrawCircle2DAnyAxis(IByteGridContext bgc, PenTwist ptt, int x0, int y0, int z, int radius);
        void DrawFillCircle2D(IByteGridContext bgc, PenTwist ptt, int x1, int y1, int z, int radius);
        void DrawWallCube(IByteGridContext bgc, byte bitmask);
        void DrawSupports(IByteGridContext bgc);
        void DrawPen(IByteGridContext bgc, int x, int y, int z);
        void DrawLine3D(IByteGridContext bgc, int x1, int y1, int z1, int x2, int y2, int z2);
        void DrawArc(IByteGridContext bgc, PenTwist twistType, int x, int y, int z, int radius, int startAnglePercent, int stopAnglePercent);
        void DrawStairs(IByteGridContext bgc, int x1, int y1, int z1, int x2, int y2, int z2, int w, int h, int d);
        void DrawCircle3D(IByteGridContext bgc, int sx, int sy, int sz, int radius);
        void DrawMaskHollowRect(IByteGridContext bgc, int x1, int y1, int z1, int x2, int y2, int z2, byte bitmask);
        void DrawMaskFillRect(IByteGridContext bgc, int x1, int y1, int z1, int x2, int y2, int z2, byte bitmask);
        void DrawOval3D(IByteGridContext bgc, int sx, int sy, int sz, int width, int height, int depth);
        void Pyramid3D(IByteGridContext bgc, int x1, int y1, int z1, int x2, int y2, int z2);
        void Diamond2D(IByteGridContext bgc, int sx, int sy, int sz, int width, int height);
        void DrawTriangle3D(IByteGridContext bgc, int x1, int y1, int z1, int x2, int y2, int z2, int x3, int y3, int z3);

        void EdgeColor(IByteGridContext bgc, int axis, byte ri, byte gi, byte bi, byte ai);
        void UpV(IByteGridContext bgc, int reps, byte ri, byte gi, byte bi, byte ai);
        
        void Quad2D(IByteGridContext bgc, int x1, int y1, int x2, int y2, int z, int height);
        void DrawFillCircle3D(IByteGridContext bgc, int x1, int y1, int z, int radius);
        void ArchRect(IByteGridContext bgc, int x1, int y1, int z1, int x2, int y2, int z2, int shape, int rotation, int spacing);
        void ArchLine(IByteGridContext bgc, int x1, int y1, int z1, int x2, int y2, int z2, int shape, int rotation, int spacing);
        void DrawPolygon(IByteGridContext bgc, PenTwist twistType, int x, int y, int z, int radius, int sides);
        void Extrude(IByteGridContext bgc, int x1, int y1, int z1, int x2, int y2, int z2, int shape, int startScale, int stopScale);

        void ExtrudeX(IByteGridContext bgc, int startX, int startY, int startZ, int stopX, int shape, int startScale, int stopScale, int skips);
        void ExtrudeY(IByteGridContext bgc, int startX, int startY, int startZ, int stopX, int shape, int startScale, int stopScale, int skips);
        void ExtrudeZ(IByteGridContext bgc, int startX, int startY, int startZ, int stopX, int shape, int startScale, int stopScale, int skips);

        void FlipX(IByteGridContext bgc);
        void FlipY(IByteGridContext bgc);
        void FlipZ(IByteGridContext bgc);
        void MirrorX(IByteGridContext bgc);
        void MirrorY(IByteGridContext bgc);
        void MirrorZ(IByteGridContext bgc);
        void RotateX(IByteGridContext bgc);
        void RotateY(IByteGridContext bgc);
        void RotateZ(IByteGridContext bgc);

        void PalGen1D(IGrid pal, byte vs1, byte vs2, byte vs3, byte vs4, byte vd1, byte vd2, byte vd3, byte vd4);
        void PalGen1DBanded(IGrid pal, List<ulong> bandColors);
        void PalGen1DBand(int numBands, IGrid pal, ulong color1, ulong color2, int band);

        void HueShift(IByteGridContext bgc, double hue);
        void Brightness(IByteGridContext bgc, double factor);
        void Saturation(IByteGridContext bgc, double factor);
        void Invert(IByteGridContext bgc);
        void Grayscale(IByteGridContext bgc);
        void Colorize(IByteGridContext bgc, double hue, double saturation);
        void Palettize(IByteGridContext bgc, IGrid palette);
        void Shade(IByteGridContext bgc, int axis, byte r1, byte g1, byte b1, byte r2, byte g2, byte b2);

        void ApplyFilterBlur(IByteGridContext bgc);
        void ApplyFilterMotion(IByteGridContext bgc);
        void ApplyFilterEdgeHorizontal(IByteGridContext bgc);
        void ApplyFilterEdgeVertical(IByteGridContext bgc);
        void ApplyFilterEdgeAll(IByteGridContext bgc);
        void ApplyFilterSharpen(IByteGridContext bgc);
        void ApplyFilterEmboss(IByteGridContext bgc);
        void ApplyFilterSmooth(IByteGridContext bgc);

        void DrawCorners(IByteGridContext bgc, int x1, int y1, int z1, int x2, int y2, int z2);
        void DrawShadows(IByteGridContext bgc);

        void DrawMaze(IByteGridContext bgc, int x1, int y, int z1, int x2, int z2);
    }
}
