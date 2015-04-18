#region Copyright
/*Copyright (c) 2015, Katascope
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/
#endregion
using System;
using System.IO;

namespace GlyphicsLibrary.ByteGrid
{
    //Utility class for writing STL files
    internal class FileStlWrite
    {
        private FileStlWrite() { }

        //Write a binary STL file
        public static void WriteFile(ITriangles triangles, string filename)
        {
            using (var writer = new BinaryWriter(File.Open(filename, FileMode.Create)))
            {
                ITriangle[] triSet = triangles.GetTriangleArray();

                //Write 80 bytes of header
                for (int i = 0; i < 80; i++)
                {
                    writer.Write((byte)0);
                }

                //Then write the length as 4 bytes
                writer.Write((UInt32)triSet.Length);

                //Then write the triangles
                foreach (ITriangle triangle in triSet)
                {
                    //Write normal as float
                    writer.Write(triangle.Normal.X);
                    writer.Write(triangle.Normal.Z);
                    writer.Write(triangle.Normal.Y);

                    //Write vertex1 as float
                    writer.Write(triangle.Vertex1.X);
                    writer.Write(triangle.Vertex1.Z);
                    writer.Write(triangle.Vertex1.Y);

                    //Write vertex2 as float
                    writer.Write(triangle.Vertex2.X);
                    writer.Write(triangle.Vertex2.Z);
                    writer.Write(triangle.Vertex2.Y);

                    //Write vertex3 as float
                    writer.Write(triangle.Vertex3.X);
                    writer.Write(triangle.Vertex3.Z);
                    writer.Write(triangle.Vertex3.Y);

                    //Just write 0 for attribute_by_count as 2 bytes
                    writer.Write((UInt16)0xFFFF);
                }
            }
        }
    }
}
