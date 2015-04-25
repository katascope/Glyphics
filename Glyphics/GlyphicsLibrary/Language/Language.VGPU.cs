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
        internal static void ExecuteGlyph(IGridContext bgc, byte glyphicsCodeLine, IGlyph glyph, byte[] args)
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
                case GlyphId.PrimaNop: return;

                case GlyphId.PrimaSize: bgc.Grid = new CGrid(args[0], args[1], args[2], 4); return; //same as Size3D4
                case GlyphId.PrimaRgba: bgc.Pen.SetColor(args[0], args[1], args[2], args[3]); return; //same as PenColorD4
                case GlyphId.PrimaRect: Painter.DrawFillRect(bgc, args[0], args[1], args[2], args[3], args[4], args[5]); return; //same as FillRect

                case GlyphId.Size1D1: bgc.Grid = new CGrid(args[0], 1, 1, 1); return;
                case GlyphId.Size2D1: bgc.Grid = new CGrid(args[0], args[1], 1, 1); return;
                case GlyphId.Size3D1: bgc.Grid = new CGrid(args[0], args[1], args[2], 1); return;
                case GlyphId.Size1D2: bgc.Grid = new CGrid(args[0], 1, 1, 2); return;
                case GlyphId.Size2D2: bgc.Grid = new CGrid(args[0], args[1], 1, 2); return;
                case GlyphId.Size3D2: bgc.Grid = new CGrid(args[0], args[1], args[2], 2); return;
                case GlyphId.Size1D3: bgc.Grid = new CGrid(args[0], 1, 1, 3); return;
                case GlyphId.Size2D3: bgc.Grid = new CGrid(args[0], args[1], 1, 3); return;
                case GlyphId.Size3D3: bgc.Grid = new CGrid(args[0], args[1], args[2], 3); return;
                case GlyphId.Size1D4: bgc.Grid = new CGrid(args[0], 1, 1, 3); return;
                case GlyphId.Size2D4: bgc.Grid = new CGrid(args[0], args[1], 1, 4); return;
                case GlyphId.Size3D4: bgc.Grid = new CGrid(args[0], args[1], args[2], 4); return;

                case GlyphId.PenColorD1: bgc.Pen.SetColor(args[0]); return;
                case GlyphId.PenColorD2: bgc.Pen.SetColor(args[0], args[1]); return;
                case GlyphId.PenColorD3: bgc.Pen.SetColor(args[0], args[1], args[2]); return;
                case GlyphId.PenColorD4: bgc.Pen.SetColor(args[0], args[1], args[2], args[3]); return;
                case GlyphId.PenWidth: bgc.Pen.SetSize(args[0], bgc.Pen.Height, bgc.Pen.Depth); return;
                case GlyphId.PenHeight: bgc.Pen.SetSize(bgc.Pen.Width, args[0], bgc.Pen.Depth); return;
                case GlyphId.PenDepth: bgc.Pen.SetSize(bgc.Pen.Width, bgc.Pen.Height, args[0]); return;
                case GlyphId.PenSize: bgc.Pen.SetSize(args[0], args[1], args[2]); return;
                case GlyphId.PenHatch: bgc.Pen.SetHatch(args[0], args[1], args[2]); return;
                case GlyphId.PenShape: bgc.Pen.SetShape(args[0]); return;
                case GlyphId.PenTex: bgc.Pen.SetTexture(args[0]); return;
                case GlyphId.PenAnim: bgc.Pen.SetAnimation(args[0]); return;
                case GlyphId.PenPhysics: bgc.Pen.SetPhysics(args[0]); return;

                case GlyphId.Plot: Painter.DrawPen(bgc, args[0], args[1], args[2]); return;
                case GlyphId.Clear: Painter.DrawClear(bgc); return;

                case GlyphId.WallCube: Painter.DrawWallCube(bgc, args[0]); return;

                case GlyphId.Line: Painter.DrawLine3D(bgc, args[0], args[1], args[2], args[3], args[4], args[5]); return;
                case GlyphId.Stairs: Painter.DrawStairs(bgc, args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7], args[8]); return;
                case GlyphId.FillRect: Painter.DrawFillRect(bgc, args[0], args[1], args[2], args[3], args[4], args[5]); return;
                case GlyphId.Rect: Painter.DrawHollowRect(bgc, args[0], args[1], args[2], args[3], args[4], args[5]); return;
                case GlyphId.Scissor: bgc.Grid.SetScissor(new CRect(args[0], args[1], args[2], args[3], args[4], args[5])); return;

                case GlyphId.Circle2D:
                case GlyphId.Circle2Dxy: Painter.DrawCircle2DAnyAxis(bgc, PenTwist.XYaxis, args[0], args[1], args[2], args[3]); return;
                case GlyphId.Circle2Dyz: Painter.DrawCircle2DAnyAxis(bgc, PenTwist.YZaxis, args[2], args[1], args[0], args[3]); return;
                case GlyphId.Circle2Dxz: Painter.DrawCircle2DAnyAxis(bgc, PenTwist.XZaxis, args[0], args[2], args[1], args[3]); return;
                case GlyphId.FillCircle2D:
                case GlyphId.FillCircle2Dxy: Painter.DrawFillCircle2D(bgc, PenTwist.XYaxis, args[0], args[1], args[2], args[3]); return;
                case GlyphId.FillCircle2Dyz: Painter.DrawFillCircle2D(bgc, PenTwist.YZaxis, args[2], args[1], args[0], args[3]); return;
                case GlyphId.FillCircle2Dxz: Painter.DrawFillCircle2D(bgc, PenTwist.XZaxis, args[0], args[2], args[1], args[3]); return;

                case GlyphId.ArcXy: Painter.DrawArc(bgc, PenTwist.XYaxis, args[0], args[1], args[2], args[3], args[4], args[5]); return;
                case GlyphId.ArcYz: Painter.DrawArc(bgc, PenTwist.YZaxis, args[2], args[1], args[0], args[3], args[4], args[5]); return;
                case GlyphId.ArcXz: Painter.DrawArc(bgc, PenTwist.XZaxis, args[0], args[2], args[1], args[3], args[4], args[5]); return;

                case GlyphId.MaskRect: Painter.DrawMaskHollowRect(bgc, args[0], args[1], args[2], args[3], args[4], args[5], args[6]); return;
                case GlyphId.MaskFillRect: Painter.DrawMaskFillRect(bgc, args[0], args[1], args[2], args[3], args[4], args[5], args[6]); return;
                case GlyphId.TubeXy: Painter.DrawMaskFillRect(bgc, args[0], args[1], args[2], args[3], args[4], args[5], (int)(CubeFaceMask.Left | CubeFaceMask.Right | CubeFaceMask.Top | CubeFaceMask.Bottom)); return;
                case GlyphId.TubeYz: Painter.DrawMaskFillRect(bgc, args[0], args[1], args[2], args[3], args[4], args[5], (int)(CubeFaceMask.Left | CubeFaceMask.Right | CubeFaceMask.Front | CubeFaceMask.Back)); return;
                case GlyphId.TubeXz: Painter.DrawMaskFillRect(bgc, args[0], args[1], args[2], args[3], args[4], args[5], (int)(CubeFaceMask.Front | CubeFaceMask.Back | CubeFaceMask.Top | CubeFaceMask.Bottom)); return;

                case GlyphId.Circle3D: Painter.DrawCircle3D(bgc, args[0], args[1], args[2], args[3]); return;
                case GlyphId.Oval3D: Painter.DrawOval3D(bgc, args[0], args[1], args[2], args[3], args[4], args[5]); return;
                case GlyphId.Diamond2D: Painter.Diamond2D(bgc, args[0], args[1], args[2], args[3], args[4]); return;
                case GlyphId.Quad2D: Painter.Quad2D(bgc, args[0], args[1], args[2], args[3], args[4], args[5]); return;
                case GlyphId.Pyramid3D: Painter.Pyramid3D(bgc, args[0], args[1], args[2], args[3], args[4], args[5]); return;

                case GlyphId.FillCircle3D: Painter.DrawFillCircle3D(bgc, args[0], args[1], args[2], args[3]); return;
                case GlyphId.Triangle: Painter.DrawTriangle3D(bgc, args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7], args[8]); return;
                case GlyphId.FillTriangle2D: Painter.DrawFillTriangle2D(bgc, args[0], args[1], args[2], args[3], args[4], args[5]); return;
                case GlyphId.Polygon: Painter.DrawPolygon(bgc, PenTwist.XYaxis, args[0], args[1], args[2], args[3], args[4]); return;
                case GlyphId.Text:  Painter.DrawLetter(bgc, PenTwist.XYaxis, args[0], args[1], args[2], args[3], true); return;

                case GlyphId.ExtrudeX: Painter.ExtrudeX(bgc, args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7]); return;
                case GlyphId.ExtrudeY: Painter.ExtrudeY(bgc, args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7]); return;
                case GlyphId.ExtrudeZ: Painter.ExtrudeZ(bgc, args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7]); return;
                case GlyphId.ExtrudeLine: Painter.Extrude(bgc, args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7], args[8]); return;

                case GlyphId.UpV: Painter.UpV(bgc, args[0], args[1], args[2], args[3], args[4]); return;

                case GlyphId.ImgFlipX: Painter.FlipX(bgc); return;
                case GlyphId.ImgFlipY: Painter.FlipY(bgc); return;
                case GlyphId.ImgFlipZ: Painter.FlipZ(bgc); return;
                case GlyphId.ImgMirrorX: Painter.MirrorX(bgc); return;
                case GlyphId.ImgMirrorY: Painter.MirrorY(bgc); return;
                case GlyphId.ImgMirrorZ: Painter.MirrorZ(bgc); return;
                case GlyphId.ImgRotX: Painter.RotateX(bgc); return;
                case GlyphId.ImgRotY: Painter.RotateY(bgc); return;
                case GlyphId.ImgRotZ: Painter.RotateZ(bgc); return;

                case GlyphId.ImgPalettize: Painter.Palettize(bgc, bgc.GetPalette(args[0])); return;
                case GlyphId.ImgInvert: Painter.Invert(bgc); return;
                case GlyphId.ImgGrayscale: Painter.Grayscale(bgc); return;
                case GlyphId.ImgColorize: Painter.Colorize(bgc, args[0], args[1]); return;
                case GlyphId.ImgHueshift: Painter.HueShift(bgc, args[0]); return;
                case GlyphId.ImgSaturate: Painter.Saturation(bgc, args[0]); return;
                case GlyphId.ImgBrightness: Painter.Brightness(bgc, args[0]); return;

                case GlyphId.ImgShadeX: Painter.Shade(bgc, 0, args[0], args[1], args[2], args[3], args[4], args[5]); return;
                case GlyphId.ImgShadeY: Painter.Shade(bgc, 1, args[0], args[1], args[2], args[3], args[4], args[5]); return;
                case GlyphId.ImgShadeZ: Painter.Shade(bgc, 2, args[0], args[1], args[2], args[3], args[4], args[5]); return;
                case GlyphId.ImgEdge:
                    Painter.EdgeColor(bgc, 0, args[0], args[1], args[2], args[3]);
                    Painter.EdgeColor(bgc, 1, args[0], args[1], args[2], args[3]);
                    Painter.EdgeColor(bgc, 2, args[0], args[1], args[2], args[3]);
                    return;
                case GlyphId.ImgEdgeX: Painter.EdgeColor(bgc, 0, args[0], args[1], args[2], args[3]); return;
                case GlyphId.ImgEdgeY: Painter.EdgeColor(bgc, 1, args[0], args[1], args[2], args[3]); return;
                case GlyphId.ImgEdgeZ: Painter.EdgeColor(bgc, 2, args[0], args[1], args[2], args[3]); return;

                case GlyphId.FilterBlur: Painter.ApplyFilterBlur(bgc); return;
                case GlyphId.FilterMotion: Painter.ApplyFilterMotion(bgc); return;
                case GlyphId.FilterEdgeHor: Painter.ApplyFilterEdgeHorizontal(bgc); return;
                case GlyphId.FilterEdgeVer: Painter.ApplyFilterEdgeVertical(bgc); return;
                case GlyphId.FilterEdgeAll: Painter.ApplyFilterEdgeAll(bgc); return;
                case GlyphId.FilterSharpen: Painter.ApplyFilterSharpen(bgc); return;
                case GlyphId.FilterEmboss: Painter.ApplyFilterEmboss(bgc); return;
                case GlyphId.FilterSmooth: Painter.ApplyFilterSmooth(bgc); return;

                case GlyphId.Pal1D: bgc.AddPalette(new CGrid(args[0], 1, 1, bgc.Grid.Bpp)); return;
                case GlyphId.Pal2D: bgc.AddPalette(new CGrid(args[0], args[1], 1, bgc.Grid.Bpp)); return;
                case GlyphId.Pal3D: bgc.AddPalette(new CGrid(args[0], args[1], args[2], bgc.Grid.Bpp)); return;
                case GlyphId.PalFromVal1D: bgc.AddPalette(GridCreator.CreateGridFromValues(args[0], 1, 1, bgc.Grid.Bpp, args)); return;
                case GlyphId.PalFromVal2D: bgc.AddPalette(GridCreator.CreateGridFromValues(args[0], args[1], 1, bgc.Grid.Bpp, args)); return;
                case GlyphId.PalFromVal3D: bgc.AddPalette(GridCreator.CreateGridFromValues(args[0], args[1], args[2], bgc.Grid.Bpp, args)); return;
                case GlyphId.PalFromGrid1D: bgc.AddPalette(GridCreator.CreateGridFromRectangle(bgc.Grid, args[0], 0, 0, args[1], 1, 1)); return;
                case GlyphId.PalFromGrid2D: bgc.AddPalette(GridCreator.CreateGridFromRectangle(bgc.Grid, args[0], args[1], 0, args[2], args[3], 1)); return;
                case GlyphId.PalFromGrid3D: bgc.AddPalette(GridCreator.CreateGridFromRectangle(bgc.Grid, args[0], args[1], args[2], args[3], args[4], args[5])); return;
                case GlyphId.PalGen1D1: Painter.PalGen1D(bgc.GetPalette(args[0]), args[1], 0, 0, 0, args[2], 0, 0, 0); return;
                case GlyphId.PalGen1D2: Painter.PalGen1D(bgc.GetPalette(args[0]), args[1], args[2], 0, 0, args[3], args[4], 0, 0); return;
                case GlyphId.PalGen1D3: Painter.PalGen1D(bgc.GetPalette(args[0]), args[1], args[2], args[3], 0, args[4], args[5], args[6], 0); return;
                case GlyphId.PalGen1D4: Painter.PalGen1D(bgc.GetPalette(args[0]), args[1], args[2], args[3], args[4], args[5], args[6], args[7], args[8]); return;

                case GlyphId.Blit1D: Painter.Blit(bgc, bgc.GetPalette(args[0]), args[1], 0, 0); return;
                case GlyphId.Blit2D: Painter.Blit(bgc, bgc.GetPalette(args[0]), args[1], args[2], 0); return;
                case GlyphId.Blit3D: Painter.Blit(bgc, bgc.GetPalette(args[0]), args[1], args[2], args[3]); return;
                case GlyphId.RectBlit1D: Painter.RectBlit(bgc, bgc.GetPalette(args[0]), args[1], 0, 0, args[2], 0, 0); return;
                case GlyphId.RectBlit2D: Painter.RectBlit(bgc, bgc.GetPalette(args[0]), args[1], args[2], 0, args[3], args[4], 0); return;
                case GlyphId.RectBlit3D: Painter.RectBlit(bgc, bgc.GetPalette(args[0]), args[1], args[2], args[3], args[4], args[5], args[6]); return;
                case GlyphId.BlendBlit1D: Painter.BlendBlit(bgc, bgc.GetPalette(args[0]), args[1], 0, 0, args[2], 0, 0, args[3]); return;
                case GlyphId.BlendBlit2D: Painter.BlendBlit(bgc, bgc.GetPalette(args[0]), args[1], args[2], 0, args[3], args[4], 0, args[5]); return;
                case GlyphId.BlendBlit3D: Painter.BlendBlit(bgc, bgc.GetPalette(args[0]), args[1], args[2], args[3], args[4], args[5], args[6], args[7]); return;

                case GlyphId.ArchPut:  Painter.CopyInto(bgc, args[0], args[1], args[2], args[3]); return;
                case GlyphId.ArchRect: Painter.ArchRect(bgc, args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7], args[8]); return;
                case GlyphId.ArchLine: Painter.ArchLine(bgc, args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7], args[8]); return;

                case GlyphId.Spawn:
                    bgc.SpawnPoint.X = args[0];
                    bgc.SpawnPoint.Y = args[1];
                    bgc.SpawnPoint.Z = args[2];
                    return;

                case GlyphId.Genesis3D:
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

                case GlyphId.Alien: Painter.DrawAlien(bgc, args[0], args[1], args[2], args[3]); return;
                case GlyphId.Star: Painter.DrawStar(bgc, args[0], args[1], args[2], args[3]); return;
                case GlyphId.Supports: Painter.DrawSupports(bgc); return;
                case GlyphId.CornerSupports: Painter.DrawCorners(bgc, args[0], args[1], args[2], args[3], args[4], args[5]); return;

                case GlyphId.FillTriangle: Painter.DrawFillTriangle3D(bgc, args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7], args[8]); return;

                case GlyphId.Shadows: Painter.DrawShadows(bgc); return;
                case GlyphId.Maze: Painter.DrawMaze(bgc, args[0], args[1], args[2], args[3], args[4], args[5]); return;

            }
        }

        //TokensToContext a list of glyph tokenList against a Grid context
        static public void ExecuteGlyphTokens(IGridContext bgc, ITokenList glyphTokens)
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

