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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GlyphicsUnitTests
{
    [TestClass]
    public class GlyphicsCodeFuzzUnitTests
    {
        #region No Resharper
        //Resharper here because this is a utility function only for unit tests. It's okay that we don't "use" expectedException or name
        private static void FuzzTestCode(bool expectedException, string name, string codeString)
        {
            Assert.IsTrue(name != null);
            try
            {
                ICode code = GlyphicsApi.CreateCode(codeString);
                GlyphicsApi.CodeToGrid(code);
            }
            catch (Exception)
            {
                Assert.IsTrue(expectedException);
                return;
            }
            Assert.IsFalse(expectedException);
        }
        #endregion

        [TestMethod]
        public void TestNormalCode()
        {
            FuzzTestCode(false, "Normal name/code", "Size3D4 64 64 64;PenColorD3 31 127 255;WallCube 1");
        }

        [TestMethod]
        public void TestNamedOnlyCode()
        {
            FuzzTestCode(true, "Bad name/code", "TestName");
        }

        [TestMethod]
        public void TestNamedEmptyCode()
        {
            FuzzTestCode(false, "Name with Empty code", "TestName,");
        }

        [TestMethod]
        public void TestNamedGoodCode()
        {
            FuzzTestCode(false, "Good name/code", "TestName,Size3D4 64 64 64");
        }

        [TestMethod]
        public void TestWrongArgsCode()
        {
            FuzzTestCode(true, "Wrong # Args", "TestName,Size3D4 64 64");
        }

        [TestMethod]
        public void TestGarbageCode()
        {
            FuzzTestCode(true, "Garbage", "!@#%^!@#&^!#$%!@#$qwel;12kj34;lkjzdfasdf");
        }
    }
}
