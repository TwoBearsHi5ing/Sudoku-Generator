
namespace Sudoku_Generator
{
    internal interface IBoardSolver
    {
        bool SolveSudoku(int[,] board, List<int> numbersToWrite);
    }
}