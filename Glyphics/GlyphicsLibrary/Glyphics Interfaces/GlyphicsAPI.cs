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
//Revamp project
    //Project Insidious - Version after Vernacular
    //to create a space between the spaces, a conduit where one can be used for the other
    //Example: Editing in sl, exporting back to serial, then used elsewhere
    //Example: Javascript editors
    //Example: Into Unity environment
    //Example: To STL, for 3d-printing
    //Example: To Isometric 
    //Example: To Minecraft
    //Example: From Minecraft
    //Example: From 3d editors
    //Example: From jpegs/photos
    //Example: From OBJ, for splitting into multiple prints
    //Example: From GIS, for generating height maps
    //Example: From heightmaps..

    //Meta-language for actions
    // "code" > rects > triangles > translate 4 0 4 ; clone ; translate

    //To multiple STL
    //Easy image to glyphics, extrudable then in that world
    //Good idea
    // Using 'inside detection' to make a scissoring area
    // Can calculate area by making another grid and 'clearing' which are fully inside

    //TODO: Prima
    //TODO: Secundus

    //New use cases
    // * 3D editor
    // * Unity interface with oculus support
    // * Animation support
    // * Deformation triangle creator logic - for corners
    // * Prima engine
    // * PostProcessors - run after code, on rects?

    //Flesh out new model
    // Need to understand Anything-to-Anything model better
    
    // For UI: "document" - grid?
    //  Primary view - grid to oblique ( or changeable)
    //  Use cases:
    //   Code editor
    //   Export ortho to png
    //   Export grid to png
    //   Copy serialized to clipboard
    //   


    //Glyphics Prima language
    // Code, can convert to rects, and back to code
    // Subset language of glyphics(first four operations), like just enough to create grids, set colors, and draw rects
    // Nop, Size3D4, PenColorD4, FillRect

    //Glyphics Secundus language
    //first 16 or so?, candidates?
    // Rect-Equivalents: Clear, Plot, HollowRect, 
    // Special: Line, 

    //Simulation - micropocket worlds
    // Functions that only process when grid is run if certain values
    // State machine : Trigger can change state of grid

    //TODO: Future: Lights
    //TODO: Future: Materials
    //TODO: Future: Triggers and Sensors
    //TODO: Future: Editor
    //TODO: Future: Explorer
    //TODO: Future: Generate grid of previews of a bunch of other grids (using the scaler)

    //TODO: Future: Integrate with unity?
    //TODO: Future: Render scenes to grid (with animations, full triangles)
    //TODO: Future: UI units as equivalent to rects
    //TODO: Future: Speed improvements & profiling
    //TODO: Future: Corner/STL-solving issue for supports & smoothing
    //TODO: Future: Importers/Exporters for .obj, .mesh, .gif, .minecraft?
    //TODO: Future: 4D grids
    //TODO: Future: Render rects to oblique grid, without borders or cells - as triangles/quads

    //Primary Glyphics API call - intended for use by other programs
    public static class GlyphicsApi 
    {
        //Versioning for API
        private const string CurrentVersion = "1.01";
        private const string CurrentVersionName = "Vernacular";

        //Versioning methods
        public static string Version { get { return CurrentVersion; } }
        public static string VersionName { get { return CurrentVersionName; } }

        //API-Level Painter
        private static readonly IPainter RealPainter = new CPainter();
        public static IPainter Painter { get { return RealPainter; } }

        //API-Level Renderer
        private static readonly IRenderer RealRenderer = new Renderer();
        public static IRenderer Renderer { get { return RealRenderer; } }

        //Creational
        public static IGridContext CreateContext(IGrid grid) { return new CGridContext(grid); }
        public static IDouble3 CreatePoint(int x, int y, int z) { return new CDouble3(x, y, z); }
        public static IGrid CreateGrid(int x, int y, int z, int bpp) { return new CGrid(x, y, z, bpp); }
        public static ICode CreateCode(string code) { return new CCode(code); }
        public static ITriangle CreateTriangle(float x1, float y1, float z1, float x2, float y2, float z2, float x3, float y3, float z3) {  return new CTriangle(x1,y1,z1,x2,y2,z2,x3,y3,z3);}
        public static IRect CreateRect(double minx, double miny, double minz, double maxx, double maxy, double maxz) { return new CRect(minx, miny, minz, maxx, maxy, maxz); }
        public static ITriangles CreateTriangles() { return new CTriangles(); }
        public static ITrianglesList CreateTrianglesList() { return new CTrianglesList(); }
        public static ISerializedRects CreateSerializedRects(string serialized) { return new CSerializedRects(serialized); }
        public static ICodeList CreateCodes() { return new CCodeList(); }

        //Glyphs
        public static IGlyph[] GetGlyphDefs() { return Glyphs.GetGlyphDefs(); }
        public static IGlyph GetGlyph(int id) { return Glyphs.GetGlyph(id); }
        public static int GetId(string name) { return Glyphs.GetId(name); }

        //Code-To
        public static IGrid CodeToGrid(ICode glyphicsCode) { if (glyphicsCode == null) return null; return Language.Converter.CodeToGrid(glyphicsCode.Code); }
        public static IRectList CodeToRects(ICode glyphicsCode) { return Language.Converter.CodeToRects(glyphicsCode); }
        public static IGrid CodeToObliqueCells(ICode glyphicsCode) { return Renderer.RenderObliqueCells(CodeToGrid(glyphicsCode)); }
        public static ITokenList CodeToTokens(ICode glyphicsCode) { return Language.Converter.CodeToTokens(glyphicsCode); }
        public static ICodename CodeToCodename(ICode glyphicsCode) { return new CCodename(glyphicsCode); }
        public static IBytecode CodeToBytes(ICode glyphicsCode) { return Language.Converter.TokensToBytes(Language.Converter.CodeToTokens(glyphicsCode)); }
        public static ICode CodeToRescaledCode(ICode glyphicsCode, int toX, int toY, int toZ) { return Language.Converter.CodeToRescaledCode(glyphicsCode, toX, toY, toZ); }
        //Generate thumbnail grids from code by rescaling the codelines
        public static IGridList CodeToThumbnailed(ICode code) { return Thumbnails.CodeToThumbnailed(code); }

        //Tokens-To
        public static IGrid TokensToGrid(ITokenList tokens) { return Language.Converter.TokensToGrid(tokens); }
        //public static IExecutionContext TokensToContext(ITokenList glyphTokens) { return Language.Converter.TokensToExecutionContext(glyphTokens); }
        public static IBytecode TokensToBytes(ITokenList glyphTokens) { return Language.Converter.TokensToBytes(glyphTokens); }
        //public static string TokensToString(ITokenList glyphTokens, string separator) { return Conversions.TokensToString(glyphTokens, separator); }
        public static ITokenList BytecodeToTokens(IBytecode glyphicsBytecode) { return Language.Converter.BytecodeToTokens(glyphicsBytecode); }

        //Context-To
        //public static IGrid ContextToGrid(IExecutionContext gec) { if (gec == null || gec.GridContext == null) return null; return gec.GridContext.Grid; }

        //Grid-To
        public static IRectList GridToRects(IGrid grid) { return ByteGrid.Converter.GridToRects(grid); }
        public static string GridTo3DDescription(IGrid grid, int ax, int ay, int az) { return ByteGrid.Converter.GridTo3DDescription(grid, ax, ay, az); }
        public static string GridToHexDescription(IGrid grid) { return ByteGrid.Converter.GridToHexDescription(grid); }

        //Rect(s)-To
        public static ICode RectsToCode(IRectList rectSet) { if (rectSet == null) return null; return Language.Converter.RectsToCode(rectSet); }
        public static IRect RectsToBoundaries(IRectList rectSet) { if (rectSet == null) return null; return rectSet.Boundaries; }
        public static ISerializedRects RectsToSerializedRects(IRectList rectSet) { return Atomics.Converter.RectsToSerializedRects(rectSet); }
        public static IRectList SerializedRectsToRects(ISerializedRects serializedRects) { return Atomics.Converter.SerializedRectsToRects(serializedRects); }
        public static ITriangles RectsToTrianglesCube(IRectList rectSet) { return Atomics.Converter.RectsToTrianglesCube(rectSet); }
        public static ISerializedRects RectsToSerializedRectsLimit255(IRectList rectSet) { return new CSerializedRects(Atomics.Converter.SerializeLimit255(rectSet)); }

        //RGBA-To
        public static ulong Rgba2Ulong(byte r, byte g, byte b, byte a) { return Atomics.Converter.Rgba2Ulong(r, g, b, a); }
        public static void Ulong2Rgba(ulong val, out byte r, out byte g, out byte b, out byte a) { Atomics.Converter.Ulong2Rgba(val, out r, out g, out b, out a); }

        //OBJ file
        public static ITriangles ObjToTriangles(string filename) { return FileObjRead.ReadfileAscii(filename); }

        //DAE Collada/Mesh file
        public static void SaveDae(string filename, IRectList rects) { ByteGrid.FileDaeWrite.ExportRectsCollada.WriteMesh(filename, rects); }

        //Text file
        public static void SaveFlatText(string filename, string text) { Atomics.FileTxtWrite.SaveFlatText(filename,text); }

        //File IO for Glyphics files
        public static bool CodesToGly(string filename, ICodeList codes) { return GlyphicsFile.CodesToGly(filename, codes); }
        public static ICodeList GlyToCodes(string filename) { return GlyphicsFile.GlyToCodes(filename); }
        public static bool LoadArchetypes(string filename) { return GlyphicsFile.LoadArchetypes(filename); }
        public static void PreSerializeGlyphicsFile(string filename) { GlyphicsFile.PreSerializeGlyphicsFile(filename); }

        //File IO for PNG files
        public static IGrid PngToGrid(string filename) { return FilePngRead.PngToGrid(filename); }
        public static void SaveFlatPng(string filename, IGrid grid) { FilePngWrite.SaveFlatPng(filename, grid); }

        //File IO for GIF files
        public static IGrid GifToGrid(string filename) { return FileGifRead.GifToGrid(filename); }
        public static IGridList GifToGrids(string filename) { return FileGifRead.GifToGrids(filename); }
        public static void SaveFlatGif(string filename, IGrid grid) { FileGifWrite.SaveFlatGif(filename, grid); }

        //File IO for STL files
        public static ITriangles StlToTriangles(string filename) { return FileStlRead.ReadFile(filename); }
        public static void SaveTrianglesToStl(string filename, ITriangles triangles) { FileStlWrite.WriteFile(triangles, filename); }
        public static void SaveTrianglesToStlAscii(string filename, ITriangles triangles) { FileStlWrite.WriteAsciiFile(triangles, filename); }

        //Float & Double comparison
        public static bool DoublesAreEqual(double v1, double v2) { return Compare.CompareDoubleAreEqual(v1,v2); }
        public static bool FloatsAreEqual(float v1, float v2)  { return Compare.CompareFloatAreEqual(v1, v2); }

        //Translates hex data in string format to byte array
        public static byte[] HexDataToBytes(string data) { return Transcode64.HexDataToBytes(data); }

        //Bytes
        public static string BytesToString(byte[] bytes) { return Atomics.Converter.BytesToString(bytes); }        
        public static bool CompareBytes(byte[] result, byte[] expectedResult) { return Compare.CompareBytes(result, expectedResult); }

        //IQuad and IQuadList
        public static IQuadList RectsToQuads(IRectList rectSet) { return Atomics.Converter.RectsToQuads(rectSet); }
        public static ITriangles QuadsToTriangles(IQuadList quads) { return Atomics.Converter.QuadsToTriangles(quads); }
        public static int RemoveRedundantQuads(IQuadList quads) { return Atomics.Converter.RemoveRedundantQuads(quads); }

        #region SceneGraph
        //Scenegraph Creational 
        public static ITransform CreateTransform() { return new CTransform(); }
        public static IElement CreateElement() { return new CElement(); }
        public static IScene CreateScene() { return new CScene(); }
        public static IDeck CreateDeck() { return new CDeck(); }

        //Scenegraph functions
        public static IScene RectsToScene(IRectList rects) { return Atomics.Converter.RectsToScene(rects); }
        public static IRectList SceneToRects(IScene scene) { return Atomics.Converter.SceneToRects(scene); }
        public static IRect ElementToRect(IElement element) { return Atomics.Converter.ElementToRect(element); }
        public static IElement RectToElement(IRect rect) { return Atomics.Converter.RectToElement(rect); }

        // ICodelist to IDeck
        //public static ICodeList DeckToCodelist(IDeck deck);
        // IDeck to ICodelist 
        //public static IDeck CodelistToDeck(ICodeList codelist);
        #endregion
    }
}

