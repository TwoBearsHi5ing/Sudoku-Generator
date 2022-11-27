using Sudoku_Generator;

Generator generator = new Generator();

// currently only works reasonably with number <= 55
int emptySpaces = 50;
float maxTimeToGenerate = 100f;
float maxTimeToSartAgain = 1000f;

int[,] testArray = new int[9, 9];        
                                                                           // optional parameter to get the final result as a int[9,9] array
generator.GenerateSudoku( emptySpaces, maxTimeToGenerate, maxTimeToSartAgain, testArray );

//generator.SolveSudoku(testArray, generator.AllowDigitsReadOnly);
//generator.ShowBoard(testArray);
Console.ReadKey();

