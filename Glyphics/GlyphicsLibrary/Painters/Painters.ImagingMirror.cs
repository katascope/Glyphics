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
        /// <summary>
        /// Mirror Grid on X-axis
        /// </summary>
        public void MirrorX(IByteGridContext bgc)
        {
            if (bgc == null) return;

            IGrid grid = bgc.Grid;
            //Inhibit because it would affect ALL pixels
            grid.InhibitCodeTracking();
            for (int bz = 0; bz < grid.SizeZ; bz++)
            {
                for (int by = 0; by < grid.SizeY; by++)
                {
                    for (int bx = 0; bx < grid.SizeX / 2; bx++)
                    {
                        ulong b = grid.GetRgba(bx, by, bz);
                        ICellProperties cp = grid.GetProperty(bx, by, bz);

                        grid.Plot(grid.SizeX - 1 - bx, by, bz, b, 
                            (byte)cp.PhysicsId, (byte)cp.ShapeId, (byte)cp.TextureId, (byte)cp.AnimateId);
                    }
                }
            }
            grid.AllowCodeTracking();
        }

        /// <summary>
        /// Mirror Grid on Y-axis
        /// </summary>
        public void MirrorY(IByteGridContext bgc)
        {
            if (bgc == null) return;

            IGrid grid = bgc.Grid;
            //Inhibit because it would affect ALL pixels
            grid.InhibitCodeTracking();
            for (int bz = 0; bz < grid.SizeZ; bz++)
            {
                for (int bx = 0; bx < grid.SizeX; bx++)
                {
                    for (int by = 0; by < grid.SizeY / 2; by++)
                    {
                        ulong b = grid.GetRgba(bx, by, bz);
                        ICellProperties cp = grid.GetProperty(bx, by, bz);

                        grid.Plot(bx, grid.SizeY - 1 - by, bz, b,
                            (byte)cp.PhysicsId, (byte)cp.ShapeId, (byte)cp.TextureId, (byte)cp.AnimateId);
                    }
                }
            }
            grid.AllowCodeTracking();
        }

        /// <summary>
        /// Mirror Grid on Z-axis
        /// </summary>
        public void MirrorZ(IByteGridContext bgc)
        {
            if (bgc == null) return;

            IGrid grid = bgc.Grid;
            //Inhibit because it would affect ALL pixels
            grid.InhibitCodeTracking();
            for (int by = 0; by < grid.SizeY; by++)
            {
                for (int bx = 0; bx < grid.SizeX; bx++)
                {
                    for (int bz = 0; bz < grid.SizeZ / 2; bz++)
                    {
                        ulong b = grid.GetRgba(bx, by, bz);
                        ICellProperties cp = grid.GetProperty(bx, by, bz);

                        grid.Plot(bx, by, grid.SizeZ - 1 - bz, b,
                            (byte)cp.PhysicsId, (byte)cp.ShapeId, (byte)cp.TextureId, (byte)cp.AnimateId);

                    }
                }
            }
            grid.AllowCodeTracking();
        }
    }
}
