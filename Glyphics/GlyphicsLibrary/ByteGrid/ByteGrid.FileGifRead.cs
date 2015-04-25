#region Copyright
/*Copyright (c) 2015, Katascope
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/
#endregion
using System.IO;
using System;
#if !NET2
using System.Windows.Media.Imaging;
#endif

namespace GlyphicsLibrary.ByteGrid
{
    //Utility class for reading PNG files
    internal class FileGifRead
    {
        private FileGifRead() { }

        internal static void CopyBitmapSourceToGridPalette(BitmapSource bitmapSource, IGrid grid, int top, int left)
        {
            int width = bitmapSource.PixelWidth;
            int height = bitmapSource.PixelHeight;
            BitmapPalette palette = bitmapSource.Palette;

            var originalPixels = new byte[bitmapSource.PixelWidth * bitmapSource.PixelHeight * 4];
            int stride = bitmapSource.PixelWidth * bitmapSource.Format.BitsPerPixel / 8;
            stride = stride + (stride % 4) * 4;
            bitmapSource.CopyPixels(originalPixels, stride, 0);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    byte r = 255;
                    byte g = 255;
                    byte b = 255;
                    byte a = 255;

                    if (bitmapSource.Format.BitsPerPixel == 8)
                    {
                        int palVal = originalPixels[(y *stride) + x ];
                        r = palette.Colors[palVal].R;
                        g = palette.Colors[palVal].G; 
                        b = palette.Colors[palVal].B;
                        a = palette.Colors[palVal].A;
                    }

                    if (a == 255)
                    {
                        ulong u = GlyphicsApi.Rgba2Ulong(r, g, b, a);

                        grid.Plot(x + top, y + left, 0, u);
                    }
                }
            }
        }

        //Load grid and return as 2d Grid
        public static IGrid GifToGrid(string filename)
        {
#if !NET2
            IGridList grids = GifToGrids(filename);
            return grids.GetGrid(0);

#else
            return null;
#endif
        }

        public static int GetMaxX(GifBitmapDecoder decoder)
        {
            int maxWidth = 0;
            foreach (BitmapFrame frame in decoder.Frames)
            {
// Get a clone copy of the metadata
                var sourceMetadata = frame.Metadata as BitmapMetadata;
                int left = Int32.Parse(sourceMetadata.GetQuery("/imgdesc/Left").ToString());

                int width = Int32.Parse(sourceMetadata.GetQuery("/imgdesc/Width").ToString());
                if ((width+left) > maxWidth)
                    maxWidth = width+left;
            }
            return maxWidth;
        }


        public static int GetMaxY(GifBitmapDecoder decoder)
        {
            int maxHeight = 0;
            foreach (BitmapFrame frame in decoder.Frames)
            {
// Get a clone copy of the metadata
                var sourceMetadata = frame.Metadata as BitmapMetadata;
                int top = Int32.Parse(sourceMetadata.GetQuery("/imgdesc/Top").ToString());

                int height = Int32.Parse(sourceMetadata.GetQuery("/imgdesc/Height").ToString());
                if ((height + top) > maxHeight)
                    maxHeight = height + top;
            }
            return maxHeight;
        }

        //Load grid and return as 2d Grids
        public static IGridList GifToGrids(string filename)
        {
#if !NET2
            IGridList grids = new CGridList();

            // Open a Stream and decode a GIF image
            Stream imageStreamSource = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
            var decoder = new GifBitmapDecoder(imageStreamSource, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            decoder.Frames[0].Metadata.Freeze();

            int width = GetMaxX(decoder);
            int height = GetMaxY(decoder);

            foreach (BitmapFrame frame in decoder.Frames)
            {
                frame.Freeze();
                // Get a clone copy of the metadata
                var sourceMetadata = frame.Metadata as BitmapMetadata;
                int top = Int32.Parse(sourceMetadata.GetQuery("/imgdesc/Top").ToString());
                int left = Int32.Parse(sourceMetadata.GetQuery("/imgdesc/Left").ToString());

                IGrid grid = new CGrid(width, height, 1, 4);
                CopyBitmapSourceToGridPalette(frame, grid, top, left);
                grids.AddGrid(grid);
            }
            return grids;
#else
            return null;
#endif
        }
    }
}
