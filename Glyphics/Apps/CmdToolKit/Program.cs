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

namespace GlyphicsInfo
{
    enum ArgType
    {
        None,
        Version,
        Code,
        Codename,
        Tokens,
        Bytecode,
        Grid,
        RawBytes,
        Rects,
        Quads,
        Triangles,
        SerializedString,
        GridOblique,
        GlyFile,
        PngFile,
        StlFile,
        QuadStlFile //used for autoplating stl files for 3d printing

    };
    class Program
    {
        private static void ProcessInputCode(string inputText , ArgType output , string outputText)
        {
            Console.WriteLine("Code to Output {0} ({1}) ", output, outputText);
            ICode code = GlyphicsApi.CreateCode(inputText);
            switch (output)
            {
                case ArgType.Codename:
                {
                    ICodename codename = GlyphicsApi.CodeToCodename(code);
                    Console.WriteLine("codename : " + codename);
                    break;
                }
                case ArgType.Tokens:
                {
                    ITokenList tokens = GlyphicsApi.CodeToTokens(code);
                    Console.WriteLine("tokens :\n" + tokens);
                    break;
                }
                case ArgType.Rects:
                {
                    IRectList rects = GlyphicsApi.CodeToRects(code);
                    Console.WriteLine("rects :\n" + rects);
                    break;
                }
                case ArgType.Quads:
                {
                    IRectList rects = GlyphicsApi.CodeToRects(code);
                    IQuadList quads = GlyphicsApi.RectsToQuads(rects);
                    Console.WriteLine("quads :\n" + quads);
                    break;
                }
                case ArgType.Triangles:
                {
                    IRectList rects = GlyphicsApi.CodeToRects(code);
                    IQuadList quads = GlyphicsApi.RectsToQuads(rects);
                    ITriangles triangles = GlyphicsApi.QuadsToTriangles(quads);
                    Console.WriteLine("triangles :\n" + triangles);
                    break;
                }
                case ArgType.SerializedString:
                {
                    IRectList rects = GlyphicsApi.CodeToRects(code);
                    ISerializedRects serializedRects = GlyphicsApi.RectsToSerializedRects(rects);
                    Console.WriteLine("serializedRects :\n" + serializedRects);
                    break;
                }
                case ArgType.Grid:
                {
                    IGrid grid = GlyphicsApi.CodeToGrid(code);
                    Console.WriteLine("grid :\n" + grid);
                    break;
                }
                case ArgType.RawBytes:
                {
                    IGrid grid = GlyphicsApi.CodeToGrid(code);
                    string rawdataString = GlyphicsApi.BytesToString(grid.CloneData());
                    Console.WriteLine("grid raw bytes:\n" + rawdataString);
                    break;
                }
                case ArgType.PngFile:
                {
                    IGrid grid = GlyphicsApi.CodeToGrid(code);
                    GlyphicsApi.SaveFlatPng(outputText, grid);
                    Console.WriteLine("PNG filename:\n" + outputText);
                    break;
                }
                case ArgType.GridOblique:
                {
                    IGrid grid = GlyphicsApi.CodeToGrid(code);
                    IGrid gridOblique = GlyphicsApi.Renderer.RenderObliqueCells(grid);
                    GlyphicsApi.SaveFlatPng(outputText, gridOblique);
                    Console.WriteLine("Oblique PNGt filename:\n" + outputText);
                    break;
                }
                case ArgType.StlFile:
                {
                    IRectList rects = GlyphicsApi.CodeToRects(code);
                    ITriangles triangles = GlyphicsApi.RectsToTrianglesCube(rects);
                    GlyphicsApi.SaveTrianglesToStl(outputText, triangles);
                    Console.WriteLine("STL filename:\n" + outputText);
                    break;
                }
                 //TODO: GLY
            }
        }

