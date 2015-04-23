using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlyphicsLibrary.Atomics
{
    internal class TriangleUnitCube
    {
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
