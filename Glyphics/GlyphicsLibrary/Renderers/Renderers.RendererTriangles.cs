#region Copyright
/*Copyright (c) 2015, Katascope
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/
#endregion
using System.Collections.Generic;

namespace GlyphicsLibrary.Renderers
{
    //Partial class to render triangles
    internal partial class Renderer : IRenderer
    {
        //Assumes triangle are already unit-normalized to 1x1x1
        public static void MapRectToTriangles(ref List<ITriangle> renderedTriangleSoup, ITriangles triangles, IRect rect)
        {
            double midX = (rect.Width)/2 + rect.Pt1.X;
            double midY = (rect.Height)/2 + rect.Pt1.Y;
            double midZ = (rect.Depth)/2 + rect.Pt1.Z;

            ITriangles trianglesClone = triangles.Clone();

            trianglesClone.Scale((float)rect.Width, (float)rect.Height, (float)rect.Depth);
            trianglesClone.Translate((float)midX, (float)midY, (float)midZ);

            renderedTriangleSoup.AddRange(trianglesClone.GetTriangleArray());
        }

        //Render rectangles out to Stl using a list to select shapes from
        public ITriangles RenderRectsAsStlMapping(IRectList rects, ITrianglesList trianglesList)
        {
            if (rects == null || trianglesList == null) return null;

            var renderedTriangleSoup = new List<ITriangle>();
            foreach (IRect rect in rects)
            {
                if (rect.Properties.ShapeId >= trianglesList.Count)
                    return null;

                ITriangles triangles = trianglesList.GetTriangles(rect.Properties.ShapeId);

                MapRectToTriangles(ref renderedTriangleSoup, triangles, rect);
            }
            ITriangles triangleSet = new Atomics.CTriangles();
            ITriangle[] triangleArray = renderedTriangleSoup.ToArray();
            triangleSet.SetTriangles(triangleArray);

            //Center entire grid/set on origin, mirror, and rotate to correct
            triangleSet.Translate(-rects.SizeX / 2.0f, -rects.SizeY / 2.0f, -rects.SizeZ / 2.0f);
            
            return triangleSet;
        }

        //Render triangles directly to a grid by drawing the triangles
        public void RenderTrianglesToGrid(ITriangles triangles, IGrid grid)
        {
            if (triangles == null || grid == null) return;

            IPainter painter = new Painters.CPainter();
            IGridContext bgc = new ByteGrid.CGridContext(grid);

            float sx = grid.SizeX - 1;
            float sy = grid.SizeY - 1;
            float sz = grid.SizeZ - 1;

            bgc.Pen.Rgba = Atomics.Converter.Rgba2Ulong(255, 255, 255, 255);
            foreach (ITriangle triangle in triangles.GetTriangleArray())
            {
                var x1 = (int)((triangle.Vertex1.X + 0.5f) * sx);
                var y1 = (int)((triangle.Vertex1.Y + 0.5f) * sy);
                var z1 = (int)((triangle.Vertex1.Z + 0.5f) * sz);

                var x2 = (int)((triangle.Vertex2.X + 0.5f) * sx);
                var y2 = (int)((triangle.Vertex2.Y + 0.5f) * sy);
                var z2 = (int)((triangle.Vertex2.Z + 0.5f) * sz);

                var x3 = (int)((triangle.Vertex3.X + 0.5f) * sx);
                var y3 = (int)((triangle.Vertex3.Y + 0.5f) * sy);
                var z3 = (int)((triangle.Vertex3.Z + 0.5f) * sz);

                //This requires a filled triangle3d function
                painter.DrawFillTriangle3D(bgc, x1, y1, z1, x2, y2, z2, x3, y3, z3);
            }
        }
    }
}
