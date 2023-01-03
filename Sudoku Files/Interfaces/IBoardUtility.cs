namespace Sudoku_Generator
{
    internal interface IBoardUtility
    {
        bool CheckIfBoardIsEmpty(int[,] board);
        void ResetBoard(int[,] board);
    }
}