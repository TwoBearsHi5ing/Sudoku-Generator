using System.Diagnostics;

namespace Sudoku_Generator
{
    internal class ReadyToPlayBoardGenerator : IReadyToPlayBoardGenerator
    {
        private Stopwatch generatingWatch = new Stopwatch();

        private readonly IBoardDisplay _displayer;
        private readonly IFilledBoardGenerator _filledBoardGenerator;
        private readonly IDigitsRemover _digitsRemover;

        private float TotalTimeToGenerate;

        public ReadyToPlayBoardGenerator(IBoardDisplay displayer, IFilledBoardGenerator filledBoardGenerator, IDigitsRemover digitsRemover)
        {
            _displayer = displayer;
            _filledBoardGenerator = filledBoardGenerator;
            _digitsRemover = digitsRemover;
        }

        #region Main Method Used To Generate Ready To Play Board
        public bool GenerateSudoku(int emptySpaces, float maxTimeToEmptyBoard, float maxTimeToStartAgain, int[,] returningBoard = null)
        {
            TotalTimeToGenerate += generatingWatch.ElapsedMilliseconds;
            generatingWatch.Restart();

            int[,] board = new int[9, 9];

            int[,] originalBoard = new int[9, 9];

            _filledBoardGenerator.GenerateValidBoard(originalBoard);

            Array.Copy(originalBoard, board, originalBoard.Length);

            while (!_digitsRemover.RemovingDigits(board, originalBoard, emptySpaces, maxTimeToEmptyBoard))
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
