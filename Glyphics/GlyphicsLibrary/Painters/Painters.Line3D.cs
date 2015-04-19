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
        //Draw a line in 3D to Grid
        public void DrawLine3D(IByteGridContext bgc, int x1, int y1, int z1, int x2, int y2, int z2)
        {
            //MinMax(ref x1, ref x2);
            //MinMax(ref y1, ref y2);
            //MinMax(ref z1, ref z2);

            int dx = x2 - x1;
            int dy = y2 - y1;
            int dz = z2 - z1;

            int ax = Math.Abs(dx) * 2;
            int ay = Math.Abs(dy) * 2;
            int az = Math.Abs(dz) * 2;

            int sx = Math.Sign(dx);
            int sy = Math.Sign(dy);
            int sz = Math.Sign(dz);

            int x = x1;
            int y = y1;
            int z = z1;
            int idx = 1;

            if(ax>=Math.Max(ay,az))			//% x dominant
            {
               int yd = ay - ax/2;
               int zd = az - ax/2;

               while(true)
               {
                  DrawPen(bgc, x, y, z);
                  idx = idx + 1;

                  if(x == x2)
                  { 		//% end
                     break;
                  }

                  if(yd >= 0)
                  {//		% move along y
                     y = y + sy;
                     yd = yd - ax;
                  }

                  if(zd >= 0)
                  {//		% move along z
                     z = z + sz;
                     zd = zd - ax;
                  }

                  x  = x  + sx;		//% move along x
                  yd = yd + ay;
                  zd = zd + az;
               }
            }
            else if (ay>=Math.Max(ax,az))	
            {	//% y dominant
               int xd = ax - ay/2;
               int zd = az - ay/2;

               while(true)
               {
                  DrawPen(bgc, x, y, z);
                  idx = idx + 1;

                  if(y == y2)
                  {//		% end
                     break;
                  }

                  if(xd >= 0)
                  {//		% move along x
                     x = x + sx;
                     xd = xd - ay;
                  }

                  if(zd >= 0)
                  {//		% move along z
                     z = z + sz;
                     zd = zd - ay;
                  }

                  y  = y  + sy;		//% move along y
                  xd = xd + ax;
                  zd = zd + az;
               }
            }
            else if (az >= Math.Max(ax, ay))
            {//		% z dominant
                int xd = ax - az / 2;
                int yd = ay - az / 2;

                while (true)
                {
                    DrawPen(bgc, x, y, z);
                    idx = idx + 1;

                    if (z == z2)
                    {//	% end
                        break;
                    }

                    if (xd >= 0)
                    {		//% move along x
                        x = x + sx;
                        xd = xd - az;
                    }

                    if (yd >= 0)
                    {//	% move along y
                        y = y + sy;
                        yd = yd - az;
                    }

                    z = z + sz;	//	% move along z
                    xd = xd + ax;
                    yd = yd + ay;
                }
            }
        }
    }
}
