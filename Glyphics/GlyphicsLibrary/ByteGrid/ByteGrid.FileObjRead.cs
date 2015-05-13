using System;
using System.Collections.Generic;
using System.IO;
using GlyphicsLibrary.Atomics;

namespace GlyphicsLibrary.ByteGrid
{
    //Utility class for reading OBJ files
    internal class FileObjRead
    {
        private FileObjRead() { }

        public static ITriangles ReadfileAscii(string filename)
        {
            using (var reader = new StreamReader(filename))
            {
                var triangles = new List<ITriangle>();
                var vertices = new List<IFloat3>();

                while (reader.EndOfStream == false)
                {
                    string str = reader.ReadLine().TrimStart();
                    string command = str.Split(' ')[0];

                    if (String.CompareOrdinal("#", command)==0)
                    {
                        //Just a comment, ignore
                    }
                    if (String.CompareOrdinal("g", command)==0)
                    {
                        //g Object001
                    }
                    if (String.CompareOrdinal("v", command)==0)
                    {
                        //v 0.000000E+00 0.000000E+00 78.0000

                        string[] parts = str.Split(' ');
                        var x = (float)Convert.ToDouble(parts[1]);
                        var y = (float)Convert.ToDouble(parts[2]);
                        var z = (float)Convert.ToDouble(parts[3]);

                        //Flip y, z
                        IFloat3 float3 = new CFloat3(x, z, y);
                        vertices.Add(float3);
                    }
                    if (String.CompareOrdinal("f", command) == 0)
                    {
                        //f   1 2 3
                        str = str.Replace("  ", " ").Replace("  ", " ").Replace("  ", " ");
                        string[] parts = str.Split(' ');
                        int v1 = Convert.ToInt32(parts[1]) - 1;
                        int v2 = Convert.ToInt32(parts[2]) - 1;
                        int v3 = Convert.ToInt32(parts[3]) - 1;
 
                        ITriangle triangle = new CTriangle(
                                        vertices[v1].X,
                                        vertices[v1].Y,
                                        vertices[v1].Z,
                                        vertices[v2].X,
                                        vertices[v2].Y,
                                        vertices[v2].Z,
                                        vertices[v3].X,
                                        vertices[v3].Y,
                                        vertices[v3].Z);

                        triangles.Add(triangle);
                    }
                }
                ITriangles triangleSet = new CTriangles();
                triangleSet.SetTriangles(triangles.ToArray());
                return triangleSet;
            }
        }
    }
}
                        
                        