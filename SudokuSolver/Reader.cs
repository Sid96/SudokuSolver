using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;

namespace SudokuSolver
{
    class Reader
    {
        
        static void Main(string[] args)
        {
            var directory = ConfigurationManager.AppSettings["SudokuPath"];
            Console.WriteLine("Reading Sudoku files from path: {0}", directory);
            var files = Directory.GetFiles(directory, "*.txt");
            if (files.Count()==0)
            {
                Console.WriteLine("No Files Found");
            }
            foreach (var file in files)
            {
                Console.WriteLine(file);
                var fileContent = File.ReadAllLines(file);
                if (ValidateFile(fileContent))
                {
                    var solution = SudokuSolver.Solve(fileContent);
                    if (solution == null)
                    {
                        Console.WriteLine("No Solutions found");
                    }
                    else
                    {
                        foreach (var i in solution)
                        {
                            foreach(var j in i)
                            {
                                Console.Write(j);
                            }
                            Console.WriteLine();
                        }
                    }

                }
                else
                {
                    Console.WriteLine("The file {0} is not in a proper 3x3 sudoku format", file);
                }
            }
            Console.ReadKey();
        }

        static bool ValidateFile(string[] fileContent)
        {                                                       
            if (fileContent.Length == 9)
            {
                foreach (var line in fileContent)
                {
                    if (line.Length!= 9)
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
    }
}
