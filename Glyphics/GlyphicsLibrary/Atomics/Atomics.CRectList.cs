#region Copyright
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
    //Implementation of IRectList, see for usage
    internal class CRectList : IRectList
    {
        //Dimensions X/Y/Z
        public int SizeX { get; set; }
        public int SizeY { get; set; }
        public int SizeZ { get; set; }

        //Actual list of IRect
        private List<IRect> _rects { get; set; }

        //Constructor
        public CRectList()
        {
            _rects = new List<IRect>();
        }

        //Number of rectangles in list
        public int Count { get { return _rects.Count; } }

        //Add a rectangle to the list
        public void AddRect(IRect rect)
        {
            _rects.Add(rect);
        }

        //Get a rectangle from the list
        public IRect GetRect(int id)
        {
            if (id < 0 || id > Count) return null;
            return _rects[id];
        }

        //True if same
        public bool CompareTo(IRectList rects)
        {
            if (rects.Count != Count)
                return false;

            for (int i=0;i<Count;i++)
            {
                IRect rect1 = rects.GetRect(i);
                IRect rect2 = _rects[i];

                if (rect1.CompareTo(rect2) == false)
                    return false;
            }
            return true;
        }

        //Readable description
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (IRect rect in _rects)
            {
                sb.Append(rect + "\n");
            }
            return sb.ToString();
        }

        //Make enumerable instead
        #region Implementation of IEnumerable
        public IEnumerator<IRect> GetEnumerator()
        {
            return _rects.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}
