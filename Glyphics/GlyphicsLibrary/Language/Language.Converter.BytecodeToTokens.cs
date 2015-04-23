#region Copyright
/*Copyright (c) 2015, Katascope
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/
#endregion
using System.Collections.Generic;

namespace GlyphicsLibrary.Language
{
    //General converters
    internal partial class Converter
    {
        //Convert bytes into a list of GlyphTokens
        public static ITokenList BytecodeToTokens(IBytecode glyphicsBytecode)
        {
            if (glyphicsBytecode == null)
                return null;

            ITokenList tokens = new CTokenList();

            byte[] glyphBytes = glyphicsBytecode.GetBytes();

            for (int i = 0; i < glyphBytes.Length; i++)
            {
                IToken glyphToken = new CToken();
                glyphToken.Glyph = Glyphs.GetGlyph(glyphBytes[i]);
                int num = glyphToken.Glyph.Args;

                if (glyphToken.Glyph.Varargs == 1) num += glyphBytes[i + 1];
                if (glyphToken.Glyph.Varargs == 2) num += glyphBytes[i + 1] * glyphBytes[i + 2];
                if (glyphToken.Glyph.Varargs == 3) num += glyphBytes[i + 1] * glyphBytes[i + 2] * glyphBytes[i + 3];

                var args = new List<byte>();
                for (int l = 0; l < num; l++)
                {
                    args.Add(glyphBytes[i + 1 + l]);
                }
                glyphToken.SetArgs(args.ToArray());
                tokens.AddToken(glyphToken);
                i += num;
            }
            return tokens;
        }
    }
}
