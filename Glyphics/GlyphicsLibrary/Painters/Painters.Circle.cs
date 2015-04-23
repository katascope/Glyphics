#region Copyright
/*Copyright (c) 2015, Katascope
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/
#endregion

namespace GlyphicsLibrary.Painters
{
    internal partial class CPainter
    {
        //Draw a circle on any 3d axis
        public void DrawCircle2DAnyAxis(IGridContext bgc, PenTwist ptt, int x0, int y0, int z, int radius)
        {
            int sx = radius, sy = 0;
            int radiusError = 1 - sx;

            while (sx >= sy)
            {
                DrawAxisPen(bgc, ptt, sx + x0, sy + y0, z);
                DrawAxisPen(bgc, ptt, sy + x0, sx + y0, z);
                DrawAxisPen(bgc, ptt, -sx + x0, sy + y0, z);
                DrawAxisPen(bgc, ptt, -sy + x0, sx + y0, z);
                DrawAxisPen(bgc, ptt, -sx + x0, -sy + y0, z);
                DrawAxisPen(bgc, ptt, -sy + x0, -sx + y0, z);
                DrawAxisPen(bgc, ptt, sx + x0, -sy + y0, z);
                DrawAxisPen(bgc, ptt, sy + x0, -sx + y0, z);

                sy++;
                if (radiusError < 0)
                    radiusError += 2 * sy + 1;
                else
                {
                    sx--;
                    radiusError += 2 * (sy - sx + 1);
                }
            }
        }
    }
}
