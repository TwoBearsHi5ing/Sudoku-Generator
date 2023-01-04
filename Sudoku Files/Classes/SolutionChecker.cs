using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Generator
{
    internal class SolutionChecker : ISolutionChecker
    {
       

        private readonly IBoardSolver _solver;

        public SolutionChecker(IBoardSolver solver)
        {
            _solver = solver;        
        }

        // returns true if number of solutions = 1
        public bool CheckNumberOfSolutions(int[,] board, List<int> allowedNumbers)
        {
            int numberOfTries = 0;
            int[,] firstSolution = new int[9, 9];
            int[,] secondSolution = new int[9, 9];
            int[,] thirdSolution = new int[9, 9];
            int[,] originalBoard = new int[9, 9];

            var ascendingList = allowedNumbers.OrderBy(x => x).ToList();
            var DescendingList = allowedNumbers.OrderByDescending(x => x).ToList();

            Array.Copy(board, originalBoard, board.Length);
            while (numberOfTries < 3)
            {
                Array.Copy(originalBoard, board, board.Length);

                if (numberOfTries == 0)
                {
                    _solver.SolveSudoku(board, allowedNumbers);
                    Array.Copy(board, firstSolution, board.Length);
                }

                else if (numberOfTries == 1)
                {
                    _solver.SolveSudoku(board, ascendingList);
                    Array.Copy(board, secondSolution, board.Length);
                }

                else if (numberOfTries > 1)
                {
                    _solver.SolveSudoku(board, DescendingList);
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
