#region Copyright
/*Copyright (c) 2015, Katascope
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/
#endregion
using System.Collections.Generic;
using GlyphicsLibrary.Atomics;

namespace GlyphicsLibrary.ByteGrid
{
    // Generalized grid of bytes, supports 1byte/2byte/3bytes/RGBA values
    internal class CGrid : IGrid
    {
        //Actual size of Grid
        private readonly int _sizeX;
        private readonly int _sizeY;
        private readonly int _sizeZ;
        //Actual bits-per-pixel of grid
        private readonly int _bpp;
        //Actual total size in bytes of grid
        private readonly int _size;
        //Actual Size of one 2D grid in bytes
        private readonly int _size2D;
        //Actual Scissoring rect
        private IRect _scissor;
        //Actual Code track ID
        private byte _trackId;
        //Actual properties
        private ICellProperties[] _properties;
        //Actual boolean for tracking code lines to cells
        private bool _codeTrackingInhibit;

        //See IGrid for usage of these
        public int SizeX { get { return _sizeX; } }
        public int SizeY { get { return _sizeY; } }
        public int SizeZ { get { return _sizeZ; } }
        public int Bpp { get { return _bpp; } }
        public int Size { get { return _size; } }
        //public byte[] GetRawBytes() { return null; }
        public void SetTracker(byte tracker) { _trackId = tracker; }
        public void InhibitCodeTracking() { _codeTrackingInhibit = true; }
        public void AllowCodeTracking(){_codeTrackingInhibit = false;}
        public void SetScissor(IRect nScissor){_scissor = nScissor;}
        public void NoScissor(){_scissor = new CRect(0, 0, 0, _sizeX, _sizeY, _sizeZ);}

        //Assignment constructor
        public CGrid(int sx, int sy, int sz, int bpp)
        {
            _sizeX = sx; _sizeY = sy; _sizeZ = sz;
            _bpp = bpp;
            _size2D = _sizeX * _sizeY;
            if (_sizeZ > 0) _size = _sizeX * _sizeY * _sizeZ;
            else if (_sizeY > 0) _size = _sizeX * _sizeY;
            else _size = _sizeX;
            CreatePropertyStorage();
            _scissor = new CRect(0, 0, 0, _sizeX, _sizeY, _sizeZ);
        }

        //Duplicate Object
        public IGrid Clone()
        {
            var newGrid = new CGrid(_sizeX, _sizeY, _sizeZ, _bpp);
            newGrid._scissor = _scissor;
            newGrid.CopyFrom(this);
            return newGrid;
        }

        //Plot an rgba values straight to grid
        public void Plot(int x, int y, int z, ulong rgba)
        {
            if (!InRange(x, y, z)) return;
            long offsetTrack = (z * _size2D) + (y * _sizeX) + (x);
            _properties[offsetTrack].Rgba = rgba;
        }

        //Copy rgba values from another grid
        public void CopyFrom(IGrid src)
        {
            if (src == null) return;

            for (int z = 0; z < _sizeZ; z++)
                for (int y = 0; y < _sizeY; y++)
                    for (int x = 0; x < _sizeX; x++)
                        Plot(x, y, z, src.GetRgba(x, y, z));
        }

        //Check if xyz is in range of the grid's boundaries
        public bool InRange(int x, int y, int z)
        {
            if (x < 0 || y < 0 || z < 0 || x >= _sizeX || y >= _sizeY || z >= _sizeZ)
                return false;
            if (_scissor.Contains(x, y, z) == false)
                return false;
            return true;
        }

        //Get the rgba values at xyz
        public ulong GetRgba(int x, int y, int z)
        {
            if ((x < 0) || (y < 0) || (z < 0) || (x >= _sizeX) || (y >= _sizeY) || (z >= _sizeZ)) return 0;
            long offsetTrack = (z * _size2D) + (y * _sizeX) + (x);
            return _properties[offsetTrack].Rgba;
        }

        //Get the cell properties at xyz
        public ICellProperties GetProperty(int x, int y, int z)
        {
            if (x < 0 || y < 0 || z < 0 || x >= _sizeX || y >= _sizeY || z >= _sizeZ)
                return null;
            int offset = (z * _size2D) + (y * _sizeX) + (x);
            return _properties[offset];
        }

        //Plot a set of cell properties at xyz
        public void Plot(int x, int y, int z, ulong rgba, byte physics, byte shape, byte texture, byte anim)
        {
            if (!InRange(x, y, z)) return;
            long offsetTrack = (z * _size2D) + (y * _sizeX) + (x);
            _properties[offsetTrack].Rgba = rgba;
            if (_codeTrackingInhibit == false)
            {
                _properties[offsetTrack].TrackId = _trackId;
            }
            SetProperties(offsetTrack, texture, shape, anim, physics);
        }

        //Set the properties at a specific offset
        private void SetProperties(long offsetTrack, byte texture, byte shape, byte anim, byte physics)
        {
            _properties[offsetTrack].TextureId = texture;
            _properties[offsetTrack].ShapeId = shape;
            _properties[offsetTrack].AnimateId = anim;
            _properties[offsetTrack].PhysicsId = physics;
        }

        //Allocate storage for the properties
        private void CreatePropertyStorage()
        {
            _properties = new ICellProperties[_size];
            for (long i = 0; i < _size; i++)
                _properties[i] = new CCellProperties();
        }

        //Duplicate object
        public byte[] CloneData()
        {
            var bytes = new List<byte>();
            for (int i = 0; i < Size; i++)
            {
                if (_bpp == 4)
                {
                    byte r, g, b, a;
                    Pixel.Ulong2Rgba(_properties[i].Rgba, out r, out g, out b, out a);
                    bytes.Add(r); bytes.Add(g); bytes.Add(b); bytes.Add(a);
                }
                else if (_bpp == 3)
                {
                    byte r, g, b, a;
                    Pixel.Ulong2Rgba(_properties[i].Rgba, out r, out g, out b, out a);
                    bytes.Add(r); bytes.Add(g); bytes.Add(b);
                }
                else if (_bpp == 2)
                {
                    byte r, g, b, a;
                    Pixel.Ulong2Rgba(_properties[i].Rgba, out r, out g, out b, out a);
                    bytes.Add(r); bytes.Add(g);
                }
                else bytes.Add((byte)_properties[i].Rgba);
            }
            return bytes.ToArray();
        }

        //Compare grid to another, return true if identical
        public bool CompareTo(byte[] expectedResult)
        {
            if (expectedResult == null)
                return false;

            if (Size * _bpp != expectedResult.Length) 
                return false;

            byte[] data = CloneData();
            for (int i = 0; i < Size; i++)
            {
                if (data[i] != expectedResult[i])
                    return false;
            }
            return true;
        }

        //Return true if the raw bytes = expectedResult bytes
        public bool CompareTo(IGrid grid)
        {
            if ((grid.SizeX != SizeX)
                || (grid.SizeY != SizeY)
                || (grid.SizeZ != SizeZ)
                || (grid.Bpp != Bpp)
                || (grid.Size != Size))
                return false;

            for (int z = 0; z < grid.SizeZ;z++)
            {
                for (int y = 0; y < grid.SizeY; y++)
                {
                    for (int x = 0; x < grid.SizeX; x++)
                    {
                        ICellProperties v1 = grid.GetProperty(x, y, z);
                        ICellProperties v2 = GetProperty(x, y, z);
                        
                        if (v1.CompareTo(v2)== false)
                        {
                            return false;
                        }

                    }
                }
            }
            return true;
        }
    }
}

