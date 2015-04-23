#region Copyright
/*Copyright (c) 2015, Katascope
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/
#endregion
using System.Collections.Generic;

namespace GlyphicsLibrary.Atomics
{
    //Converts rects to quads to triangles
    internal partial class Converter
    {
        private Converter() { }

        //Convert 6 Quads to 12 triangles, along the faces of a cube
        public static void RectToTrianglesCube(ref List<ITriangle> triangles, IRect rect)
        {
            IQuadList quads = Converter.RectToQuads(rect);
            //RectToTrianglesCube(ref triangles, rect);

            foreach (IQuad quad in quads)
            {
                ITriangles twoTriangles = Converter.QuadToTwoTriangles(quad);
                triangles.Add(twoTriangles.GetTriangleArray()[0]);
                triangles.Add(twoTriangles.GetTriangleArray()[1]);
            }
        }
        
        //This should be deprecated as it creates it's own vertices instead of using STL
        public static ITriangles RectsToTrianglesCube(IRectList rectSet)
        {
            var triangles = new List<ITriangle>();

            //Iterate through reach rectangle(volume/cube) creating triangles
            foreach (IRect rect in rectSet)
            {
                IQuadList quads = Converter.RectToQuads(rect);
                //RectToTrianglesCube(ref triangles, rect);
                
                foreach (IQuad quad in quads)
                {
                    ITriangles twoTriangles = Converter.QuadToTwoTriangles(quad);
                    triangles.Add(twoTriangles.GetTriangleArray()[0]);
                    triangles.Add(twoTriangles.GetTriangleArray()[1]);
                }
            }

            return new CTriangles(triangles.ToArray());
        }
    }
}


