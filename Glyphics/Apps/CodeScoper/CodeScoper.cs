#region Copyright
/*Copyright (c) 2015, Katascope
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/
#endregion
using System;
using GlyphicsLibrary;

namespace CodeScoper
{
    /* Example: CodeScoper
     * Purpose: Utility for showing detailed information from GlyphicsApi
     */
    class CodeScoper
    {
        static void Main()
        {
            //const string codeString = "Maze,Size3D1 20 5 20;PenColorD1 1;Maze 0 0 0 0 20 20;UpV 2 1 0 0 0";
            //const string codeString = "Text2D,Size3D1 8 9 1;PenColorD1 1;Text 2 2 0 65;ImgFlipY;Rect 0 0 0 7 8 1";
            //const string codeString = "Size3D1 4 4 3;PenColorD1 56;FillTriangle 1 1 0 3 3 1 2 3 2";
            //const string codeString = "Scoper,Size3D1 4 4 1;PenColorD4 63 127 255 255;WallCube 1;PenColorD4 255 255 127 255;Plot 2 2 0;Shadows";

            const string codeString = @"PrintableNexus,Size3D4 64 64 64;Spawn 25 5 25;PenColorD4 31 127 255 255;
            PenShape 1;WallCube 1;PenColorD4 255 255 255 255;
PenSize 1 2 1;Rect 0 0 0 31 0 31;Rect 0 0 32 31 0 63;Rect 32 0 0 63 0 31;Rect 32 0 32 63 0 63;Rect 16 0 16 48 0 48;
PenSize 1 1 1;PenColorD4 31 127 255 255;FillRect 17 0 17 47 0 47;FillRect 16 1 49 48 16 63;
PenColorD4 0 0 0 0;
FillRect 17 1 49 47 15 63;
Rect 0 1 0 63 63 63;
ImgEdgeX 255 255 255 255;ImgEdgeY 255 255 255 255;ImgEdgeZ 255 255 255 255;
PenShape 2;PenColorD3 255 255 255;FillRect 26 17 51 36 28 62;
PenColorD3 127 255 127;FillRect 2 1 2 13 12 13; 
PenColorD3 255 127 127;FillRect 2 1 18 13 12 29;
PenColorD3 127 127 255;FillRect 2 1 34 13 12 45;
PenColorD3 255 255 127;FillRect 2 1 50 13 12 61;

PenColorD3 255 127 255;FillRect 18 1 2 29 12 13;
ImgMirrorX
";

            SuperDebug(codeString);
        }
        static void SuperDebug(string codeString)
        {
            ICode code = GlyphicsApi.CreateCode(codeString);
            Console.WriteLine("Code: {0}\n", codeString);

            ICodename codename = GlyphicsApi.CodeToCodename(code);
            Console.WriteLine("Codename: {0}\n", codename);

            IGrid grid = GlyphicsApi.CodeToGrid(code);
            Console.WriteLine("Grid: {0} {1} non-empty\n", grid, grid.CountNonZero());

            IGridList grids = GlyphicsApi.CodeToThumbnailed(code);
            Console.WriteLine("Thumbnails: {0}", grids);

            foreach (IGrid g in grids)
            {
                IGrid gOblique = GlyphicsApi.Renderer.RenderObliqueCells(g);
                GlyphicsApi.SaveFlatPng("..\\..\\foo-" + g.SizeX + ".png", gOblique);
            }

            /*
            string bytesDesc = GlyphicsApi.BytesToString(grid.CloneData());Console.WriteLine("GridBytes:\n{0}\n", bytesDesc);
            IRectList rects = GlyphicsApi.GridToRects(grid);Console.WriteLine("Rects: {0}\n{1}", rects.Count, rects);
            string serialized = GlyphicsApi.RectsToSerializedRects(rects).SerializedData;
            Console.WriteLine("Serialized Rects: (len={0})\n{1}\n", serialized.Length, serialized);
            Console.WriteLine("Preserialized Code:\n{0}\n", codeString + serialized);
            IQuadList quads = GlyphicsApi.RectsToQuads(rects); Console.WriteLine("Quads: {0}\n", quads);
            ITriangles triangles = GlyphicsApi.QuadsToTriangles(quads); Console.WriteLine("Triangles: {0}\n", triangles);*/
            //Console.WriteLine("2d view:\n{0}", GlyphicsApi.GridToHexDescription(grid));
            //Console.WriteLine("3d view:\n{0}", GlyphicsApi.GridTo3DDescription(grid, 0, 0, 0));
        }
    }
}
