using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Sudoku_Generator
{
    internal class DigitsRemover : IDigitsRemover
    {
        private readonly ISolutionChecker _solutionChecker;

        public DigitsRemover(ISolutionChecker solutionChecker)
        {
            _solutionChecker = solutionChecker;
        }

        #region Removing Digits To Ready The Board
        public bool RemovingDigits(int[,] board, int[,] originalSolution, int emptySpaces, float maxTimeToRemove, List<int> allowedNumbers)
        {
            int[,] backup = new int[9, 9];

            Array.Copy(originalSolution, board, originalSolution.Length);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            while (!NumberOfEmptySpaces(board, emptySpaces))
            {
                if (stopwatch.ElapsedMilliseconds > maxTimeToRemove)
                {
                    stopwatch.Stop();
                    Console.WriteLine("New Try to empty the board");
                    return false;
                }

                Array.Copy(board, backup, board.Length);

                int i, j;
                int number;
                i = Shuffle.random.Next(0, 9);
                j = Shuffle.random.Next(0, 9);

                number = board[i, j];
                backup[i, j] = 0;

                if (_solutionChecker.CheckNumberOfSolutions(backup, allowedNumbers))
                {
                    board[i, j] = 0;
                }
                else
                {
                    backup[i, j] = number;
                }
            }

            stopwatch.Stop();
            return true;
        }
        private bool NumberOfEmptySpaces(int[,] board, int amount)
        {
            int k = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (board[i, j] == 0)
                    {
                        k++;
                    }
                }
            }
            if (k == amount)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}
