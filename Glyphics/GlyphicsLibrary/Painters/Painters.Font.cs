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

namespace GlyphicsLibrary.Painters
{
    internal partial class CPainter
    {
        //Draw an arbitrary letter to Grid
        public void DrawLetter(IByteGridContext bgc, PenTwist twistType, int x, int y, int z, int letter, bool flip)
        {
            string[] font = GetCharacterFont((char)letter);
            if (font == null) return;

            for (int sy = 0; sy < 5; sy++)
            {
                for (int sx = 0; sx < 5; sx++)
                {
                    if (font[sy][sx] == '#')
                    {
                        if (flip)
                            DrawAxisPen(bgc, twistType, x + sx, y + (4 - sy), z);
                        else
                            DrawAxisPen(bgc, twistType, x + sx, y + sy, z);
                    }
                }
            }
        }

        //Return the string(font) for a character
        public static string[] GetCharacterFont(char c)
        {
            c = Char.ToUpper(c);
            string[] result = LetterToFontLookupTable[c];
            return result;
        }

        static readonly string[] LetterA =
            {
                " ##  ",
                "#  # ",
                "#### ",
                "#  # ",
                "#  # "
            };
        static readonly string[] LetterB =
            {
                "###  ",
                "#  # ",
                "###  ",
                "#  # ",
                "###  "
            };
        static readonly string[] LetterC =
            {
                " ### ",
                "#    ",
                "#    ",
                "#    ",
                " ### "
            };
        static readonly string[] LetterD =
            {
                "###  ",
                "#  # ",
                "#  # ",
                "#  # ",
                "###  "
            };
        static readonly string[] LetterE =
            {
                "#### ",
                "#    ",
                "###  ",
                "#    ",
                "#### "
            };
        static readonly string[] LetterF =
            {
                "#### ",
                "#    ",
                "###  ",
                "#    ",
                "#    "
            };
        static readonly string[] LetterG =
            {
                " ##  ",
                "#    ",
                "# ## ",
                "#  # ",
                " ### "
            };
        static readonly string[] LetterH =
            {
                "#  # ",
                "#  # ",
                "#### ",
                "#  # ",
                "#  # "
            };
        static readonly string[] LetterI =
            {
                " ### ",
                "  #  ",
                "  #  ",
                "  #  ",
                " ### "
            };
        static readonly string[] LetterJ =
            {
                "#### ",
                "   # ",
                "   # ",
                "#  # ",
                " ##  "
            };
        static readonly string[] LetterK =
            {
                "#  # ",
                "# #  ",
                "##   ",
                "# #  ",
                "#  # "
            };
        static readonly string[] LetterL =
            {
                "#    ",
                "#    ",
                "#    ",
                "#    ",
                "#### "
            };
        static readonly string[] LetterM =
            {
                "## # ",
                "# # #",
                "# # #",
                "# # #",
                "# # #"
            };
        static readonly string[] LetterN =
            {
                "###  ",
                "#  # ",
                "#  # ",
                "#  # ",
                "#  # "
            };
        static readonly string[] LetterO =
            {
                " ##  ",
                "#  # ",
                "#  # ",
                "#  # ",
                " ##  "
            };
        static readonly string[] LetterP =
            {
                "###  ",
                "#  # ",
                "###  ",
                "#    ",
                "#    "
            };
        static readonly string[] LetterQ =
            {
                " ### ",
                "#   #",
                "# # #",
                "#  ##",
                " ####"
            };
        static readonly string[] LetterR =
            {
                "###  ",
                "#  # ",
                "###  ",
                "#  # ",
                "#  # "
            };
        static readonly string[] LetterS =
            {
                " ##  ",
                "#    ",
                " ##  ",
                "   # ",
                "###  "
            };
        static readonly string[] LetterT =
            {
                "#####",
                "  #  ",
                "  #  ",
                "  #  ",
                "  #  "
            };
        static readonly string[] LetterU =
            {
                "#  # ",
                "#  # ",
                "#  # ",
                "#  # ",
                " ### "
            };
        static readonly string[] LetterV =
            {
                "#  # ",
                "#  # ",
                "#  # ",
                "#  # ",
                " ##  "
            };
        static readonly string[] LetterW =
            {
                "# # #",
                "# # #",
                "# # #",
                "# # #",
                "## # "
            };
        static readonly string[] LetterX =
            {
                " # # ",
                " # # ",
                "  #  ",
                " # # ",
                " # # "
            };
        static readonly string[] LetterY =
            {
                "#  # ",
                "#  # ",
                "#### ",
                "   # ",
                "###  "
            };
        static readonly string[] LetterZ =
            {
                "#### ",
                "   # ",
                "  #  ",
                " #   ",
                "#### "
            };
        static readonly string[] LetterBlank =
            {
                "     ",
                "     ",
                "     ",
                "     ",
                "     "
            };
        static readonly string[] LetterExclamation =
            {
                "  #  ",
                "  #  ",
                "  #  ",
                "     ",
                "  #  "
            };
        static readonly string[] LetterAt =
            {
                " ### ",
                "# # #",
                "# ###",
                "#    ",
                " ####"
            };
        static readonly string[] LetterPound =
            {
                " # # ",
                "#####",
                " # # ",
                "#####",
                " # # "
            };
        static readonly string[] LetterDollar =
            {
                " ### ",
                "# #  ",
                " ### ",
                "  # #",
                "#### "
            };
        static readonly string[] LetterPercent =
            {
                "#   #",
                "   # ",
                "  #  ",
                " #   ",
                "#   #"
            };
        static readonly string[] LetterCaret =
            {
                "  #  ",
                " # # ",
                "     ",
                "     ",
                "     "
            };
        static readonly string[] LetterAmpersand =
            {
                " ##  ",
                "#  # ",
                " ## #",
                "#  # ",
                " ## #"
            };
        static readonly string[] LetterStar =
            {
                "     ",
                " ### ",
                "#####",
                " ### ",
                "     "
            };
        static readonly string[] LetterLBrace =
            {
                "  #  ",
                " #   ",
                " #   ",
                " #   ",
                "  #  "
            };
        static readonly string[] LetterRBrace =
            {
                "  #  ",
                "   # ",
                "   # ",
                "   # ",
                "  #  "
            };
        static readonly string[] LetterSubtract =
            {
                "     ",
                "     ",
                " ### ",
                "     ",
                "     "
            };
        static readonly string[] LetterUnderscore =
            {
                "     ",
                "     ",
                "     ",
                "     ",
                "#### "
            };
        static readonly string[] LetterEqual =
            {
                "     ",
                " ### ",
                "     ",
                " ### ",
                "     "
            };
        static readonly string[] LetterAdd =
            {
                "     ",
                "  #  ",
                " ### ",
                "  #  ",
                "     "
            };
        static readonly string[] LetterLBracket =
            {
                " ##  ",
                " #   ",
                " #   ",
                " #   ",
                " ##  "
            };
        static readonly string[] LetterRBracket =
            {
                " ##  ",
                "  #  ",
                "  #  ",
                "  #  ",
                " ##  "
            };
        static readonly string[] LetterLessThan =
            {
                "   # ",
                "  #  ",
                " #   ",
                "  #  ",
                "   # "
            };
        static readonly string[] LetterGreaterThan =
            {
                " #   ",
                "  #  ",
                " # # ",
                "  #  ",
                " #   "
            };

