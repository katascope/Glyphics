#region Copyright
/*Copyright (c) 2015, Katascope
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/
#endregion

namespace GlyphicsLibrary 
{
    //Cell properties, likely of an IGrid unit
    public interface ICellProperties
    {
        //8-16-24-32-bit value RGBA
        ulong Rgba { get; set; }

        //Which line of Glyphics code drew this cell
        int TrackId { get; set; }

        //Properties of the cell
        int TextureId { get; set; }
        int ShapeId { get; set; }
        int AnimateId { get; set; }
        int PhysicsId { get; set; }

        //Grand unified value of texture/shape/animation/physics
        ulong UnifiedValue { get; set; }

        //Calculate the unified values for the property
        ulong CalcUnified();

        //Check to compare if another cell matches this cell
        bool CompareTo(ICellProperties asCell);

        //Revert cell to identity values
        void Identity();

        //Absorb cell values from src
        void CopyFrom(ICellProperties src);

        //Interpolate propsA to propsB by mux (0 to 1)
        void Lerp(double mux, ICellProperties propsA, ICellProperties propsB);

        //Duplicate object
        ICellProperties Clone();
    }
}
