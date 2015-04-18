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

namespace GlyphicsLibrary.Language
{
    //Implementation of ICodeList, see for usage
    internal class CCodeList : ICodeList
    {
        //Actual list of ICode
        private readonly List<ICode> _codes;

        //Default constructor
        public CCodeList()
        {
            _codes = new List<ICode>();
        }

        //Number of ICode in list
        public int Count { get { return _codes.Count; } }

        //Get ICode from list, null if out of range
        public ICode GetCode(int id)
        {
            if (id < 0 || id > Count) return null;
            return _codes[id];
        }

        //Add ICode to list
        public void AddCode(ICode code)
        {
            _codes.Add(code);
        }

        //Make enumerable instead
        #region Implementation of IEnumerable
        public IEnumerator<ICode> GetEnumerator()
        {
            return _codes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}
