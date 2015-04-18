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
    //Implementation of ITriangle, see interface for general usage
    internal class CTriangle : ITriangle
    {
        //Actual vertex information
        public IFloat3 Normal { get; private set; }

        //Three points
        public IFloat3 Vertex1 { get; private set; }
        public IFloat3 Vertex2 { get; private set; }
        public IFloat3 Vertex3 { get; private set; }

        //Vectors
        public IFloat2 TexCoords1 { get; private set; }
        public IFloat2 TexCoords2 { get; private set; }
        public IFloat2 TexCoords3 { get; private set; }

        //Basic constructor
        public CTriangle()
        {
            Normal = new CFloat3();
            Vertex1 = new CFloat3();
            Vertex2 = new CFloat3();
            Vertex3 = new CFloat3();

            TexCoords1 = new CFloat2();
            TexCoords2 = new CFloat2();
            TexCoords3 = new CFloat2();
        }

        //Assignment constructor
        public CTriangle(float x1, float y1, float z1, float x2, float y2, float z2, float x3, float y3, float z3)
        {
            Normal = new CFloat3();
            Vertex1 = new CFloat3();
            Vertex2 = new CFloat3();
            Vertex3 = new CFloat3();

            TexCoords1 = new CFloat2();
            TexCoords2 = new CFloat2();
            TexCoords3 = new CFloat2();

            SetTriangle(x1, y1, z1, x2, y2, z2, x3, y3, z3);
        }

        //Basic assignment
        public void SetTriangle(float x1, float y1, float z1, float x2, float y2, float z2, float x3, float y3, float z3)
        {
            Vertex1.X = x1;
            Vertex1.Y = y1;
            Vertex1.Z = z1;

            Vertex2.X = x2;
            Vertex2.Y = y2;
            Vertex2.Z = z2;

            Vertex3.X = x3;
            Vertex3.Y = y3;
            Vertex3.Z = z3;
        }

        //Full assignment 
        public void SetTriangle(float x1, float y1, float z1, float x2, float y2, float z2, float x3, float y3, float z3,
            float t1X, float t1Y, float t2X, float t2Y, float t3X, float t3Y)
        {
            SetTriangle(x1,y1,z1,x2,y2,z2,x3,y3,z3);
        }

        //Calculate the normal of a triangle. It's "useful"
        public void CalcNormal()    			    // Calculates Normal For A Quad Using 3 Points
        {
            var v1 = new float[3];
            var v2 = new float[3];				// Vector 1 (x,y,z) & Vector 2 (x,y,z)
            const int x = 0;						// Define X Coord
            const int y = 1;						// Define Y Coord
            const int z = 2;						// Define Z Coord

            // Finds The Vector Between 2 Points By Subtracting
            // The x,y,z Coordinates From One Point To Another.

            // Calculate The Vector From Point 1 To Point 0
            v1[x] = Vertex1.X - Vertex2.X;					// Vector 1.x=Vertex[0].x-Vertex[1].x
            v1[y] = Vertex1.Y - Vertex2.Y;					// Vector 1.y=Vertex[0].y-Vertex[1].y
            v1[z] = Vertex1.Z - Vertex2.Z;					// Vector 1.z=Vertex[0].y-Vertex[1].z
            // Calculate The Vector From Point 2 To Point 1
            v2[x] = Vertex2.X - Vertex3.X;					// Vector 2.x=Vertex[0].x-Vertex[1].x
            v2[y] = Vertex2.Y - Vertex3.Y;					// Vector 2.y=Vertex[0].y-Vertex[1].y
            v2[z] = Vertex2.Z - Vertex3.Z;					// Vector 2.z=Vertex[0].z-Vertex[1].z
            // Compute The Cross Product To Give Us A Surface Normal
            Normal.X = v1[y] * v2[z] - v1[z] * v2[y];				// Cross Product For Y - Z
            Normal.Y = v1[z] * v2[x] - v1[x] * v2[z];				// Cross Product For X - Z
            Normal.Z = v1[x] * v2[y] - v1[y] * v2[x];				// Cross Product For X - Y

            ReduceToUnit();						    // Normalize The Vectors
        }

        //Reduce a normal vector to a unit vector
        public void ReduceToUnit()                 // Reduces A Normal Vector (3 Coordinates)
        {                                           // To A Unit Normal Vector With A Length Of One.
            // Holds Unit Length
            var length = (float)Math.Sqrt((Normal.X * Normal.X) + (Normal.Y * Normal.Y) + (Normal.Z * Normal.Z));

            if ((length <= 0.01f)&&(length >= 0))                      // Prevents Divide By 0 Error By Providing
                length = 1.00f;                      // An Acceptable Value For Vectors To Close To 0.

            Normal.X /= length;                   // Dividing Each Element By
            Normal.Y /= length;                   // The Length Results In A
            Normal.Z /= length;                   // Unit Normal Vector.
        }

        //Transfomr:Translate - relative not absolute
        public void Translate(float tx, float ty, float tz)
        {
            Vertex1.X += tx; Vertex1.Y += ty; Vertex1.Z += tz;
            Vertex2.X += tx; Vertex2.Y += ty; Vertex2.Z += tz;
            Vertex3.X += tx; Vertex3.Y += ty; Vertex3.Z += tz;
            CalcNormal();
        }

        //Transform:RotateX in degrees not radians
        public void RotateX(float angle)
        {
            float y1 = Vertex1.Y;
            float z1 = Vertex1.Z;
            float y2 = Vertex2.Y;
            float z2 = Vertex2.Z;
            float y3 = Vertex3.Y;
            float z3 = Vertex3.Z;
            Trigonometry.RotateX(angle, ref y1, ref z1);
            Trigonometry.RotateX(angle, ref y2, ref z2);
            Trigonometry.RotateX(angle, ref y3, ref z3);
            CalcNormal();

            Vertex1.Y = y1; Vertex1.Z = z1;
            Vertex2.Y = y2; Vertex2.Z = z2;
            Vertex3.Y = y3; Vertex3.Z = z3;
        }

        //Transform:RotateY in degrees not radians
        public void RotateY(float angle)
        {
            float x1 = Vertex1.X;
            float z1 = Vertex1.Z;
            float x2 = Vertex2.X;
            float z2 = Vertex2.Z;
            float x3 = Vertex3.X;
            float z3 = Vertex3.Z;
            Trigonometry.RotateY(angle, ref x1, ref z1);
            Trigonometry.RotateY(angle, ref x2, ref z2);
            Trigonometry.RotateY(angle, ref x3, ref z3);
            CalcNormal();
            Vertex1.X = x1; Vertex1.Z = z1;
            Vertex2.X = x2; Vertex2.Z = z2;
            Vertex3.X = x3; Vertex3.Z = z3;
        }

        //Transform:RotateZ in degrees not radians
        public void RotateZ(float angle)
        {
            float x1 = Vertex1.X;
            float y1 = Vertex1.Y;
            float x2 = Vertex2.X;
            float y2 = Vertex2.Y;
            float x3 = Vertex3.X;
            float y3 = Vertex3.Y;
            Trigonometry.RotateZ(angle, ref x1, ref y1);
            Trigonometry.RotateZ(angle, ref x2, ref y2);
            Trigonometry.RotateZ(angle, ref x3, ref y3);
            CalcNormal();
            Vertex1.X = x1; Vertex1.Y = y1; 
            Vertex2.X = x2; Vertex2.Y = y2; 
            Vertex3.X = x3; Vertex3.Y = y3; 
        }

        public void Scale(float sx, float sy, float sz)
        {
            Vertex1.X *= sx; Vertex1.Y *= sy; Vertex1.Z *= sz;
            Vertex2.X *= sx; Vertex2.Y *= sy; Vertex2.Z *= sz;
            Vertex3.X *= sx; Vertex3.Y *= sy; Vertex3.Z *= sz;
            CalcNormal();
        }

        //Mirror X, equivalent to scale(-1,1,1)
        public void MirrorX()
        {
            Vertex1.X = 1 - Vertex1.X;
            Vertex2.X = 1 - Vertex2.X;
            Vertex3.X = 1 - Vertex3.X;
            CalcNormal();
        }

        //Mirror Y, equivalent to scale(1,-1,1)
        public void MirrorY()
        {
            Vertex1.Y *= -1;
            Vertex2.Y *= -1;
            Vertex3.Y *= -1;
            CalcNormal();
        }

        //Mirror Z, equivalent to scale(1,1,-1)
        public void MirrorZ()
        {
            Vertex1.Z *= -1;
            Vertex2.Z *= -1;
            Vertex3.Z *= -1;
            CalcNormal();
        }

        //Nothing fancy, create a copy and return it
        public ITriangle Clone()
        {
            ITriangle triangle2 = new CTriangle();
            
            //Use easy call
            triangle2.SetTriangle(
                Vertex1.X,Vertex1.Y,Vertex1.Z,
                Vertex2.X,Vertex2.Y,Vertex2.Z,
                Vertex3.X,Vertex3.Y,Vertex3.Z,
                TexCoords1.X,TexCoords1.Y,
                TexCoords2.X,TexCoords2.Y,
                TexCoords3.X,TexCoords3.Y);
            
            //GetToken the normal too
            triangle2.Normal.X = Normal.X; 
            triangle2.Normal.Y = Normal.Y; 
            triangle2.Normal.Z = Normal.Z;
            return triangle2;
        }
    }
}
