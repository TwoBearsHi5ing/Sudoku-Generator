namespace Sudoku_Generator
{
    internal interface IRulesChecker
    {
        bool CheckAllConditions(int y, int x, int PotentialNumber, int[,] board);
    }
}