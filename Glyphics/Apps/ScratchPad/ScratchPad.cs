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

namespace ScratchPad
{
    /* Example: ScratchPad */
    class ScratchPad
    {
        static void SuperDebug(string codeString)
        {
            ICode code = GlyphicsApi.CreateCode(codeString);
            Console.WriteLine("Code: {0}\n", codeString);

            ICodename codename = GlyphicsApi.CodeToCodename(code);
            Console.WriteLine("Codename: {0}\n", codename);

            ITokenList glyphTokens = GlyphicsApi.CodeToTokens(code);
            string tokenDesc = "Tokens:\n" + GlyphicsApi.TokensToString(glyphTokens, "\n") + "\n";
            Console.WriteLine(tokenDesc);

            IBytecode glyphicsBytecode = GlyphicsApi.TokensToBytes(glyphTokens);
            Console.WriteLine("Bytecode:\n{0}\n", glyphicsBytecode.ToString());

            IExecutionContext context = GlyphicsApi.TokensToContext(glyphTokens);
            Console.WriteLine("Context:\n{0}\n", context);

            IGrid grid = GlyphicsApi.CodeToGrid(code);
            Console.WriteLine("Grid: {0} {1} non-empty\n", grid, grid.CountNonZero());

            string bytesDesc = GlyphicsApi.BytesToString(grid.CloneData());
            Console.WriteLine("GridBytes:\n{0}\n", bytesDesc);

            IRectList rects = GlyphicsApi.GridToRects(grid);
            Console.WriteLine("Rects: {0}\n{1}", rects.Count, rects);

            string serialized = GlyphicsApi.RectsToSerializedRects(rects).SerializedData;
            Console.WriteLine("Serialized Rects: (len={0})\n{1}\n", serialized.Length, serialized);

            IScene scene = GlyphicsApi.RectsToScene(rects);
            Console.WriteLine("Scene: {0}", scene);

            IQuadList quads = GlyphicsApi.RectsToQuads(rects);
            Console.WriteLine("Quads: {0}", quads);

            ITriangles triangles = GlyphicsApi.QuadsToTriangles(quads);
            Console.WriteLine("Triangles: {0}", triangles);

            Console.WriteLine("2d view:\n{0}", GlyphicsApi.GridToHexDescription(grid));
            Console.WriteLine("3d view:\n{0}", GlyphicsApi.GridTo3DDescription(grid, 0, 0, 0));
        }
        static void Main()
        {
            const string codeString2 = "Size3D1 4 4 4;PenColorD1 56;FillRect 0 0 0 4 4 4;";
            const string codeString = "Size3D1 4 4 4;PenColorD1 56;FillRect 0 0 0 2 4 4;PenColorD1 38;FillRect 2 0 0 4 4 4";
            const string codeString3 =
                @"Simple,

#Size of Grid
Size3D4 16 16 16;

#Blue color ground
PenColorD4 31 127 255 255;WallCube 1;

#White border around ground's edge
PenColorD4 255 255 255 255;Rect 0 0 0 15 0 15;

#Red box in center
PenColorD4 255 31 127 255;FillRect 4 1 4 11 2 11;

#Green text letter 'A' (ascii 65) on top
PenColorD4 31 255 127 255;Text 6 3 8 65";

            SuperDebug(codeString);
        }
    }
}
