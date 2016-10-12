using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuSolver
{
    class SudokuSolver
    {
        internal static int[][] Solve(string[] fileContent)
        {
            bool failed = false;
            int[][] lastGuess = new int[9][];
            for (var i =0; i<9; i++)
            {
                var row = new int[9];
                for (var j= 0; j<9; j++)
                {
                    row[j] = 0;
                }
                lastGuess[i] = row;                                      
            }

            Stack<GridInfo> gridHistory = new Stack<GridInfo>();
            var grid = new int[9][];

            for (var i = 0; i < 9; i++)
            {
                var row = new int[9];
                var numbers = fileContent[i];
                for (int j = 0; j < 9; j++)
                {
                    int number;
                    row[j] = int.TryParse(numbers[j].ToString(),out number) ? number : 0;
                }
                grid[i] = row;
            }                                           
            gridHistory.Push(new GridInfo(grid,0,0));

            for (var i = 0; i < 9; i++)
            {
                for (var j = 0; j < 9 && !failed; j++)
                {
                    System.Threading.Thread.Sleep(500);
                    Console.WriteLine(i + " " + j);
                    if (failed)
                    {
                        var gridInfo = gridHistory.Pop();
                        grid = gridInfo.grid;
                        i = gridInfo.row;
                        j = gridInfo.column - 1;
                        failed = false;
                    }
                    if (grid[i][j] != 0)
                    {
                        continue;
                    }
                    for (var guess = lastGuess[i][j] + 1; guess < 9; guess++)
                    {                        
                        if (CheckIfNumberIsValid(i, j, grid, guess))
                        {
                            grid[i][j] = guess;
                            gridHistory.Push(new GridInfo(grid, i, j));
                            lastGuess[i][j] = guess;
                            continue;
                        }
                    }
                    failed = true;
                }
            }

            if(CheckIfSudokuIsSolved(grid))
            {
                return grid;
            }
            else
            {
                return null;
            }
        }

        private static bool CheckIfSudokuIsSolved(int[][] grid)
        {
            foreach (var row in grid)
            {
                foreach(var num in row)
                {
                    if (num == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool CheckIfNumberIsValid(int row, int col, int[][] grid, int number)
        {
            for(var i = 0; i < 9; i++)
            {
                if (grid[row][i] == number || grid [i][col] == number)
                {
                    return false;
                }
            }

            var boxRow = row / 3;
            var boxCol = col / 3;

            for (var i = boxRow*3; i<(boxRow+1)*3; i++)
            {
                for (var j = boxCol*3; j<(boxCol+1)*3; j++)
                {
                    if (grid[i][j] == number)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
