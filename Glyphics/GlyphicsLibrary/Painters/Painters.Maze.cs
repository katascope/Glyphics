﻿#region Copyright
/*Copyright (c) 2015, Katascope
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/
#endregion
using System;
using System.Collections.Generic;

namespace GlyphicsLibrary.Painters
{
    //Taken from gist on github
    //https://gist.github.com/anonymous/828805

    [Flags]
    enum Directions
    {
        N = 1,
        S = 2,
        E = 4,
        W = 8
    }

    class MazeGrid
    {
        public const int WidthDimension = 0;
        public const int HeightDimension = 1;

        private static Random _rng;

        public int MinSize { get; private set; }
        public int MaxSize { get; private set; }
        public int[,] Cells { get; private set; }

        public MazeGrid(byte seed, int width, int height)
        {
            _rng = seed > 0 ? new Random(seed) : new Random();

            MinSize = 0;

            MaxSize = height > width ? height : width;

            Cells = Initialise(width, height);
            Cells = Generate();
        }

        private int[,] Initialise(int width, int height)
        {
            if (height < MinSize)
                height = MinSize;

            if (width < MinSize)
                width = MinSize;

            if (height > MaxSize)
                height = MaxSize;

            if (width > MaxSize)
                width = MaxSize;

            var cells = new int[width, height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    cells[x, y] = 0;
                }
            }

            return cells;
        }

        private readonly Dictionary<Directions, int> _directionX = new Dictionary<Directions, int>
        {
            {Directions.N, 0},
            {Directions.S, 0},
            {Directions.E, 1},
            {Directions.W, -1}
        };

        private readonly Dictionary<Directions, int> _directionY = new Dictionary<Directions, int>
        {
            {Directions.N, -1},
            {Directions.S, 1},
            {Directions.E, 0},
            {Directions.W, 0}
        };

        private readonly Dictionary<Directions, Directions> _opposite = new Dictionary<Directions, Directions>
        {
            {Directions.N, Directions.S},
            {Directions.S, Directions.N},
            {Directions.E, Directions.W},
            {Directions.W, Directions.E}
        };

        private int[,] Generate()
        {
            var cells = Cells;
            CarvePassagesFrom(0, 0, ref cells);
            return cells;
        }

        //Fisher-Yates shuffle
        public static void Shuffle<T>(ref List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = _rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        private static void Randomize(ref List<Directions> directions)
        {
            //Shuffle as many times as there are entries
            for (int i = 0; i < directions.Count; i++)
            {
                Shuffle(ref directions);
            }
        }

        private void CarvePassagesFrom(int currentX, int currentY, ref int[,] grid)
        {
            var directions = new List<Directions>
            {
                Directions.E,
                Directions.N,
                Directions.W,
                Directions.S
            };
            Randomize(ref directions);

            foreach (Directions direction in directions)
            {
                var nextX = currentX + _directionX[direction];
                var nextY = currentY + _directionY[direction];

                if (IsOutOfBounds(nextX, nextY, grid))
                    continue;

                if (grid[nextX, nextY] != 0) // has been visited
                    continue;

                grid[currentX, currentY] |= (int) direction;
                grid[nextX, nextY] |= (int) _opposite[direction];

                CarvePassagesFrom(nextX, nextY, ref grid);
            }
        }

        private static bool IsOutOfBounds(int x, int y, int[,] grid)
        {
            if (x < 0 || x > grid.GetLength(WidthDimension) - 1)
                return true;

            if (y < 0 || y > grid.GetLength(HeightDimension) - 1)
                return true;

            return false;
        }

        public static string ToString(int[,] cells)
        {
            var columns = cells.GetLength(WidthDimension);
            var rows = cells.GetLength(HeightDimension);

            string str = "";

            for (int y = 0; y < rows; y++)
            {
                str += " |";

                for (int x = 0; x < columns; x++)
                {
                    var directions = (Directions)cells[x, y];
                    
                    var s = directions.HasFlag(Directions.S) ? " " : "_";
                    str += s;

                    s = directions.HasFlag(Directions.E) ? " " : "|";
                    str += s;
                }
                str += "\n";
            }
            return str;
        }

        public static string RemapCells(string cellStr)
        {
            string returnStr = "";

            string[] strs = cellStr.Split('\n');

            for (int i=0;i<strs.Length-1;i++)
            {
                string str = strs[i];
                string nextStr = "";
                for (int c =1;c<str.Length;c++)
                {
                    if (str[c] == '_')
                    {
                        returnStr += " ";
                        nextStr += '#';
                    }
                    else
                    {
                        const char ch = '#';
                        if (str[c] == '|') 
                            returnStr += ch;
                        else returnStr += str[c];

                        if (str[c] != ' ')
                            nextStr += ch;
                        else nextStr += ' ';
                    }
                }

                if (i != 0) // skip first
                    returnStr += "\n" + nextStr;

                returnStr += "\n";
            }
            return returnStr;
        }
    }

    internal partial class CPainter
    {
        public void DrawMaze(IGridContext bgc, byte seed, int x1, int y, int z1, int x2, int z2)
        {
            if (bgc == null || bgc.Grid == null) return;
            IGrid grid = bgc.Grid;

            MinMax(ref x1, ref x2);
            MinMax(ref z1, ref z2);
            int width = x2 - x1;
            int depth = z2 - z1;

            //Scale down by two, to pickup walls
            var mg = new MazeGrid(seed, width/2, depth/2);

            string cells = MazeGrid.ToString(mg.Cells);
            string remappedCells = MazeGrid.RemapCells(cells);

            string[] remappedStrings = remappedCells.Split('\n');

            int cursorZ = 0;
            ulong rgba = bgc.Pen.Rgba;

            foreach (string str in remappedStrings)
            {
                for (int i=0;i<str.Length;i++)
                {
                    if (str[i] != ' ')
                        grid.Plot(x1 + i, y, z1 + cursorZ, rgba);
                }
                cursorZ++;
            }
        }
    }
}
