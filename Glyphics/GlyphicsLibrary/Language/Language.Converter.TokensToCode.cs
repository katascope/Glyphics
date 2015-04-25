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
        //Convert tokenList to one-line string
        static public string TokensToCodeString(ITokenList glyphTokens)
        {
            return TokensToString(glyphTokens, ";");
        }

        //Convert tokenList to a seperator-delimited string.
        static private string TokensToString(IEnumerable<IToken> glyphTokens, string separator)
        {
            IEnumerable<string> strings = TokensToList(glyphTokens);
            
            string finalStr = "";
            foreach (string str in strings)
                finalStr += str + separator;
            return finalStr;
        }

        //Convert tokenList to a List of Glyphics codelines
        static private IEnumerable<string> TokensToList(IEnumerable<IToken> glyphTokens)
        {
            var tokenStrList = new List<string>();
            foreach (IToken token in glyphTokens)
                tokenStrList.Add(token.ToString());
            return tokenStrList;
        }
    }
}
