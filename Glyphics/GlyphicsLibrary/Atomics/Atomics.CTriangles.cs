#region Copyright
/*Copyright (c) 2015, Katascope
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/
#endregion
using System.Text;
using System.Collections.Generic;

namespace GlyphicsLibrary.Atomics
{
    //Implementation of ITriangles, see for usage
    internal class CTriangles : ITriangles
    {
        //Actual string name
        public string Name { get; set; }

        private ITriangle[] _triangleArray;

        //Get count of triangles in array
        public int Count { get { return _triangleArray.Length; } }

        //Constructor
        public CTriangles()
        {
            _triangleArray = null;
        }

        //Add triangles
        public void Add(ITriangles tris)
        {
            if (tris == null) return;

            //Will need to resize triangle array, so copy it into a list for now

            var triList = new List<ITriangle>();

            //Add current ones to new list
            if (_triangleArray != null)
            {
                triList.AddRange(_triangleArray);
            }

            //Add tris's triangles to list
            triList.AddRange(tris.GetTriangleArray());

            _triangleArray = triList.ToArray();
        }

        //Assignment constructor
        public CTriangles(ITriangle[] tris)
        {
            _triangleArray = tris;
        }

        //Get the array of triangles
        public ITriangle[] GetTriangleArray()
        {
            return _triangleArray;
        }

        //Set the array of triangles
        public void SetTriangles(ITriangle[] triangles)
        {
            _triangleArray = triangles;
        }

        //Compare to another ITriangles
        public bool IsEqualTo(ITriangles triangles)
        {
            if (triangles == null || triangles.Count != Count)
                return false;

            for (int i = 0; i < triangles.Count; i++)
            {
                ITriangle t1 = _triangleArray[i];
                ITriangle t2 = triangles.GetTriangleArray()[i];

                if (CompareNormals(t1, t2) != true)
                    return false;

                if (CompareVertices(t1, t2) != true)
                    return false;
            }
            return true;
        }

        private static bool CompareVertices(ITriangle triangle1, ITriangle triangle2)
        {
            if (Compare.CompareFloatAreEqual(triangle1.Vertex1.X, triangle2.Vertex1.X)) return true;
            if (Compare.CompareFloatAreEqual(triangle1.Vertex1.Y, triangle2.Vertex1.Y)) return true;
            if (Compare.CompareFloatAreEqual(triangle1.Vertex1.Z, triangle2.Vertex1.Z)) return true;

            if (Compare.CompareFloatAreEqual(triangle1.Vertex2.X, triangle2.Vertex2.X)) return true;
            if (Compare.CompareFloatAreEqual(triangle1.Vertex2.Y, triangle2.Vertex2.Y)) return true;
            if (Compare.CompareFloatAreEqual(triangle1.Vertex2.Z, triangle2.Vertex2.Z)) return true;

            if (Compare.CompareFloatAreEqual(triangle1.Vertex3.X, triangle2.Vertex3.X)) return true;
            if (Compare.CompareFloatAreEqual(triangle1.Vertex3.Y, triangle2.Vertex3.Y)) return true;
            if (Compare.CompareFloatAreEqual(triangle1.Vertex3.Z, triangle2.Vertex3.Z)) return true;
            
            return false;
        }

        private static bool CompareNormals(ITriangle triangle1, ITriangle triangle2)
        {
            if (Compare.CompareFloatAreEqual(triangle1.Normal.X, triangle2.Normal.X)) return true;
            if (Compare.CompareFloatAreEqual(triangle1.Normal.Y, triangle2.Normal.Y)) return true;
            if (Compare.CompareFloatAreEqual(triangle1.Normal.Z, triangle2.Normal.Z)) return true;

            return false;
        }

        //Duplicate object
        public ITriangles Clone()
        {
            var triangleList = new List<ITriangle>();

            foreach (ITriangle triangle in _triangleArray)
            {
                triangleList.Add(triangle.Clone());

            }
            ITriangles newTriangles = new CTriangles(triangleList.ToArray());

            newTriangles.Name = Name;
            return newTriangles;
        }

        //Calculate boundaries of vertices, as a volume
        public IRect TrianglesBoundaries
        {
            get
            {
                const int limit = 10000;
                IRect rect = new CRect(limit, limit, limit, -limit, -limit, -limit);

                foreach (ITriangle triangle in _triangleArray)
                {
                    double minX = Min(triangle.Vertex1.X, triangle.Vertex2.X, triangle.Vertex2.X);
                    double maxX = Max(triangle.Vertex1.X, triangle.Vertex2.X, triangle.Vertex2.X);
                    double minY = Min(triangle.Vertex1.Y, triangle.Vertex2.Y, triangle.Vertex2.Y);
                    double maxY = Max(triangle.Vertex1.Y, triangle.Vertex2.Y, triangle.Vertex2.Y);
                    double minZ = Min(triangle.Vertex1.Z, triangle.Vertex2.Z, triangle.Vertex2.Z);
                    double maxZ = Max(triangle.Vertex1.Z, triangle.Vertex2.Z, triangle.Vertex2.Z);
                    if (minX < rect.Pt1.X) rect.Pt1.X = minX;
                    if (maxX > rect.Pt2.X) rect.Pt2.X = maxX;
                    if (minY < rect.Pt1.Y) rect.Pt1.Y = minY;
                    if (maxY > rect.Pt2.Y) rect.Pt2.Y = maxY;
                    if (minZ < rect.Pt1.Z) rect.Pt1.Z = minZ;
                    if (maxZ > rect.Pt2.Z) rect.Pt2.Z = maxZ;
                }
                return rect;
            }
        }

        //Find the minimum of v1, v2, v3
        private static double Min(double v1, double v2, double v3)
        {
            if (v1 < v2 && v1 < v3) return v1;
            if (v2 < v3 && v2 < v1) return v2;
            if (v3 < v1 && v3 < v2) return v3;
            return v1;
        }

        //Find the maximum of v1, v2, v3
        private static double Max(double v1, double v2, double v3)
        {
            if (v1 > v2 && v1 > v3) return v1;
            if (v2 > v3 && v2 > v1) return v2;
            if (v3 > v1 && v3 > v2) return v3;
            return v1;
        }

        //Relative translation XYZ
        public void Translate(float x, float y, float z)
        {
            foreach (ITriangle triangle in _triangleArray)
                triangle.Translate(x, y, z);
        }

        //Relative rotation angle in degrees not radians
        public void RotateX(float angle)
        {
            foreach (ITriangle triangle in _triangleArray)
                triangle.RotateX(angle);
        }

        //Relative rotation angle in degrees not radians
        public void RotateY(float angle)
        {
            foreach (ITriangle triangle in _triangleArray)
                triangle.RotateY(angle);
        }

        //Relative rotation angle in degrees not radians
        public void RotateZ(float angle)
        {
            foreach (ITriangle triangle in _triangleArray)
                triangle.RotateZ(angle);
        }

        //Relative scale XYZ
        public void Scale(float scaleX, float scaleY, float scaleZ)
        {
            foreach (ITriangle triangle in _triangleArray)
                triangle.Scale(scaleX, scaleY, scaleZ);
        }

        //Flip X
        public void MirrorX()
        {
            foreach (ITriangle triangle in _triangleArray)
                triangle.MirrorX();
        }

        //Flip Y
        public void MirrorY()
        {
            foreach (ITriangle triangle in _triangleArray)
                triangle.MirrorY();
        }

        //Flip Z
        public void MirrorZ()
        {
            foreach (ITriangle triangle in _triangleArray)
                triangle.MirrorZ();
        }

        //Rescales a set of triangles to a max size of 1 on any dimension
        // i.e. normalizes to 1x1x1 if perfect cube, otherwise only 1x?x? for the longest dimension
        public void ReduceToUnit()
        {
            IRect rect = TrianglesBoundaries;

            var max = (float)Max(rect.Width, rect.Height, rect.Depth);
            float factor = (1 / max);

            double centerX = ((rect.Width )  / 2 + rect.Pt1.X) * factor;
            double centerY = ((rect.Height ) / 2 + rect.Pt1.Y) * factor;
            double centerZ = ((rect.Depth )  / 2 + rect.Pt1.Z) * factor;

            foreach (ITriangle triangle in _triangleArray)
            {
                triangle.Vertex1.X *= factor;
                triangle.Vertex1.Y *= factor;
                triangle.Vertex1.Z *= factor;

                triangle.Vertex2.X *= factor;
                triangle.Vertex2.Y *= factor;
                triangle.Vertex2.Z *= factor;

                triangle.Vertex3.X *= factor;
                triangle.Vertex3.Y *= factor;
                triangle.Vertex3.Z *= factor;

                triangle.Vertex1.X -= (float)centerX;
                triangle.Vertex1.Y -= (float)centerY;
                triangle.Vertex1.Z -= (float)centerZ;

                triangle.Vertex2.X -= (float)centerX;
                triangle.Vertex2.Y -= (float)centerY;
                triangle.Vertex2.Z -= (float)centerZ;

                triangle.Vertex3.X -= (float)centerX;
                triangle.Vertex3.Y -= (float)centerY;
                triangle.Vertex3.Z -= (float)centerZ;
            }
        }

        //Readable description
        public override string ToString()
        {
            var sb = new StringBuilder();

            //sb.Append(_triangleArray.Length + "\r\n"); 
            
            foreach (ITriangle triangle in _triangleArray)
            {
                sb.Append("[");
                sb.Append(triangle.Normal);
                sb.Append("/");
                sb.Append(triangle.Vertex1 + ",");
                sb.Append(triangle.Vertex2 + ",");
                sb.Append(triangle.Vertex3);
                if (triangle.Properties != null)
                {
                    sb.Append("/");
                    sb.Append(triangle.Properties);
                }
                sb.Append("]\r\n");
            }
            return sb.ToString();
        }
        
        //For putting on ground (elevation zero, for easy printing)
        public void PutOnGround()
        {
            var y = (float)TrianglesBoundaries.Pt1.Y;

            foreach (ITriangle triangle in _triangleArray)
                triangle.Translate(0, -y, 0);
        }

        //Calculate normals for all triangles
        public void CalcNormals()
        {
            foreach (ITriangle triangle in _triangleArray)
            {
                triangle.CalcNormal();
            }
        }
    }
}
