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
    //Axis that a pen can twist on when drawing
    public enum PenTwist { XYaxis, YZaxis, XZaxis };

    //Pen properties class for when drawing
    //Does things like color, size, hatching, cell proprties
    public interface IPen
    {
        //Yes if size 1x1x1
        bool IsUnit { get; set; }

        //RGBA value
        ulong Rgba { get; set; }

        //Width/Height/Depth = size of pen
        int Width { get; set; }
        int Height { get; set; }
        int Depth { get; set; }

        //Scissor minimums
        int StartX { get; set; }
        int StartY { get; set; }
        int StartZ { get; set; }

        //Scissor maximums
        int StopX { get; set; }
        int StopY { get; set; }
        int StopZ { get; set; }

        //Hatching along x/y/z axis
        int HatchX { get; set; }
        int HatchY { get; set; }
        int HatchZ { get; set; }

        //Value to draw shape as
        byte ShapeByte { get; set; }
        //Value to draw texture as
        byte TextureByte { get; set; }
        //Value to draw physics as
        byte PhysicsByte { get; set; }
        //Value to draw animation as
        byte AnimByte { get; set; }

        //Set overall size xyz = whd
        void SetSize(int w, int h, int d);

        //Set overall hatching xyz
        void SetHatch(int x, int y, int z);

        //Set color to draw 1BPP = 8bit
        void SetColor(byte val);
        //Set color to draw 2BPP = 16bit
        void SetColor(byte val1, byte val2);
        //Set color to draw 3BPP = 24bit
        void SetColor(byte r, byte g, byte b);
        //Set color to draw 4BPP = 32bit
        void SetColor(byte r, byte g, byte b, byte a);
        //Set color to draw 4BPP = 32 bit as one ulong
        void SetColor(ulong val);

        //Set the shape to draw
        void SetShape(byte val);
        //Set the texture to draw
        void SetTexture(byte val);
        //Set the animation to draw
        void SetAnimation(byte val);
        //Set the physics to draw
        void SetPhysics(byte val);

        //Calculate the borders of the pen, internally (to startx/y/z to stopx/y/z)
        void CalculateBorders();
    }
}
