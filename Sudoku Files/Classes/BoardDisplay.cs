using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_Generator
{
    internal class BoardDisplay : IBoardDisplay
    {
        public void ShowBoard(int[,] board)
        {
            Console.WriteLine();
            for (int i = 0; i < 9; i++)
            {
                if (i % 3 == 0 && i != 0)
                {
                    Console.WriteLine("--------------------------");
                }

                for (int j = 0; j < 9; j++)
                {
                    if (j % 3 == 0 && j != 0)
                    {
                        Console.Write(" |  ");
                    }

                    if (j == 8)
                    {
                        Console.Write(board[i, j] + " ");
                        Console.WriteLine("");
                    }

                    else
                    {
                        Console.Write(board[i, j] + " ");
                    }

                }
            }
            Console.WriteLine();
        }
    }
}
