using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Generator
{
    internal class RulesChecker : IRulesChecker
    {
        #region Conditions Checking
        private bool CheckRow(int y, int PotentialNumber, int[,] board)
        {
            for (int i = 0; i < 9; i++)
            {
                if (board[y, i] == PotentialNumber)
                {
                    return false;
                }
            }
            return true;
        }
        private bool CheckColumn(int x, int PotentialNumber, int[,] board)
        {
            for (int i = 0; i < 9; i++)
            {
                if (board[i, x] == PotentialNumber)
                {
                    return false;
                }
            }
            return true;
        }
        private bool CheckSquare(int y, int x, int PotentialNumber, int[,] board)
        {
            int square_x = x / 3 * 3;
            int square_y = y / 3 * 3;

            for (int i = square_y; i < square_y + 3; i++)
            {
                for (int j = square_x; j < square_x + 3; j++)
                {

                    if (board[i, j] == PotentialNumber)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public bool CheckAllConditions(int y, int x, int PotentialNumber, int[,] board)
        {
            if (CheckRow(y, PotentialNumber, board) && CheckColumn(x, PotentialNumber, board) && CheckSquare(y, x, PotentialNumber, board))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
