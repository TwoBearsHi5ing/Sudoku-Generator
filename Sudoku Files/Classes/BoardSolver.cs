using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Generator
{
    internal class BoardSolver : IBoardSolver
    {
        private readonly IRulesChecker _rulesChecker;

        public BoardSolver(IRulesChecker rulesChecker)
        {
            _rulesChecker = rulesChecker;
        }
        

        #region Soliving Given Board
        public bool SolveSudoku(int[,] board, List<int> numbersToWrite)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (board[i, j] == 0)
                    {
                        return SolveOneCell(i, j, board, numbersToWrite);
                    }
                }
            }
            return true;
        }
        private bool SolveOneCell(int y, int x, int[,] board, List<int> Numbers)
        {
            foreach (int n in Numbers)
            {
                if (_rulesChecker.CheckAllConditions(y, x, n, board))
                {
                    board[y, x] = n;
                    if (SolveSudoku(board, Numbers))
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
