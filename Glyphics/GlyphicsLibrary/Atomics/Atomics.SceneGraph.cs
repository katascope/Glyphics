using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlyphicsLibrary.Atomics
{
    //Utility for converting from/to elements, scenes
    internal class SceneGraph
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
