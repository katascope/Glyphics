#region Copyright
/*Copyright (c) 2015, Katascope
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlyphicsLibrary.Language
{
    //FlowSolver class, not really that efficient, but good for making inputs solve out to all possible outputs
    public class DownSolver
    {
        public ICodeList codes;
        public IGridList grids;
        public ICode code;
        public IBytecode bytecode;
        public ICodename codename;
        public ITokenList tokens;
        public IGrid grid;
        public IGrid gridOblique;
        public IRectList rects;
        public ISerializedRects serializedRects;
        public ISerializedRects serializedRectsLimit255;
        public IQuadList quads;
        public ITriangles triangles;
        public byte[] rawbytes;

        public DownSolver() { }
        public DownSolver(ICode inCode) { FromCode(inCode); }
        public DownSolver(string filename) { FromFilename(filename); }
        public DownSolver(ICodename inCodename) { codename = inCodename; }
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
                grids = GlyphicsApi.GifToGrids(filename);
                grid = grids.GetGrid(0);
                FromGrid(grid);
            }
            if (filename.ToUpper().Contains(".GLY"))
            {
                codes = GlyphicsApi.GlyToCodes(filename);
                code = codes.GetCode(0);
                FromCode(code);
            }
        }

        public void FromCode(ICode inCode)
        {
            code = inCode;
            codename = GlyphicsApi.CodeToCodename(code);
            tokens = GlyphicsApi.CodeToTokens(code);
            FromTokens(tokens);
        }

        public void FromBytecode(IBytecode inBytecode)
        {
            bytecode = inBytecode;
            tokens = GlyphicsApi.BytecodeToTokens(bytecode);
            FromTokens(tokens);
        }

        public void FromTokens(ITokenList inTokens)
        {
            tokens = inTokens;
            grid = GlyphicsApi.TokensToGrid(tokens);
            FromGrid(grid);
        }

        public void FromGrid(IGrid inGrid)
        {
            grid = inGrid;
            gridOblique = GlyphicsApi.Renderer.RenderObliqueCells(grid);
            rects = GlyphicsApi.GridToRects(grid);
            FromRects(rects);
        }

        public void FromRects(IRectList inRects)
        {
            rects = inRects;
            serializedRects = GlyphicsApi.RectsToSerializedRects(rects);
            serializedRectsLimit255 = GlyphicsApi.RectsToSerializedRectsLimit255(rects);
            FromSerializedRects(serializedRects);
        }

        public void FromSerializedRects(ISerializedRects inSerializedRects)
        {
            serializedRects = inSerializedRects;
            rects = GlyphicsApi.SerializedRectsToRects(serializedRects);
            quads = GlyphicsApi.RectsToQuads(rects);
            FromQuads(quads);
        }

        public void FromQuads(IQuadList quads)
        {
            triangles = GlyphicsApi.QuadsToTriangles(quads);
            FromTriangles(triangles);
        }

        public void FromTriangles(ITriangles inTriangles)
        {
            triangles = inTriangles;
        }
    }
}

