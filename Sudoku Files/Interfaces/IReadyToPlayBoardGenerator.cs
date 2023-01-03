namespace Sudoku_Generator
{
    internal interface IReadyToPlayBoardGenerator
    {
        bool GenerateSudoku(int emptySpaces, float maxTimeToGenerate, float maxTimeToStartAgain, int[,] returningBoard = null);
    }
}