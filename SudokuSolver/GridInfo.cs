using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuSolver
{
    public class GridInfo
    {
        public int[][] grid;
        public int row;
        public int column;     

        public GridInfo(int[][] grid, int v1, int v2)
        {
            this.grid = grid;
            row = v1;
            column = v2;
        }
    }
}
