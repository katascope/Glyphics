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
using GlyphicsLibrary.Language;
using GlyphicsLibrary.Painters;
using GlyphicsLibrary.Renderers;

namespace GlyphicsLibrary
{
    //TODO: Export to ascii STL option

    //TODO: Step-Exporter - renders code, creating a new image for each line

    //Primary Glyphics API call - intended for use by other programs
    public static class GlyphicsApi
    {
        //Versioning for API
        private const string CurrentVersion = "1";
        private const string CurrentVersionName = "Vernacular";
        public static string GetVersion() { return CurrentVersion; }
        public static string GetVersionName() { return CurrentVersionName; }

        //API-Level Painter
        private static readonly IPainter RealPainter = new CPainter();
        public static IPainter Painter { get { return RealPainter; } }

        //API-Level Renderer
        private static readonly IRenderer RealRenderer = new Renderer();
        public static IRenderer Renderer { get { return RealRenderer; } }

        //Creational
        public static IByteGridContext CreateContext(IGrid grid) { return new CByteGridContext(grid); }
        public static IDouble3 CreatePoint(int x, int y, int z) { return new CDouble3(x, y, z); }
        public static IGrid CreateGrid(int x, int y, int z, int bpp) { return new CGrid(x, y, z, bpp); }
        public static ICode CreateCode(string code) { return new CCode(code); }
        public static ITriangle CreateTriangle(float x1, float y1, float z1, float x2, float y2, float z2, float x3, float y3, float z3) {  return new CTriangle(x1,y1,z1,x2,y2,z2,x3,y3,z3);}
        public static IRect CreateRect(double minx, double miny, double minz, double maxx, double maxy, double maxz) { return new CRect(minx, miny, minz, maxx, maxy, maxz); }
        public static ITriangles CreateTriangles() { return new CTriangles(); }
        public static ITrianglesList CreateTrianglesList() { return new CTrianglesList(); }
        public static ISerializedRects CreateSerializedRects(string serialized) { return new CSerializedRects(serialized); }
        public static ICodeList CreateCodes() { return new CCodeList(); }

        //Code-To
        public static IGrid CodeToGrid(ICode glyphicsCode) { if (glyphicsCode == null) return null; return Conversions.CodeToGrid(glyphicsCode.Code); }
        public static IRectList CodeToRects(ICode glyphicsCode) { return Conversions.CodeToRects(glyphicsCode); }
        public static IGrid CodeToObliqueCells(ICode glyphicsCode) { return Renderer.RenderObliqueCells(CodeToGrid(glyphicsCode)); }
        public static ITokenList CodeToTokens(ICode glyphicsCode) { return Tokenizer.CodeToTokens(glyphicsCode); }
        public static ICodename CodeToCodename(ICode glyphicsCode) { return new CCodename(glyphicsCode.Code.Split(glyphicsCode.NameCodeSplitCharacter)[0]); }
        public static IBytecode CodeToBytes(ICode glyphicsCode) { return Compiler.CodeToBytes(glyphicsCode); }
        public static ICode CodeToRescaledCode(ICode glyphicsCode, int toX, int toY, int toZ) { return Rescaler.Rescale(glyphicsCode, toX, toY, toZ); }

        //Tokens-To
        public static IExecutionContext TokensToContext(ITokenList glyphTokens) { return Executor.Execute(glyphTokens); }
        public static IBytecode TokensToBytes(ITokenList glyphTokens) { return Compiler.TokensToBytes(glyphTokens); }
        public static string TokensToString(ITokenList glyphTokens, string separator) { return Conversions.TokensToString(glyphTokens, separator); }
        public static ITokenList BytecodeToTokens(IBytecode glyphicsBytecode) { return Disassembler.BytecodeToTokens(glyphicsBytecode); }

        //Context-To
        public static IGrid ContextToGrid(IExecutionContext gec) { if (gec == null || gec.Bgc == null) return null; return gec.Bgc.Grid; }

        //Grid-To
        public static IRectList GridToRects(IGrid grid) { return RectReducer.GridToRects(grid); }
        public static string GridTo3DDescription(IGrid grid, int ax, int ay, int az) { return Conversions.GridTo3DDescription(grid, ax, ay, az); }
        public static string GridToHexDescription(IGrid grid) { return Conversions.GridToHexDescription(grid); }

        //Rect(s)-To
        public static IRect RectsToBoundaries(IRectList rectSet) { return RectMath.RectsToBoundaries(rectSet); }
        public static ISerializedRects RectsToSerializedRects(IRectList rectSet) { return RectSerializer.Serialize(rectSet); }
        public static IRectList SerializedRectsToRects(ISerializedRects serializedRects) { return RectSerializer.Deserialize(serializedRects); }
        public static ITriangles RectsToTrianglesCube(IRectList rectSet) { return QuadToTriangles.RectsToTrianglesCube(rectSet); }
        public static string RectsToDescription(IRectList rectSet) { return RectMath.RectsToDescription(rectSet); }

        //RGBA-To
        public static ulong Rgba2Ulong(byte r, byte g, byte b, byte a) { return Pixel.Rgba2Ulong(r, g, b, a); }
        public static void Ulong2Rgba(ulong val, out byte r, out byte g, out byte b, out byte a) { Pixel.Ulong2Rgba(val, out r, out g, out b, out a); }

        //File IO for Glyphics files
        public static bool CodesToGly(string filename, ICodeList codes) { return GlyphicsFile.CodesToGly(filename, codes); }
        public static ICodeList GlyToCodes(string filename) { return GlyphicsFile.GlyToCodes(filename); }
        public static bool LoadArchetypes(string filename) { return GlyphicsFile.LoadArchetypes(filename); }
        public static void PreSerializeGlyphicsFile(string filename) { GlyphicsFile.PreSerializeGlyphicsFile(filename); }

        //File IO for PNG files
        public static IGrid PngToGrid(string filename) { return FilePngRead.PngToGrid(filename); }
        public static void SaveFlatPng(string filename, IGrid grid) { FilePngWrite.SaveFlatPng(filename, grid); }

        //File IO for STL files
        public static ITriangles StlToTriangles(string filename) { return FileStlRead.ReadFile(filename); }
        public static void SaveTrianglesToStl(string filename, ITriangles triangles) { FileStlWrite.WriteFile(triangles, filename); }

        //Float & Double comparison
        public static bool DoublesAreEqual(double v1, double v2) { return Compare.CompareDoubleAreEqual(v1,v2); }
        public static bool FloatsAreEqual(float v1, float v2)  { return Compare.CompareFloatAreEqual(v1, v2); }

        //Translates hex data in string format to byte array
        public static byte[] HexDataToBytes(string data) { return Transcode64.HexDataToBytes(data); }

        //Bytes
        public static string BytesToString(byte[] bytes) { return Conversions.BytesToString(bytes); }        
        public static bool CompareBytes(byte[] result, byte[] expectedResult) { return Compare.CompareBytes(result, expectedResult); }
        
        //Glyphs
        public static IGlyph[] GetGlyphDefs() { return Glyphs.GetGlyphDefs(); }
        public static IGlyph GetGlyph(int id) { return Glyphs.GetGlyph(id); }
        public static int GetId(string name) { return Glyphs.GetId(name); }
    }
}

