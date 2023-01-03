using Sudoku_Generator;

/* 1. Generate valid filled sudoku board
 * 2. Remove random number (Set value of a random position in array to 0)
 * 3. Check if board still has only one solution
 * 4. If it does keep going, else restore value
 * 5. Keep removing untill amount of empty spaces is met
 * 6. If specifiad time limit is reached restore original board and try from a diffrent place
 * 7. If another time limit is reached go back to step 1
 * 8. Display generated board
 */

RulesChecker rulesChecker = new RulesChecker();

FilledBoardGenerator filledBoardGenerator = new FilledBoardGenerator(rulesChecker);

BoardSolver boardSolver = new BoardSolver(rulesChecker);

SolutionChecker solutionChecker = new SolutionChecker(boardSolver);

DigitsRemover remover = new DigitsRemover(solutionChecker);

BoardDisplay display = new BoardDisplay();

ReadyToPlayBoardGenerator main_generator = new ReadyToPlayBoardGenerator(display, filledBoardGenerator, remover);

// currently only works reasonably with number <= 55
int emptySpaces = 50;
float maxTimeToEmptyBoard = 100f;
float maxTimeToStartAgain = 1000f;

int[,] testArray = new int[9, 9];        
                                                                           // optional parameter to get the final result as a int[9,9] array
main_generator.GenerateSudoku(emptySpaces, maxTimeToEmptyBoard, maxTimeToStartAgain, testArray );


Console.ReadKey();
