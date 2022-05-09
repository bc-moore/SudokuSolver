# SudokuSolver
Solves Sudoku puzzles from text files

## Credits
Adapted from https://www.c-sharpcorner.com/blogs/sudoku-solver </br>
by Rithik Banerjee on 23 September 2020. 

## Dependencies
.NET Framework 4.6.1 </br>
Prism NuGet package https://www.nuget.org/packages/Prism.Core/ (Prism.dll)

## Test Files
Files are provided to demonstrate the acceptable input and output.

### File Format
The input Sudoku file format must contain 9 lines with 9 characters each. </br>
Unknown blocks must contain the 'X' character (captialized), while known blocks contain the value 1-9.

### Output
The solution file will be written to the same directory as the input file, with ".sln.txt" as the extension.
