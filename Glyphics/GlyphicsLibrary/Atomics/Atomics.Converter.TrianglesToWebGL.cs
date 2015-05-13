#region Copyright
/*Copyright (c) 2015, Katascope
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/
#endregion
using System.Collections.Generic;
using System.Text;

namespace GlyphicsLibrary.Atomics
{
    internal partial class Converter
    {
        class CIndexedTriangles : IIndexedTriangles
        {
            public List<IFloat3> Vertices { get; private set; }
            public List<IInt3> Faces { get; private set; }

            public string VerticesString
            {
                get
                {
                    var sb = new StringBuilder();

                    foreach (IFloat3 f3 in Vertices)
                    {
                        sb.Append(" " + f3.X + "," + f3.Y + "," + f3.Z + ",\r\n");
                    }

                    return sb.ToString();
                }
            }

            public string FacesString
            {
                get
                {
                    var sb = new StringBuilder();

                    foreach (IInt3 i3 in Faces)
                    {
                        sb.Append(" " + i3.V1 + "," + i3.V2 + "," + i3.V3 + ",\r\n");
                    }

                    return sb.ToString();
                }
            }

            public CIndexedTriangles(ITriangles triangles)
            {
                //Indexer doesn't *actually* have to try and reduce..

                Vertices = new List<IFloat3>();
                Faces = new List<IInt3>();

                int faceId = 0;
                ITriangle[] triangleSet = triangles.GetTriangleArray();

                foreach (ITriangle triangle in triangleSet)
                {
                    Vertices.Add(triangle.Vertex1); 
                    Vertices.Add(triangle.Vertex2);
                    Vertices.Add(triangle.Vertex3);
                    IInt3 i3 = new CInt3(faceId, faceId + 1, faceId + 2);
                    faceId+=3;
                    Faces.Add(i3);
                }
            }
        }

        public static string TrianglesToWebGl(ITriangles triangles, string declarationName)
        {
            IIndexedTriangles iit = new CIndexedTriangles(triangles);

            var sb = new StringBuilder();

            sb.Append("var " + declarationName + "Vertices = [ ");
            sb.Append(iit.VerticesString);
            sb.Append("]\r\n");

            sb.Append("\r\n");

            sb.Append("var " + declarationName + "Faces = [ ");
            sb.Append(iit.FacesString);
            sb.Append("]\r\n");


            string str = sb.ToString();
            return str;
        }
    }
}



