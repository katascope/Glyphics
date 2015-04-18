#region Copyright
/*Copyright (c) 2015, Katascope
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GlyphicsLibrary.Atomics;
using GlyphicsLibrary.ByteGrid;

namespace GlyphicsLibrary.Language
{
    //General converters
    internal class Conversions
    {
        private Conversions() { }

        //Input: Glyphics Code
        //Output: Execution
        public static IExecutionContext CodeToContext(string code)
        {
            return Executor.Execute(Tokenizer.CodeToTokens(new CCode(code)));
        }

        //Input: Glyphics code
        //Output: Grid
        public static IGrid CodeToGrid(string code)
        {
            return CodeToContext(code).Bgc.Grid;
        }

        //Convert tokenList to one-line string
        static public string TokensToString(ITokenList glyphTokens)
        {
            return TokensToString(glyphTokens, ";");
        }

        //Convert tokenList to a seperator-delimited string.
        static public string TokensToString(ITokenList glyphTokens, string separator)
        {
            List<string> strings = TokensToList(glyphTokens);
            return strings.Aggregate((i, j) => i + separator + j);
        }

        //Convert tokenList to a List of Glyphics codelines
        static public List<string> TokensToList(ITokenList glyphTokens)
        {
            return glyphTokens.Select(token => token.ToString()).ToList();
        }

        //Input: Glyphics code
        //Output: Rectangles
        //Side-Effects: May use deserialized cache at end of code instead
        public static IRectList CodeToRects(ICode glyphicsCode)
        {
            string code = glyphicsCode.Code;
            if (code.Contains("" + RectSerializer.CharRgba))
            {
                string start = code.Substring(code.IndexOf('*'));
                return RectSerializer.Deserialize(new CSerializedRects(start));
            }
            IRectList rectSet = RectReducer.GridToRects(Executor.Execute(Tokenizer.CodeToTokens(glyphicsCode)).Bgc.Grid);
            return rectSet;
        }

        //Input: array of bytes (raw, snot glyphbytecode)
        //Output: string
        public static string BytesToString(byte[] bytes)
        {
            var builder = new StringBuilder("");

            foreach (byte b in bytes)
            {
                string str = Convert.ToString(b, 16).PadLeft(2, '0').ToUpper();
                builder.Append(str);
            }
            string finalStr = builder.ToString();
            return finalStr;
        }

        //Input: Grid
        //Output: Hexadecimal description of Grid
        public static string GridToHexDescription(IGrid grid)
        {
            var builder = new StringBuilder("");

            for (int z = 0; z < grid.SizeZ; z++)
            {
                for (int y = 0; y < grid.SizeY; y++)
                {
                    for (int x = 0; x < grid.SizeX; x++)
                    {
                        ulong val = grid.GetRgba(x, y, z);
                        builder.Append(val == 0 ? "00 " : String.Format("{0:X2} ", val));
                    }
                    builder.Append("\n");
                } builder.Append("\n");
            }
            return builder.ToString();
        }

        //Input: Grid
        //Output: Pseudo-rendering to text of Grid
        public static string GridTo3DDescription(IGrid grid, int ax, int ay, int az)
        {
            var builder = new StringBuilder("");
            
            string empty = "__";
            if (grid.Bpp == 1) empty = "__";
            if (grid.Bpp == 2) empty = "____";
            if (grid.Bpp == 3) empty = "______";
            if (grid.Bpp == 4) empty = "________";

            for (int y = grid.SizeY - 1; y >= 0; y--)
            {
                for (int z = grid.SizeZ - 1; z >= 0; z--)
                {
                    for (int i = z; i > 0; i--) builder.Append(' ');
                    for (int x = 0; x < grid.SizeX; x++)
                    {
                        if ((ax == x) && (ay == y) && (az == z))
                            builder.Append("XX");
                        else
                        {
                            ulong u = grid.GetRgba(x, y, z);
                            builder.Append(u == 0 ? empty : String.Format("{0:X2}", grid.GetRgba(x, y, z)));
                        }
                    }
                    builder.Append("\n");
                }
            }
            return builder.ToString();
        }
    }
}
