#region Copyright
/*Copyright (c) 2015, Katascope
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/
#endregion
using System;
using System.Linq;
using GlyphicsLibrary;

namespace DocumentationGenerator
{
    interface IDocumenter
    {
        //Create document index
        void DocumentIndex(string documentationPath);

        //Document by code example
        void DocumentByCode(string documentationPath, string title, string code);
    }

    internal class CDocumenterMarkdown : IDocumenter
    {
        //Create Markdown document index
        public void DocumentIndex(string documentationPath)
        {
            IGlyph[] glyphs = GlyphicsApi.GetGlyphDefs();

            var glyphNames = glyphs.Select(glyph => glyph.Name).ToList();
            glyphNames.Sort();
            using (var file = new System.IO.StreamWriter(documentationPath + "\\README.md"))
            {
                file.WriteLine("#Glyph Functions\n");
                foreach (string str in glyphNames)
                {
                    file.WriteLine("![](" + str + ".png)");
                    file.WriteLine("[" + str + "](" + str + ".md)\n");
                }
            }
        }

        //Add list of tokenList linked to Glyph tokenList
        public static string TokensToHotlinkedCode(ITokenList glyphTokens)
        {
            string str = "";
            foreach (IToken token in glyphTokens)
            {
                str += "- [" + token.Glyph.Name + "](" + token.Glyph.Name + ".md)";
                str = token.GetArgs().Aggregate(str, (current, t) => current + (" " + t));
                str += "\n";
            }
            return str;
        }

        //Document by code example
        public void DocumentByCode(string documentationPath, string title, string code)
        {
            string path = documentationPath;

            IRenderer renderer = GlyphicsApi.Renderer;

            IGrid grid = GlyphicsApi.CodeToGrid(GlyphicsApi.CreateCode(code));
            //            using ()
            {
                IGrid grid2 = renderer.RenderObliqueCells(grid);
                GlyphicsApi.SaveFlatPng(path + title + ".png", grid2);

                int id = GlyphicsApi.GetId(title);
                if (id >= 0)
                {//Glyph description
                    IGlyph glyph = GlyphicsApi.GetGlyph(id);

                    string description = glyph.Desc;

                    string glyphDoc = "#" + title + "\n"
                                     + "\n##Description: " + description
                                     + "\n##Arguments: " + glyph.Syntax
                                     + "\n"
                                     + "\n![](" + title + ".png)\n"
                                     ;
                    Console.WriteLine(glyphDoc+"\n");

                    string crCode = TokensToHotlinkedCode(GlyphicsApi.CodeToTokens(GlyphicsApi.CreateCode(code)));
                    glyphDoc += "\nFigure Code:\n" + crCode + "\nCondensed: " + code + "\n";

                    using (var file = new System.IO.StreamWriter(path + title + ".md"))
                    {
                        file.WriteLine(glyphDoc);
                    }
                }
                else
                {
                    //TODO: Implement glyphics example writer later
                }
            }
        }
    }
        

    //A little bit hacky thing to spew out html documentation about glyphs, also can render out GLY files to make examples/documentation
    internal class CDocumenterHtml : IDocumenter
    {
        //Create html document index
        public void DocumentIndex(string documentationPath)
        {
            IGlyph[] glyphs = GlyphicsApi.GetGlyphDefs();

            var glyphNames = glyphs.Select(glyph => glyph.Name).ToList();
            glyphNames.Sort();
            using (var file = new System.IO.StreamWriter(documentationPath + "\\index.html"))
            {
                file.WriteLine("<title>Glyph Functions</title>");
                file.WriteLine("<h1>Glyph Functions</h1>");
                file.WriteLine("<h1>Glyph Functions</h1>");
                foreach (string str in glyphNames)
                {
                    file.WriteLine("<a href=\"" + str + ".html\">" + str + "</a>");
                    file.WriteLine("<img src=\"" + str + ".png\"><br>");
                }
            }
        }

        //Add list of tokenList linked to Glyph tokenList
        public static string TokensToHotlinkedCode(ITokenList glyphTokens)
        {
            string str = "";
            foreach (IToken token in glyphTokens)
            {
                str += "<a href=\"" + token.Glyph.Name + ".html\">" + token.Glyph.Name + "</a>";
                str = token.GetArgs().Aggregate(str, (current, t) => current + (" " + t));
                str += "\n";
            }
            return str;
        }

        //Generate documentation for a piece of Glyphics code
        public void DocumentByCode(string documentationPath, string title, string code)
        {
            string path = documentationPath;

            IRenderer renderer = GlyphicsApi.Renderer;

            IGrid grid = GlyphicsApi.CodeToGrid(GlyphicsApi.CreateCode(code));
            //            using ()
            {
                IGrid grid2 = renderer.RenderObliqueCells(grid);
                GlyphicsApi.SaveFlatPng(path + title + ".png", grid2);

                int id = GlyphicsApi.GetId(title);
                if (id >= 0)
                {//Glyph description
                    IGlyph glyph = GlyphicsApi.GetGlyph(id);

                    string description = glyph.Desc;
                    description = description.Replace("<", "&lt");
                    description = description.Replace(">", "&gt");

                    string glyphDoc = "<title>" + title + "</title>"
                                     + "<h1>" + title + " Glyph function</h1>"
                                     + "<h3>Description:</h3>" + description
                                     + "<br><img src=\"" + title + ".png\">"
                                     ;
                    Console.WriteLine(glyphDoc + "\n");

                    string crCode = TokensToHotlinkedCode(GlyphicsApi.CodeToTokens(GlyphicsApi.CreateCode(code)));
                    glyphDoc += "<h3>Figure Code</h3><pre>" + crCode + "</pre><h4>Condensed</h4><pre>" + code + "</pre><br>";

                    using (var file = new System.IO.StreamWriter(path + title + ".html"))
                    {
                        file.WriteLine(glyphDoc);
                    }
                }
                else
                {//Example
                    string exampleDoc = "<title>" + title + "</title>"
                                     + "<h1>" + title + " Glyph Function Example</h1>"
                                     ;

                    string str = title + ".png";
                    str = "<a href=\"" + str + "\"><img src=\"" + str + "\"></a>";
                    exampleDoc += str + "\n";


                    string crCode = TokensToHotlinkedCode(GlyphicsApi.CodeToTokens(GlyphicsApi.CreateCode(code)));
                    exampleDoc += "<h3>Example Code</h3><pre>" + crCode + "</pre><h4>Condensed</h4><pre>" + code + "</pre><br>";

                    using (var file = new System.IO.StreamWriter(path + title + ".html"))
                    {
                        file.WriteLine(exampleDoc);
                    }
                }
            }
        }
    }

    class Documenter
    {
        static IDocumenter documenterHtml = new CDocumenterHtml();
        static IDocumenter documenterMarkdown = new CDocumenterMarkdown();

        public static void DocumentIndex(string documentationPath)
        {
            documenterHtml.DocumentIndex(documentationPath + "HTML\\");
            documenterMarkdown.DocumentIndex(documentationPath + "Markdown\\");
        }

        //Generate documentation for a piece of Glyphics code
        private static void DocumentByCode(string documentationPath, 
                                           string title,
                                           string code)
        {
            documenterHtml.DocumentByCode(documentationPath + "HTML\\", title, code);
            documenterMarkdown.DocumentByCode(documentationPath+ "Markdown\\", title, code);
        }

        //High level documentation report generation
        public static void DocumentByCode(string documentationPath)
        {
            DocumentIndex(documentationPath);

            DocumentByCode(documentationPath, "Alien", @"Genesis3D 64;PenColorD4 127 127 255 255;Alien 31 31 31 30");
            DocumentByCode(documentationPath, "Star", @"Genesis3D 64;PenColorD4 127 127 255 255;Star 31 31 31 30");

            DocumentByCode(documentationPath, "Size1D1", @"Size1D1 16;PenColorD1 127;Clear");
            DocumentByCode(documentationPath, "Size2D1", @"Size2D1 16 16;PenColorD1 127;Clear");
            DocumentByCode(documentationPath, "Size3D1", @"Size3D1 16 16 16;PenColorD1 127;Clear");

            DocumentByCode(documentationPath, "Size1D2", @"Size1D2 16;PenColorD2 127 127;Clear");
            DocumentByCode(documentationPath, "Size2D2", @"Size2D2 16 16;PenColorD2 127 127;Clear");
            DocumentByCode(documentationPath, "Size3D2", @"Size3D2 16 16 16;PenColorD2 127 127;Clear");

            DocumentByCode(documentationPath, "Size1D3", @"Size1D3 16;PenColorD3 127 127 255;Clear");
            DocumentByCode(documentationPath, "Size2D3", @"Size2D3 16 16;PenColorD3 127 127 255;Clear");
            DocumentByCode(documentationPath, "Size3D3", @"Size3D3 16 16 16;PenColorD3 127 127 255;Clear");

            DocumentByCode(documentationPath, "Size1D4", @"Size1D4 16;PenColorD4 127 127 255 255;Clear");
            DocumentByCode(documentationPath, "Size2D4", @"Size2D4 16 16;PenColorD4 127 127 255 255;Clear");
            DocumentByCode(documentationPath, "Size3D4", @"Size3D4 16 16 16;PenColorD4 127 127 255 255;Clear");

            DocumentByCode(documentationPath, "PenColorD1", @"Genesis3D 16;WallCube 37;PenColorD1 127;Plot 4 1 4");
            DocumentByCode(documentationPath, "PenColorD2", @"Genesis3D 16;WallCube 37;PenColorD2 127 127;Plot 4 1 4");
            DocumentByCode(documentationPath, "PenColorD3", @"Genesis3D 16;WallCube 37;PenColorD3 255 127 127;Plot 4 1 4;PenColorD3 127 255 127;Plot 5 1 4;PenColorD3 127 127 255;Plot 6 1 4");
            DocumentByCode(documentationPath, "PenColorD4", @"Genesis3D 16;WallCube 37;PenColorD4 255 127 127 255;Plot 4 1 4;PenColorD4 127 255 127 255;Plot 5 1 4;PenColorD4 127 127 255 255;Plot 6 1 4");

            DocumentByCode(documentationPath, "ImgMirrorX", @"Genesis3D 16;PenColorD4 255 127 127 255;Plot 4 1 4;PenColorD4 127 255 127 255;Plot 5 1 4;PenColorD4 127 127 255 255;Plot 6 1 4;ImgMirrorX");
            DocumentByCode(documentationPath, "ImgMirrorY", @"Genesis3D 16;PenColorD4 255 127 127 255;Plot 4 1 4;PenColorD4 127 255 127 255;Plot 5 1 4;PenColorD4 127 127 255 255;Plot 6 1 4;ImgMirrorY");
            DocumentByCode(documentationPath, "ImgMirrorZ", @"Genesis3D 16;PenColorD4 255 127 127 255;Plot 4 1 4;PenColorD4 127 255 127 255;Plot 5 1 4;PenColorD4 127 127 255 255;Plot 6 1 4;ImgMirrorZ");
            DocumentByCode(documentationPath, "ImgFlipX", @"Genesis3D 16;PenColorD4 255 127 127 255;Plot 4 1 4;PenColorD4 127 255 127 255;Plot 5 1 4;PenColorD4 127 127 255 255;Plot 6 1 4;ImgFlipX");
            DocumentByCode(documentationPath, "ImgFlipY", @"Genesis3D 16;PenColorD4 255 127 127 255;Plot 4 1 4;PenColorD4 127 255 127 255;Plot 5 1 4;PenColorD4 127 127 255 255;Plot 6 1 4;ImgFlipY");
            DocumentByCode(documentationPath, "ImgFlipZ", @"Genesis3D 16;PenColorD4 255 127 127 255;Plot 4 1 4;PenColorD4 127 255 127 255;Plot 5 1 4;PenColorD4 127 127 255 255;Plot 6 1 4;ImgFlipZ");
            DocumentByCode(documentationPath, "ImgEdge", @"Genesis3D 16;WallCube 1;PenColorD4 127 127 255 255;FillRect 1 1 1 7 7 7;ImgEdge 255 0 0 255");
            DocumentByCode(documentationPath, "ImgEdgeX", @"Genesis3D 16;WallCube 1;PenColorD4 127 127 255 255;FillRect 1 1 1 7 7 7;ImgEdgeX 255 0 0 255");
            DocumentByCode(documentationPath, "ImgEdgeY", @"Genesis3D 16;WallCube 1;PenColorD4 127 127 255 255;FillRect 1 1 1 7 7 7;ImgEdgeY 255 0 0 255");
            DocumentByCode(documentationPath, "ImgEdgeZ", @"Genesis3D 16;WallCube 1;PenColorD4 127 127 255 255;FillRect 1 1 1 7 7 7;ImgEdgeZ 255 0 0 255");
            DocumentByCode(documentationPath, "ImgGrayscale", @"Genesis3D 16;WallCube 1;PenColorD4 127 127 255 255;Rect 1 1 1 14 1 14;ImgGrayscale");

            DocumentByCode(documentationPath, "Genesis3D", @"Genesis3D 16");
            DocumentByCode(documentationPath, "WallCube", @"Genesis3D 16;WallCube 37;");
            DocumentByCode(documentationPath, "Text", @"Genesis3D 16;PenColorD4 127 127 255 255;Text 1 7 7 65;Text 7 7 7 66;Text 13 7 7 67");
            DocumentByCode(documentationPath, "Plot", @"Genesis3D 16;WallCube 1;PenColorD4 127 127 255 255;Plot 8 1 8");
            DocumentByCode(documentationPath, "Rect", @"Genesis3D 16;WallCube 1;PenColorD4 127 127 255 255;Rect 1 1 1 14 1 14");
            DocumentByCode(documentationPath, "FillRect", @"Genesis3D 16;WallCube 1;PenColorD4 127 127 255 255;FillRect 1 1 1 14 1 14");
            DocumentByCode(documentationPath, "Clear", @"Genesis3D 16;PenColorD4 127 127 255 255;Clear");
            DocumentByCode(documentationPath, "Line", @"Genesis3D 16;WallCube 37;PenColorD4 127 127 255 255;Line 0 0 8 15 8 8");
            DocumentByCode(documentationPath, "Stairs", @"Genesis3D 16;WallCube 37;PenColorD4 127 127 255 255;Stairs 7 0 0 7 8 15 5 1 1");
            DocumentByCode(documentationPath, "Triangle", @"Genesis3D 16;WallCube 37;PenColorD4 127 127 255 255;Triangle 1 1 1 15 5 10 8 15 8");
            DocumentByCode(documentationPath, "Polygon", @"Genesis3D 16;WallCube 37;PenColorD4 127 127 255 255;Polygon 7 7 7 7 5");
            DocumentByCode(documentationPath, "ArcXY", @"Genesis3D 16;WallCube 37;PenColorD4 127 127 255 255;ArcXY 7 7 7 5 0 50");
            DocumentByCode(documentationPath, "ArcYZ", @"Genesis3D 16;WallCube 37;PenColorD4 127 127 255 255;ArcYZ 7 7 7 5 0 50");
            DocumentByCode(documentationPath, "ArcXZ", @"Genesis3D 16;WallCube 37;PenColorD4 127 127 255 255;ArcXZ 7 7 7 5 0 50");

            DocumentByCode(documentationPath, "Circle2D", @"Genesis3D 16;WallCube 37;PenColorD4 127 127 255 255;Circle2D 7 7 7 5");
            DocumentByCode(documentationPath, "Circle2DXY", @"Genesis3D 16;WallCube 37;PenColorD4 127 127 255 255;Circle2DXY 7 7 7 5");
            DocumentByCode(documentationPath, "Circle2DYZ", @"Genesis3D 16;WallCube 37;PenColorD4 127 127 255 255;Circle2DYZ 7 7 7 5");
            DocumentByCode(documentationPath, "Circle2DXZ", @"Genesis3D 16;WallCube 37;PenColorD4 127 127 255 255;Circle2DXZ 7 7 7 5");
            DocumentByCode(documentationPath, "FillCircle2D", @"Genesis3D 16;WallCube 37;PenColorD4 127 127 255 255;FillCircle2D 7 7 7 5");
            DocumentByCode(documentationPath, "FillCircle2DXY", @"Genesis3D 16;WallCube 37;PenColorD4 127 127 255 255;FillCircle2DXY 7 7 7 5");
            DocumentByCode(documentationPath, "FillCircle2DYZ", @"Genesis3D 16;WallCube 37;PenColorD4 127 127 255 255;FillCircle2DYZ 7 7 7 5");
            DocumentByCode(documentationPath, "FillCircle2DXZ", @"Genesis3D 16;WallCube 37;PenColorD4 127 127 255 255;FillCircle2DXZ 7 7 7 5");
            DocumentByCode(documentationPath, "Circle3D", @"Genesis3D 16;WallCube 37;PenColorD4 127 127 255 255;Circle3D 7 7 7 5");
            DocumentByCode(documentationPath, "FillCircle3D", @"Genesis3D 16;WallCube 37;PenColorD4 127 127 255 255;FillCircle3D 7 7 7 5");
            DocumentByCode(documentationPath, "Oval3D", @"Genesis3D 16;WallCube 37;PenColorD4 127 127 255 255;Oval3D 7 7 7 6 4 3");
            DocumentByCode(documentationPath, "ExtrudeX", @"Genesis3D 16;WallCube 37;PenColorD4 127 127 255 255;ExtrudeX 0 7 7 15 6 3 7 0");
            DocumentByCode(documentationPath, "ExtrudeY", @"Genesis3D 16;WallCube 37;PenColorD4 127 127 255 255;ExtrudeY 7 0 7 15 6 3 7 0");
            DocumentByCode(documentationPath, "ExtrudeZ", @"Genesis3D 16;WallCube 37;PenColorD4 127 127 255 255;ExtrudeZ 7 7 0 15 6 3 7 0");

            DocumentByCode(documentationPath, "FilterBlur", @"Genesis3D 16;PenColorD4 127 127 255 255;Rect 1 7 1 14 7 14;FilterBlur");
            DocumentByCode(documentationPath, "FilterEdgeAll", @"Genesis3D 16;PenColorD4 127 127 255 255;Rect 1 7 1 14 7 14;FilterEdgeAll");
            DocumentByCode(documentationPath, "FilterEdgeHor", @"Genesis3D 16;PenColorD4 127 127 255 255;Rect 1 7 1 14 7 14;FilterEdgeHor");
            DocumentByCode(documentationPath, "FilterEdgeVer", @"Genesis3D 16;PenColorD4 127 127 255 255;Rect 1 7 1 14 7 14;FilterEdgeVer");

            DocumentByCode(documentationPath, "FilterEmboss", @"Genesis3D 16;PenColorD4 127 127 255 255;Rect 1 7 1 14 7 14;FilterEmboss");
            DocumentByCode(documentationPath, "FilterMotion", @"Genesis3D 16;PenColorD4 127 127 255 255;Rect 1 7 1 14 7 14;FilterMotion");
            DocumentByCode(documentationPath, "FilterSharpen", @"Genesis3D 16;PenColorD4 127 127 255 255;Rect 1 7 1 14 7 14;FilterSharpen");
            DocumentByCode(documentationPath, "FilterSmooth", @"Genesis3D 16;PenColorD4 127 127 255 255;Rect 1 7 1 14 7 14;FilterSmooth");

            DocumentByCode(documentationPath, "ImgBrightness", @"Genesis3D 16;PenColorD4 127 127 255 255;Rect 1 7 1 14 7 14;ImgBrightness 130");
            DocumentByCode(documentationPath, "ImgColorize", @"Genesis3D 16;PenColorD4 127 127 255 255;Rect 1 7 1 14 7 14;ImgColorize 10 10");
            DocumentByCode(documentationPath, "ImgInvert", @"Genesis3D 16;PenColorD4 127 127 255 255;Rect 1 7 1 14 7 14;ImgInvert");

            DocumentByCode(documentationPath, "GenChaos", @"Genesis3D 16;PenColorD4 127 127 255 255;Rect 1 7 1 14 7 14;GenChaos 3 0 0 1");

        }

        public static void DocumentByGlyphicsFile(string documentationPath, string filename)
        {
            ICodeList codes = GlyphicsApi.GlyToCodes(filename);
            foreach (ICode code in codes)
            {
                string name = code.Code.Split(',')[0];
                ICode glyphicsCode = GlyphicsApi.CreateCode(code.Code);

                const int size = 192;
                DocumentByCode(documentationPath, name, code.Code.Contains("Size3D4 255") ? GlyphicsApi.CodeToRescaledCode(glyphicsCode, size, size, size).Code : glyphicsCode.Code);
            }
        }
    }

    class DocumentationGenerator
    {
        static void Main()
        {
            const string documentationPath = "\\GitHub\\Glyphics\\Documentation\\";
            Console.WriteLine("Documenting glyphs to "+documentationPath + "\n");
            Documenter.DocumentByCode(documentationPath);

            /*
            Console.WriteLine("Generating from GLY file\n");
            const string mediaPath = "\\GitHub\\Glyphics\\Glyph Cores\\";        
            Documenter.DocumentByGlyphicsFile(documentationPath , mediaPath+"default.gly");
            */
        }
    }
}
