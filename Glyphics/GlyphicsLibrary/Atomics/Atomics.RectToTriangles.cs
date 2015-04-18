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
    internal class QuadToTriangles
    {
        private QuadToTriangles() { }

        //Create triangles from quads, along same X axis
        public static void QuadToTrianglesSameX(ref List<ITriangle> triangles, double y1, double z1, double y2, double z2, double x)
        {
            var triangle = new CTriangle();
            triangle.SetTriangle((float)x, (float)y1, (float)z1,
                              (float)x, (float)y2, (float)z1,
                              (float)x, (float)y2, (float)z2);

            triangles.Add(triangle);

            triangle = new CTriangle();
            triangle.SetTriangle((float)x, (float)y1, (float)z1,
                              (float)x, (float)y2, (float)z2,
                              (float)x, (float)y1, (float)z2);
            triangles.Add(triangle);
        }

        //Create triangles from quads, along same Y axis
        public static void QuadToTrianglesSameY(ref List<ITriangle> triangles, double x1, double z1, double x2, double z2, double y)
        {
            var triangle = new CTriangle();
            triangle.SetTriangle((float)x1, (float)y, (float)z1,
                              (float)x2, (float)y, (float)z2,
                              (float)x1, (float)y, (float)z2);
            triangles.Add(triangle);

            triangle = new CTriangle();
            triangle.SetTriangle((float)x1, (float)y, (float)z1,
                              (float)x2, (float)y, (float)z1,
                              (float)x2, (float)y, (float)z2);
            triangles.Add(triangle);
        }

        //Create triangles from quads, along same Z axis
        public static void QuadToTrianglesSameZ(ref List<ITriangle> triangles, double x1, double y1, double x2, double y2, double z)
        {
            var triangle = new CTriangle();
            triangle.SetTriangle((float)x1, (float)y1, (float)z,
                              (float)x2, (float)y2, (float)z,
                              (float)x1, (float)y2, (float)z);
            triangles.Add(triangle);

            triangle = new CTriangle();
            triangle.SetTriangle((float)x1, (float)y1, (float)z,
                              (float)x2, (float)y1, (float)z,
                              (float)x2, (float)y2, (float)z);
            triangles.Add(triangle);
        }

        //Convert 6 Quads to 12 triangles, along the faces of a cube
        public static void RectToTrianglesCube(ref List<ITriangle> triangles, IRect rect)
        {
            //Front
            QuadToTrianglesSameZ(ref triangles, rect.Pt1.X, rect.Pt1.Y, rect.Pt2.X, rect.Pt2.Y, rect.Pt2.Z);
            //Back                                  
            QuadToTrianglesSameZ(ref triangles, rect.Pt1.X, rect.Pt1.Y, rect.Pt2.X, rect.Pt2.Y, rect.Pt1.Z);

            //Top                                   
            QuadToTrianglesSameY(ref triangles, rect.Pt1.X, rect.Pt1.Z, rect.Pt2.X, rect.Pt2.Z, rect.Pt2.Y);
            //Bottom                                
            QuadToTrianglesSameY(ref triangles, rect.Pt1.X, rect.Pt1.Z, rect.Pt2.X, rect.Pt2.Z, rect.Pt1.Y);

            //Left                                  
            QuadToTrianglesSameX(ref triangles, rect.Pt1.Y, rect.Pt1.Z, rect.Pt2.Y, rect.Pt2.Z, rect.Pt1.X);
            //Right                                 
            QuadToTrianglesSameX(ref triangles, rect.Pt1.Y, rect.Pt1.Z, rect.Pt2.Y, rect.Pt2.Z, rect.Pt2.X);
        }

        //This should be deprecated as it creates it's own vertices instead of using STL
        public static ITriangles RectsToTrianglesCube(IRectList rectSet)
        {
            var triangles = new List<ITriangle>();

            //Iterate through reach rectangle(volume/cube) creating triangles
            foreach (IRect rect in rectSet)
                RectToTrianglesCube(ref triangles, rect);

            return new CTriangles(triangles.ToArray());
        }
    }
}

