namespace Sudoku_Generator
{
    internal interface IFilledBoardGenerator
    {
        bool GenerateValidBoard(int[,] board);
    }
}