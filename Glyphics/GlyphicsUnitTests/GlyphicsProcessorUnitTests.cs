#region Copyright
/*Copyright (c) 2015, Katascope
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/
#endregion
using GlyphicsLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GlyphicsUnitTests
{
    [TestClass]
    public class GlyphicsProcessorUnitTests
    {
        const string Code = @"Size3D4 4 4 4;PenColorD4 255 255 255 255;Rect 1 1 1 2 2 2";
        const string Expected = @"000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000FFFFFFFFFFFFFFFF0000000000000000FFFFFFFFFFFFFFFF00000000000000000000000000000000000000000000000000000000000000000000000000000000FFFFFFFFFFFFFFFF0000000000000000FFFFFFFFFFFFFFFF000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";

        [TestMethod]
        public void ConvertCodeToTokensToContextToGrid()
        {
            ICode glyphicscode = GlyphicsApi.CreateCode(Code);
            IGrid grid = GlyphicsApi.CodeToGrid(glyphicscode);
            Assert.IsTrue(grid.CompareTo(GlyphicsApi.HexDataToBytes(Expected)));
        }

        [TestMethod]
        public void ConvertCodeToBytesToTokensToContextToGrid()
        {
            ICode glyphicscode = GlyphicsApi.CreateCode(Code);

            IBytecode bytecode = GlyphicsApi.CodeToBytes(glyphicscode);
            ITokenList tokens = GlyphicsApi.BytecodeToTokens(bytecode);
            IGrid grid = GlyphicsApi.TokensToContext(tokens).Bgc.Grid;

            Assert.IsTrue(grid.CompareTo(GlyphicsApi.HexDataToBytes(Expected)));
        }

        [TestMethod]
        public void ConvertCodeToTokensToContextToGridToRectsToGrid()
        {
            ICode glyphicscode = GlyphicsApi.CreateCode(Code);
            ITokenList tokens = GlyphicsApi.CodeToTokens(glyphicscode);
            IExecutionContext ec = GlyphicsApi.TokensToContext(tokens);
            IGrid grid = ec.Bgc.Grid;

            IRectList rects = GlyphicsApi.GridToRects(grid);
            IGrid grid2 = GlyphicsApi.CreateGrid(grid.SizeX, grid.SizeY, grid.SizeZ, grid.Bpp);
            GlyphicsApi.Renderer.RenderRectsToGrid(rects, grid2);

            Assert.IsTrue(grid2.CompareTo(GlyphicsApi.HexDataToBytes(Expected)));
        }

        [TestMethod]
        public void ConvertCodeToBytesToTokensToContextToGridToRectsToGrid()
        {
            ICode glyphicscode = GlyphicsApi.CreateCode(Code);
            IBytecode bytecode = GlyphicsApi.CodeToBytes(glyphicscode);
            ITokenList tokens = GlyphicsApi.BytecodeToTokens(bytecode);
            IExecutionContext ec = GlyphicsApi.TokensToContext(tokens);
            IGrid grid = ec.Bgc.Grid;

            IRectList rects = GlyphicsApi.GridToRects(grid);
            IGrid grid2 = GlyphicsApi.CreateGrid(grid.SizeX, grid.SizeY, grid.SizeZ, grid.Bpp);
            GlyphicsApi.Renderer.RenderRectsToGrid(rects, grid2);

            Assert.IsTrue(grid2.CompareTo(GlyphicsApi.HexDataToBytes(Expected)));
        }

        [TestMethod]
        public void ConvertCodeToTokensToContextToGridToSerializedToDeserializedToRectsToGrid()
        {
            ICode glyphicscode = GlyphicsApi.CreateCode(Code);
            ITokenList tokens = GlyphicsApi.CodeToTokens(glyphicscode);
            IExecutionContext ec = GlyphicsApi.TokensToContext(tokens);
            IGrid grid = ec.Bgc.Grid;
            IRectList rects = GlyphicsApi.GridToRects(grid);
            ISerializedRects serRects = GlyphicsApi.RectsToSerializedRects(rects);
            IRectList rects2 = GlyphicsApi.SerializedRectsToRects(serRects);

            IGrid grid2 = GlyphicsApi.CreateGrid(grid.SizeX, grid.SizeY, grid.SizeZ, grid.Bpp);
            GlyphicsApi.Renderer.RenderRectsToGrid(rects2, grid2);
            Assert.IsTrue(grid2.CompareTo(GlyphicsApi.HexDataToBytes(Expected)));
        }
    }
}
