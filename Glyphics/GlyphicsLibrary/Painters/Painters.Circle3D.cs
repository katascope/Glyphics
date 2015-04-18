#region Copyright
/*Copyright (c) 2015, Katascope
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/
#endregion
using System;

namespace GlyphicsLibrary.Painters
{
    internal partial class CPainter
    {
        //Draw filled 3d circle to Grid
        public void DrawFillCircle3D(IByteGridContext bgc, int sx, int sy, int sz, int radius)
        {
            for (int z = sz - radius; z <= sz + radius; z++)
            {
                for (int y = sy - radius; y <= sy + radius; y++)
                {
                    for (int x = sx - radius; x <= sx + radius; x++)
                    {
                        double distance = GetDistance(x, y, z, sx, sy, sz);
                        if ((int)distance <= radius)
                            DrawPen(bgc, x, y, z);
                    }
                }
            }
        }

        //GetToken the distance between two XYZ points
        public static double GetDistance(double x1, double y1, double z1, double x2, double y2, double z2)
        {
            double dist = Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2) + (z1 - z2) * (z1 - z2));
            return dist;
        }

        //Draw a circle in 3d
        public void DrawCircle3D(IByteGridContext bgc, int sx, int sy, int sz, int radius)
        {
            for (int z = sz - radius; z <= sz + radius; z++)
            {
                for (int y = sy - radius; y <= sy + radius; y++)
                {
                    for (int x = sx - radius; x <= sx + radius; x++)
                    {
                        double distance = GetDistance(x, y, z, sx, sy, sz);
                        if ((int)distance == radius)
                            DrawPen(bgc, x, y, z);
                    }
                }
            }
        }
    }
}
