#region Copyright
/*Copyright (c) 2015, Katascope
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/
#endregion
using System;
using GlyphicsLibrary;

namespace ExampleSTLToQuadPrint
{
    /* Example: STL To Quad Print
     * Purpose: Make a given STL into four of them, for easier 3d printing
     * 
     * Example Concepts:
     *  1) Loading ITriangles from STL file
     *  2) Cloning ITriangles
     *  3) Moving ITriangles
     *  4) Combining ITiangles into a new set
     *  5)
     */
    class ExampleStlToQuadPrint
    {
        static void Main()
        {
            //Use nice companion cube, source: http://www.thingiverse.com/thing:13588
            const string inputFilenameStl = "..\\..\\archquad.stl";
            Console.WriteLine("Input filename: {0}", inputFilenameStl);

            //Load the triangles from the STL file and reduce to a unit 1x1x1 size
            ITriangles triangles = GlyphicsApi.StlToTriangles(inputFilenameStl);
            Console.WriteLine("Triangle count: {0}", triangles.Count);

            //Say the dimensions of it too
            IRect triangleBoundaries = triangles.TrianglesBoundaries;
            Console.WriteLine("{0} dimensions {1}mm x {2}mm x {3}mm", 
                inputFilenameStl,
                (int)triangleBoundaries.Width,
                (int)triangleBoundaries.Height,
                (int)triangleBoundaries.Depth);

            //Create four (for the quad)
            Console.WriteLine("Cloning quads");
            ITriangles trianglesNw = triangles.Clone();
            ITriangles trianglesNe = triangles.Clone();
            ITriangles trianglesSw = triangles.Clone();
            ITriangles trianglesSe = triangles.Clone();

            ITriangles trianglesQuad = GlyphicsApi.CreateTriangles();

            //Get the boundaries of the STL model, use that to calculate offset sizes
            IRect boundaries = trianglesNw.TrianglesBoundaries;

            //Calculate offsets 
            var offsetX = (float)boundaries.Width;
            var offsetZ = (float)boundaries.Depth;

            //Translate each model out to four corners
            Console.WriteLine("Translating quads");
            trianglesNw.Translate(-offsetX, 0, offsetZ);
            trianglesNe.Translate(offsetX, 0, offsetZ);
            trianglesSw.Translate(-offsetX, 0, -offsetZ);
            trianglesSe.Translate(offsetX, 0, -offsetZ);

            //Add them all to triangle array
            Console.WriteLine("Creating final model");
            trianglesQuad.Add(trianglesNw);
            trianglesQuad.Add(trianglesNe);
            trianglesQuad.Add(trianglesSw);
            trianglesQuad.Add(trianglesSe);

            //Write model
            const string outputFilenameStl = "..\\..\\ArchQuad-X4.stl";
            Console.WriteLine("Writing " + outputFilenameStl);
            GlyphicsApi.SaveTrianglesToStl(outputFilenameStl, trianglesQuad);

            Console.WriteLine("Done");
        }
    }
}
