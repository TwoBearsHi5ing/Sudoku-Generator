using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Generator
{
    internal class SolutionChecker : ISolutionChecker
    {
        private List<int> allowedDigits = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        private List<int> ReversedAllowedDigits = new List<int> { 9, 8, 7, 6, 5, 4, 3, 2, 1 };
        private List<int> RandomizedAllowedDigits = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        private readonly IBoardSolver _solver;

        public SolutionChecker(IBoardSolver solver)
        {
            _solver = solver;
            Shuffle.ShuffleList(RandomizedAllowedDigits);
        }

        public bool CheckNumberOfSolutions(int[,] board)
        {
            int numberOfTries = 0;
            int[,] firstSolution = new int[9, 9];
            int[,] secondSolution = new int[9, 9];
            int[,] thirdSolution = new int[9, 9];
            int[,] originalBoard = new int[9, 9];

            Array.Copy(board, originalBoard, board.Length);
            while (numberOfTries < 3)
            {
                Array.Copy(originalBoard, board, board.Length);

                if (numberOfTries == 0)
                {
                    _solver.SolveSudoku(board, RandomizedAllowedDigits);
                    Array.Copy(board, firstSolution, board.Length);
                }

                else if (numberOfTries == 1)
                {
                    _solver.SolveSudoku(board, allowedDigits);
                    Array.Copy(board, secondSolution, board.Length);
                }

                else if (numberOfTries > 1)
                {
                    _solver.SolveSudoku(board, ReversedAllowedDigits);
                    Array.Copy(board, thirdSolution, board.Length);
                }

                if (!CompareSolutions(firstSolution, secondSolution) && numberOfTries == 1)
                {
                    return false;
                }

                if (!CompareSolutions(firstSolution, thirdSolution) && numberOfTries == 2)
                {
                    return false;
                }

                numberOfTries++;
            }
            return true;

        }
        private bool CompareSolutions(int[,] originalBoard, int[,] newBoard)
        {
            return newBoard.Rank == originalBoard.Rank &&
                   Enumerable.Range(0, newBoard.Rank).All(dimension => newBoard.GetLength(dimension) == originalBoard.GetLength(dimension)) &&
                   newBoard.Cast<int>().SequenceEqual(originalBoard.Cast<int>());
        }
    }
}
