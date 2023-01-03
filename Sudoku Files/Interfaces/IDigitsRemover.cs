namespace Sudoku_Generator
{
    internal interface IDigitsRemover
    {
        bool RemovingDigits(int[,] board, int[,] originalSolution, int emptySpaces, float maxTimeToRemove);
    }
}