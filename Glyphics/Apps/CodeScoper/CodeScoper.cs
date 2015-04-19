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
            const string codeString2 = "Size3D1 8 9 1;PenColorD1 1;Text 2 2 0 65;ImgFlipY;Rect 0 0 0 7 8 1";
            const string codeString = "Size3D1 4 4 3;PenColorD1 56;FillTriangle 1 1 0 3 3 1 2 3 2";

            CodeScope(codeString);
        }

        //View it all nicely and drawn out
        private static void CodeScope(string codeString)
        {
            ICode code = GlyphicsApi.CreateCode(codeString);

            string codeDesc = "Code: (len=" + codeString.Length + ")\n" + codeString + "\n";
            Console.WriteLine(codeDesc);

            ICodename codename = GlyphicsApi.CodeToCodename(code);
            string codenameDesc = "Codename: \"" + codename.Name + "\"\n";
            Console.WriteLine(codenameDesc);

            ITokenList glyphTokens = GlyphicsApi.CodeToTokens(code);
            string tokenDesc = "Tokens:\n" + GlyphicsApi.TokensToString(glyphTokens, "\n") + "\n";
            Console.WriteLine(tokenDesc);

            IBytecode glyphicsBytecode = GlyphicsApi.TokensToBytes(glyphTokens);
            var bytecodeDesc = "Compiled: (len=" + GlyphicsApi.BytesToString(glyphicsBytecode.GetBytes()).Length + ")\n" + GlyphicsApi.BytesToString(glyphicsBytecode.GetBytes()) + "\n";
            Console.WriteLine(bytecodeDesc);

            IExecutionContext context = GlyphicsApi.TokensToContext(glyphTokens);
            string contextDesc = "Context:\n" + context + "\n";
            Console.WriteLine(contextDesc);
            
            IGrid grid = context.Bgc.Grid;

            byte[] data = grid.CloneData();
            string gridDesc = "ByteGrid: (len=" + grid.Size + ")\n" + GlyphicsApi.BytesToString(data) + "\n";
            Console.WriteLine(gridDesc);

            Console.WriteLine("Optomizing rectangles");
            IRectList rectSet = GlyphicsApi.GridToRects(grid);

            string rectlistDesc = "Rects:\n" + GlyphicsApi.RectsToDescription(rectSet);
            Console.WriteLine(rectlistDesc);

            string serialized = GlyphicsApi.RectsToSerializedRects(rectSet).SerializedData;
            string serializedDesc = "Serialized Rects: (len=" + serialized.Length + ")\n" + serialized + "\n";
            Console.WriteLine(serializedDesc);

            IDouble3 spawnPoint = context.Bgc.SpawnPoint;
            string spawnpointDesc = "Spawn Point:\n"+ spawnPoint.X + "," + spawnPoint.Y + "," + spawnPoint.Z + "\n";
            Console.WriteLine(spawnpointDesc);

            Console.WriteLine("2d view:");
            Console.WriteLine(GlyphicsApi.GridToHexDescription(grid));

            Console.WriteLine("3d view:");
            Console.WriteLine(GlyphicsApi.GridTo3DDescription(grid, 0, 0, 0));
        }
    }
}
