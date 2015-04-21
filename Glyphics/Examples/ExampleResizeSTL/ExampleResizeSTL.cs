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

namespace ExampleResizeStl
{
    /* Example: Resize STL
     * Purpose: Resize an STL and write back out to another STL
     * 
     * Example concepts:
     *  1) Loading STL file to ITriangles
     *  2) Resizing (change the scale of) ITriangles
     *  3) Write ITriangles to STL file
     */
    class ExampleResizeStl
    {
        static void Main()
        {
            const string inputFilenameStl = "..\\..\\archquad.stl";
            Console.WriteLine("Input filename: {0}", inputFilenameStl);

            //Load STL file
            ITriangles triangles = GlyphicsApi.StlToTriangles(inputFilenameStl);

            //Write message if encountered problem
            if (triangles == null)
            {
                Console.WriteLine("Error loading " + inputFilenameStl);
                return;
            }

            //Just output # of triangles to show it is real data
            Console.WriteLine("Triangle count (source): {0}", triangles.Count);

            //Say the dimensions of it too
            IRect triangleBoundariesInput = triangles.TrianglesBoundaries;
            Console.WriteLine("Input {0} Dimensions = {1}mm x {2}mm x {3}mm",
                inputFilenameStl,
                (int)triangleBoundariesInput.Width,
                (int)triangleBoundariesInput.Height,
                (int)triangleBoundariesInput.Depth);

            //Clone the triangles
            ITriangles trianglesResized = triangles.Clone();

            //Scale everything up by 2.5
            const float scale = 2.5f;
            Console.WriteLine("Scaling model size up by {0}%", scale*100.0f);
            trianglesResized.Scale(scale,scale,scale);

            //Just state the directory we are writing to.
            const string outputfilenameStl = "..\\..\\archquad-resized.stl";
            Console.WriteLine("Output filename: {0}", outputfilenameStl);

            //Say the dimensions of it too
            IRect triangleBoundariesOutput = trianglesResized.TrianglesBoundaries;
            Console.WriteLine("Output {0} Dimensions = {1}mm x {2}mm x {3}mm",
                outputfilenameStl,
                (int)triangleBoundariesOutput.Width,
                (int)triangleBoundariesOutput.Height,
                (int)triangleBoundariesOutput.Depth);

            trianglesResized.PutOnGround();

            //Finally save out the resized triangles back to STL file
            Console.WriteLine("Writing file {0}", outputfilenameStl);
            GlyphicsApi.SaveTrianglesToStlAscii(outputfilenameStl, trianglesResized);

            
            Console.WriteLine("Done.");
        }
    }
}
