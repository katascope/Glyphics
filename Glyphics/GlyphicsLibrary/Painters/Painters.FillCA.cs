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
        /* Logic for 1D : We need to evaluate at x,y,z and check y+1, then set appropriately
         * 
         * caCode bits: 
         *  currentBlock = empty
         *  0 : Above
         */
        private static bool MatchRule1D(IGrid grid, int x, int y, int z, out ulong val)
        {
            if (grid.GetRgba(x, y, z) == 0) //check that we are empty
            {
                ulong uAbove = grid.GetRgba(x, y + 1, z);
                bool hasAbove = (uAbove != 0);
                if (hasAbove)
                {
                    val = uAbove;
                    return true;
                }
            }
            val = 0;
            return false;
        }

        //Draw supports underneath any suspended block
        public static void DrawSupports1D(IByteGridContext bgc, int x1, int y1, int z1, int x2, int y2, int z2)
        {
            //Ensure x2 >= x1, y2 >= y1, z2 >= z1
            MinMax(ref x1, ref x2);
            MinMax(ref y1, ref y2);
            MinMax(ref z1, ref z2);

            IGrid grid = bgc.Grid;
            ulong val;

            for (int y = y2; y >= y1; y--)
                for (int x = x1; x < x2; x++)
                    for (int z = z1; z < z2; z++)
                        if (MatchRule1D(grid, x, y, z, out val)) grid.Plot(x, y, z, val);
        }


        /* Logic for 3D
         * 
         * What we want:
         *  For planes to split out into corner-supported pillars
         *  
         * caCode bits: 
         *  currentBlock = empty
         *  aboveBlock = not empty
         *  0 : BottomLeft
         *  1 : BottomRight
         *  2 : TopLeft
         *  3 : TopRight
         */
        private static bool MatchRule3D(IGrid grid, int x, int y, int z, out ulong val)
        {
            ulong uAbove = grid.GetRgba(x, y + 1, z);

            bool bNotThis = grid.GetRgba(x, y, z) == 0;
            bool bAbove = grid.GetRgba(x, y + 1, z) != 0;
            bool bAboveLeft = grid.GetRgba(x - 1, y + 1, z) != 0;
            bool bAboveRight = grid.GetRgba(x + 1, y + 1, z) != 0;

            bool bAboveFront = grid.GetRgba(x, y + 1, z + 1) != 0;
            bool bAboveBack  = grid.GetRgba(x, y + 1, z - 1) != 0;

            val = 0;

            int countAbove1 = 0;
            if (bAboveLeft) countAbove1++;
            if (bAboveRight) countAbove1++;
            if (bAboveFront) countAbove1++;
            if (bAboveBack) countAbove1++;

            int countAboveAcross = 0;
            if (bAboveLeft && bAboveRight) countAboveAcross++;
            if (bAboveFront && bAboveBack) countAboveAcross++;

            if (bNotThis && bAbove) //Basic check to apply rule at all (or not)
            {
                //Nothing
                if (countAbove1 == 0) { val = uAbove; return true; }

                //Corners rule
                if (countAbove1 == 2 && countAboveAcross == 0) { val = uAbove; return true; }
            }

            return false;
        }


        //Draw supports under suspended corner points
        public void DrawCorners(IByteGridContext bgc, int x1, int y1, int z1, int x2, int y2, int z2)
        {
            if (bgc == null) return;

            //Ensure x2 >= x1, y2 >= y1, z2 >= z1
            MinMax(ref x1, ref x2);
            MinMax(ref y1, ref y2);
            MinMax(ref z1, ref z2);

            IGrid grid = bgc.Grid;
            ulong val;

            for (int y = y2; y >= y1; y--)
                for (int x = x1; x < x2; x++)
                    for (int z = z1; z < z2; z++)
                        if (MatchRule3D(grid, x, y, z, out val)) grid.Plot(x, y, z, val);
        }
    }
}

