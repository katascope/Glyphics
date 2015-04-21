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
            
            /*const string rawCode = @"Size3D4 64 64 64;Spawn 25 5 25;PenColorD4 31 127 255 255;
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
";*/

            //const string rawCode = "Size3D4 64 64 64;Spawn 6 3 5;PenColorD3 31 127 255;WallCube 1;ImgEdge 255 255 255 255;PenColorD3 127 192 127;ExtrudeZ 33 36 35 48 6 6 6 0;ExtrudeY 34 1 31 31 6 6 6 0;PenColorD3 127 127 192;FillRect 24 31 24 36 31 36;PenColorD3 192 127 192;PenSize 5 1 2;Line 25 1 3 25 23 40;Line 4 31 11 15 51 48;Line 15 51 11 26 58 51;Line 31 23 36 31 23 191;Line 25 23 40 25 23 55;Line 19 20 58 20 31 27;PenColorD3 192 127 127;PenSize 1 1 7;Line 32 31 51 57 31 51;Line 11 51 52 63 51 52;Line 0 31 7 58 31 7;PenSize 7 1 1;Line 55 31 11 55 31 51;PenColorD3 192 192 127;PenSize 1 1 1;ExtrudeZ 60 56 10 51 6 9 6 0;PenColorD3 31 127 255;FillRect 11 51 0 63 51 10;PenColorD3 127 192 127;PenColorD3 127 192 192;PenSize 7 1 3;Line 26 58 53 68 58 59;Line 61 60 51 61 57 57;PenSize 1 1 7;PenColorD3 192 127 192;PenSize 1 1 1;PenColorD4 127 127 192 255;FillRect 50 1 50 61 13 61;PenColorD4 0 0 0 0;FillRect 55 1 49 57 7 59;FillRect 51 1 51 60 12 60;PenColorD4 127 127 192 63;FillRect 50 4 54 50 7 58;PenColorD4 255 0 0 255;PenSize 3 1 3;Plot 60 1 2";
            const string rawCode = "Size3D4 64 64 64;Spawn 5 5 5;PenTex 4;PenColorD4 31 127 255 255;WallCube 1;FillRect 1 0 0 63 0 5;PenTex 25;PenSize 1 1 5;Line 25 53 59 61 53 59;PenColorD4 127 255 31 255;PenSize 5 1 5;Line 5 53 4 35 53 4;PenColorD4 255 31 31 255;Line 27 53 55 35 53 9;PenSize 1 1 1;FillRect 2 53 7 23 62 25;PenColorD4 0 0 0 0;FillRect 3 54 5 23 61 22;PenTex 4;PenColorD4 31 127 255 255;FillRect 6 0 6 57 1 57;FillRect 12 0 12 51 14 52;PenColorD3 0 0 0;FillRect 18 1 18 45 15 45;FillRect 13 14 13 50 20 50;FillRect 29 2 10 34 8 20;PenColorD4 31 127 255 255;PenSize 5 1 1;Line 21 1 28 21 12 45;ImgEdgeX 224 224 224 255;ImgEdgeZ 224 224 224 255;PenColorD4 31 127 255 255;PenTex 25;PenColorD4 127 127 127 127;Stairs 59 1 5 59 52 56 5 1 1;PenColorD4 255 255 255 255;PenShape 10;PenTex 14;FillRect 10 2 8 10 41 11;FillRect 10 2 53 10 41 56;FillRect 53 2 53 53 41 56;FillRect 53 2 9 53 41 12;*UUU@04000@0000100!@01@0!00@@0@616V1661761UV17V1Ui1hJ1hh1ihdJK1iKdJj1sj1sn1sn1si1KJbK61VV1Vc2cs8cz2cO8cc2dcdPs2ds8gz2dz8gP2dPdPi2hs8hz2hJ8hj2tn2uc2QPeQj3vj3vn3vn3vj4wn4xj5yj5yn5yn5yj6zn6Aj7Bn7Cj8Dj8Dn8Dn8Dc9cOdci9hJdhj9En9FjaGjaGnaGnaGjbHnbIjcJncJicKicKocKJcKidKJdKcecceccePPeP*7v@@04101!0!717U1g71hh1hK1hU1h71ig1JL1iU1Jk1sm1s71Kh1KK1KU1K71LU1Ud2dr8gA2dO8gd2hhdhK2hOdhd2igdJL2iOdJd2KhdKK2KOdKd2LOdPk3vm3vk5ym5yk8Dm8Dd9dOdgkaGmaGjcKncKdecOeccedceOPedPeO*vvvv0pV15Z15V26Z26V37Z37V48Z48V59Z59V6aZ6aV7bZ7bV8cZ8cV9dZ9dVaeZaeVbfZbfVcgZcgVdhZdhVeiZeiVfjZfjVgkZgkVhlZhlVimZimVjnZjnVkoZkoVlpZlpVmqZmqVnrZnrVosZosVptZptVquZquVrvZrvVswZswVtxZtxVuyZuyVvzZvzVwAZwAVxBZxBVyCZyCVzDZzDVAEZAEVBFZBFVCGZCGVDHZDHVEIZEIVFJZFJVGKZGKVHLZHLVIMZIMVJNZJNVKOZKOVLPZLPVMQZMQVNRZNRVOSZOSVPTZPTVQUZQU*@@@@ae828cFbP29TFc82RcFUP2RTFU*UUU@0p3R2BR23R33R6BR3BRdoR6wR62R72RonR7nRoxR7xR9wRawRfAReARjvRgvRlzRkzRpuRmuRr2Rpn!pyRqyRvtRstRwxRwxRAsRxsRCwRBwRGrRDrRIvRHvRMqRJqROuRNuRSpRPpRYtRTtRUuRVZRVZRWZRYpRZZRZ2S72Z72SnnZn2So2!onSon!o2!7n!72!82!nn!8n!n*v@7@0p4R3AR54R6nR6xR6AR6*@77@0p3R7mRoyR7AR9xRaARdxRezRfwRgzRjwRkyRlvRmyRpvRqxRruRsxRvuRwwRwtRxwRAtRBvRCsRDvRGsRHuRIrRJuRMrRNtROqRPtRSqRTsRUqRVtRV2S82Zm3Som!o3!8m!n*7v@@0pqRWYRY";

            const string prefix = "Arena";
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

