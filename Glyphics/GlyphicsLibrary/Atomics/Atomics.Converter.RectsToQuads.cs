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
    internal partial class Converter
    {
        //Convert rectangles to their 6 quads
        public static IQuadList RectsToQuads(IRectList rectSet)
        {
            IQuadList quadsMacro = new CQuadList();

            foreach (IRect rect in rectSet)
            {
                IQuadList quads = RectToQuads(rect);

                foreach (IQuad quad in quads)
                    quadsMacro.AddQuad(quad);
            }

            //Remove redundant ones automatically
            RemoveRedundantQuads(quadsMacro);

            return quadsMacro;
        }

        //Remove redundant quads to reduce total count
        public static int RemoveRedundantQuads(IQuadList quads)
        {
            int removedCount = 0;

            for (int me = 0; me < quads.Count; me++)
            {
                for (int you = me; you < quads.Count; you++)
                {
                    if (me != you)
                    {
                        IQuad meQuad = quads.GetQuad(me);
                        IQuad youQuad = quads.GetQuad(you);
                        //I saw a duplicate
                        if ((meQuad.Pt1.IsEqualTo(youQuad.Pt1))
                              && (meQuad.Pt2.IsEqualTo(youQuad.Pt2)))
                        {
                            //we have a redundancy
                            removedCount++;
                            quads.RemoveQuad(youQuad);
                        }
                    }
                }
            }
            return removedCount;
        }

        //Convert rectangle to its 6 quads
        public static IQuadList RectToQuads(IRect rect)
        {
            IQuadList quads = new CQuadList();

            IQuad quadTopper = new CQuad(rect.Pt1.X, rect.Pt2.Y, rect.Pt1.Z, rect.Pt2.X, rect.Pt2.Y, rect.Pt2.Z);
            quadTopper.Properties = rect.Properties.Clone();
            IQuad quadBottom = new CQuad(rect.Pt1.X, rect.Pt1.Y, rect.Pt1.Z, rect.Pt2.X, rect.Pt1.Y, rect.Pt2.Z);
            quadBottom.Properties = rect.Properties.Clone();

            IQuad quadFront = new CQuad(rect.Pt1.X, rect.Pt1.Y, rect.Pt2.Z, rect.Pt2.X, rect.Pt2.Y, rect.Pt2.Z);
            quadFront.Properties = rect.Properties.Clone();
            IQuad quadBack = new CQuad(rect.Pt1.X, rect.Pt1.Y, rect.Pt1.Z, rect.Pt2.X, rect.Pt2.Y, rect.Pt1.Z);
            quadBack.Properties = rect.Properties.Clone();

            IQuad quadRight = new CQuad(rect.Pt2.X, rect.Pt1.Y, rect.Pt1.Z, rect.Pt2.X, rect.Pt2.Y, rect.Pt2.Z);
            quadRight.Properties = rect.Properties.Clone();
            IQuad quadLeft = new CQuad(rect.Pt1.X, rect.Pt1.Y, rect.Pt1.Z, rect.Pt1.X, rect.Pt2.Y, rect.Pt2.Z);
            quadLeft.Properties = rect.Properties.Clone();

            quads.AddQuad(quadFront);
            quads.AddQuad(quadBack);
            quads.AddQuad(quadTopper);
            quads.AddQuad(quadBottom);
            quads.AddQuad(quadRight);
            quads.AddQuad(quadLeft);

            return quads;
        }
    }
}
