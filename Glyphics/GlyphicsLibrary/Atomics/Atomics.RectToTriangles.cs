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
    internal class RectToTriangles
    {
        private RectToTriangles() { }

        //Convert 6 Quads to 12 triangles, along the faces of a cube
        public static void RectToTrianglesCube(ref List<ITriangle> triangles, IRect rect)
        {
            IQuadList quads = RectToQuads(rect);
            //RectToTrianglesCube(ref triangles, rect);

            foreach (IQuad quad in quads)
            {
                ITriangles twoTriangles = QuadToTwoTriangles(quad);
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
                IQuadList quads = RectToQuads(rect);
                //RectToTrianglesCube(ref triangles, rect);
                
                foreach (IQuad quad in quads)
                {
                    ITriangles twoTriangles = QuadToTwoTriangles(quad);
                    triangles.Add(twoTriangles.GetTriangleArray()[0]);
                    triangles.Add(twoTriangles.GetTriangleArray()[1]);
                }
            }

            return new CTriangles(triangles.ToArray());
        }

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

        //Convert rectangle to its 6 quads
        public static IQuadList RectToQuads(IRect rect)
        {
            IQuadList quads = new CQuadList();

            IQuad quadTopper = new CQuad(rect.Pt1.X, rect.Pt2.Y, rect.Pt1.Z, rect.Pt2.X, rect.Pt2.Y, rect.Pt2.Z);
            quadTopper.Properties = rect.Properties.Clone();
            IQuad quadBottom = new CQuad(rect.Pt1.X, rect.Pt1.Y, rect.Pt1.Z, rect.Pt2.X, rect.Pt1.Y, rect.Pt2.Z);
            quadBottom.Properties = rect.Properties.Clone();

            IQuad quadFront = new CQuad(rect.Pt1.X, rect.Pt1.Y, rect.Pt2.Z, rect.Pt2.X, rect.Pt2.Y, rect.Pt2.Z);
            quadFront.Properties = rect.Properties.Clone();
            IQuad quadBack = new CQuad(rect.Pt1.X, rect.Pt1.Y, rect.Pt1.Z, rect.Pt2.X, rect.Pt2.Y, rect.Pt1.Z);
            quadBack.Properties = rect.Properties.Clone();

            IQuad quadRight = new CQuad(rect.Pt2.X, rect.Pt1.Y, rect.Pt1.Z, rect.Pt2.X, rect.Pt2.Y, rect.Pt2.Z);
            quadRight.Properties = rect.Properties.Clone();
            IQuad quadLeft = new CQuad(rect.Pt1.X, rect.Pt1.Y, rect.Pt1.Z, rect.Pt1.X, rect.Pt2.Y, rect.Pt2.Z);
            quadLeft.Properties = rect.Properties.Clone();

            quads.AddQuad(quadFront);
            quads.AddQuad(quadBack);
            quads.AddQuad(quadTopper);
            quads.AddQuad(quadBottom);
            quads.AddQuad(quadRight);
            quads.AddQuad(quadLeft);

            return quads;
        }

        //Convert rectangles to their 6 quads
        public static IQuadList RectsToQuads(IRectList rectSet)
        {
            IQuadList quadsMacro = new CQuadList();

            foreach (IRect rect in rectSet)
            {
                IQuadList quads = RectToQuads(rect);

                foreach (IQuad quad in quads)
                    quadsMacro.AddQuad(quad);
            }

            //Remove redundant ones automatically
            RemoveRedundantQuads(quadsMacro);

            return quadsMacro;
        }

        //Remove redundant quads to reduce total count
        public static int RemoveRedundantQuads(IQuadList quads)
        {
            int removedCount = 0;

            for (int me = 0;me<quads.Count;me++)
            {
                for (int you = me; you < quads.Count; you++)
                {
                    if (me != you)
                    {
                        IQuad meQuad = quads.GetQuad(me);
                        IQuad youQuad = quads.GetQuad(you);
                        //I saw a duplicate
                        if  ( (meQuad.Pt1.IsEqualTo(youQuad.Pt1))
                              && (meQuad.Pt2.IsEqualTo(youQuad.Pt2)))
                        {
                            //we have a redundancy
                            removedCount++;
                            quads.RemoveQuad(youQuad);
                        }
                    }
                }
            }
            return removedCount;
        }

        public static ITriangles GetUnitCube()
        {
            List<ITriangle> triangles = new List<ITriangle>();

            ITriangle triangle = null;

            //Front lower right
            triangle = new CTriangle(0.0f, 0.0f, 1.0f);
            triangle.SetTriangle(0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 1.0f, 1.0f, 0.0f);
            triangles.Add(triangle);
            //Front Upper left
            triangle = new CTriangle(0.0f, 0.0f, 1.0f);
            triangle.SetTriangle(0.0f, 0.0f, 0.0f, 1.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.0f);
            triangles.Add(triangle);
            
            //Left Side back bottom
            triangle = new CTriangle(-1.0f, 0.0f, 0.0f);
            triangle.SetTriangle(0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 1.0f, 0.0f, 1.0f, 0.0f);
            triangles.Add(triangle);
            //Left side front top
            triangle = new CTriangle(-1.0f, 0.0f, 0.0f);
            triangle.SetTriangle(0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 1.0f, 1.0f);
            triangles.Add(triangle);
            
            //Top
            triangle = new CTriangle(0.0f, 1.0f, 0.0f);
            triangle.SetTriangle(0.0f, 1.0f, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 0.0f);
            triangles.Add(triangle);
            triangle = new CTriangle(0.0f, 1.0f, 0.0f);
            triangle.SetTriangle(0.0f, 1.0f, 0.0f, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f);
            triangles.Add(triangle);

            //Right
            triangle = new CTriangle(1.0f, 0.0f, 0.0f);
            triangle.SetTriangle(1.0f, 0.0f, 0.0f, 1.0f, 1.0f, 0.0f, 1.0f, 1.0f, 1.0f);
            triangles.Add(triangle);
            triangle = new CTriangle(1.0f, 0.0f, 0.0f);
            triangle.SetTriangle(1.0f, 0.0f, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 0.0f, 1.0f);
            triangles.Add(triangle);
            
            //Bottom
            triangle = new CTriangle(0.0f, -1.0f, 0.0f);
            triangle.SetTriangle(0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.0f, 1.0f);
            triangles.Add(triangle);
            triangle = new CTriangle(0.0f, -1.0f, 0.0f);
            triangle.SetTriangle(0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 1.0f, 0.0f, 0.0f, 1.0f);
            triangles.Add(triangle);

            //Back
            triangle = new CTriangle(0.0f, 0.0f, 1.0f);
            triangle.SetTriangle(0.0f, 0.0f, 1.0f, 1.0f, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f);
            triangles.Add(triangle);
            triangle = new CTriangle(0.0f, 0.0f, 1.0f);
            triangle.SetTriangle(0.0f, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 0.0f, 1.0f, 1.0f);
            triangles.Add(triangle);
            
            return new CTriangles(triangles.ToArray());
        }
    }
}


