#region Copyright
/*Copyright (c) 2015, Katascope
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/
#endregion
using GlyphicsLibrary.Atomics;
using GlyphicsLibrary.ByteGrid;

namespace GlyphicsLibrary.Language
{
    //Static set of Glyphics file operations
    internal class GlyphicsFile
    {
        private GlyphicsFile() { }

        //Write a series of Glyphics codes to file
        public static bool CodesToGly(string filename, ICodeList codes)
        {
            using (var file = new System.IO.StreamWriter(filename))
            {
                foreach (ICode code in codes)
                {
                    file.WriteLine(code.Code);
                }
            }
            return false;
        }

        //Read and return series of Glyphics code from a file
        public static ICodeList GlyToCodes(string filename)
        {
            ICodeList codes = new CCodeList();
            using (var file = new System.IO.StreamReader(filename))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    string codeString = line;
                    if (!line.Contains(","))
                        codeString = "noname," + codeString;

                    ICode code = new CCode(codeString);
                    codes.AddCode(code);
                }
            }
            return codes;
        }

        //Load archetypes from a file
        public static bool LoadArchetypes(string filename)
        {
            using (var file = new System.IO.StreamReader(filename))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    IGrid grid = Executor.Execute(Tokenizer.CodeToTokens(new CCode(line))).Bgc.Grid;
                    if (grid != null)
                        ArchetypeLibrary.GridList.AddEntry(grid);
                }
            }
            return false;
        }

        //Load a glyphics file, precompute rectangles and append them to code as speedy cache
        public static void PreSerializeGlyphicsFile(string filename)
        {
            ICodeList codes = GlyToCodes(filename);

            foreach (ICode t in codes)
            {
                string code = t.Code;
                    
                if (code.Contains("*"))
                {
                    code = code.Split('*')[0];
                }

                IGrid grid = Executor.Execute(Tokenizer.CodeToTokens(new CCode(code))).Bgc.Grid;

                if (grid != null)
                {
                    IRectList rectSet = RectReducer.GridToRects(grid);
                    string serializedRects = RectSerializer.Serialize(rectSet).SerializedData;
                    code = code + serializedRects;
                    t.Code = code;
                }
            }
            CodesToGly(filename, codes);
        }
    }
}
