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
                for (var j= 0; j<9; j++)
                {
                    lastGuess[i][j] = 0;
                }                                        
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
                    if (failed)
                    {                           
                        var gridInfo = gridHistory.Pop();
                        grid = gridInfo.grid;
                        i = gridInfo.row;
                        j = gridInfo.column - 1;
                    }
                    if(grid[i][j] != 0)
                    {
                        continue;
                    }
                    for (var guess = lastGuess[i][j]+1; guess < 9; guess++)
                    {
                        if(CheckIfNumberIsValid(i,j,grid, guess))
                        {    
                            grid[i][j] = guess;
                            gridHistory.Push(new GridInfo(grid,i, j));
                            continue;
                        }
                    }
                    failed = true;
                }
            }
            throw new NotImplementedException();         
        }

        public static bool CheckIfNumberIsValid(int row, int col, int[][] grid, int number)
        {
            //foreach (in)
            throw new NotImplementedException();
        }
    }
}
