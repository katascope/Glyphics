﻿#region Copyright
/*Copyright (c) 2015, Katascope
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/
#endregion

namespace GlyphicsLibrary.Atomics
{
    //Implementation of IRect, see for usage
    internal class CRect : IRect
    {
        public IDouble3 Pt1 { get; set; }
        public IDouble3 Pt2 { get; set; }
        public ICellProperties Properties { get; set; }

        public double Width { get { return Pt2.X - Pt1.X; } }
        public double Height { get { return Pt2.Y - Pt1.Y; } }
        public double Depth { get { return Pt2.Z - Pt1.Z; } }

        //Default Constructor
        public CRect() { Set(0,0,0,0,0,0); }
        
        //Assignment constructor
        public CRect(double nx1, double ny1, double nz1, double nx2, double ny2, double nz2) { Set(nx1, ny1, nz1, nx2, ny2, nz2); }

        //Assignment constructor
        public CRect(double nx1, double ny1, double nz1, double nx2, double ny2, double nz2, ICellProperties newProperties)
        {
            Set(nx1, ny1, nz1, nx2, ny2, nz2);
            Properties.Rgba = newProperties.Rgba;
            Properties.ShapeId = newProperties.ShapeId;
            Properties.TextureId = newProperties.TextureId;
            Properties.AnimateId = newProperties.AnimateId;
            Properties.PhysicsId = newProperties.PhysicsId;
        }
        
        //Set dimensions of Rect
        private void Set(double nx1, double ny1, double nz1, double nx2, double ny2, double nz2)
        {
            Pt1 = new CDouble3();
            Pt2 = new CDouble3();
            Properties = new CCellProperties();
            Properties.ShapeId = 1;
            Properties.TextureId = 1;
            Pt1.X = nx1; Pt1.Y = ny1; Pt1.Z = nz1; Pt2.X = nx2; Pt2.Y = ny2; Pt2.Z = nz2;
        }

        //Reset to defaults
        public void Identity()
        {
            Pt1.Identity();
            Pt2.Identity();
            Properties.Identity();
        }

        //Copy properties and points from another IRect
        public void CopyFrom(IRect srcRect)
        {
            if (srcRect == null) return;

            Pt1.CopyFrom(srcRect.Pt1);
            Pt2.CopyFrom(srcRect.Pt2);
            Properties.CopyFrom(srcRect.Properties);
        }

        //Readable description
        public override string ToString()
        {
            double w = Pt2.X - Pt1.X;
            double h = Pt2.Y - Pt1.Y;
            double d = Pt2.Z - Pt1.Z;
            string dimStr = "(w=" + w + ",h=" + h + ",d=" + d + ")";
            return "[" + dimStr + Pt1 + "," + Pt2 + "/" + Properties + "]";
        }

        //True if x,y,z is in the rect
        public bool Contains(double x, double y, double z)
        {
            if ((x > Pt2.X) || (x < Pt1.X) || (y > Pt2.Y) || (y < Pt1.Y) || (z > Pt2.Z) || (z < Pt1.Z)) return false;
            return true;
        }

        //True if same
        public bool IsEqualTo(IRect rect)
        {
            if (rect == null)
                return false;

            if ( (rect.Pt1.IsEqualTo(Pt1) == false)
                || (rect.Pt2.IsEqualTo(Pt2) == false) )
            {
                return false;
            }

            return true;
        }
    }
}