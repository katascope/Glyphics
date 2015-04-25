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
    internal partial class Converter
    {
        //Convert one quad into two triangles
        public static ITriangles QuadToTwoTriangles(IQuad quad)
        {
            var triangleList = new List<ITriangle>();
            ITriangles triangles = new CTriangles();

            var x1 = (float)quad.Pt1.X;
            var x2 = (float)quad.Pt2.X;
            var y1 = (float)quad.Pt1.Y;
            var y2 = (float)quad.Pt2.Y;
            var z1 = (float)quad.Pt1.Z;
            var z2 = (float)quad.Pt2.Z;

            QuadAxis qa = quad.FindAxis();

            if (qa == QuadAxis.X)
            {
                float sameX = x1;

                ITriangle triangle1 = new CTriangle();
                triangle1.SetTriangle(
                    sameX, y1, z1,
                    sameX, y2, z1,
                    sameX, y2, z2
                    );

                /*
                    sameX, y1, z1,
                    sameX, y2, z1,
                    sameX, y2, z2
                 * );
                 */
                triangle1.Properties = quad.Properties.Clone();
                triangleList.Add(triangle1);

                ITriangle triangle2 = new CTriangle();
                triangle2.SetTriangle(
                    sameX, y1, z1,
                    sameX, y1, z2,
                    sameX, y2, z2);
                triangle2.Properties = quad.Properties.Clone();
                triangleList.Add(triangle2);
            }
            if (qa == QuadAxis.Y)
            {
                float sameY = y1;

                ITriangle triangle1 = new CTriangle();
                triangle1.SetTriangle(
                    x1, sameY, z1,
                    x2, sameY, z2,
                    x1, sameY, z2);
                triangle1.Properties = quad.Properties.Clone();
                triangleList.Add(triangle1);

                ITriangle triangle2 = new CTriangle();
                triangle2.SetTriangle(
                    x1, sameY, z1,
                    x2, sameY, z1,
                    x2, sameY, z2);
                triangle2.Properties = quad.Properties.Clone();
                triangleList.Add(triangle2);
            }
            if (qa == QuadAxis.Z)
            {
                float sameZ = z1;

                ITriangle triangle1 = new CTriangle();
                triangle1.SetTriangle(
                    x1, y1, sameZ,
                    x2, y2, sameZ,
                    x1, y2, sameZ);
                triangle1.Properties = quad.Properties.Clone();
                triangleList.Add(triangle1);

                ITriangle triangle2 = new CTriangle();
                triangle2.SetTriangle(
                    x1, y1, sameZ,
                    x2, y1, sameZ,
                    x2, y2, sameZ);
                triangle2.Properties = quad.Properties.Clone();
                triangleList.Add(triangle2);
            }

            triangles.SetTriangles(triangleList.ToArray());
            return triangles;
        }

        //convert a quad to its two triangles
        public static ITriangles QuadsToTriangles(IQuadList quads)
        {
            var triangleList = new List<ITriangle>();

            foreach (IQuad quad in quads)
            {
                ITriangles triangles = QuadToTwoTriangles(quad);
                triangles.GetTriangleArray()[0].CalcNormal();
                triangles.GetTriangleArray()[1].CalcNormal();
                triangleList.Add(triangles.GetTriangleArray()[0]);
                triangleList.Add(triangles.GetTriangleArray()[1]);
            }

            ITriangles trianglesPack = new CTriangles();
            trianglesPack.SetTriangles(triangleList.ToArray());

            return trianglesPack;
        }
    }
}
