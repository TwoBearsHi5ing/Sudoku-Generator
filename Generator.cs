using System.Diagnostics;

namespace Sudoku_Generator
{
    internal class Generator
    {
        private List<int> allowedDigits = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        public IList<int> AllowDigitsReadOnly => allowedDigits.AsReadOnly();

        private List<int> ReversedAllowedDigits = new List<int> { 9, 8, 7, 6, 5, 4, 3, 2, 1 };
        private List<int> RandomizedAllowedDigits = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        private Random random = new Random();
        private Stopwatch generatingWatch = new Stopwatch();

        private float TotalTimeToGenerate;

        #region Main Public Method Used To Generate Ready To Play Board
        public bool GenerateSudoku(int emptySpaces, float maxTimeToGenerate, float maxTimeToStartAgain, int[,] returningBoard = null)
        {
            TotalTimeToGenerate += generatingWatch.ElapsedMilliseconds;
            generatingWatch.Restart();

            Shuffle.ShuffleList(RandomizedAllowedDigits);

            int[,] board = new int[9, 9];

            int[,] originalBoard = new int[9, 9];

            GenerateValidBoard(originalBoard);

            Array.Copy(originalBoard, board, originalBoard.Length);

            while (!RemovingDigits(board, originalBoard, emptySpaces, maxTimeToGenerate))
            {
                if (generatingWatch.ElapsedMilliseconds > maxTimeToStartAgain)
                {
                    Console.WriteLine("New Board");
                    GenerateSudoku(emptySpaces, maxTimeToGenerate, maxTimeToStartAgain);

                    return false;
                }
            }
            TotalTimeToGenerate += generatingWatch.ElapsedMilliseconds;
            generatingWatch.Stop();

            ShowBoard(board);
            Console.WriteLine($"generated in: {TotalTimeToGenerate} ms");

            if (returningBoard != null)
                Array.Copy(board, returningBoard, board.Length);
          
            generatingWatch.Reset();
            ResetTimer();
        
            return true;
        }
        #endregion

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
        private bool CheckAllConditions(int y, int x, int PotentialNumber, int[,] board)
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

        #region Soliving Given Board
        private bool SolveOneCell(int y, int x, int[,] board, IList<int> Numbers)
        {
            foreach (int n in Numbers)
            {
                if (CheckAllConditions(y, x, n, board))
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
        public bool SolveSudoku(int[,] board, IList<int> numbersToWrite)
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
        #endregion

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
                int a = random.Next(1, 10);

                if (CheckAllConditions(y, x, a, board))
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

        #region Removing Digits To Ready The Board
        private bool RemovingDigits(int[,] board, int[,] originalSolution, int emptySpaces, float MaxTimeToGenerate)
        {
            int[,] backup = new int[9, 9];


            Array.Copy(originalSolution, board, originalSolution.Length);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            while (!ZeroAmount(board, emptySpaces))
            {
                if (stopwatch.ElapsedMilliseconds > MaxTimeToGenerate)
                {
                    stopwatch.Stop();
                    Console.WriteLine("New Try to empty the board");
                    return false;
                }

                Array.Copy(board, backup, board.Length);

                int i, j;
                int number;
                i = random.Next(0, 9);
                j = random.Next(0, 9);

                number = board[i, j];
                backup[i, j] = 0;

                if (CheckNumberOfSolutions(backup))
                {
                    board[i, j] = 0;
                }
                else
                {
                    backup[i, j] = number;
                }
            }

            stopwatch.Stop();
            return true;
        }
        private bool ZeroAmount(int[,] board, int amount)
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
            if (k == amount)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool CheckNumberOfSolutions(int[,] board)
        {
            int numberOfSolutions = 0;
            int[,] firstSolution = new int[9, 9];
            int[,] secondSolution = new int[9, 9];
            int[,] thierdSolution = new int[9, 9];
            int[,] originalBoard = new int[9, 9];

            Array.Copy(board, originalBoard, board.Length);
            while (numberOfSolutions < 3)
            {
                Array.Copy(originalBoard, board, board.Length);

                if (numberOfSolutions == 0)
                {
                    SolveSudoku(board, RandomizedAllowedDigits);
                    Array.Copy(board, firstSolution, board.Length);
                }

                else if (numberOfSolutions == 1)
                {
                    SolveSudoku(board, allowedDigits);
                    Array.Copy(board, secondSolution, board.Length);
                }

                else if (numberOfSolutions > 1)
                {
                    SolveSudoku(board, ReversedAllowedDigits);
                    Array.Copy(board, thierdSolution, board.Length);
                }

                if (!CompareSolutions(firstSolution, secondSolution) && numberOfSolutions == 1)
                {
                    return false;
                }

                if (!CompareSolutions(firstSolution, thierdSolution) && numberOfSolutions == 2)
                {
                    return false;
                }

                numberOfSolutions++;
            }
            return true;

        }
        private bool CompareSolutions(int[,] originalBoard, int[,] newBoard)
        {
            return newBoard.Rank == originalBoard.Rank &&
                   Enumerable.Range(0, newBoard.Rank).All(dimension => newBoard.GetLength(dimension) == originalBoard.GetLength(dimension)) &&
                   newBoard.Cast<int>().SequenceEqual(originalBoard.Cast<int>());
        }
        #endregion

        #region Other Public Utility
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
        public void ResetTimer()
        {
            TotalTimeToGenerate = 0;
        }
        public float GetTotalTimeToGenerate()
        {
            return TotalTimeToGenerate;
        }
        
        #endregion
    }
}
