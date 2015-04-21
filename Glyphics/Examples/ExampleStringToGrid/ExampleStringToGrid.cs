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

namespace ExampleStringToGrid
{
    /* Example: String To Grid
     * Purpose: Illustrate how to create Glyphics code for drawing a string
     * 
     * Example concepts:
     *  1) Append Glyphics code for string to existing code
     *  2) Render code to oblique cells
     *  3) Save grid to PNG
     */
    class ExampleStringToGrid
    {
        //Letter sizing
        const int LetterWidth = 7;
        const int LetterHeight = 6;
        const int BorderX = 1;
        const int BorderY = 1;

        private static string CreateCodeForText(string str, int x, int y, int z)
        {
            //Use these to track cursor location
            int xOffset = 0;
            int yOffset = 0;

            string result = "";

            //Iterate through characters in string
            for (int i = 0; i < str.Length;i++ )
            {
                int cVal = str[i];

                //If carriage return, advance up a letter height and reset x offset
                if (str[i] == '\n')
                {
                    yOffset += LetterHeight + BorderY;
                    xOffset = 0;
                }
                else
                {
                    //Create glyphics code for it procedurally
                    result += "Text "
                        + (x + xOffset * (LetterWidth + BorderX)) + " "
                        + (y - yOffset) + " "
                        + z + " "
                        + cVal + ";"
                        ;                        

                    //Advance to next character offset
                    xOffset++;
                }

                Console.WriteLine("Letter : '{0}' = {1}\n{2} : {3}\n", str[i], cVal, i, result);

            }
            return result;
        }

        static void Main()
        {
            //Simple Glyphics code
            string code =
                @"TextStringTest,Size3D4 146 31 4;PenColorD4 255 63 63 255;WallCube 1;";

            const int top = 16;

            //Draw the bottom text dark to seem like shadows
            code += "PenColorD4 255 127 255 255;";
            code += CreateCodeForText("Hello World\nHow are you doing?", 2, top, 1);
            code += "PenColorD4 31 255 127 255;";
            code += CreateCodeForText("Well I hope?", 3, top - (LetterHeight + BorderY) * 2 , 1);
            code += "Shadows;";

            Console.WriteLine("Code: {0}", code);

            //Glyphics code object
            ICode glyphicsCode = GlyphicsApi.CreateCode(code);

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
