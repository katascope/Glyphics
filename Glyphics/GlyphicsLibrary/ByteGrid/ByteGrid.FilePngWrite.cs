#region Copyright
/*Copyright (c) 2015, Katascope
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/
#endregion
using System.IO;
#if !NET2
using System.Windows.Media;
using System.Windows.Media.Imaging;
#endif
using GlyphicsLibrary.Atomics;

namespace GlyphicsLibrary.ByteGrid
{
    //Utility class for writing PNG files
    internal class FilePngWrite
    {
        private FilePngWrite() { }

        //Save a 2d grid to png
        public static void SaveFlatPng(string filename, IGrid grid)
        {
            const int pngBpp = 4;
            int width = grid.SizeX;
            int height = grid.SizeY * grid.SizeZ;
            int stride = width * pngBpp;
            var pixels = new byte[height * stride];

            for (int y = 0; y < grid.SizeY; y++)
            {
                for (int x = 0; x < grid.SizeX; x++)
                {
                    ulong val = grid.GetRgba(x, y, 0);
                    if (val != 0)
                    {
                        //int foo;
                        //foo = 1;
                    }
                    long offset = (y * grid.SizeX * pngBpp) + (x * pngBpp);
                    byte r, g, b, a;
                    Pixel.Ulong2Rgba(val, out r, out g, out b, out a);

                    if (grid.Bpp == 1)
                    {
                        pixels[offset + 0] = r;
                        pixels[offset + 1] = r;
                        pixels[offset + 2] = r;
                        pixels[offset + 3] = 255;
                    }
                    else if (grid.Bpp == 2)
                    {
                        pixels[offset + 0] = r;
                        pixels[offset + 1] = g;
                        pixels[offset + 2] = 255;
                        pixels[offset + 3] = 255;
                    }
                    else if (grid.Bpp == 3)
                    {
                        pixels[offset + 0] = b;
                        pixels[offset + 1] = g;
                        pixels[offset + 2] = r;
                        pixels[offset + 3] = 255;
                    }
                    else if (grid.Bpp == 4)
                    {
                        pixels[offset + 0] = b;
                        pixels[offset + 1] = g;
                        pixels[offset + 2] = r;
                        pixels[offset + 3] = 255;
                    }
                }
            }

#if !NET2
            BitmapSource image = BitmapSource.Create(width, height, 96, 96, PixelFormats.Bgra32, null, pixels, stride);

            var stream = new FileStream(filename, FileMode.Create);
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));
            encoder.Save(stream);

            stream.Close();
#endif
        }
    }
}
