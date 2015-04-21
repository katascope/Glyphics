﻿#region Copyright
/*Copyright (c) 2015, Katascope
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/
#endregion
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace GlyphicsLibrary.Atomics
{
    //Implementation of IQuadList, see for usage
    internal class CQuadList : IQuadList
    {
        //Actual list of IQuad
        private List<IQuad> Quads { get; set; }

        //Constructor
        public CQuadList()
        {
            Quads = new List<IQuad>();
        }

        //Number of rectangles in list
        public int Count { get { return Quads.Count; } }

        //Add quad to the list
        public void AddQuad(IQuad quad)
        {
            Quads.Add(quad);
        }

        //Get quad from the list
        public IQuad GetQuad(int id)
        {
            if (id < 0 || id > Count) return null;
            return Quads[id];
        }

        //True if same
        public bool IsEqualTo(IQuadList quads)
        {
            if (quads.Count != Count)
                return false;

            for (int i = 0; i < Count; i++)
            {
                IQuad quad1 = quads.GetQuad(i);
                IQuad quad2 = Quads[i];

                if (quad1.IsEqualTo(quad2) == false)
                    return false;
            }
            return true;
        }

        //Readable description
        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append(Quads.Count + "\n");
            foreach (IQuad quad in Quads)
            {
                sb.Append(quad + "\n");
            }
            return sb.ToString();
        }

        //Remove a quad 
        public void RemoveQuad(IQuad quad)
        {
            Quads.Remove(quad);
        }

        //Make enumerable instead
        #region Implementation of IEnumerable
        public IEnumerator<IQuad> GetEnumerator()
        {
            return Quads.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}