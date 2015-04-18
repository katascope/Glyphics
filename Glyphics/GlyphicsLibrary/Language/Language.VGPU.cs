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

namespace GlyphicsLibrary.Language
{
    //Virtual Graphics-Processing-Unit, a simplified virtual machine for generating grids
    internal class Vrgpu
    {
        private Vrgpu() { }

        //Keep a painter around by default
        private static readonly IPainter Painter = new CPainter();

        //Execute a given IGlyph on the IByteGridContext, tracked by glyphics code line, and arguments to the glyph
        internal static void ExecuteGlyph(IByteGridContext bgc, byte glyphicsCodeLine, IGlyph glyph, byte[] args)
        {
            //No context, nothing to do
            if (bgc == null) 
                return;

            //If we have a grid, set the code tracker to the current line of glyphics code
            if (bgc.Grid != null)
                bgc.Grid.SetTracker(glyphicsCodeLine);            

            //Big switch to map each Glyph to the command
            switch (glyph.Glyph)
            {
                case GlyphId.GlyphNop: return;

                case GlyphId.GlyphSize1D1: bgc.Grid = new CGrid(args[0], 1, 1, 1); return;
                case GlyphId.GlyphSize2D1: bgc.Grid = new CGrid(args[0], args[1], 1, 1); return;
                case GlyphId.GlyphSize3D1: bgc.Grid = new CGrid(args[0], args[1], args[2], 1); return;
                case GlyphId.GlyphSize1D2: bgc.Grid = new CGrid(args[0], 1, 1, 2); return;
                case GlyphId.GlyphSize2D2: bgc.Grid = new CGrid(args[0], args[1], 1, 2); return;
                case GlyphId.GlyphSize3D2: bgc.Grid = new CGrid(args[0], args[1], args[2], 2); return;
                case GlyphId.GlyphSize1D3: bgc.Grid = new CGrid(args[0], 1, 1, 3); return;
                case GlyphId.GlyphSize2D3: bgc.Grid = new CGrid(args[0], args[1], 1, 3); return;
                case GlyphId.GlyphSize3D3: bgc.Grid = new CGrid(args[0], args[1], args[2], 3); return;
                case GlyphId.GlyphSize1D4: bgc.Grid = new CGrid(args[0], 1, 1, 3); return;
                case GlyphId.GlyphSize2D4: bgc.Grid = new CGrid(args[0], args[1], 1, 4); return;
                case GlyphId.GlyphSize3D4: bgc.Grid = new CGrid(args[0], args[1], args[2], 4); return;

                case GlyphId.GlyphPenColorD1: bgc.Pen.SetColor(args[0]); return;
                case GlyphId.GlyphPenColorD2: bgc.Pen.SetColor(args[0], args[1]); return;
                case GlyphId.GlyphPenColorD3: bgc.Pen.SetColor(args[0], args[1], args[2]); return;
                case GlyphId.GlyphPenColorD4: bgc.Pen.SetColor(args[0], args[1], args[2], args[3]); return;
                case GlyphId.GlyphPenWidth: bgc.Pen.SetSize(args[0], bgc.Pen.Height, bgc.Pen.Depth); return;
                case GlyphId.GlyphPenHeight: bgc.Pen.SetSize(bgc.Pen.Width, args[0], bgc.Pen.Depth); return;
                case GlyphId.GlyphPenDepth: bgc.Pen.SetSize(bgc.Pen.Width, bgc.Pen.Height, args[0]); return;
                case GlyphId.GlyphPenSize: bgc.Pen.SetSize(args[0], args[1], args[2]); return;
                case GlyphId.GlyphPenHatch: bgc.Pen.SetHatch(args[0], args[1], args[2]); return;
                case GlyphId.GlyphPenShape: bgc.Pen.SetShape(args[0]); return;
                case GlyphId.GlyphPenTex: bgc.Pen.SetTexture(args[0]); return;
                case GlyphId.GlyphPenAnim: bgc.Pen.SetAnimation(args[0]); return;
                case GlyphId.GlyphPenPhysics: bgc.Pen.SetPhysics(args[0]); return;

                case GlyphId.GlyphPlot: Painter.DrawPen(bgc, args[0], args[1], args[2]); return;
                case GlyphId.GlyphClear: Painter.DrawClear(bgc); return;

                case GlyphId.GlyphWallCube: Painter.DrawWallCube(bgc, args[0]); return;

                case GlyphId.GlyphLine: Painter.DrawLine3D(bgc, args[0], args[1], args[2], args[3], args[4], args[5]); return;
                case GlyphId.GlyphStairs: Painter.DrawStairs(bgc, args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7], args[8]); return;
                case GlyphId.GlyphFillRect: Painter.DrawFillRect(bgc, args[0], args[1], args[2], args[3], args[4], args[5]); return;
                case GlyphId.GlyphRect: Painter.DrawHollowRect(bgc, args[0], args[1], args[2], args[3], args[4], args[5]); return;
                case GlyphId.GlyphScissor: bgc.Grid.SetScissor(new CRect(args[0], args[1], args[2], args[3], args[4], args[5])); return;

                case GlyphId.GlyphCircle2D:
                case GlyphId.GlyphCircle2Dxy: Painter.DrawCircle2DAnyAxis(bgc, PenTwist.XYaxis, args[0], args[1], args[2], args[3]); return;
                case GlyphId.GlyphCircle2Dyz: Painter.DrawCircle2DAnyAxis(bgc, PenTwist.YZaxis, args[2], args[1], args[0], args[3]); return;
                case GlyphId.GlyphCircle2Dxz: Painter.DrawCircle2DAnyAxis(bgc, PenTwist.XZaxis, args[0], args[2], args[1], args[3]); return;
                case GlyphId.GlyphFillCircle2D:
                case GlyphId.GlyphFillCircle2Dxy: Painter.DrawFillCircle2D(bgc, PenTwist.XYaxis, args[0], args[1], args[2], args[3]); return;
                case GlyphId.GlyphFillCircle2Dyz: Painter.DrawFillCircle2D(bgc, PenTwist.YZaxis, args[2], args[1], args[0], args[3]); return;
                case GlyphId.GlyphFillCircle2Dxz: Painter.DrawFillCircle2D(bgc, PenTwist.XZaxis, args[0], args[2], args[1], args[3]); return;

                case GlyphId.GlyphArcXy: Painter.DrawArc(bgc, PenTwist.XYaxis, args[0], args[1], args[2], args[3], args[4], args[5]); return;
                case GlyphId.GlyphArcYz: Painter.DrawArc(bgc, PenTwist.YZaxis, args[2], args[1], args[0], args[3], args[4], args[5]); return;
                case GlyphId.GlyphArcXz: Painter.DrawArc(bgc, PenTwist.XZaxis, args[0], args[2], args[1], args[3], args[4], args[5]); return;

                case GlyphId.GlyphMaskRect: Painter.DrawMaskHollowRect(bgc, args[0], args[1], args[2], args[3], args[4], args[5], args[6]); return;
                case GlyphId.GlyphMaskFillRect: Painter.DrawMaskFillRect(bgc, args[0], args[1], args[2], args[3], args[4], args[5], args[6]); return;
                case GlyphId.GlyphTubeXy: Painter.DrawMaskFillRect(bgc, args[0], args[1], args[2], args[3], args[4], args[5], (int)(CubeFaceMask.Left | CubeFaceMask.Right | CubeFaceMask.Top | CubeFaceMask.Bottom)); return;
                case GlyphId.GlyphTubeYz: Painter.DrawMaskFillRect(bgc, args[0], args[1], args[2], args[3], args[4], args[5], (int)(CubeFaceMask.Left | CubeFaceMask.Right | CubeFaceMask.Front | CubeFaceMask.Back)); return;
                case GlyphId.GlyphTubeXz: Painter.DrawMaskFillRect(bgc, args[0], args[1], args[2], args[3], args[4], args[5], (int)(CubeFaceMask.Front | CubeFaceMask.Back | CubeFaceMask.Top | CubeFaceMask.Bottom)); return;

                case GlyphId.GlyphCircle3D: Painter.DrawCircle3D(bgc, args[0], args[1], args[2], args[3]); return;
                case GlyphId.GlyphOval3D: Painter.DrawOval3D(bgc, args[0], args[1], args[2], args[3], args[4], args[5]); return;
                case GlyphId.GlyphDiamond2D: Painter.Diamond2D(bgc, args[0], args[1], args[2], args[3], args[4]); return;
                case GlyphId.GlyphQuad2D: Painter.Quad2D(bgc, args[0], args[1], args[2], args[3], args[4], args[5]); return;
                case GlyphId.GlyphPyramid3D: Painter.Pyramid3D(bgc, args[0], args[1], args[2], args[3], args[4], args[5]); return;

                case GlyphId.GlyphFillCircle3D: Painter.DrawFillCircle3D(bgc, args[0], args[1], args[2], args[3]); return;
                case GlyphId.GlyphTriangle: Painter.DrawTriangle3D(bgc, args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7], args[8]); return;
                case GlyphId.GlyphFillTriangle2D: Painter.DrawFillTriangle2D(bgc, args[0], args[1], args[2], args[3], args[4], args[5]); return;
                case GlyphId.GlyphPolygon: Painter.DrawPolygon(bgc, PenTwist.XYaxis, args[0], args[1], args[2], args[3], args[4]); return;
                case GlyphId.GlyphText:  Painter.DrawLetter(bgc, PenTwist.XYaxis, args[0], args[1], args[2], args[3], true); return;

