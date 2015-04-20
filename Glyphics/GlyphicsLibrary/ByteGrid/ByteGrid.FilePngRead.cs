﻿#region Copyright
/*Copyright (c) 2015, Katascope
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/
#endregion
using System.IO;
#if !NET2
using System.Windows.Media.Imaging;
#endif

namespace GlyphicsLibrary.ByteGrid
{
    //Utility class for reading PNG files
    internal class FilePngRead
    {
        private FilePngRead() { }

        //Load grid and return as 2d Grid
        public static IGrid PngToGrid(string filename)
        {
#if !NET2

            Stream imageStreamSource = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
            var decoder = new PngBitmapDecoder(imageStreamSource, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            BitmapSource bitmapSource = decoder.Frames[0];

            int bytesPerPixel = bitmapSource.Format.BitsPerPixel / 8;
            var originalPixels = new byte[bitmapSource.PixelWidth * bitmapSource.PixelHeight * 4];
            int stride = bitmapSource.PixelWidth * bitmapSource.Format.BitsPerPixel / 8;
            stride = stride + (stride % 4) * 4;
            bitmapSource.CopyPixels(originalPixels, stride, 0);

            int width = bitmapSource.PixelWidth;
            int height = bitmapSource.PixelHeight;

            IGrid grid = GlyphicsApi.CreateGrid(width, height, 1, 4);

            imageStreamSource.Close();

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    byte r = 255;
                    byte g = 255;
                    byte b = 255;
                    byte a = 255;
                    if (bitmapSource.Format.BitsPerPixel > 0)
                        b = originalPixels[(y * width * bytesPerPixel) + (x * bytesPerPixel + 0)];
                    if (bitmapSource.Format.BitsPerPixel > 1)
                        g = originalPixels[(y * width * bytesPerPixel) + (x * bytesPerPixel + 1)];
                    if (bitmapSource.Format.BitsPerPixel > 2)
                        r = originalPixels[(y * width * bytesPerPixel) + (x * bytesPerPixel + 2)];
                    if (bitmapSource.Format.BitsPerPixel > 3)
                        a = originalPixels[(y * width * bytesPerPixel) + (x * bytesPerPixel + 3)];

                    ulong u = GlyphicsApi.Rgba2Ulong(r, g, b, a);
                    grid.Plot(x, y, 0, u);
                }
            }
            return grid;
#else
            return null;
#endif
        }
    }
}
