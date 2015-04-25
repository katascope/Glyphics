#region Copyright
/*Copyright (c) 2015, Katascope
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/
#endregion
using System.Collections;
using System.Collections.Generic;
//using GlyphicsLibrary.Painters;

namespace GlyphicsLibrary.ByteGrid
{
    //Implementation of IGridList, see for usage
    internal class CGridList : IGridList
    {
        private readonly List<IGrid> _grids = new List<IGrid>();

        //Readable description
        public override string ToString()
        {
            return _grids.Count + " grids";
        }

        //Add a grid to library
        public void AddGrid(IGrid grid)
        {
            _grids.Add(grid);
        }

        //Return grid library entry
        public IGrid GetGrid(int id)
        {
            if (_grids.Count == 0) return null;
            if (id < 0 || id > _grids.Count) return null;
            return _grids[id];
        }

        //Make enumerable instead
        #region Implementation of IEnumerable
        public IEnumerator<IGrid> GetEnumerator()
        {
            return _grids.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}
