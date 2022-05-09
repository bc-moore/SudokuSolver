using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    /// <summary>
    /// 
    /// </summary>
    public class SudokuFileHandler
    {
        /// <summary>
        /// Reads the sudoku puzzle file and returns the data in it, replacing 'X' placeholders for 0s
        /// </summary>
        /// <param name="fileName">Puzzle file to read</param>
        /// <returns>Data in the file as a sudoku board</returns>
        /// <exception cref="Exception">If the file is null, doesn't exist, or failed to meet the format requirements.</exception>
        public static int[,] ReadSudokuFile(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new Exception("Sudoku file cannot be null.");
            if (!System.IO.File.Exists(fileName))
                throw new Exception("Sudoku file '" + fileName + "' does not exist.");
            int[,] fileData = new int[9, 9];
            System.IO.TextReader reader = new System.IO.StreamReader(fileName);

            try
            {
                string fileLine = reader.ReadLine();
                List<string> lines = new List<string>();
                while (fileLine != null)
                {
                    lines.Add(fileLine);
                    fileLine = reader.ReadLine();
                }
                if (lines.Count != 9)
                    throw new System.IO.FileFormatException("Sudoku file must have 9 lines.");
                for (int i = 0; i < 9; i++)
                {
                    string line = lines[i];
                    for (int j = 0; j < 9; j++)
                    {
                        if (line.Length != 9)
                            throw new System.IO.FileFormatException("Sudoku file must have 9 characters per line.");
                        char val = line[j];
                        if (val == 'X')
                            fileData[i, j] = 0;
                        else
                        {
                            string val_str = val.ToString();
                            bool? success = int.TryParse(val_str, out int value);
                            if (success.HasValue && success.Value && 1 <= value && value <= 9)
                                fileData[i, j] = value;
                            else
                                throw new System.IO.FileFormatException("Sudoku file has invalid characters. Must only contain X and integers between 1 and 9.");
                        }
                    }
                }
                return fileData;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to read the puzzle file.", ex);
            }
            finally
            {
                reader.Close();
            }
        }

        /// <summary>
        /// Writes the sudoku solution file
        /// </summary>
        /// <param name="fileName">Name of file to write the solution to</param>
        /// <param name="data">Solved puzzle data</param>
        public static void WriteSudokuFile(string fileName, int[,] data)
        {
            System.IO.TextWriter writer = new System.IO.StreamWriter(fileName);
            try
            {
                for (int i = 0; i < 9; i++)
                {
                    string line = "";
                    for (int j = 0; j < 9; j++)
                    {
                        line += data[i, j].ToString();
                    }
                    writer.WriteLine(line);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to write the solution file.", ex);
            }
            finally 
            { 
                writer.Close(); 
            }
        }
    }
}
