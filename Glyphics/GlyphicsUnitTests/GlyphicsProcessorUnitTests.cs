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
            Assert.IsTrue(grid.CompareBytes(GlyphicsApi.HexDataToBytes(Expected)));
        }

        [TestMethod]
        public void ConvertCodeToBytesToTokensToContextToGrid()
        {
            ICode glyphicscode = GlyphicsApi.CreateCode(Code);

            IBytecode bytecode = GlyphicsApi.CodeToBytes(glyphicscode);
            ITokenList tokens = GlyphicsApi.BytecodeToTokens(bytecode);
            IGrid grid = GlyphicsApi.TokensToGrid(tokens);

            Assert.IsTrue(grid.CompareBytes(GlyphicsApi.HexDataToBytes(Expected)));
        }

        [TestMethod]
        public void ConvertCodeToGridToRectsToQuadsToTriangles()
        {
            ICode glyphicscode = GlyphicsApi.CreateCode(Code);
            Assert.IsNotNull(glyphicscode);
            IGrid grid = GlyphicsApi.CodeToGrid(glyphicscode);
            Assert.IsNotNull(grid);
            IRectList rects = GlyphicsApi.GridToRects(grid);
            Assert.IsNotNull(rects);
            IQuadList quads = GlyphicsApi.RectsToQuads(rects);
            Assert.IsNotNull(quads);
            ITriangles triangles = GlyphicsApi.QuadsToTriangles(quads);
            Assert.IsNotNull(triangles);
        }

        [TestMethod]
        public void ConvertCodeToGridToRectsToScene()
        {
            ICode glyphicscode = GlyphicsApi.CreateCode(Code);
            IGrid grid = GlyphicsApi.CodeToGrid(glyphicscode);
            IRectList rectsFromGrid = GlyphicsApi.GridToRects(grid);

            IGrid gridFromRects = grid.Clone();
            Assert.IsTrue(grid.IsEqualTo(gridFromRects));

            GlyphicsApi.Renderer.RenderRectsToGrid(rectsFromGrid, gridFromRects);
            Assert.IsTrue(grid.IsEqualTo(gridFromRects));

            IScene sceneFromRects = GlyphicsApi.RectsToScene(rectsFromGrid);
            IRectList rectsFromScene = GlyphicsApi.SceneToRects(sceneFromRects);

            Assert.IsTrue(rectsFromGrid.IsEqualTo(rectsFromScene));

            IGrid gridMega = grid.Clone();
            GlyphicsApi.Renderer.RenderRectsToGrid(rectsFromScene, gridMega);

            Assert.IsTrue(grid.IsEqualTo(gridMega));
        }
    }
}
