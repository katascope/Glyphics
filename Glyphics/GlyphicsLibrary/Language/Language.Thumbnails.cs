using System;
using GlyphicsLibrary;
using System.Threading.Tasks;

namespace GlyphicsLibrary.Language
{
    internal class Thumbnails
    {
        //Generates a set of thumbnails of the code, down to 1 pixels size
        public static IGridList CodeToThumbnailed(ICode code)
        {
            IGridList gridList = new ByteGrid.CGridList();
            IGrid grid = Converter.CodeToGrid(code.Code);

            int toX = grid.SizeX;
            int toY = grid.SizeY;
            int toZ = grid.SizeZ;

            while ((toX > 0) && (toY > 0) && (toZ > 0))
            {
                ICode newCode = Converter.CodeToRescaledCode(code, toX, toY, toZ);
                IGrid newGrid = Converter.CodeToGrid(newCode.Code);
                gridList.AddGrid(newGrid);
                toX /= 2;
                toY /= 2;
                toZ /= 2;
            }

            return gridList;
        }
    }
}
