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
    internal class TriangleUnitCube
    {
        private TriangleUnitCube() { }

        public static ITriangles GetUnitCube()
        {
            var triangles = new List<ITriangle>();

            //Front lower right
            ITriangle triangle = new CTriangle(0.0f, 0.0f, 1.0f);
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
