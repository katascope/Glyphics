#region Copyright
/*Copyright (c) 2015, Katascope
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/
#endregion
using GlyphicsLibrary.Atomics;

namespace GlyphicsLibrary.Painters
{
    internal partial class CPainter
    {
        //Draw Polygon to Grid
        public void DrawPolygon(IByteGridContext bgc, PenTwist twistType, int x, int y, int z, int radius, int sides)
        {
            double angleSize = 360.0 / sides;
            for (int i = 0; i < sides; i++)
            {
                double vx1 = radius;
                double vy1 = 0;
                var angle1 = (int)(i * angleSize);
                Trigonometry.RotateZ(-angle1, ref vx1, ref vy1);

                double vx2 = radius;
                double vy2 = 0;
                var angle2 = (int)((i + 1) * angleSize);

                Trigonometry.RotateZ(-angle2, ref vx2, ref vy2);

                if (i == sides - 1) //connect last segment
                {
                    vx2 = radius;
                    vy2 = 0;
                }
                DrawAxisLine2D(bgc, twistType, x + (int)vx1, y + (int)vy1, x + (int)vx2, y + (int)vy2, z);
            }
        }
    }
}

