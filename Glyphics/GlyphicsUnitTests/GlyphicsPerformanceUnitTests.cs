#region Copyright
/*Copyright (c) 2015, Katascope
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/
#endregion
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GlyphicsLibrary;

namespace GlyphicsUnitTests
{
    [TestClass]
    public class GlyphicsPerformanceUnitTests
    {
        const string ComplexCode = "Ascent,Size3D4 64 64 64;Spawn 6 3 5;PenColorD3 31 127 255;WallCube 1;ImgEdge 255 255 255 255;PenColorD3 127 192 127;ExtrudeZ 33 36 35 48 6 6 6 0;ExtrudeY 34 1 31 31 6 6 6 0;PenColorD3 127 127 192;FillRect 24 31 24 36 31 36;PenColorD3 192 127 192;PenSize 5 1 2;Line 25 1 3 25 23 40;Line 4 31 11 15 51 48;Line 15 51 11 26 58 51;Line 31 23 36 31 23 191;Line 25 23 40 25 23 55;Line 19 20 58 20 31 27;PenColorD3 192 127 127;PenSize 1 1 7;Line 32 31 51 57 31 51;Line 11 51 52 63 51 52;Line 0 31 7 58 31 7;PenSize 7 1 1;Line 55 31 11 55 31 51;PenColorD3 192 192 127;PenSize 1 1 1;ExtrudeZ 60 56 10 51 6 9 6 0;PenColorD3 31 127 255;FillRect 11 51 0 63 51 10;PenColorD3 127 192 127;PenColorD3 127 192 192;PenSize 7 1 3;Line 26 58 53 68 58 59;Line 61 60 51 61 57 57;PenSize 1 1 7;PenColorD3 192 127 192;PenSize 1 1 1;PenColorD4 127 127 192 255;FillRect 50 1 50 61 13 61;PenColorD4 0 0 0 0;FillRect 55 1 49 57 7 59;FillRect 51 1 51 60 12 60;PenColorD4 127 127 192 63;FillRect 50 4 54 50 7 58;PenColorD4 255 0 0 255;PenSize 3 1 3;Plot 60 1 2";

        [TestMethod]
        public void TestCreateCodeTime()
        {
            var sw = new Stopwatch();
            sw.Start();
            //ICode glyphicsCode = GlyphicsApi.CreateCode(complexCode);
            ICode glyphicsCode = GlyphicsApi.CreateCode(ComplexCode);
            GlyphicsApi.CodeToGrid(glyphicsCode);
            sw.Stop();
            long val = sw.ElapsedMilliseconds;

            Assert.IsTrue(val < 3000);
        }

        [TestMethod]
        public void TestCodeToGridTime()
        {
            var sw = new Stopwatch();
            ICode glyphicsCode = GlyphicsApi.CreateCode(ComplexCode);
            sw.Start();
            GlyphicsApi.CodeToGrid(glyphicsCode);
            sw.Stop();
            long val = sw.ElapsedMilliseconds;
            
            Assert.IsTrue(val < 3100);
        }

        [TestMethod]
        public void TestGridToRectsTime()
        {
            var sw = new Stopwatch();
            ICode glyphicsCode = GlyphicsApi.CreateCode(ComplexCode);
            IGrid grid = GlyphicsApi.CodeToGrid(glyphicsCode);
            sw.Start();
            GlyphicsApi.GridToRects(grid);
            sw.Stop();
            long val = sw.ElapsedMilliseconds;

            Assert.IsTrue(val < 4000);
        }

        [TestMethod]
        public void TestRectsToTrianglesCubeTime()
        {
            var sw = new Stopwatch();
            ICode glyphicsCode = GlyphicsApi.CreateCode(ComplexCode);
            IGrid grid = GlyphicsApi.CodeToGrid(glyphicsCode);
            IRectList rects = GlyphicsApi.GridToRects(grid);
            sw.Start();
            GlyphicsApi.RectsToTrianglesCube(rects);
            sw.Stop();
            long val = sw.ElapsedMilliseconds;

            Assert.IsTrue(val < 500);
        }

        [TestMethod]
        public void TestRenderObliqueCellsTime()
        {
            var sw = new Stopwatch();
            ICode glyphicsCode = GlyphicsApi.CreateCode(ComplexCode);
            IGrid grid = GlyphicsApi.CodeToGrid(glyphicsCode);

            sw.Start();
            GlyphicsApi.Renderer.RenderObliqueCells(grid);
            sw.Stop();
            long val = sw.ElapsedMilliseconds;

            Assert.IsTrue(val < 35000);
        }
    }
}
