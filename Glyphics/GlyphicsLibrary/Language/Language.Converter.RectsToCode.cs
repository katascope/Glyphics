#region Copyright
/*Copyright (c) 2015, Katascope
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/
#endregion

using System.Text;

namespace GlyphicsLibrary.Language
{
    internal partial class Converter
    {
        //Converts a set of rectangles back into Glyphics code that can be modified/executed
        public static ICode RectsToCode(IRectList rects)
        {
            ulong rgba = 0x12345678;//Set to unlikely initial color, then detect changes

            var sb = new StringBuilder();

            string sizeStr = "Size3D4 " + (rects.Boundaries.Width + 1) + " " + (rects.Boundaries.Height + 1) + " " + (rects.Boundaries.Depth + 1);
            sb.Append(sizeStr);

            foreach (IRect rect in rects)
            {
                if (rect.Properties.Rgba != rgba) //Only emit PenColor when it changes
                {
                    rgba = rect.Properties.Rgba;
                    byte r, g, b, a;
                    GlyphicsApi.Ulong2Rgba(rect.Properties.Rgba, out r, out g, out b, out a);
                    string rgbaStr = ";PenColorD4 " + r + " " + g + " " + b + " " + a;
                    sb.Append(rgbaStr);
                }
                sb.Append(";FillRect " + rect.Pt1.X + " " + rect.Pt1.Y + " " + rect.Pt1.Z + " " + rect.Pt2.X + " " + rect.Pt2.Y + " " + rect.Pt2.Z);
            }

            string codeStr = sb.ToString();
            return new CCode(codeStr);
        }
    }
}
