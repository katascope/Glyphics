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

//CommandLine app - general commandline tool for glyphics
namespace GlyphicsCommandLine
{

    class CommandLine
    {
        public static void PrintUsage()
        {
            Console.WriteLine("Usage");
            Console.WriteLine(" -ver : Display version information");
            Console.WriteLine(" (-i Inputs)");
            Console.WriteLine("  -icode <glyphics code>");
            Console.WriteLine("  -iser <serialized rects>");
            Console.WriteLine("  -ibytecode <bytecode>");
            Console.WriteLine("  -ibytes <bytes>");
            Console.WriteLine("  -igly <glyphics file> : Creates codelist, then code");
            Console.WriteLine("  -ipng <glyphics file>");
            Console.WriteLine("  -igif <glyphics file> : Creates gridlist, if animated, then grid");
            Console.WriteLine("  -istl <glyphics file>");
            Console.WriteLine("  -idae <collada mesh file> (not yet implemented)");
            Console.WriteLine(" (-fx Functions) - not yet implemented");
            Console.WriteLine("  -fxquadruple : quadruples the set for 4x 3d printing");
            Console.WriteLine("  -fxresize <x> <y> <z> : resizes it");
            Console.WriteLine(" (-o Outputs)");
            Console.WriteLine("  -ocode");
            Console.WriteLine("  -ocodename : output codename");
            Console.WriteLine("  -otokens : output tokens");
            Console.WriteLine("  -obytecode : output bytecode in hex");
            Console.WriteLine("  -ogrid <filename>: output grid");
            Console.WriteLine("  -orawbytes : output raw bytes ");
            Console.WriteLine("  -oquads : output quads");
            Console.WriteLine("  -orects : output rects");
            Console.WriteLine("  -oser : output serialized rects");
            Console.WriteLine("  -oser255 : output serialized rects limit line to 255");
            Console.WriteLine("  -ogly <glyphics file>");
            Console.WriteLine("  -opng <glyphics file>");
            Console.WriteLine("  -oopng <filename> : output oblique grid");
            Console.WriteLine("  -ostl <glyphics file>");
            Console.WriteLine("  -odae <collada mesh file>");
            Console.WriteLine("");
        }

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                PrintUsage();
                return;
            }

            if (args.Length == 1)
            {
                Console.WriteLine("Glyphics version {0} \"{1}\"", GlyphicsApi.Version, GlyphicsApi.VersionName);
                return;
            }

            string result = "";
            string outputText = "";
            GlyphicsLibrary.Language.DownSolver hc = null;

            for (int i = 0; i < args.Length; i++)
            {
                string arg = args[i];
                string nextArg = "";
                if (i < args.Length - 1)
                    nextArg = args[i + 1];

                if (String.CompareOrdinal(arg, "-icode") == 0) { Console.WriteLine("Input code: {0}", nextArg); hc = new GlyphicsLibrary.Language.DownSolver(GlyphicsApi.CreateCode(nextArg)); }
                if (String.CompareOrdinal(arg, "-iser") == 0) { Console.WriteLine("Input serialized rects:\n{0}", nextArg); hc = new GlyphicsLibrary.Language.DownSolver(GlyphicsApi.CreateSerializedRects(nextArg)); }
                //if (String.CompareOrdinal(arg, "-ibytes") == 0) { hc = new FlowSolve(GlyphicsApi.createBytecode)
                if (String.CompareOrdinal(arg, "-ipng") == 0) { Console.WriteLine("Input PNG filename: {0}", nextArg); hc = new GlyphicsLibrary.Language.DownSolver(nextArg); }
                if (String.CompareOrdinal(arg, "-igif") == 0) { Console.WriteLine("Input GIF filename: {0}", nextArg); hc = new GlyphicsLibrary.Language.DownSolver(nextArg); }
                if (String.CompareOrdinal(arg, "-istl") == 0) { Console.WriteLine("Input STL filename: {0}", nextArg); hc = new GlyphicsLibrary.Language.DownSolver(nextArg); }
                if (String.CompareOrdinal(arg, "-iobj") == 0) { Console.WriteLine("Input OBJ filename: {0}", nextArg); hc = new GlyphicsLibrary.Language.DownSolver(nextArg); }
                if (String.CompareOrdinal(arg, "-igly") == 0) { Console.WriteLine("Input GLY filename: {0}", nextArg); hc = new GlyphicsLibrary.Language.DownSolver(nextArg); }
                //if (String.CompareOrdinal(arg, "-idae") == 0) { Console.WriteLine("Input DAE filename: {0}", nextArg); hc = new FlowSolve(nextArg); }
            }

            for (int i = 0; i < args.Length; i++)
            {
                string arg = args[i];
                string nextArg = "";
                if (i < args.Length - 1)
                    nextArg = args[i + 1];

                if (String.CompareOrdinal(arg, "-ocode") == 0) result = (hc.code != null) ? hc.code.ToString() : "implement more converters.";
                if (String.CompareOrdinal(arg, "-ocodename") == 0) result = (hc.codename != null) ? hc.code.ToString() : "implement more converters.";
                if (String.CompareOrdinal(arg, "-otokens") == 0) result = (hc.tokens != null) ? hc.tokens.ToString() : "implement more converters.";
                if (String.CompareOrdinal(arg, "-obytecode") == 0) result = (hc.bytecode != null) ? hc.bytecode.ToString() : "implement more converters.";
                if (String.CompareOrdinal(arg, "-ogrid") == 0) result = (hc.grid != null) ? hc.grid.ToString() : "Filename ";
                if (String.CompareOrdinal(arg, "-oogrid") == 0) result = (hc.gridOblique != null) ? hc.gridOblique.ToString() : "implement more converters.";
                if (String.CompareOrdinal(arg, "-orawbytes") == 0) result = (hc.rawbytes != null) ? hc.rawbytes.ToString() : "implement more converters.";
                if (String.CompareOrdinal(arg, "-orects") == 0) result = (hc.rects != null) ? hc.rects.ToString() : "implement more converters.";
                if (String.CompareOrdinal(arg, "-oquads") == 0) result = (hc.quads != null) ? hc.quads.ToString() : "implement more converters.";
                if (String.CompareOrdinal(arg, "-oser") == 0) result = (hc.serializedRects != null) ? hc.serializedRects.ToString() : "implement more converters.";
                if (String.CompareOrdinal(arg, "-oser") == 0) result = (hc.serializedRects != null) ? hc.serializedRects.ToString() : "implement more converters.";
                if (String.CompareOrdinal(arg, "-oser255") == 0) result = (hc.serializedRectsLimit255 != null) ? hc.serializedRectsLimit255.ToString() : "implement more converters.";
                if (String.CompareOrdinal(arg, "-opng") == 0)
                {
                    Console.WriteLine("PNG Output file: " + nextArg);
                    GlyphicsApi.SaveFlatPng(nextArg, hc.grid);
                }
                if (String.CompareOrdinal(arg, "-oopng") == 0)
                {
                    Console.WriteLine("Oblique PNG Output file: " + nextArg);
                    GlyphicsApi.SaveFlatPng(nextArg, hc.gridOblique);
                }
                if (String.CompareOrdinal(arg, "-ostl") == 0)
                {
                    result = (hc.triangles != null) ? hc.triangles.ToString() : "implement more converters.";
                    Console.WriteLine("STL Output file: " + nextArg);
                    GlyphicsApi.SaveTrianglesToStl(nextArg, hc.triangles);
                }
            }
            Console.WriteLine("Result:\n" + result);
        }
    }
}

