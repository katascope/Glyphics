using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlyphicsLibrary.ByteGrid;

namespace GlyphicsLibrary.Language
{
    internal partial class Converter
    {
        //Input: Glyphics code
        //Output: Rectangles
        //Side-Effects: May use deserialized cache at end of code instead
        public static IRectList CodeToRects(ICode glyphicsCode)
        {
            string code = glyphicsCode.Code;
            if (code.Contains("" + Atomics.Converter.CharRgba))
            {
                string start = code.Substring(code.IndexOf('*'));
                return Atomics.Converter.SerializedRectsToRects(new Atomics.CSerializedRects(start));
            }
            IRectList rectSet = ByteGrid.Converter.GridToRects(Executor.Execute(Converter.CodeToTokens(glyphicsCode)).Bgc.Grid);
            return rectSet;
        }
    }
}
