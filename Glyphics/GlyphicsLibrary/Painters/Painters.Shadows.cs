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
        //Draw shadows
        public void DrawShadows(IGridContext bgc)
        {
            if (bgc == null || bgc.Grid == null) return;

            IGrid grid = bgc.Grid;
            
            for (int z=0;z<grid.SizeZ;z++)
            {
                for (int y = grid.SizeY-1;y>1;y--) //go from top to bottom for y
                {
                    for (int x=0;x<grid.SizeX;x++)
                    {
                        ulong u = grid.GetRgba(x, y, z);
                        ulong ubelow = grid.GetRgba(x, y - 1, z);
                        ulong ubelow2 = grid.GetRgba(x, y - 2, z);

                        
                        if ( (u != 0) //make sure we have a block
                          && (ubelow == 0 && ubelow2 != 0) // make sure under is empty and under that is not
                          && (u != ubelow2) //make sure block and two below are not same type
                            )
                        {
                            byte r, g, b, a;
                            Atomics.Converter.Ulong2Rgba(ubelow2, out r, out g, out b, out a);
                            r /= 2;
                            g /= 2;
                            b /= 2;

                            ubelow2 = Atomics.Converter.Rgba2Ulong(r, g, b, a);
                            grid.Plot(x, y - 2, z, ubelow2);
                        }
                    }
                }
            }
        }
    }
}
