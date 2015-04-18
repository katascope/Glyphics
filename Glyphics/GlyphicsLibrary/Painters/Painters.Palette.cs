#region Copyright
/*Copyright (c) 2015, Katascope
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/
#endregion
using System.Collections.Generic;
using GlyphicsLibrary.Atomics;

namespace GlyphicsLibrary.Painters
{
    internal partial class CPainter
    {
        //Generate a palette between two rgba values
        public void PalGen1D(IGrid pal, byte vs1, byte vs2, byte vs3, byte vs4, byte vd1, byte vd2, byte vd3, byte vd4)
        {
            if (pal == null) return;

            for (int x = 0; x < pal.SizeX; x++)
            {
                double factor = x / (double)(pal.SizeX);
                var lerp1 = (byte)(((vs1 + ((vd1 - vs1) * factor))));
                var lerp = (ulong)(lerp1 << 0);
                if (pal.Bpp > 1) lerp |= (((ulong)(((vs2 + ((vd2 - vs2) * factor)))) << 8));
                if (pal.Bpp > 2) lerp |= (((ulong)(((vs3 + ((vd3 - vs3) * factor)))) << 16));
                if (pal.Bpp > 3) lerp |= (((ulong)(((vs4 + ((vd4 - vs4) * factor)))) << 24));
                pal.Plot(x, 0, 0, lerp);
            }
        }

        //Generate a palette from a List of banding rgba
        public void PalGen1DBanded(IGrid pal, List<ulong> bandColors)
        {
            if (pal == null || bandColors == null) return;

            double sectionSize = 256.0 / (bandColors.Count - 1);

            for (int band = 0; band < bandColors.Count-1; band++)
            {
                ulong a = bandColors[band];
                ulong b = bandColors[band+1];
                for (int i = 0; i < sectionSize; i++)
                {
                    double mux = i/ sectionSize;
                    ulong color = Lerper.LerpRgba(mux, a, b);
                    pal.Plot((int)(band*sectionSize+i),0,0,color);
                }
            }
        }

        //Generate one banding of a palette
        public void PalGen1DBand(int numBands, IGrid pal, ulong color1, ulong color2, int band)
        {
            if (pal == null) return;

            double sectionSize = 256.0 / numBands/2;
            ulong a = color1;
            ulong b = color2;
            var offset = (int)(band * sectionSize*2);

            for (int i = 0; i <= sectionSize; i++)
            {
                double mux = i / sectionSize;
                ulong color = Lerper.LerpRgba(mux, a, b);
                pal.Plot((i+offset), 0, 0, color);
            }
            for (int i = 0; i <= sectionSize; i++)
            {
                double mux = i / sectionSize;
                ulong color = Lerper.LerpRgba(mux, b, a);
                pal.Plot((int)(i+sectionSize+offset), 0, 0, color);
            }
        }
    }
}
