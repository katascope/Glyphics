#region Copyright
/*Copyright (c) 2015, Katascope
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/
#endregion
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GlyphicsLibrary;

namespace GlyphicsUnitTests
{
    [TestClass]
    public class GlyphicsApiUnitTests
    {
        private const string FinalCubeCodeName = "FinalCube";
        private const string FinalCubeCode = FinalCubeCodeName + ",Size3D4 8 8 8;PenColorD4 255 255 255 255;FillRect 0 0 0 8 8 8*@@@@00000777";
        private const string SerializedFinalCubeCode = "*@@@@00000777";

        [TestMethod]
        public void TestPainter()
        {
            Assert.IsNotNull(GlyphicsApi.Painter);
        }

        [TestMethod]
        public void TestRenderer()
        {
            Assert.IsNotNull(GlyphicsApi.Renderer);
        }

        [TestMethod]
        public void TestCreateContext()
        {
            ICode code = GlyphicsApi.CreateCode(FinalCubeCode);
            IGrid grid = GlyphicsApi.CodeToGrid(code);
            Assert.IsNotNull(GlyphicsApi.CreateContext(grid));
        }

        [TestMethod]
        public void TestCreateCode()
        {
            Assert.IsNotNull(GlyphicsApi.CreateCode(FinalCubeCode));
        }

        [TestMethod]
        public void TestCreateRect()
        {
            IRect rect = GlyphicsApi.CreateRect(0, 0, 0, 10, 10, 10);
           
            Assert.IsTrue(GlyphicsApi.DoublesAreEqual(rect.Pt1.X, 0));
            Assert.IsTrue(GlyphicsApi.DoublesAreEqual(rect.Pt1.Y, 0));
            Assert.IsTrue(GlyphicsApi.DoublesAreEqual(rect.Pt1.Z, 0));

            Assert.IsTrue(GlyphicsApi.DoublesAreEqual(rect.Pt2.X, 10));
            Assert.IsTrue(GlyphicsApi.DoublesAreEqual(rect.Pt2.Y, 10));
            Assert.IsTrue(GlyphicsApi.DoublesAreEqual(rect.Pt2.Z, 10));
        }

        [TestMethod]
        public void TestCreateTriangles()
        {
            Assert.IsNotNull(GlyphicsApi.CreateTriangles());
        }

        [TestMethod]
        public void TestCreateTrianglesList()
        {
            Assert.IsNotNull(GlyphicsApi.CreateTrianglesList());
        }

        [TestMethod]
        public void TestCreateSerializedRects()
        {
            Assert.IsNotNull(GlyphicsApi.CreateSerializedRects(SerializedFinalCubeCode));
        }

        [TestMethod]
        public void TestGridTo3DDescription()
        {
            ICode code = GlyphicsApi.CreateCode(FinalCubeCode);
            IGrid grid = GlyphicsApi.CodeToGrid(code);
            string desc = GlyphicsApi.GridTo3DDescription(grid, 0, 0, 0);

            Assert.IsNotNull(desc);
        }

        [TestMethod]
        public void TestCodeToCodename()
        {
            ICode code = GlyphicsApi.CreateCode(FinalCubeCode);
            ICodename codename = GlyphicsApi.CodeToCodename(code);

            Assert.IsTrue(String.CompareOrdinal(codename.Name, "FinalCube") == 0);
        }

        [TestMethod]
        public void TestCodeToTokens()
        {
            ICode code = GlyphicsApi.CreateCode(FinalCubeCode);
            ITokenList tokens = GlyphicsApi.CodeToTokens(code);

            Assert.IsTrue(tokens.Count == 3);
        }

        [TestMethod]
        public void TestCodeToGrid()
        {
            ICode code = GlyphicsApi.CreateCode(FinalCubeCode);
            IGrid grid = GlyphicsApi.CodeToGrid(code);

            Assert.IsTrue(grid.SizeX == 8);
            Assert.IsTrue(grid.SizeY == 8);
            Assert.IsTrue(grid.SizeZ == 8);
        }

        [TestMethod]
        public void TestCodeToRects()
        {
            ICode code = GlyphicsApi.CreateCode(FinalCubeCode);
            IGrid grid = GlyphicsApi.CodeToGrid(code);
            IRectList rects = GlyphicsApi.GridToRects(grid);

            Assert.IsTrue(rects.Count == 1);
        }

        [TestMethod]
        public void TestCodeToTriangles()
        {
            ICode code = GlyphicsApi.CreateCode(FinalCubeCode);
            IGrid grid = GlyphicsApi.CodeToGrid(code);
            IRectList rects = GlyphicsApi.GridToRects(grid);
            ITriangles triangles = GlyphicsApi.RectsToTrianglesCube(rects);

            Assert.IsTrue(triangles.GetTriangleArray().Length == 12);
        }

        [TestMethod]
        public void TestCodeToOblique()
        {
            ICode code = GlyphicsApi.CreateCode(FinalCubeCode);
            IGrid grid = GlyphicsApi.CodeToGrid(code);

            IRenderer renderer = GlyphicsApi.Renderer;
            IGrid renderedObliqueGrid = renderer.RenderObliqueCells(grid);

            Assert.IsTrue(renderedObliqueGrid.SizeX == 96);
            Assert.IsTrue(renderedObliqueGrid.SizeY == 96);
            Assert.IsTrue(renderedObliqueGrid.SizeZ == 1);
        }

        [TestMethod]
        public void TestCodeToBytes()
        {
            ICode code = GlyphicsApi.CreateCode(FinalCubeCode);
            IBytecode bytecode = GlyphicsApi.CodeToBytes(code);

            Assert.IsTrue(bytecode.GetBytes().Length == 16);
        }

        [TestMethod]
        public void TestRectsToBoundaries()
        {
            ICode code = GlyphicsApi.CreateCode(FinalCubeCode);
            IGrid grid = GlyphicsApi.CodeToGrid(code);
            IRectList rects = GlyphicsApi.GridToRects(grid);
            IRect rect = GlyphicsApi.RectsToBoundaries(rects);

            Assert.IsTrue(GlyphicsApi.DoublesAreEqual(rect.Pt1.X, 0));
            Assert.IsTrue(GlyphicsApi.DoublesAreEqual(rect.Pt1.Y, 0));
            Assert.IsTrue(GlyphicsApi.DoublesAreEqual(rect.Pt1.Z, 0));

            Assert.IsTrue(GlyphicsApi.DoublesAreEqual(rect.Pt2.X, 7));
            Assert.IsTrue(GlyphicsApi.DoublesAreEqual(rect.Pt2.Y, 7));
            Assert.IsTrue(GlyphicsApi.DoublesAreEqual(rect.Pt2.Z, 7));
        }

        [TestMethod]
        public void TestRectsToTrianglesCube()
        {
            ICode code = GlyphicsApi.CreateCode(FinalCubeCode);
            IGrid grid = GlyphicsApi.CodeToGrid(code);
            IRectList rects = GlyphicsApi.GridToRects(grid);
            ITriangles triangles = GlyphicsApi.RectsToTrianglesCube(rects);

            Assert.IsTrue(triangles.GetTriangleArray().Length == 12);
        }

        [TestMethod]
        public void TestRectsToSerialized()
        {
            ICode code = GlyphicsApi.CreateCode(FinalCubeCode);
            IGrid grid = GlyphicsApi.CodeToGrid(code);
            IRectList rects = GlyphicsApi.GridToRects(grid);
            ISerializedRects serializedRects = GlyphicsApi.RectsToSerializedRects(rects);

            Assert.IsTrue((String.CompareOrdinal(serializedRects.SerializedData, SerializedFinalCubeCode) == 0));
        }

        [TestMethod]
        public void TestSerializedRectsToRects()
        {
            ISerializedRects serializedRects = GlyphicsApi.CreateSerializedRects(SerializedFinalCubeCode);
            IRectList rects = GlyphicsApi.SerializedRectsToRects(serializedRects);

            Assert.IsTrue(rects.Count == 1);
        }

        [TestMethod]
        public void TestCodeToRescaledCode()
        {
            ICode codeInitial = GlyphicsApi.CreateCode(FinalCubeCode);
            ICode codeRescaled = GlyphicsApi.CodeToRescaledCode(codeInitial, 38, 38, 38);

            IGrid grid = GlyphicsApi.CodeToGrid(codeRescaled);

            Assert.IsTrue(grid.SizeX == 38);
            Assert.IsTrue(grid.SizeY == 38);
            Assert.IsTrue(grid.SizeZ == 38);
        }

        [TestMethod]
        public void TestRgba2Ulong()
        {
            ulong val = GlyphicsApi.Rgba2Ulong(255, 127, 63, 255);

            Assert.IsTrue(val == 0xFF3F7FFF);
        }

        [TestMethod]
        public void TestUlong2Rgba()
        {
            byte r, g, b, a;
            GlyphicsApi.Ulong2Rgba(0xFF3F7FFF, out r, out g, out b, out a);

            Assert.IsTrue(r == 255);
            Assert.IsTrue(g == 127);
            Assert.IsTrue(b == 63);
            Assert.IsTrue(a == 255);
        }
    }
}
