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

namespace ExampleCodeToPNG
{
    /* Example: Code To PNG
     * Purpose: Illustrate the path from raw glyphics code to a saved PNG file
     * 
     * Example concepts:
     *  1) Create ICode from code string
     *  2) Extracting codename from ICode
     *  3) Converting ICode to IGrid
     *  4) Rendering 3D IGrid to 2D IGrid
     *  5) Saving IGrid to PNG file
     */
    class ExampleCodeToPng
    {
        static void Main()
        {
            //Glyphics code string
            const string codeString = @"PrintableNexus,Size3D4 64 64 64;Spawn 25 5 25;PenColorD4 31 127 255 255;
            PenShape 1;WallCube 1;PenColorD4 255 255 255 255;
PenSize 1 2 1;Rect 0 0 0 31 0 31;Rect 0 0 32 31 0 63;Rect 32 0 0 63 0 31;Rect 32 0 32 63 0 63;Rect 16 0 16 48 0 48;
PenSize 1 1 1;PenColorD4 31 127 255 255;FillRect 17 0 17 47 0 47;FillRect 16 1 49 48 16 63;
PenColorD4 0 0 0 0;
FillRect 17 1 49 47 15 63;
Rect 0 1 0 63 63 63;
ImgEdgeX 255 255 255 255;ImgEdgeY 255 255 255 255;ImgEdgeZ 255 255 255 255;
PenShape 2;PenColorD3 255 255 255;FillRect 26 17 51 36 28 62;
PenColorD3 127 255 127;FillRect 2 1 2 13 12 13; 
PenColorD3 255 127 127;FillRect 2 1 18 13 12 29;
PenColorD3 127 127 255;FillRect 2 1 34 13 12 45;
PenColorD3 255 255 127;FillRect 2 1 50 13 12 61;

PenColorD3 255 127 255;FillRect 18 1 2 29 12 13;
ImgMirrorX
";
            //const string codeString = "Maze,Size3D4 64 64 64;PenColorD4 127 127 127 255;WallCube 1;PenColorD4 31 127 255 255;Maze 0 0 1 0 64 64;UpV 2 31 127 255 255";
            //const string codeString = "Ascent,Size3D4 64 64 64;Spawn 6 3 5;PenColorD3 31 127 255;WallCube 1;ImgEdge 255 255 255 255;PenColorD3 127 192 127;ExtrudeZ 33 36 35 48 6 6 6 0;ExtrudeY 34 1 31 31 6 6 6 0;PenColorD3 127 127 192;FillRect 24 31 24 36 31 36;PenColorD3 192 127 192;PenSize 5 1 2;Line 25 1 3 25 23 40;Line 4 31 11 15 51 48;Line 15 51 11 26 58 51;Line 31 23 36 31 23 191;Line 25 23 40 25 23 55;Line 19 20 58 20 31 27;PenColorD3 192 127 127;PenSize 1 1 7;Line 32 31 51 57 31 51;Line 11 51 52 63 51 52;Line 0 31 7 58 31 7;PenSize 7 1 1;Line 55 31 11 55 31 51;PenColorD3 192 192 127;PenSize 1 1 1;ExtrudeZ 60 56 10 51 6 9 6 0;PenColorD3 31 127 255;FillRect 11 51 0 63 51 10;PenColorD3 127 192 127;PenColorD3 127 192 192;PenSize 7 1 3;Line 26 58 53 68 58 59;Line 61 60 51 61 57 57;PenSize 1 1 7;PenColorD3 192 127 192;PenSize 1 1 1;PenColorD4 127 127 192 255;FillRect 50 1 50 61 13 61;PenColorD4 0 0 0 0;FillRect 55 1 49 57 7 59;FillRect 51 1 51 60 12 60;PenColorD4 127 127 192 63;FillRect 50 4 54 50 7 58;PenColorD4 255 0 0 255;PenSize 3 1 3;Plot 60 1 2";

            Console.WriteLine("Code: {0}", codeString);

            //Glyphics code object
            ICode glyphicsCode = GlyphicsApi.CreateCode(codeString);

            //Extract codename from code object, to use for filename
            ICodename codename = GlyphicsApi.CodeToCodename(glyphicsCode);

            //Create filename
            string filename = "..\\..\\" + codename.Name + ".PNG";
            Console.WriteLine("\nOutput Filename: {0}", filename);

            //Convert the code to actual grid
            IGrid grid = GlyphicsApi.CodeToGrid(glyphicsCode);

            //Render the 3-dimensional grid to a new 2-dimensional oblique image
            IGrid gridRendered = GlyphicsApi.Renderer.RenderObliqueCells(grid);

            //Save final result to PNG file
            GlyphicsApi.SaveFlatPng(filename, gridRendered);
            
            //.. and write finish
            Console.WriteLine("\nDone.");
        }
    }
}
