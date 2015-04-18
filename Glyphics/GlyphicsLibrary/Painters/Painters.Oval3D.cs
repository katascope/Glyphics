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
        //Draw an oval in 3d
        public void DrawOval3D(IByteGridContext bgc, int sx, int sy, int sz, int width, int height, int depth)
        {
            for (int z = sz - depth; z <= sz + depth; z++)
            {
                for (int y = sy - height; y <= sy + height; y++)
                {
                    for (int x = sx - width; x <= sx + width; x++)
                    {
                        double dx = (x - sx) / (double)(width * 2);
                        double dy = (y - sy) / (double)(height * 2);
                        double dz = (z - sz) / (double)(depth * 2);
                        double dist = Math.Sqrt(dx * dx + dy * dy + dz * dz);
                        if (dist < 0.5) DrawPen(bgc, x, y, z);
                    }
                }
            }
        }
    }
}
