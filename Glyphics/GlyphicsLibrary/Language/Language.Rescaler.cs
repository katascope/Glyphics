#region Copyright
/*Copyright (c) 2015, Katascope
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/
#endregion

namespace GlyphicsLibrary.Language
{
    //Rescales the arguments to functions in a set of tokens, allows rescaling (up or down) of code to grids
    internal class Rescaler
    {
        private Rescaler() { }

        //Resize a glyphics token
        private static void RescaleGlyphToken(ref IToken token, double scaleX, double scaleY, double scaleZ)
        {
            byte[] args = token.GetArgs();

            for (int arg = 0; arg < args.Length; arg++)
            {
                char syntax = token.Glyph.Syntax[arg * 2];
                switch (syntax)
                {
                    case 'w':
                    case 'x':
                    case 'X':
                        if (token.GetArgs()[arg] > 1)
                            args[arg] = (byte)(args[arg] * scaleX);
                        break;
                    case 'y':
                    case 'Y':
                    case 'h':
                        if (args[arg] > 1)
                            args[arg] = (byte)(args[arg] * scaleY);
                        break;
                    case 'z':
                    case 'Z':
                    case 'd':
                        if (args[arg] > 1)
                            args[arg] = (byte)(args[arg] * scaleZ);
                        break;
                    case 's':
                    case 'S':
                        if (args[arg]  > 1)
                            args[arg] = (byte)(args[arg] * scaleX);
                        break;
                }
            }
        }

        //Resize a glyphics code
        public static ICode Rescale(ICode glyphicsCode, int toX, int toY, int toZ)
        {
            string code = glyphicsCode.Code;
            ITokenList glyphTokens = Tokenizer.CodeToTokens(new CCode(code));

            string prepend = code.Split(glyphicsCode.NameCodeSplitCharacter)[0];

            double scaleX;
            double scaleY;
            double scaleZ;

            IToken sizeToken = glyphTokens.GetToken(0);
            byte[] args = sizeToken.GetArgs();

            switch (args.Length)
            {
                case 1:
                    scaleX = scaleY = scaleZ = (double)toX / args[0];
                    break;
                case 2:
                    scaleX = (double)toX / args[0];
                    scaleY = (double)toY / args[1];
                    scaleZ = 1;
                    break;
                case 3:
                    scaleX = (double)toX / args[0];
                    scaleY = (double)toY / args[1];
                    scaleZ = (double)toZ / args[2];
                    break;
                default:
                    return null;
            }

            foreach (IToken t in glyphTokens)
            {
                IToken token = t;
                RescaleGlyphToken(ref token, scaleX, scaleY, scaleZ);
            }
            return new CCode(prepend + "," + Conversions.TokensToString(glyphTokens));
        }
    }
}
