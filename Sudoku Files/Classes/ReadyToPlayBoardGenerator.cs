using System.Diagnostics;

namespace Sudoku_Generator
{
    internal class ReadyToPlayBoardGenerator : IReadyToPlayBoardGenerator
    {
        private Stopwatch generatingWatch = new Stopwatch();

        private readonly IBoardDisplay _displayer;
        private readonly IBoardSolver _solver;
        private readonly IDigitsRemover _digitsRemover;

        private List<int> RandomizedAllowedDigits = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        private float TotalTimeToGenerate;

        public ReadyToPlayBoardGenerator(IBoardDisplay displayer, IBoardSolver solver, IDigitsRemover digitsRemover)
        {
            _displayer = displayer;
            _digitsRemover = digitsRemover;
            _solver = solver;
        }

        #region Main Method Used To Generate Ready To Play Board
        public bool GenerateSudoku(int emptySpaces, float maxTimeToEmptyBoard, float maxTimeToStartAgain, int[,] returningBoard = null)
        {
            TotalTimeToGenerate += generatingWatch.ElapsedMilliseconds;
            generatingWatch.Restart();

            int[,] board = new int[9, 9];

            int[,] originalBoard = new int[9, 9];

            Shuffle.ShuffleList(RandomizedAllowedDigits);

            _solver.SolveSudoku(originalBoard, RandomizedAllowedDigits);

            Array.Copy(originalBoard, board, originalBoard.Length);

            while (!_digitsRemover.RemovingDigits(board, originalBoard, emptySpaces, maxTimeToEmptyBoard, RandomizedAllowedDigits))
            {
                if (generatingWatch.ElapsedMilliseconds > maxTimeToStartAgain)
                {
                    Console.WriteLine("New Board");
                    GenerateSudoku(emptySpaces, maxTimeToEmptyBoard, maxTimeToStartAgain, returningBoard);

                    return false;
                }
            }
            TotalTimeToGenerate += generatingWatch.ElapsedMilliseconds;
            generatingWatch.Stop();

            _displayer.ShowBoard(board);
            Console.WriteLine($"generated in: {TotalTimeToGenerate} ms");

            if (returningBoard != null)
                Array.Copy(board, returningBoard, board.Length);

            generatingWatch.Reset();
            ResetTimer();

            return true;
        }
        #endregion


        private void ResetTimer()
        {
            TotalTimeToGenerate = 0;
        }

    }
}
