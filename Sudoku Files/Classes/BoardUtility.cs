using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Generator
{
    internal class BoardUtility : IBoardUtility
    {
        public void ResetBoard(int[,] board)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    board[i, j] = 0;
                }
            }
        }
        public bool CheckIfBoardIsEmpty(int[,] board)
        {
            int k = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (board[i, j] == 0)
                    {
                        k++;
                    }
                }
            }
            if (k == 81)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
