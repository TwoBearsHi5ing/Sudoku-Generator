
namespace Sudoku_Generator
{
    internal interface ISolutionChecker
    {
        bool CheckNumberOfSolutions(int[,] board);
    }
}