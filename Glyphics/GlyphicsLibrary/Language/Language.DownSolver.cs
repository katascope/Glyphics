#region Copyright
/*Copyright (c) 2015, Katascope
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/
#endregion

namespace GlyphicsLibrary.Language
{
    //FlowSolver class, not really that efficient, but good for making inputs solve out to all possible outputs
    public class DownSolver
    {
        public ICodeList Codes;
        public IGridList Grids;
        public ICode Code;
        public IBytecode Bytecode;
        public ICodename Codename;
        public ITokenList Tokens;
        public IGrid Grid;
        public IGrid GridOblique;
        public IRectList Rects;
        public ISerializedRects SerializedRects;
        public ISerializedRects SerializedRectsLimit255;
        public IQuadList Quads;
        public ITriangles Triangles;
        public byte[] Rawbytes;

        public DownSolver() { }
        public DownSolver(ICode inCode) { FromCode(inCode); }
        public DownSolver(string filename) { FromFilename(filename); }
        public DownSolver(ICodename inCodename) { Codename = inCodename; }
        public DownSolver(IBytecode inBytecode) { FromBytecode(inBytecode); }
        public DownSolver(ITokenList inTokens) { FromTokens(inTokens); }
        public DownSolver(IGrid inGrid) { FromGrid(inGrid); }
        public DownSolver(IRectList inRects) { FromRects(inRects); }
        public DownSolver(ISerializedRects inSerializedRects) { FromSerializedRects(inSerializedRects); }
        public DownSolver(IQuadList inQuads) { FromQuads(inQuads); }
        public DownSolver(ITriangles inTriangles) { FromTriangles(inTriangles); }

        public void FromFilename(string filename)
        {
            if (filename.ToUpper().Contains(".PNG")) FromGrid(GlyphicsApi.PngToGrid(filename));
            if (filename.ToUpper().Contains(".STL")) FromTriangles(GlyphicsApi.StlToTriangles(filename));
            if (filename.ToUpper().Contains(".OBJ")) FromTriangles(GlyphicsApi.ObjToTriangles(filename));
            if (filename.ToUpper().Contains(".GIF"))
            {
                Grids = GlyphicsApi.GifToGrids(filename);
                Grid = Grids.GetGrid(0);
                FromGrid(Grid);
            }
            if (filename.ToUpper().Contains(".GLY"))
            {
                Codes = GlyphicsApi.GlyToCodes(filename);
                Code = Codes.GetCode(0);
                FromCode(Code);
            }
        }

        public void FromCode(ICode inCode)
        {
            Code = inCode;
            Codename = GlyphicsApi.CodeToCodename(Code);
            Tokens = GlyphicsApi.CodeToTokens(Code);
            FromTokens(Tokens);
        }

        public void FromBytecode(IBytecode inBytecode)
        {
            Bytecode = inBytecode;
            Tokens = GlyphicsApi.BytecodeToTokens(Bytecode);
            FromTokens(Tokens);
        }

        public void FromTokens(ITokenList inTokens)
        {
            Tokens = inTokens;
            Grid = GlyphicsApi.TokensToGrid(Tokens);
            FromGrid(Grid);
        }

        public void FromGrid(IGrid inGrid)
        {
            Grid = inGrid;
            GridOblique = GlyphicsApi.Renderer.RenderObliqueCells(Grid);
            Rects = GlyphicsApi.GridToRects(Grid);
            FromRects(Rects);
        }

        public void FromRects(IRectList inRects)
        {
            Rects = inRects;
            SerializedRects = GlyphicsApi.RectsToSerializedRects(Rects);
            SerializedRectsLimit255 = GlyphicsApi.RectsToSerializedRectsLimit255(Rects);
            FromSerializedRects(SerializedRects);
        }

        public void FromSerializedRects(ISerializedRects inSerializedRects)
        {
            SerializedRects = inSerializedRects;
            Rects = GlyphicsApi.SerializedRectsToRects(SerializedRects);
            Quads = GlyphicsApi.RectsToQuads(Rects);
            FromQuads(Quads);
        }

        public void FromQuads(IQuadList quads)
        {
            Triangles = GlyphicsApi.QuadsToTriangles(quads);
            FromTriangles(Triangles);
        }

        public void FromTriangles(ITriangles inTriangles)
        {
            Triangles = inTriangles;
        }
    }
}

