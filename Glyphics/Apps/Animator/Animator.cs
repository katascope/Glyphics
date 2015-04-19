﻿#region Copyright
/*Copyright (c) 2015, Katascope
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/
#endregion
using System;
using GlyphicsLibrary;

namespace Animator
{
    //
    class Animator
    {
        static void ExecutCodeToPng(string code)
        {
            //Glyphics code object
            ICode glyphicsCode = GlyphicsApi.CreateCode(code);

            //Save final result to PNG file
            string filename = "..\\..\\" + GlyphicsApi.CodeToCodename(glyphicsCode).Name + ".PNG";

            //Execute, render, and save to png
            GlyphicsApi.SaveFlatPng(filename,
                GlyphicsApi.Renderer.RenderObliqueCells(
                    GlyphicsApi.CodeToGrid(glyphicsCode)));
        }

        static void Main()
        {
            //Simple Glyphics code
            //const string rawCode = "Size3D4 16 16 16;PenColorD4 31 127 255 255;WallCube 1;PenColorD4 255 255 255 255;Rect 0 0 0 15 0 15;PenColorD4 255 31 127 255;FillRect 4 1 4 11 2 11;PenColorD4 31 255 127 255;Text 6 3 8 65";
            
            const string rawCode = @"Size3D4 64 64 64;Spawn 25 5 25;PenColorD4 31 127 255 255;
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
            const string prefix = "PrintableNexus";
            const string codeString = prefix + "," + rawCode;
            ICode code = GlyphicsApi.CreateCode(codeString);
            ITokenList tokens = GlyphicsApi.CodeToTokens(code);

            int tokenId = 0;
            int actualCount = 1;
            while (tokenId < tokens.Count)
            {
                //Skip pen changes, they are boring
                string str = tokens.GetToken(tokenId).ToString();

                if (str.StartsWith("Pen") == false)
                {
                    string curCode = prefix + "-" + actualCount + ",";
                    for (int i = 0; i <= tokenId; i++)
                    {
                        curCode += tokens.GetToken(i) + ";";
                    }
                    if (tokenId > 1)
                    {
                        Console.WriteLine("Token " + curCode);
                        ExecutCodeToPng(curCode);
                        actualCount++;
                    }
                }
                tokenId++;
            }
        }
    }
}
