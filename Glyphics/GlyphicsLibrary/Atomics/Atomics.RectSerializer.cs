#region Copyright
/*Copyright (c) 2015, Katascope
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/
#endregion
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlyphicsLibrary.Atomics
{
    //Serializer for rectangles
    internal class RectSerializer
    {
        //Parsing characters
        public const char CharRgba = '*';
        public const char CharRects = ':';
        public const char CharLimit255 = '\n';
        public const char CharAnim = '|';

        //Search through a list of list of rects
        private static List<IRect> FindListInLists(IEnumerable<List<IRect>> rects, ulong unifiedValue)
        {
            if (rects == null) return null;

            return rects.FirstOrDefault(rectList => rectList[0].Properties.UnifiedValue == unifiedValue);
        }

        //Sort a set of rectangles into a list of rects by unified value
        private static IEnumerable<List<IRect>> SuperSort(IRectList rects)
        {
            var superRects = new List<List<IRect>>();

            foreach (IRect rect in rects)
            {
                rect.Properties.UnifiedValue = rect.Properties.CalcUnified();
            }

            foreach (IRect rect in rects)
            {
                List<IRect> sortedList = FindListInLists(superRects,rect.Properties.UnifiedValue);
                if (sortedList == null)
                {
                    var newList = new List<IRect>();
                    newList.Add(rect);
                    superRects.Add(newList);
                }
                else sortedList.Add(rect);
            }
            return superRects;
        }

        //Serialize cellproperties to a string
        private static string SerializeRectProperties(ICellProperties properties)
        {
            string str = "";

            str += ""+CharRgba;
            byte r, g, b, a;
            Transcode64.RecodeUlongtoRgba(properties.Rgba, out r, out g, out b, out a);
            str += Transcode64.To64(r);
            str += Transcode64.To64(g);
            str += Transcode64.To64(b);
            str += Transcode64.To64(a);
            str += Transcode64.To64(properties.ShapeId);
            str += Transcode64.To64(properties.TextureId);

            if (properties.AnimateId > 0)
            {
                var x = (byte)((properties.AnimateId >> 0) & 63);
                var y = (byte)((properties.AnimateId >> 8) & 63);
                var z = (byte)((properties.AnimateId >> 16) & 63);
                str += "" + CharAnim;
                str += Transcode64.To64(x);
                str += Transcode64.To64(y);
                str += Transcode64.To64(z);
            }

            return str;
        }

        //Serialize a set of rectangles(IRectList) to ISerializedRects
        public static ISerializedRects Serialize(IRectList rectSet)
        {
            var sb = new StringBuilder();
            IEnumerable<List<IRect>> superRects = SuperSort(rectSet);

            foreach (List<IRect> rectList in superRects)
            {
                sb.Append(SerializeRectProperties(rectList[0].Properties));

                foreach (IRect rect in rectList)
                {
                    sb.Append(Transcode64.SerializeRectToString(rect, 1));
                }
            }
            return new CSerializedRects(sb.ToString());
        }

        //Deserialized ISerializedRects back to set of rectangles(IRectList)
        public static IRectList Deserialize(ISerializedRects serializedRects)
        {
            IRectList rects = new CRectList();
            char state = ' ';
            byte shapeId = 0;
            byte textureId = 0;
            ulong rgba = 0;
            int anim = 0;

            string serial = serializedRects.SerializedData;

            for (int i = 0; i < serial.Length; i++)
            {
                if (serial[i] == CharRgba) state = CharRgba;
                if (serial[i] == CharAnim) state = CharAnim;
                if (serial[i] == CharLimit255) state = CharLimit255;
                switch (state)
                {
                    case CharAnim:
                        {
                            i++;
                            int x = Transcode64.From64(serial[i++]);
                            int y = Transcode64.From64(serial[i++]);
                            int z = Transcode64.From64(serial[i]);
                            anim = (x << 0) | (y << 8) | (z << 16);
                            state = CharRects;
                            break;
                        }
                    case CharRgba://Reading RGBA palette
                        {
                            i++;
                            int r = Transcode64.From64(serial[i++]);
                            int g = Transcode64.From64(serial[i++]);
                            int b = Transcode64.From64(serial[i++]);
                            int a = Transcode64.From64(serial[i++]);

                            rgba = Transcode64.RecodeRgbatoUlong((byte)r, (byte)g, (byte)b, (byte)a);
                            shapeId = Transcode64.From64(serial[i++]);
                            textureId = Transcode64.From64(serial[i]);
                            state = CharRects;
                            break;
                        }
                    case CharRects://Reading emergents
                        {
                            IRect rect = new CRect();
                            rect.Pt1.X = Transcode64.From64(serial[i++]);
                            rect.Pt1.Y = Transcode64.From64(serial[i++]);
                            rect.Pt1.Z = Transcode64.From64(serial[i++]);
                            rect.Pt2.X = Transcode64.From64(serial[i++]);
                            rect.Pt2.Y = Transcode64.From64(serial[i++]);
                            rect.Pt2.Z = Transcode64.From64(serial[i]);

                            //Convert from inclusve
                            rect.Pt2.X++;
                            rect.Pt2.Y++;
                            rect.Pt2.Z++;

                            rect.Properties.Rgba = rgba;
                            rect.Properties.ShapeId = shapeId;
                            rect.Properties.TextureId = textureId;
                            rect.Properties.AnimateId = anim;
                            rects.AddRect(rect);
                            
                            break;
                        }
                }
            }
            return rects;
        }
    }
}