        static readonly string[] LetterComma =
            {
                "     ",
                "     ",
                "     ",
                " #   ",
                "#    "
            };
        static readonly string[] LetterPeriod =
            {
                "     ",
                "     ",
                "     ",
                "     ",
                " #   "
            };
        static readonly string[] LetterDivide =
            {
                "     ",
                "   # ",
                "  #  ",
                " #   ",
                "     "
            };
        static readonly string[] LetterQuestion =
            {
                " ### ",
                "#   #",
                "  ## ",
                "     ",
                "  #  "
            };
        static readonly string[] LetterZero =
            {
                " ### ",
                "#   #",
                "#   #",
                "#   #",
                " ### "
            };
        static readonly string[] LetterOne =
            {
                "  #  ",
                " ##  ",
                "  #  ",
                "  #  ",
                " ### "
            };
        static readonly string[] LetterTwo =
            {
                " ##  ",
                "#  # ",
                "  #  ",
                " #   ",
                "#### "
            };
        static readonly string[] LetterThree =
            {
                " ##  ",
                "   # ",
                " ##  ",
                "   # ",
                " ##  "
            };
        static readonly string[] LetterFour =
            {
                "  #  ",
                " ##  ",
                "# #  ",
                "#### ",
                "  #  "
            };
        static readonly string[] LetterFive =
            {
                "#### ",
                "#    ",
                "###  ",
                "   # ",
                "###  "
            };
        static readonly string[] LetterSix =
            {
                " #   ",
                "#    ",
                "###  ",
                "#  # ",
                " ##  "
            };
        static readonly string[] LetterSeven =
            {
                "#### ",
                "   # ",
                "  #  ",
                " #   ",
                " #   "
            };
        static readonly string[] LetterEight =
            {
                " ### ",
                "#   #",
                " ### ",
                "#   #",
                " ### "
            };
        static readonly string[] LetterNine =
            {
                " ### ",
                "#   #",
                " ####",
                "    #",
                "    #"
            };

        //This has to be at bottom
        private static readonly Dictionary<char, string[]> LetterToFontLookupTable = new Dictionary<char, string[]>
            {
                { 'A', LetterA},
                { 'B', LetterB},
                { 'C', LetterC},
                { 'D', LetterD},
                { 'E', LetterE},
                { 'F', LetterF},
                { 'G', LetterG},
                { 'H', LetterH},
                { 'I', LetterI},
                { 'J', LetterJ},
                { 'K', LetterK},
                { 'L', LetterL},
                { 'M', LetterM},
                { 'N', LetterN},
                { 'O', LetterO},
                { 'P', LetterP},
                { 'Q', LetterQ},
                { 'R', LetterR},
                { 'S', LetterS},
                { 'T', LetterT},
                { 'U', LetterU},
                { 'V', LetterV},
                { 'W', LetterW},
                { 'X', LetterX},
                { 'Y', LetterY},
                { 'Z', LetterZ},
                { ' ', LetterBlank},
                { '!', LetterExclamation},
                { '@', LetterAt},
                { '#', LetterPound},
                { '$', LetterDollar},
                { '%', LetterPercent},
                { '^', LetterCaret},
                { '&', LetterAmpersand},
                { '*', LetterStar},
                { '(', LetterLBrace},
                { ')', LetterRBrace},
                { '-', LetterSubtract},
                { '_', LetterUnderscore},
                { '=', LetterEqual},
                { '+', LetterAdd},
                { '[', LetterLBracket},
                { ']', LetterRBracket},
                { '<', LetterLessThan},
                { '>', LetterGreaterThan},
                { ',', LetterComma},
                { '.', LetterPeriod},
                { '/', LetterDivide},
                { '?', LetterQuestion},
                { '0', LetterZero},
                { '1', LetterOne},
                { '2', LetterTwo},
                { '3', LetterThree},
                { '4', LetterFour},
                { '5', LetterFive},
                { '6', LetterSix},
                { '7', LetterSeven},
                { '8', LetterEight},
                { '9', LetterNine},
            };
    }
}
