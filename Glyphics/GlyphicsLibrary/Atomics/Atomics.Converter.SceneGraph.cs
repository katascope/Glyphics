#region Copyright
/*Copyright (c) 2015, Katascope
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/
#endregion

namespace GlyphicsLibrary.Atomics
{
    //Utility for converting from/to elements, scenes
    internal partial class Converter
    {
        //Convert a rectangle volume to Translation XYZ, Scale XYZ, useful for elements
        private static void Rect2TranslateAndScale(IRect rect, out double tx, out double ty, out double tz, out double sx, out double sy, out double sz)
        {
            sx = rect.Pt2.X - rect.Pt1.X;
            sy = rect.Pt2.Y - rect.Pt1.Y;
            sz = rect.Pt2.Z - rect.Pt1.Z;
            tx = rect.Pt1.X + sx / 2;
            ty = rect.Pt1.Y + sy / 2;
            tz = rect.Pt1.Z + sz / 2;
        }

        //Convert a translation and scale to a rectangle volume
        private static IRect TranslateAndScale2Rect(double tx, double ty, double tz, double sx, double sy, double sz)
        {
            IRect rect = new CRect();
            rect.Pt1.X = tx - sx / 2;
            rect.Pt1.Y = ty - sy / 2;
            rect.Pt1.Z = tz - sz / 2;
            rect.Pt2.X = tx + sx / 2;
            rect.Pt2.Y = ty + sy / 2;
            rect.Pt2.Z = tz + sz / 2;
            return rect;
        }

        //Convert an IRect to an IElement
        public static IElement RectToElement(IRect rect)
        {
            IElement element = new CElement();

            element.Properties.CopyFrom(rect.Properties);

            double tx, ty, tz, sx, sy, sz;
            Rect2TranslateAndScale(rect, out tx, out ty, out tz, out sx, out sy, out sz);
            element.Transform.Translation.X = tx;
            element.Transform.Translation.Y = ty;
            element.Transform.Translation.Z = tz;

            element.Transform.Scale.X = sx;
            element.Transform.Scale.Y = sy;
            element.Transform.Scale.Z = sz;

            return element;
        }
        
        //Convert an IElement to an IRect
        public static IRect ElementToRect(IElement element)
        {
            IRect rect = TranslateAndScale2Rect(
                element.Transform.Translation.X,
                element.Transform.Translation.Y,
                element.Transform.Translation.Z,
                element.Transform.Scale.X,
                element.Transform.Scale.Y,
                element.Transform.Scale.Z);

            rect.Properties.CopyFrom(element.Properties);
            
            return rect;
        }

        //Convert an IRectList to an IScene
        public static IScene RectsToScene(IRectList rects)
        {
            IScene scene = new CScene();

            foreach (IRect rect in rects)
            {
                IElement element = RectToElement(rect);
                scene.AddElement(element);
            }
            return scene;
        }

        //Convert an IScene to an IRectList
        public static IRectList SceneToRects(IScene scene)
        {
            IRectList rects = new CRectList();

            foreach (IElement element in scene)
            {
                IRect rect = ElementToRect(element);
                rects.AddRect(rect);
            }
            return rects;
        }
    }
}
