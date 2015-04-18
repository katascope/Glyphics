#region Copyright
/*Copyright (c) 2015, Katascope
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/
#endregion
using System;
using System.Runtime.Serialization;

namespace GlyphicsLibrary.Language
{
    //Type of errors 
    public enum GlyphicsErrorType
    { 
        Ok = 0,
        DidNotTokenize,
        UnknownGlyph,
        WrongArgumentCount,
        NoCode,
        NoSuchShape
    };

    //Glyphics compilation exception class
    [Serializable]
    public class GlyphicsError : Exception
    {
        public GlyphicsErrorType Error { get; set; }
        readonly int _line;
        readonly string _code;
        readonly string _extended;

        //Default constructor
        public GlyphicsError()
        {            
        }

        //Default constructor
        public GlyphicsError(string errExtended)
        {
            _extended = errExtended;
        }

        //Default constructor
        public GlyphicsError(string errExtended, Exception ex)
        {
            if (ex != null)
                _extended = errExtended + ex.Message;
            else _extended = errExtended;
        }

        //Default constructor
        protected GlyphicsError(SerializationInfo info, StreamingContext context) : 
         base(info, context)
        {
            info.AddValue("Error ", GetString() );
        }

        //[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("info");

            info.AddValue("Text", GetString());
            base.GetObjectData(info, context);
        }

        //Assignment constructor : err and extended err
        public GlyphicsError(GlyphicsErrorType err, string errExtended)
        {
            Error = err;
            _extended = errExtended;
        }

        //Assignment constructor : err, extended err, and error code
        public GlyphicsError(GlyphicsErrorType err, int errLine, string errCode)
        {
            Error = err;
            _line = errLine;
            _code = errCode;
        }

        //Assignment constructor : err, error code line, extended err, and error code
        public GlyphicsError(GlyphicsErrorType err, int errLine, string errCode, string errExtended)
        {
            Error = err;
            _line = errLine;
            _code = errCode;
            _extended = errExtended;
        }

        private string GetString()
        {
            if (_extended != null)
                return Error + " " + _extended + " at line " + _line + " Code=[" + _code + "]";
            return Error + " at line " + _line + " Code=[" + _code + "]";
        }

        //Readable description
        public override string ToString()
        {
            return GetString();
        }
    }
}
