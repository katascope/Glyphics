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
    //Implementation of ICellProperties, see for usage
    internal class CCellProperties : ICellProperties
    {
        //Properties
        public ulong Rgba { get; set; }
        public int TrackId { get; set; }
        public int TextureId { get; set; }
        public int ShapeId { get; set; }
        public int AnimateId { get; set; }
        public int PhysicsId { get; set; }
        public ulong UnifiedValue
        {
            get
            { 
                return CalcUnified(); 
            }
        }

        //Set properties to default values
        public void Identity()
        {
            Rgba = 0xFFFFFFFF;
            TrackId = 0;
            TextureId = 0;
            ShapeId = 0;
            AnimateId = 0;
            PhysicsId = 0;
        }

        //Copy properties from src to self
        public void CopyFrom(ICellProperties src)
        {
            if (src == null) return;

            Rgba = src.Rgba;
            TrackId = src.TrackId;
            TextureId = src.TextureId;
            ShapeId = src.ShapeId;
            AnimateId = src.AnimateId;
            PhysicsId = src.PhysicsId;
        }

        //Duplicate object
        public ICellProperties Clone()
        {
            ICellProperties cp = new CCellProperties();
            cp.CopyFrom(this);
            return cp;
        }

        //MathCompare if two properties are equal
        public bool IsEqualTo(ICellProperties asCell)
        {
            if (asCell == null) return false;

            return ((Rgba == asCell.Rgba)
                  && (TextureId == asCell.TextureId)
                  && (ShapeId == asCell.ShapeId)
                  && (AnimateId == asCell.AnimateId));
        }

        //Linearly interpolate between properties A and B, modulated by mux (0 to 1)
        public void Lerp(double mux, ICellProperties propsA, ICellProperties propsB)
        {
            if (propsA == null || propsB == null) return;
            Rgba      = Lerper.LerpRgba(mux, propsA.Rgba, propsB.Rgba);
            TextureId = Lerper.ThresholdAb(mux, propsA.TextureId, propsB.TextureId);
            ShapeId   = Lerper.ThresholdAb(mux, propsA.ShapeId, propsB.ShapeId);
            AnimateId = Lerper.ThresholdAb(mux, propsA.AnimateId, propsB.AnimateId);
            PhysicsId = Lerper.ThresholdAb(mux, propsA.PhysicsId, propsB.PhysicsId);
            CalcUnified();
        }

        //Return string describing properties
        public override string ToString()
        {
            byte r, g, b, a;
            Converter.Ulong2Rgba(Rgba, out r, out g, out b, out a);
            return "(Cell:" + r + "," + g + "," + b + "," + a + ":"
                + TextureId + ","
                + ShapeId + ","
                + AnimateId + ","
                + PhysicsId + ")";
        }

        //Calculate the unified value
        private ulong CalcUnified()
        {
            ulong UnifiedValue = Rgba;
            UnifiedValue |= (ulong)(ShapeId) << 32;
            UnifiedValue |= (ulong)(TextureId) << 40;
            UnifiedValue |= (ulong)(AnimateId) << 48;
            UnifiedValue |= (ulong)(PhysicsId) << 56;
            return UnifiedValue;
        }
    }
}

