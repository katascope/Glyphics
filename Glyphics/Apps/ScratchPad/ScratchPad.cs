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

namespace ScratchPad
{
    /* Example: ScratchPad */
    class ScratchPad
    {   
        static void SuperDebug(string codeString)
        {
            IGrid grid = GlyphicsApi.CreateGrid(32, 32, 32, 4);

            //const string inputFilenameObj = "..\\..\\..\\..\\..\\..\\diamond.obj";
            //ITriangles triangles = GlyphicsApi.ObjToTriangles(inputFilenameObj);

            //Console.WriteLine("trianglesObj: {0}", trianglesObj);            

            const string inputFilenameStl = "..\\..\\..\\..\\..\\..\\shield.stl";
            Console.WriteLine("Input filename: {0}", inputFilenameStl);

            //Load the triangles from the STL file and reduce to a unit 1x1x1 size
            ITriangles triangles = GlyphicsApi.StlToTriangles(inputFilenameStl);
            triangles.ReduceToUnit();
            Console.WriteLine("Triangle count: {0}", triangles.Count);

            //Render the triangles to the grid, will autosize to grid size
            GlyphicsApi.Renderer.RenderTrianglesToGrid(triangles, grid);
            
            ICode code = GlyphicsApi.CreateCode(codeString);
            Console.WriteLine("Code: {0}\n", codeString);

            ICodename codename = GlyphicsApi.CodeToCodename(code);
            Console.WriteLine("Codename: {0}\n", codename);

            ITokenList glyphTokens = GlyphicsApi.CodeToTokens(code);
            string tokenDesc = "Tokens:\n" + glyphTokens + "\n";
            Console.WriteLine(tokenDesc);

            IBytecode glyphicsBytecode = GlyphicsApi.TokensToBytes(glyphTokens);
            Console.WriteLine("Bytecode:\n{0}\n", glyphicsBytecode);

          //  IGrid grid = GlyphicsApi.CodeToGrid(code);
            //Console.WriteLine("Grid: {0} {1} non-empty\n", grid, grid.CountNonZero());

            string bytesDesc = GlyphicsApi.BytesToString(grid.CloneData());
            //Console.WriteLine("GridBytes:\n{0}\n", bytesDesc);

            IRectList rects = GlyphicsApi.GridToRects(grid);
            //Console.WriteLine("Rects: {0}\n{1}", rects.Count, rects);

            string serialized = GlyphicsApi.RectsToSerializedRectsLimit255(rects).SerializedData;
            Console.WriteLine("Serialized Rects: (len={0})\n{1}\n", serialized.Length, serialized);

            ICode codeFromRects = GlyphicsApi.RectsToCode(rects);
            //Console.WriteLine("New code string:\n\"" + codeFromRects + "\"");

            GlyphicsApi.SaveFlatText("..\\..\\foo.txt", serialized);

            IGrid gridOblique = GlyphicsApi.Renderer.RenderObliqueCells(grid);
            GlyphicsApi.SaveFlatPng("..\\..\\oblique.png", gridOblique);

            //IScene scene = GlyphicsApi.RectsToScene(rects);Console.WriteLine("Scene: {0}", scene);
            //IQuadList quads = GlyphicsApi.RectsToQuads(rects);Console.WriteLine("Quads: {0}", quads);
            //ITriangles triangles = GlyphicsApi.QuadsToTriangles(quads);Console.WriteLine("Triangles: {0}", triangles);
            
            //Console.WriteLine("2d view:\n{0}", GlyphicsApi.GridToHexDescription(grid));
            //Console.WriteLine("3d view:\n{0}", GlyphicsApi.GridTo3DDescription(grid, 0, 0, 0));
        }
        static void Main()
        {
            //const string codeString = @"Ascent2,Size3D4 64 64 64;Spawn 6 3 5;PenShape 0;PenColorD4 31 127 255 192;WallCube 1;ImgEdge 255 255 255 255;PenColorD3 192 127 127;PenSize 1 1 7;Line 22 31 51 58 31 51;Line 2 51 52 63 51 52;Line 2 31 7 58 31 7;PenSize 7 1 1;Line 55 31 11 55 31 51;PenSize 1 1 1;PenColorD3 31 127 255;FillRect 11 51 2 63 51 10;PenColorD3 127 192 192;PenSize 3 1 3;Line 16 58 59 61 58 59;PenColorD4 255 255 255 255;Line 59 60 51 59 58 57;PenSize 1 1 1;PenColorD4 127 127 192 255;FillRect 50 1 50 61 13 61;PenColorD4 0 0 0 0;FillRect 55 1 49 57 7 59;FillRect 51 1 51 60 12 60;PenColorD4 127 127 192 63;FillRect 50 4 54 50 7 58;PenSize 1 1 1;PenColorD3 192 192 127;ExtrudeZ 60 56 10 51 6 6 6 0;PenColorD4 255 255 255 255;FillRect 0 0 31 63 0 32;FillRect 31 0 0 32 0 63;Rect 16 0 16 47 0 47;PenColorD4 31 127 255 255;FillRect 17 0 17 46 0 46;PenSize 1 1 1;PenColorD3 127 127 192;FillRect 24 31 24 36 31 36;ImgEdge 255 255 255 255;PenColorD3 127 192 127;ExtrudeY 33 1 31 30 6 2 2 0;ExtrudeZ 33 36 35 48 6 6 6 0;PenColorD3 192 127 192;PenSize 6 1 1;Line 25 1 18 25 23 40;Line 4 31 11 4 51 48;Line 15 51 11 15 58 57;Line 31 23 36 31 23 60;Line 25 23 40 25 23 55;Line 19 20 58 20 31 27;ArchRect 0 0 0 55 10 40 2 0 6;*@@@@00000@0000100uv01w0f@01@0ug0gL0gg0hg0uL0hL0u00vg0wL0v@0w00x00!g0xg0KL0xL0K@0x@0!g0LL0Lv0Mw0!O0OS0OW0OZ0OO0PO0YZ0PZ0YO0ZZ0Z00@@0@0100!0@10@!0O1OOcOS1OS7OW1OW7OZ1OZcOO1ZOcZZ1ZZcZ01@0!@@1@@!@T8OV8OOdOZdOOdPOdYZdPZdYOdZZdZ2v4Wv42v52v9Wv5WvR2vaPvaQvbQvLovoAvoovpovzAvpAvyovAuvAmvMPvMmvNmvRmvSWvSbP2@P2bP3bP9@P3@PSbPaVPaWPbWPM2PNVPN2PO2PSVPOVQO2PT@PTVQaVQaVQNVQNURaUSOTTaTTaTTOTTOSUaSUaSUOSUOTVaTWOWWTYWVfWW@WWfWXfWX@WX@WXfWY@WYUXaUYOWXQYXUWYOYYRVZa@ZaVZbVZN@Zb@ZNVZO@ZO0@0@@00@10@!@@1@@!0@@@@@*7v@M00101u0fx01!0f10gf0uM0g!0u10xf0LM0x!0L10Mu0!x0M!0Nx0ON0ZT0OV0O!0O!0ZP0PY0Yx0!!0!110!!00110!!@11@O!11@!!@@P1@!1@PU@P!@Q2@V!@W2@WV@WZ@W!@X2@Y!@Z2@Z9@ZP@Z!@!2@!!1@1!@!*7v@@00h0hK0KcP3!P9*@Mv@00717878m17n78B17C78Q17R7871m87nm1mn7nB1mC7nQ1mR7n71B87Cm1Bn7CB1BC7CQ1BR7C*MvM@00n1is1in2js2jn3ks3kn4ls4lo5ms5mo6ns6nn7os7on8ps8pn9qs9qnarsarnbssbsnctsctndusdunevsevnfwsfwngxsgxnhyshynizsiznjAsjAnkBskBhkVmkWnlCslChlSmlUnmDsmDhmPmmRtnAynDnnEynMhnNynOnnPynTtnUynYhoKmoMhpHmpJiqEnqGirBnrDisznsAitwntyiutnuv2vb7vbivrnvs2wc7wd2xe7xf2yg7yh2zi7zj2Ak7Al2Bm7Bn2Co7Co2Dp7Dq2Er7Es2Ft7Fu2Gv7Gw2Hx7Hy2Iz7Iz2JA7JB2KC7KD2LE7LF2MG7MH2NI7NJ2OK7OLdPbiPe2PM7PMdQfiQkdRliRrdSsiSxdTyiTEdUFiULdVMiVRdWSiWV*vMv@00w1uytuv1vvtvz1vztvw1wytw6869b9l86ob9A86Db9P86Sb968l9bol8loboA8lDboP8lSbo68A9bDl8AobDA8ADbDP8ASbDvvzAvLuwzuwLBwzBxLtxztyLCyzCzLszzszLrAzrALDAzDALsBzsCLCBzCCLtDztELBDzBELuFzAFL*vvM@00P1OR7OX1OY7OO1PO3YZ1PZcYP1ZYcZO4PO7RO4XO7YP8OS8OW8OY8OO8POcYP9OYcOPdPYdYpvpzvypvzuvz*vvMf00O4SO7W*Mvv@003v5Vv9QvaVvaRvbVvLQvMVvMnvNVvR3POUPO3PP!PS*MMv@00WPa!PaXPb!PMWPN!POVQbVQMTTbTTNSUbSUNWZb!ZN*vMM@00gWX!WX";

            /*const string codeString = @"PrintableNexus,Size3D4 64 64 64;Spawn 25 5 25;PenShape 1;
PenColorD4 31 127 255 255;WallCube 1;

PenColorD4 255 255 255 255;PenSize 1 2 1;Rect 0 0 0 31 0 31;Rect 0 0 32 31 0 63;Rect 32 0 0 63 0 31;Rect 32 0 32 63 0 63;Rect 16 0 16 48 0 48;
PenSize 1 1 1;PenColorD4 31 127 255 255;FillRect 17 0 17 47 0 47;FillRect 16 1 49 48 16 63;
PenColorD4 0 0 0 0;
FillRect 17 1 49 47 15 63;
Rect 0 1 0 63 63 63;
ImgEdgeX 255 255 255 255;ImgEdgeY 255 255 255 255;ImgEdgeZ 255 255 255 255;
#Now draw the multicolor volumes
PenShape 2;
PenColorD3 127 255 127;FillRect 2 1 2 13 12 13; 
PenColorD3 255 127 127;FillRect 2 1 18 13 12 29;
PenColorD3 127 127 255;FillRect 2 1 34 13 12 45;
PenColorD3 255 255 127;FillRect 2 1 50 13 12 61;
PenColorD3 255 127 255;FillRect 18 1 2 29 12 13;

# Shape on top
PenShape 3;PenColorD3 255 255 255;FillRect 26 17 51 36 28 62;

#Finally create a mirror image to the other side
ImgMirrorX
";
             * */
            //const string codeString2 = "Size3D1 4 4 4;PenColorD1 56;FillRect 0 0 0 4 4 4;";
            const string codeString = "Size3D4 4 4 4;PenColorD4 255 0 0 255;FillRect 0 0 0 2 4 4;PenColorD4 0 0 255 255;FillRect 2 0 0 4 4 4";
            //const string codeString4 = "Size3D4 63 63 63;PenColorD4 255 127 63 255;FillRect 0 0 0 63 63 63;";
            /*const string codeString3 =
                @"Simple,

#Size of Grid
Size3D4 16 16 16;

#Blue color ground
PenColorD4 31 127 255 255;WallCube 1;

#White border around ground's edge
PenColorD4 255 255 255 255;Rect 0 0 0 15 0 15;

#Red box in center
PenColorD4 255 31 127 255;FillRect 4 1 4 11 2 11;

#Green text letter 'A' (ascii 65) on top
PenColorD4 31 255 127 255;Text 6 3 8 65";
            */
            SuperDebug(codeString);
        }
    }
}
