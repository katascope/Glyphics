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
    public class GlyphicsCellPropertiesUnitTests
    {
        [TestMethod]
        public void SpecificTest_PropertyRGBA()
        {
            const string code = @"Size3D4 8 1 8;Spawn 1 0 1;PenColorD4 31 127 255 255;PenAnim 2;Plot 1 0 1";
            IRectList rectSet = GlyphicsApi.CodeToRects(GlyphicsApi.CreateCode(code));
            Assert.IsTrue(rectSet.GetRect(0).Properties.Rgba == 0xffff7f1f);
        }

        [TestMethod]
        public void SpecificTest_PropertyShape()
        {
            const string code = @"Size3D4 8 1 8;Spawn 1 0 1;PenColorD1 17;PenShape 2;Plot 1 0 1";
            IRectList rectSet = GlyphicsApi.CodeToRects(GlyphicsApi.CreateCode(code));
            Assert.IsTrue(rectSet.GetRect(0).Properties.ShapeId == 2);
        }

        [TestMethod]
        public void SpecificTest_PropertyTexture()
        {
            const string code = @"Size3D4 8 1 8;Spawn 1 0 1;PenColorD1 17;PenTex 2;Plot 1 0 1";
            IRectList rectSet = GlyphicsApi.CodeToRects(GlyphicsApi.CreateCode(code));
            Assert.IsTrue(rectSet.GetRect(0).Properties.TextureId == 2);
        }

        [TestMethod]
        public void SpecificTest_PropertyAnim()
        {
            const string code = @"Size3D4 8 1 8;Spawn 1 0 1;PenColorD1 17;PenAnim 2;Plot 1 0 1";
            IRectList rectSet = GlyphicsApi.CodeToRects(GlyphicsApi.CreateCode(code));
            Assert.IsTrue(rectSet.GetRect(0).Properties.AnimateId == 2);
        }
    }
}
