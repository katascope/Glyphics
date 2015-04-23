#region Copyright
/*Copyright (c) 2015, Katascope
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/
#endregion
using System;

namespace GlyphicsLibrary.Atomics
{
    //Partial pixel utility class - HSV
    internal partial class Converter
    {
        /* Convert an HSL value to a ulong RGBA value
         * Inputs:
         *  h : 0 to 360
         *  s : 0 to 100
         *  l : 0 to 100
         */
        public static ulong Hsl2Rgb(double h, double s, double l)
        {
            double fr, fg, fb;//this function works with doubles between 0 and 1 

            h = h / 360.0f;
            s = s / 100.0f;
            l = l / 100.0f;

            //If saturation is 0, the color is a shade of gray
            if (Compare.CompareDoubleAreEqual(s, 0))
            {
                fr = l;
                fg = l;
                fb = l;
            }
            else
            {
                double temp2;
                //If saturation > 0, more complex calculations are needed
                //Set the temporary values      
                if (l < 0.5f) temp2 = l * (1 + s);
                else temp2 = (l + s) - (l * s);
                double temp1 = 2 * l - temp2;
                double tempr = h + 1.0f / 3.0f;
                if (tempr > 1) tempr--;
                double tempg = h;
                double tempb = h - 1.0f / 3.0f;
                if (tempb < 0) tempb++;

                //Red     
                if (tempr < 1.0f / 6.0f) fr = temp1 + (temp2 - temp1) * 6.0f * tempr;
                else if (tempr < 0.5f) fr = temp2;
                else if (tempr < 2.0f / 3.0f) fr = temp1 + (temp2 - temp1) * ((2.0f / 3.0f) - tempr) * 6.0f;
                else fr = temp1;

                //Green       
                if (tempg < 1.0f / 6.0f) fg = temp1 + (temp2 - temp1) * 6.0f * tempg;
                else if (tempg < 0.5f) fg = temp2;
                else if (tempg < 2.0f / 3.0f) fg = temp1 + (temp2 - temp1) * ((2.0f / 3.0f) - tempg) * 6.0f;
                else fg = temp1;

                //Blue    
                if (tempb < 1.0f / 6.0f) fb = temp1 + (temp2 - temp1) * 6.0f * tempb;
                else if (tempb < 0.5f) fb = temp2;
                else if (tempb < 2.0f / 3.0f) fb = temp1 + (temp2 - temp1) * ((2.0f / 3.0f) - tempb) * 6.0f;
                else fb = temp1;
            }

            //Convert back to byte
            var r = (byte)(fr * 256f);
            var g = (byte)(fg * 256f);
            var b = (byte)(fb * 256f);

            return Converter.Rgba2Ulong(r, g, b, 255);
        }

        //Convert an R/G/B value to an HSL value
        public static void Rgb2Hsl(byte ir, byte ig, byte ib, out double h, out double s, out double l)
        {
            //this function works with doubles between 0 and 1 
            double fr = ir / 256.0f;
            double fg = ig / 256.0f;
            double fb = ib / 256.0f;

            double maxColor = Math.Max(fr, Math.Max(fg, fb));
            double minColor = Math.Min(fr, Math.Min(fg, fb));

            //R == G == B, so it's a shade of gray
            if (ir == ig && ir == ib)
            {
                h = 0.0f; //it doesn't matter what value it has       
                s = 0.0f;
                l = fr; //doesn't matter if you pick r, g, or b   
            }
            else
            {
                l = (minColor + maxColor) / 2;

                if (l < 0.5f) s = (maxColor - minColor) / (maxColor + minColor);
                else s = (maxColor - minColor) / (2.0f - maxColor - minColor);

                if (Compare.CompareDoubleAreEqual(fr, maxColor)) h = (fg - fb) / (maxColor - minColor);
                else if (Compare.CompareDoubleAreEqual(fg, maxColor)) h = 2.0f + (fb - fr) / (maxColor - minColor);
                else h = 4.0f + (fr - fg) / (maxColor - minColor);

                h /= 6; //to bring it to a number between 0 and 1
                if (h < 0) h++;
            }
            //Convert to sane units
            h *= 360.0f;
            s *= 100.0f;
            l *= 100.0f;
        }

        //Adjust the brightness level of a r/g/b/a value
        public static void Brightness(double factor, ref ulong u)
        {
            byte r, g, b, a;
            Converter.Ulong2Rgba(u, out r, out g, out b, out a);
            factor = factor / 100.0f;
            r = (byte)Math.Min(r * factor, 255);
            g = (byte)Math.Min(g * factor, 255);
            b = (byte)Math.Min(b * factor, 255);
            u = Converter.Rgba2Ulong(r, g, b, a);
        }

        //Adjust the saturation level of a r/g/b/a value
        public static void Saturation(double factor, ref ulong u)
        {
            byte r, g, b, a;
            Converter.Ulong2Rgba(u, out r, out g, out b, out a);
            double h, s, l;
            Rgb2Hsl(r, g, b, out h, out s, out l);
            factor = factor / 100.0f;
            l *= factor;
            u = Hsl2Rgb(h, s, l);
            u = Converter.SetAlpha(u, a);
        }
    }
}