        private static void ProcessInputSerializedRects(string inputText, ArgType output, string outputText)
        {
            ISerializedRects serializedRects = GlyphicsApi.CreateSerializedRects(inputText);
            IRectList rects = GlyphicsApi.SerializedRectsToRects(serializedRects);
            switch (output)
            {
                case ArgType.Codename: { Console.WriteLine("codename : n/a)"); break; }
                case ArgType.Tokens: { Console.WriteLine("tokens : n/a)"); break; }
                case ArgType.Rects: { Console.WriteLine("rects :\n" + rects); break; }
                case ArgType.Quads:
                    {
                        IQuadList quads = GlyphicsApi.RectsToQuads(rects);
                        Console.WriteLine("quads :\n" + quads);
                        break;
                    }
                case ArgType.Triangles:
                    {
                        IQuadList quads = GlyphicsApi.RectsToQuads(rects);
                        ITriangles triangles = GlyphicsApi.QuadsToTriangles(quads);
                        Console.WriteLine("triangles :\n" + triangles);
                        break;
                    }
                case ArgType.SerializedString: { Console.WriteLine("serializedRects :\n" + serializedRects); break; }
            }
        }

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage");
                Console.WriteLine("  -ver : Display version information");
                Console.WriteLine(" -i (Inputs)");
                Console.WriteLine("  -icode <glyphics code>");
                Console.WriteLine("  -ibytes <bytes>");
                Console.WriteLine("  -igly <glyphics file>");
                Console.WriteLine("  -ipng <glyphics file>");
                Console.WriteLine("  -istl <glyphics file>");
                Console.WriteLine("  -iser <serialized rects>");
                Console.WriteLine(" -o (Outputs)");
                Console.WriteLine("  -ocode");
                Console.WriteLine("  -ocodename : output codename");
                Console.WriteLine("  -otokens : output tokens");
                Console.WriteLine("  -obytecode : output bytecode in hex");
                Console.WriteLine("  -ogrid <filename>: output grid");
                Console.WriteLine("  -orawbytes : output raw bytes ");
                Console.WriteLine("  -oquads : output quads");
                Console.WriteLine("  -orects : output rects");
                Console.WriteLine("  -oser : output serialized rects");
                Console.WriteLine("  -oogrid <filename> : output oblique grid");
                Console.WriteLine("  -ogly <glyphics file>");
                Console.WriteLine("  -opng <glyphics file>");
                Console.WriteLine("  -ostl <glyphics file>");
//                Console.WriteLine("  -To");
  //              Console.WriteLine("  -Bloom");
    //            Console.WriteLine("  -Array : ex '-Array 2x4'");
                Console.WriteLine("");
                Console.WriteLine("Note: using inputs without output will do a full text display to console instead");
                return;
            }

            ArgType input = ArgType.None;
            string inputText = "";
            ArgType output = ArgType.None;
            string outputText = "";

            for (int i=0;i<args.Length;i++)
            {
                string arg = args[i];
                if (string.Compare(arg, "-icode") == 0) { input = ArgType.Code; inputText = args[i + 1]; }
                if (string.Compare(arg, "-igly") == 0) { input = ArgType.GlyFile; inputText = args[i + 1]; }
                if (string.Compare(arg, "-ipng") == 0) { input = ArgType.PngFile; inputText = args[i + 1]; }
                if (string.Compare(arg, "-istl") == 0) { input = ArgType.StlFile; inputText = args[i + 1]; }
                if (string.Compare(arg, "-iser") == 0) { input = ArgType.SerializedString; inputText = args[i + 1]; }
                if (string.Compare(arg, "-ibytes") == 0) { input = ArgType.RawBytes; inputText = args[i + 1]; }
            }

            for (int i = 0; i < args.Length; i++)
            {
                string arg = args[i];
                if (string.Compare(arg, "-ocode") == 0) { output = ArgType.Code; }
                if (string.Compare(arg, "-ocodename") == 0) { output = ArgType.Codename; }
                if (string.Compare(arg, "-otokens") == 0) { output = ArgType.Tokens; }
                if (string.Compare(arg, "-obytecode") == 0) { output = ArgType.Bytecode; }
                if (string.Compare(arg, "-ogrid") == 0) { output = ArgType.Grid; outputText = args[i + 1]; }
                if (string.Compare(arg, "-orawbytes") == 0) { output = ArgType.RawBytes; outputText = args[i + 1]; }
                if (string.Compare(arg, "-oquads") == 0) { output = ArgType.Quads; }
                if (string.Compare(arg, "-orects") == 0) { output = ArgType.Rects; }
                if (string.Compare(arg, "-oser") == 0) { output = ArgType.SerializedString; }
                if (string.Compare(arg, "-ogly") == 0) { output = ArgType.GlyFile; outputText = args[i + 1]; }
                if (string.Compare(arg, "-opng") == 0) { output = ArgType.PngFile; outputText = args[i + 1]; }
                if (string.Compare(arg, "-ostl") == 0) { output = ArgType.StlFile; outputText = args[i + 1]; }
                if (string.Compare(arg, "-oquadstl") == 0) { output = ArgType.QuadStlFile; outputText = args[i + 1]; }
                if (string.Compare(arg, "-oogrid") == 0) { output = ArgType.GridOblique; outputText = args[i + 1]; }
            }

            Console.WriteLine("Input {0} ({1}) ", input, inputText);
            Console.WriteLine("Output {0} ({1}) ", output, outputText);

            if (args.Length == 1)
            {
                Console.WriteLine("Glyphics version {0} \"{1}\"", GlyphicsApi.Version, GlyphicsApi.VersionName);
            }
            else
            {
                ProcessInputCode(inputText, output, outputText);
            }
        }
    }
}
