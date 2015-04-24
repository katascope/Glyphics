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
    public interface ITriangles
    {
        //Model name
        string Name { get; set; }

        //Get the array of triangles
        ITriangle[] GetTriangleArray();

        //Set the array of triangles
        void SetTriangles(ITriangle[] triangles);

        //Get count of triangles in array
        int Count { get; }

        //Add an array of triangles to set
        void Add(ITriangles tris);

        //Duplicate object
        ITriangles Clone();

        //Readable description
        string ToString();

        //Gets the min/max range of triangles as a 3d rectangle
        IRect TrianglesBoundaries { get; }

        //Calculate normals for all triangles
        void CalcNormals();

        //Scales triangles to fit in a 1x1x1 cube
        void ReduceToUnit();

        //Transform:Translate to relative position
        void Translate(float x, float y, float z);

        //Transform:Rotate to relative angle in degrees (not radians)
        void RotateX(float angle);
        void RotateY(float angle);
        void RotateZ(float angle);

        //Transform:Scale a relative amount
        void Scale(float sx, float sy, float sz);

        //Transform:Flip X/Y/Z values, equivalent to scale(1,-1,-1), etc
        void MirrorX();
        void MirrorY();
        void MirrorZ();

        //Compare to another ITriangles
        bool IsEqualTo(ITriangles triangles);

        //For putting on ground (elevation zero, for easy printing)
        void PutOnGround();
    }
}

