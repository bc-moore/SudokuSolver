using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    /// <summary>
    /// Class to hold the sudoku puzzle data and solve for the solution
    /// </summary>
    public class SudokuSolver
    {

        private int[,] _Data;

        /// <summary>
        /// Initializes the solver with the base puzzle
        /// </summary>
        /// <param name="data"></param>
        public SudokuSolver(int[,] data)
        {
            _Data = data;
        }

        /// <summary>
        /// Accessor to get the puzzle/solution data
        /// </summary>
        /// <returns>the data held by the solver</returns>
        public int[,] GetData()
        {
            return _Data;
        }

        /// <summary>
        /// Calls the solve code
        /// </summary>
        /// <returns>True if the board is solvable, else false</returns>
        public bool Solve()
        {
            try
            {
                return SolveSudokuBoard();
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Private recursive method to solve the board by trying different values until it comes out true.
        /// </summary>
        /// <returns>True if the board is solvable, else false</returns>
        private bool SolveSudokuBoard()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (_Data[i, j] == 0)
                    {
                        for (int val = 1; val <= 9; val++)
                        {
                            if (CheckValue(i, j, val))
                            {
                                _Data[i, j] = val;
                                if (SolveSudokuBoard())
                                    return true;
                                else
                                    _Data[i, j] = 0; //reset the value since the tested couldn't solve the board
                            }
                        }
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Checks a value for a row/col pairing to see if it is available to use.
        /// </summary>
        /// <param name="row">Row index for value</param>
        /// <param name="col">Column index for value</param>
        /// <param name="val">Value to check</param>
        /// <returns>True if the value fits in the row/col and block, else false</returns>
        private bool CheckValue(int row, int col, int val)
        {
            //lines 0-2 and columns 0-2 are in block 1
            //lines 0-2 and columns 3-5 are in block 2
            //lines 0-2 and columns 6-8 are in block 3
            //lines 3-5 and columns 0-2 are in block 4
            //lines 3-5 and columns 3-5 are in block 5
            //lines 3-5 and columns 6-8 are in block 6
            //lines 6-8 and columns 0-2 are in block 7
            //lines 6-8 and columns 3-5 are in block 8
            //lines 6-8 and columns 6-8 are in block 9
            for (int i = 0; i < 9; i++)
            {
                if (_Data[i, col] != 0 && _Data[i, col] == val)
                    return false;
                if (_Data[row, i] != 0 && _Data[row, i] == val)
                    return false;
                int block_idx_row = 3 * (row / 3) + i / 3;
                int block_idx_col = 3 * (col / 3) + i % 3;
                if (_Data[block_idx_row, block_idx_col] != 0 && _Data[block_idx_row, block_idx_col] == val)
                    return false;
            }
            return true;
        }
    }
}
