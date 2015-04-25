#region Copyright
/*Copyright (c) 2015, Katascope
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/
#endregion

namespace GlyphicsLibrary
{
    //Set of rendering methods for top level drawing convenience
    public interface IRenderer
    {
        //Renders a list of Rects to an IGrid
        void RenderRectsToGrid(IRectList rects, IGrid grid);

        //Renders IGrid to a new IGrid using oblique cell sprites
        IGrid RenderObliqueCells(IGrid grid);

        //Renders IRectList to a new IGrid using oblique cell sprites
        IGrid RenderObliqueCellsRects(IRectList rects);

        //Renders ICode directly out to a PNG filename
        void RenderObliqueOut(string fileName, ICode glyphicsCode);

        //Renders IRectList using ITrianglesList to a new ITriangles
        ITriangles RenderRectsAsStlMapping(IRectList rects, ITrianglesList trianglesList);

        //Renders ITriangles to an IGrid
        void RenderTrianglesToGrid(ITriangles triangles, IGrid grid);
    }
}
