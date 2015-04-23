#region Copyright
/*Copyright (c) 2015, Katascope
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/
#endregion
using System;
using GlyphicsLibrary.ByteGrid;
using GlyphicsLibrary.Atomics;

namespace GlyphicsLibrary.Painters
{
    internal partial class CPainter
    {
        //Apply an arbitrary image filter
        public static void ApplyImageFilter(IGrid grid, double strength, double bias, double[][] filter)
        {
            int filterWidth = 0;
            int filterHeight = filter.GetLength(0);

            for (int i = 0; i < filterHeight; i++)
            {
                filterWidth = filter[i].Length;                
            }

            IGrid origGrid = grid.Clone();

            for (int z = 0; z < grid.SizeZ; z++)
            {
                for (int y = 0; y < grid.SizeY; y++)
                {
                    for (int x = 0; x < grid.SizeX; x++)
                    {
                        double red = 0.0, green = 0.0, blue = 0.0;

                        //multiply every value of the filter with corresponding image pixel 
                        for (int filterX = 0; filterX < filterWidth; filterX++)
                        {
                            for (int filterY = 0; filterY < filterHeight; filterY++)
                            {
                                int imageX = (x - filterWidth / 2 + filterX + grid.SizeX) % grid.SizeX;
                                int imageY = (y - filterHeight / 2 + filterY + grid.SizeY) % grid.SizeY;
                                double f = filter[filterX][filterY];
                                ulong u = origGrid.GetRgba(imageX, imageY, z);
                                byte r, g, b, a;
                                Atomics.Converter.Ulong2Rgba(u, out r, out g, out b, out a);
                                red += r * f;
                                green += g * f;
                                blue += b * f;

                                //truncate values smaller than zero and larger than 255 
                                r = (byte)Math.Min(Math.Max((strength * red + bias), 0), 255);
                                g = (byte)Math.Min(Math.Max((strength * green + bias), 0), 255);
                                b = (byte)Math.Min(Math.Max((strength * blue + bias), 0), 255);

                                if ((r+g+b) < 12)
                                {
                                    r = g = b = 0;
                                    a = 0;
                                }
                                else a=255;

                                u = Atomics.Converter.Rgba2Ulong(r, g, b, a);
                                grid.Plot(x, y, z, u);
                            }
                        }
                    }
                }
            }
        }

        static private readonly double[][] FilterBlurData = 
        { 
            new [] {0.0, 0.2, 0.0 },
            new [] {0.2, 0.2, 0.2 },
            new [] {0.0, 0.2, 0.0 }
        };
        public void ApplyFilterBlur(IGridContext bgc)
        {
            if (bgc == null) return;
            ApplyImageFilter(bgc.Grid, 1.0, 0.0, FilterBlurData);
        }

        static private readonly double[][] FilterMotionData = 
        {
            new [] { 1.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 },
            new [] { 0.0, 1.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 },
            new [] { 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 },
            new [] { 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 0.0, 0.0 },
            new [] { 0.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 0.0 },
            new [] { 0.0, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0 },
            new [] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0 },
            new [] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0 },
            new [] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 1.0 }
        };
        public void ApplyFilterMotion(IGridContext bgc)
        {
            if (bgc == null) return;
            ApplyImageFilter(bgc.Grid, 1.0 / 9.0, 0.0, FilterMotionData);
        }

        static private readonly double[][] FilterEdgeHorizontalData = 
        { 
            new double[] {0,  0,  0,  0,  0},
            new double[] {0,  0,  0,  0,  0},
            new double[] {-1, -1,  2,  0,  0},
            new double[] {0,  0,  0,  0,  0},
            new double[] {0,  0,  0,  0,  0}
        };
        public void ApplyFilterEdgeHorizontal(IGridContext bgc)
        {
            if (bgc == null) return;
            ApplyImageFilter(bgc.Grid, 1.0, 0.0, FilterEdgeHorizontalData);
        }

        static private readonly double[][] FilterEdgeVerticalData = 
        { 
             new double[] {0,  0, -1,  0,  0},
             new double[] {0,  0, -1,  0,  0},
             new double[] {0,  0,  4,  0,  0},
             new double[] {0,  0, -1,  0,  0},
             new double[] {0,  0, -1,  0,  0}
        };
        public void ApplyFilterEdgeVertical(IGridContext bgc)
        {
            if (bgc == null) return;
            ApplyImageFilter(bgc.Grid, 1.0, 0.0, FilterEdgeVerticalData);
        }

        static private readonly double[][] FilterEdgeAllData = 
        { 
            new double[] {-1, -1, -1},
            new double[] {-1,  8, -1},
            new double[] {-1, -1, -1}
        };
        public void ApplyFilterEdgeAll(IGridContext bgc)
        {
            if (bgc == null) return;
            ApplyImageFilter(bgc.Grid, 1.0, 0.0, FilterEdgeAllData);
        }

        static private readonly double[][] FilterSharpenData = 
        { 
            new double[] {-1, -1, -1},
            new double[] {-1,  9, -1},
            new double[] {-1, -1, -1}
        };
        public void ApplyFilterSharpen(IGridContext bgc)
        {
            if (bgc == null) return;
            ApplyImageFilter(bgc.Grid, 1.0, 0.0, FilterSharpenData);
        }

        static private readonly double[][] FilterEmbossData = 
        { 
            new double[] {-1, -1,  0},
            new double[] {-1,  0,  1},
            new double[] { 0,  1,  1}
        };
        public void ApplyFilterEmboss(IGridContext bgc)
        {
            if (bgc == null) return;
            ApplyImageFilter(bgc.Grid, 1.0, 0.0, FilterEmbossData);
        }

        static private readonly double[][] FilterSmoothData = 
        { 
            new double[] { 1, 1, 1 },
            new double[] { 1, 1, 1 },
            new double[] { 1, 1, 1 }
        };
        public void ApplyFilterSmooth(IGridContext bgc)
        {
            if (bgc == null) return;
            ApplyImageFilter(bgc.Grid, 1.0, 0.0, FilterSmoothData);
        }
    }
}
