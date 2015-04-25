#region Copyright
/*Copyright (c) 2015, Katascope
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/
#endregion
using System;

namespace GlyphicsLibrary.Language
{
    //Tokenizer class for parsing Glyphics code into tokens
    internal partial class Converter
    {
        //Tokenize a single command
        private static IToken TokenizeLine2Token(string line, int codeLine)
        {
            if (line[0] == '#') return null;

            string[] parts = line.Split(' ');

            if (line.Contains(","))
            {
                parts = line.Split(',')[1].Split(' ');
            }

            int id = Glyphs.GetId(parts[0]);

            if (id == -1)
            {
                throw new GlyphicsError(GlyphicsErrorType.UnknownGlyph, codeLine,line);
            }

            IGlyph glyph = Glyphs.GetGlyph(id);
            if (glyph.Args != parts.Length - 1)
            {
                if (glyph.Varargs == 0)
                {
                    throw new GlyphicsError(GlyphicsErrorType.WrongArgumentCount, codeLine, line, "(Need " + glyph.Args + ", Found " + (parts.Length - 1) + ")");
                }
            }
            IToken token = new CToken();
            token.Glyph = glyph;
            var args = new byte[parts.Length - 1];
            for (int arg = 0; arg < parts.Length-1; arg++)
            {
                args[arg] = (byte)Convert.ToInt16(parts[arg+1]);
            }
            token.SetArgs(args);
            return token;
        }

        //Convert Glyphics code into a set of tokenList
        public static ITokenList CodeToTokens(ICode glyphicsCode)
        {
            string code = glyphicsCode.Code;


            if (code.Contains("" + glyphicsCode.NameCodeSplitCharacter))
            {
                code = code.Split(glyphicsCode.NameCodeSplitCharacter)[1];
            }

            if (code.Contains("*"))
            {
                code = code.Split(Atomics.Converter.CharRgba)[0];
            }
            char[] splits = { '\n', ';' };
            string[] lines = code.Split(splits);

            if ((code == null) || (code.Length < 2))
            {
                return null;
            }

            ITokenList tokens = new CTokenList();
            //if (tokens.Count == 0)                return tokens;

            int lineNumber = 0;
            foreach (string iterationLine in lines)
            {
                string line = iterationLine.Trim();
                if (line.Length > 0)
                {
                    if (line[0] == '#')
                    {
                        //Ignore the remark
                    }
                    else
                    {
                        IToken token = TokenizeLine2Token(line, lineNumber);
                        if (token != null)
                        {
                            if ((int)token.Glyph.Glyph == 0)
                            {
                                throw new GlyphicsError("No code");
                            }
                            tokens.AddToken(token);
                        }
                    }
                }
                lineNumber++;
            }
            return tokens;
        }
    }
}
