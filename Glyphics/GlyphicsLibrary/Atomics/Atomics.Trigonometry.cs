#region Copyright
/*Copyright (c) 2015, Katascope
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/
#endregion
// Reference material: http://www.cs.helsinki.fi/group/goa/mallinnus/3dtransf/3drot.html

using System;

namespace GlyphicsLibrary.Atomics
{
    //Utility Trigonometry class
    internal static class Trigonometry
    {
        //Convert degrees to radians
        public static float DegreeToRadian(float angle)
        {
            return (float)Math.PI * angle / 180.0f;
        }
        //Convert degrees to radians
        public static double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }
        
        //Rotate YZ values around X-axis
        public static void RotateX(float angle, ref float y, ref float z)
        {
            float vy = y * (float)Math.Cos(DegreeToRadian(angle)) - z * (float)Math.Sin(DegreeToRadian(angle));
            float vz = y * (float)Math.Sin(DegreeToRadian(angle)) + z * (float)Math.Cos(DegreeToRadian(angle));
            y = vy;
            z = vz;
        }

        //Rotate XZ values around Y-axis
        public static void RotateY(float angle, ref float x, ref float z)
        {
            float vx = z * (float)Math.Cos(DegreeToRadian(angle)) - x * (float)Math.Sin(DegreeToRadian(angle));
            float vz = z * (float)Math.Sin(DegreeToRadian(angle)) + x * (float)Math.Cos(DegreeToRadian(angle));
            x = vx;
            z = vz;
        }

        //Rotate XY values around Z-axis
        public static void RotateZ(float angle, ref float x, ref float y)
        {
            float vx = x * (float)Math.Cos(DegreeToRadian(angle)) - y * (float)Math.Sin(DegreeToRadian(angle));
            float vy = x * (float)Math.Sin(DegreeToRadian(angle)) + y * (float)Math.Cos(DegreeToRadian(angle));
            x = vx;
            y = vy;
        }

        //Rotate YZ values around X-axis
        public static void RotateX(double angle, ref double y, ref double z)
        {
            double vy = y * Math.Cos(DegreeToRadian(angle)) - z * Math.Sin(DegreeToRadian(angle));
            double vz = y * Math.Sin(DegreeToRadian(angle)) + z * Math.Cos(DegreeToRadian(angle));
            y = vy;
            z = vz;
        }

        //Rotate XZ values around Y-axis
        public static void RotateY(double angle, ref double x, ref double z)
        {
            double vx = z * Math.Cos(DegreeToRadian(angle)) - x * Math.Sin(DegreeToRadian(angle));
            double vz = z * Math.Sin(DegreeToRadian(angle)) + x * Math.Cos(DegreeToRadian(angle));
            x = vx;
            z = vz;
        }

        //Rotate XY values around Z-axis
        public static void RotateZ(double angle, ref double x, ref double y)
        {
            double vx = x * Math.Cos(DegreeToRadian(angle)) - y * Math.Sin(DegreeToRadian(angle));
            double vy = x * Math.Sin(DegreeToRadian(angle)) + y * Math.Cos(DegreeToRadian(angle));
            x = vx;
            y = vy;
        }
    }
}
