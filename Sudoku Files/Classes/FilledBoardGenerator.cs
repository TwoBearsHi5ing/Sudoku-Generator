using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Generator
{
    internal class FilledBoardGenerator : IFilledBoardGenerator
    {
        private readonly IRulesChecker _rulesChecker;

        public FilledBoardGenerator(IRulesChecker rulesChecker)
        {
            _rulesChecker = rulesChecker;
        }

        #region Generating Solved Valid Board
        public bool GenerateValidBoard(int[,] board)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (board[i, j] == 0)
                    {
                        return FillOneCell(i, j, board);
                    }
                }
            }
            return true;
        }
        private bool FillOneCell(int y, int x, int[,] board)
        {
            for (int i = 0; i < 9; i++)
            {
                int a = Shuffle.random.Next(1, 10);

                if (_rulesChecker.CheckAllConditions(y, x, a, board))
                {
                    board[y, x] = a;
                    if (GenerateValidBoard(board))
                    {
                        return true;
                    }
                }
            }
            board[y, x] = 0;
            return false;
        }
        #endregion
    }
}
