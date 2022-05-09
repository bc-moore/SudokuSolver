using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    /// <summary>
    /// Data Context for the SolverWindow
    /// </summary>
    public class SolverViewModel : BindableBase
    {
        /// <summary>
        /// Initializes the commands
        /// </summary>
        public SolverViewModel()
        {
            _BrowseCommand = new DelegateCommand(() => BrowseCallback());
            _SolveCommand = new DelegateCommand(() => SolveCallback());
        }

        /// <summary>
        /// Name of the file that contains the sudoku puzzle to solve
        /// </summary>
        public string FileName 
        {
            get { return _FileName; }
            set
            {
                if (value != _FileName)
                {
                    _FileName = value;
                    RaisePropertyChanged();
                }
            }
        }
        private string _FileName;

        /// <summary>
        /// Text summarizing the status of the solve state
        /// </summary>
        public string StatusText
        {
            get { return _StatusText; }
            set { _StatusText = value; RaisePropertyChanged(); }
        }
        private string _StatusText;

        /// <summary>
        /// Command to bind to the window's button
        /// </summary>
        public DelegateCommand BrowseCommand
        {
            get { return _BrowseCommand; }
            set
            {
                if(value != _BrowseCommand)
                {
                    _BrowseCommand = value;
                    RaisePropertyChanged();
                }
            }
        }
        private DelegateCommand _BrowseCommand;

        /// <summary>
        /// Executes the file dialog when the browse button is clicked
        /// </summary>
        private void BrowseCallback()
        {
            Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = ".txt",
                Multiselect = false,
                Title = "Select a Sudoku File"
            };
            bool? selected = fileDialog.ShowDialog();
            if (selected.HasValue && selected.Value)
            {
                //calling public setters so the RaisePropertyChanged is called
                FileName = fileDialog.FileName;
                StatusText = "";
            }
        }

        /// <summary>
        /// Command to bind to the window's button
        /// </summary>
        public DelegateCommand SolveCommand
        {
            get { return _SolveCommand; }
            set
            {
                if (value != _SolveCommand)
                {
                    _SolveCommand = value;
                    RaisePropertyChanged();
                }
            }
        }
        private DelegateCommand _SolveCommand;

        /// <summary>
        /// Executes the solve code when the solve button is clicked
        /// </summary>
        private void SolveCallback()
        {
            //calling public setters so the RaisePropertyChanged is called
            if (string.IsNullOrWhiteSpace(_FileName))
            {
                StatusText = "No Sudoku file provided to solve.";
                return;
            }

            try
            {
                SudokuSolver solver = new SudokuSolver(SudokuFileHandler.ReadSudokuFile(_FileName));
                if (solver.Solve())
                {
                    string outFileName = _FileName.Replace(".txt", ".sln.txt");
                    SudokuFileHandler.WriteSudokuFile(outFileName, solver.GetData());
                    StatusText = "Solution written to " + outFileName;
                }
                else
                {
                    StatusText = "Unable to solve the given Sudoku File.";
                }
            }
            catch (Exception ex)
            {
                StatusText = ex.Message;
            }

        }
    }
}