                case GlyphId.GlyphExtrudeX: Painter.ExtrudeX(bgc, args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7]); return;
                case GlyphId.GlyphExtrudeY: Painter.ExtrudeY(bgc, args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7]); return;
                case GlyphId.GlyphExtrudeZ: Painter.ExtrudeZ(bgc, args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7]); return;
                case GlyphId.GlyphExtrudeLine: Painter.Extrude(bgc, args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7], args[8]); return;

                case GlyphId.GlyphUpV: Painter.UpV(bgc, args[0], args[1], args[2], args[3], args[4]); return;

                case GlyphId.GlyphImgFlipX: Painter.FlipX(bgc); return;
                case GlyphId.GlyphImgFlipY: Painter.FlipY(bgc); return;
                case GlyphId.GlyphImgFlipZ: Painter.FlipZ(bgc); return;
                case GlyphId.GlyphImgMirrorX: Painter.MirrorX(bgc); return;
                case GlyphId.GlyphImgMirrorY: Painter.MirrorY(bgc); return;
                case GlyphId.GlyphImgMirrorZ: Painter.MirrorZ(bgc); return;
                case GlyphId.GlyphImgRotX: Painter.RotateX(bgc); return;
                case GlyphId.GlyphImgRotY: Painter.RotateY(bgc); return;
                case GlyphId.GlyphImgRotZ: Painter.RotateZ(bgc); return;

                case GlyphId.GlyphImgPalettize: Painter.Palettize(bgc, bgc.GetPalette(args[0])); return;
                case GlyphId.GlyphImgInvert: Painter.Invert(bgc); return;
                case GlyphId.GlyphImgGrayscale: Painter.Grayscale(bgc); return;
                case GlyphId.GlyphImgColorize: Painter.Colorize(bgc, args[0], args[1]); return;
                case GlyphId.GlyphImgHueshift: Painter.HueShift(bgc, args[0]); return;
                case GlyphId.GlyphImgSaturate: Painter.Saturation(bgc, args[0]); return;
                case GlyphId.GlyphImgBrightness: Painter.Brightness(bgc, args[0]); return;

                case GlyphId.GlyphImgShadeX: Painter.Shade(bgc, 0, args[0], args[1], args[2], args[3], args[4], args[5]); return;
                case GlyphId.GlyphImgShadeY: Painter.Shade(bgc, 1, args[0], args[1], args[2], args[3], args[4], args[5]); return;
                case GlyphId.GlyphImgShadeZ: Painter.Shade(bgc, 2, args[0], args[1], args[2], args[3], args[4], args[5]); return;
                case GlyphId.GlyphImgEdge:
                    Painter.EdgeColor(bgc, 0, args[0], args[1], args[2], args[3]);
                    Painter.EdgeColor(bgc, 1, args[0], args[1], args[2], args[3]);
                    Painter.EdgeColor(bgc, 2, args[0], args[1], args[2], args[3]);
                    return;
                case GlyphId.GlyphImgEdgeX: Painter.EdgeColor(bgc, 0, args[0], args[1], args[2], args[3]); return;
                case GlyphId.GlyphImgEdgeY: Painter.EdgeColor(bgc, 1, args[0], args[1], args[2], args[3]); return;
                case GlyphId.GlyphImgEdgeZ: Painter.EdgeColor(bgc, 2, args[0], args[1], args[2], args[3]); return;

                case GlyphId.GlyphFilterBlur: Painter.ApplyFilterBlur(bgc); return;
                case GlyphId.GlyphFilterMotion: Painter.ApplyFilterMotion(bgc); return;
                case GlyphId.GlyphFilterEdgeHor: Painter.ApplyFilterEdgeHorizontal(bgc); return;
                case GlyphId.GlyphFilterEdgeVer: Painter.ApplyFilterEdgeVertical(bgc); return;
                case GlyphId.GlyphFilterEdgeAll: Painter.ApplyFilterEdgeAll(bgc); return;
                case GlyphId.GlyphFilterSharpen: Painter.ApplyFilterSharpen(bgc); return;
                case GlyphId.GlyphFilterEmboss: Painter.ApplyFilterEmboss(bgc); return;
                case GlyphId.GlyphFilterSmooth: Painter.ApplyFilterSmooth(bgc); return;

                case GlyphId.GlyphPal1D: bgc.AddPalette(new CGrid(args[0], 1, 1, bgc.Grid.Bpp)); return;
                case GlyphId.GlyphPal2D: bgc.AddPalette(new CGrid(args[0], args[1], 1, bgc.Grid.Bpp)); return;
                case GlyphId.GlyphPal3D: bgc.AddPalette(new CGrid(args[0], args[1], args[2], bgc.Grid.Bpp)); return;
                case GlyphId.GlyphPalFromVal1D: bgc.AddPalette(Producer.CreateGridFromValues(args[0], 1, 1, bgc.Grid.Bpp, args)); return;
                case GlyphId.GlyphPalFromVal2D: bgc.AddPalette(Producer.CreateGridFromValues(args[0], args[1], 1, bgc.Grid.Bpp, args)); return;
                case GlyphId.GlyphPalFromVal3D: bgc.AddPalette(Producer.CreateGridFromValues(args[0], args[1], args[2], bgc.Grid.Bpp, args)); return;
                case GlyphId.GlyphPalFromGrid1D: bgc.AddPalette(Producer.CreateGridFromRectangle(bgc.Grid, args[0], 0, 0, args[1], 1, 1)); return;
                case GlyphId.GlyphPalFromGrid2D: bgc.AddPalette(Producer.CreateGridFromRectangle(bgc.Grid, args[0], args[1], 0, args[2], args[3], 1)); return;
                case GlyphId.GlyphPalFromGrid3D: bgc.AddPalette(Producer.CreateGridFromRectangle(bgc.Grid, args[0], args[1], args[2], args[3], args[4], args[5])); return;
                case GlyphId.GlyphPalGen1D1: Painter.PalGen1D(bgc.GetPalette(args[0]), args[1], 0, 0, 0, args[2], 0, 0, 0); return;
                case GlyphId.GlyphPalGen1D2: Painter.PalGen1D(bgc.GetPalette(args[0]), args[1], args[2], 0, 0, args[3], args[4], 0, 0); return;
                case GlyphId.GlyphPalGen1D3: Painter.PalGen1D(bgc.GetPalette(args[0]), args[1], args[2], args[3], 0, args[4], args[5], args[6], 0); return;
                case GlyphId.GlyphPalGen1D4: Painter.PalGen1D(bgc.GetPalette(args[0]), args[1], args[2], args[3], args[4], args[5], args[6], args[7], args[8]); return;

                case GlyphId.GlyphBlit1D: Painter.Blit(bgc, bgc.GetPalette(args[0]), args[1], 0, 0); return;
                case GlyphId.GlyphBlit2D: Painter.Blit(bgc, bgc.GetPalette(args[0]), args[1], args[2], 0); return;
                case GlyphId.GlyphBlit3D: Painter.Blit(bgc, bgc.GetPalette(args[0]), args[1], args[2], args[3]); return;
                case GlyphId.GlyphRectBlit1D: Painter.RectBlit(bgc, bgc.GetPalette(args[0]), args[1], 0, 0, args[2], 0, 0); return;
                case GlyphId.GlyphRectBlit2D: Painter.RectBlit(bgc, bgc.GetPalette(args[0]), args[1], args[2], 0, args[3], args[4], 0); return;
                case GlyphId.GlyphRectBlit3D: Painter.RectBlit(bgc, bgc.GetPalette(args[0]), args[1], args[2], args[3], args[4], args[5], args[6]); return;
                case GlyphId.GlyphBlendBlit1D: Painter.BlendBlit(bgc, bgc.GetPalette(args[0]), args[1], 0, 0, args[2], 0, 0, args[3]); return;
                case GlyphId.GlyphBlendBlit2D: Painter.BlendBlit(bgc, bgc.GetPalette(args[0]), args[1], args[2], 0, args[3], args[4], 0, args[5]); return;
                case GlyphId.GlyphBlendBlit3D: Painter.BlendBlit(bgc, bgc.GetPalette(args[0]), args[1], args[2], args[3], args[4], args[5], args[6], args[7]); return;

                case GlyphId.GlyphArchPut:  Painter.CopyInto(bgc, args[0], args[1], args[2], args[3]); return;
                case GlyphId.GlyphArchRect: Painter.ArchRect(bgc, args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7], args[8]); return;
                case GlyphId.GlyphArchLine: Painter.ArchLine(bgc, args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7], args[8]); return;

                case GlyphId.GlyphSpawn:
                    bgc.SpawnPoint.X = args[0];
                    bgc.SpawnPoint.Y = args[1];
                    bgc.SpawnPoint.Z = args[2];
                    return;

                case GlyphId.GlyphGenesis3D:
                    {
                        int size = args[0];
                        bgc.Grid = new CGrid(args[0], args[0], args[0], 4);
                        bgc.Grid.InhibitCodeTracking();
                        bgc.Pen.SetColor(255, 255, 255, 255);
                        bgc.SpawnPoint.X = size / 2.0f;
                        bgc.SpawnPoint.Y = 7;
                        bgc.SpawnPoint.Z = size / 2.0f;
                        bgc.Grid.AllowCodeTracking();
                        return;
                    }

                case GlyphId.GlyphAlien: Painter.DrawAlien(bgc, args[0], args[1], args[2], args[3]); return;
                case GlyphId.GlyphStar: Painter.DrawStar(bgc, args[0], args[1], args[2], args[3]); return;
                case GlyphId.GlyphSupports: Painter.DrawSupports(bgc); return;
                case GlyphId.GlyphCornerSupports: Painter.DrawCorners(bgc, args[0], args[1], args[2], args[3], args[4], args[5]); return;
            }
        }

        //TokensToContext a list of glyph tokenList against a Grid context
        static public void ExecuteGlyphTokens(IByteGridContext bgc, ITokenList glyphTokens)
        {
            byte line = 0;
            
            if (glyphTokens == null) //nothing to do
                return;

            foreach (IToken tok in glyphTokens)
            {
                ExecuteGlyph(bgc, line, tok.Glyph, tok.GetArgs());
                line++;
            }
        }
    }
}

