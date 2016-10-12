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
                for (var j = 0; j < 9; j++)
                {
                    //System.Threading.Thread.Sleep(100);
                    SudokuWriter(grid);

                    bool flag = true;
                    
                    if (grid[i][j] != 0)
                    {
                        continue;
                    }
                    for (var guess = lastGuess[i][j] + 1; guess < 10; guess++)
                    {                        
                        if (CheckIfNumberIsValid(i, j, grid, guess))
                        {
                            var currGrid = DeepCopy(grid);
                            gridHistory.Push(new GridInfo(currGrid, i, j));
                            grid[i][j] = guess;
                            lastGuess[i][j] = guess;
                            flag = false;
                            break;
                        }
                    }

                    if (flag)
                    {
                        lastGuess[i][j] = 0;
                        var gridInfo = gridHistory.Pop();
                        grid = gridInfo.grid;
                        i = gridInfo.row;
                        j = gridInfo.column - 1;
                        if (j < 0)
                        {
                            i = gridInfo.row - 1;
                            j = 8;
                        }
                    }
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

        private static int[][] DeepCopy(int[][] grid)
        {
            var currGrid = new int[9][];
            for (var i =0; i<9; i++)
            {
                var row = new int[9];
                for (var j= 0; j<9; j++)
                {
                    row[j] = grid[i][j];
                }
                currGrid[i] = row;
            }
            return currGrid;
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

        public static void SudokuWriter(int[][] grid)
        {
            Console.WriteLine("\n");
            foreach (var i in grid)
            {
                foreach (var j in i)
                {
                    Console.Write(j);
                }
                Console.WriteLine();
            }
        }
    
    }
}
