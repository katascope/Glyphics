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
    //Implementation of IByteGridContext, see for usage
    internal class CByteGridContext : IByteGridContext
    {
        //Actual values
        public IGrid Grid { get; set; }
        public IPen Pen { get; set; }
        public List<IGrid> Palettes { get; set; }
        public IDouble3 SpawnPoint { get; set; }

        //Return IGrid if within range, otherwise null
        public IGrid GetPalette(int pal) { return (pal < Palettes.Count) ? Palettes[pal] : null; }

        //Assignment constructor only
        public CByteGridContext(IGrid newGrid)
        {
            Grid = newGrid;
            Pen = new CPen();
            Palettes = new List<IGrid>();
            SpawnPoint = new CDouble3();
        }

        //Add a grid to the palette
        public void AddPalette(IGrid pal)
        {
            Palettes.Add(pal);
        }

        //Readable description
        public override string ToString()
        {
            return "(Grid:" + Grid + ")(Pen:" + Pen + "(Spawn:" + SpawnPoint + ")";
        }
    }
}
