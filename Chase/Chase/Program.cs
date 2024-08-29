using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Lifetime;
using System.Security.Policy;
using System.Xml.Schema;

namespace Chase
{
    internal class Program
    {
        static bool ShowMenu(Game game)
        {
            Console.Clear();
            Logo();
            Console.WriteLine();
            Console.WriteLine("======== GAME MENU ========");
            Console.WriteLine("1. New Game");
            Console.WriteLine("2. Leaderboard");
            Console.WriteLine("3. Explain Rules");
            Console.WriteLine("4. Exit\n");
            Console.Write("Please choose an option (1-4): ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    StartNewGame(game);
                    return false;
                case "2":
                    LeaderBoard(game);
                    return false;
                case "3":
                    ExplainRules();
                    return false;
                case "4":
                    return true; // Exit the game
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    Console.ReadKey();
                    return false;
            }
        }
        static void Main(string[] args)
        {
            Game game = new Game();
            bool exit = false;

            while (!exit)
            {
                exit = ShowMenu(game);
            }

        }

        static void StartNewGame(Game game)
        {
            char[,] board = new char[10, 10];
            bool gameover = false; // loop player 1 and player 2 turn until this is true
            bool whosturn = true; // true -> player1, false -> player2
            int dice = 0; // save number from Diceroll method
            int newPos;

            // Player 1
            int currentposition1 = -1; // Track the current position of the token
            bool tokenonboard1 = false; // Track if token is on the board or not. If token is not on the board, player has to throw 1 or 6 to place token on the board
            bool additionalroll1 = false; // COMBINE WITH PLAYER 2    Track if player will have one more chance to roll (when roll 6)  
            int turn1 = 0; // track how many turns it took in total
            int score1 = 0; // track score -> catching other's token, place token on shortcut will add bonus. add that total to score derived from total turn (shorter it took will give higher score) 

            // Player 2
            int currentposition2 = -1;
            bool tokenonboard2 = false;
            bool additionalroll2 = false;
            int turn2 = 0;
            int score2 = 0;

            //Initiate the game
            InitiateBoard(board);
            Display(board);
            Console.WriteLine();

            while (!gameover)
            {
                // Player 1's turn
                whosturn = true;
                turn1++;
                do
                {
                    //DiceRoll();
                    TestRoll();
                    Console.ReadLine();
                    MoveToken1(dice);
                    AdditionalRoll1();
                }
                while (additionalroll1);

                if (gameover) break; // If game is over after Player 1's turn

                // Player 2's turn
                whosturn = false;
                turn2++;
                do
                {
                    //DiceRoll();
                    TestRoll();
                    Console.ReadLine();
                    MoveToken2(dice);
                    AdditionalRoll2();
                }
                while (additionalroll2);
            }
            Console.ReadKey();

            void InitiateBoard(char[,] matrix)
            {
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        board[i, j] = '_';
                    }
                }
            }

            void MoveToken1(int diceroll)
            {
                additionalroll1 = false;

                if (tokenonboard1 == false && (diceroll != 1 && diceroll != 6)) // if token is still in base, not on board
                {
                    DisplayTurn();
                    Console.WriteLine("Please roll 1 or 6 to place the token on the board!");
                    Console.ReadKey();
                    Display(board);
                    return;
                }

                if (tokenonboard1 == false && (diceroll == 1 || diceroll == 6))
                {
                    if (board[0, 0] == '_') // if (0,0) is vacant
                    {
                        DisplayTurn();
                        Console.WriteLine("Your coin got out of the base!");
                        Console.ReadKey();
                        tokenonboard1 = true;
                        currentposition1 = 1;
                        board[0, 0] = '1';
                        Display(board);
                        if (diceroll == 1)
                        {
                            return;
                        }
                        if (diceroll == 6)
                        {
                            additionalroll1 = true;
                            return;
                        }
                    }
                    else if (board[0, 0] != '_') // if (0,0) is not vacant
                    {
                        DisplayTurn();
                        Console.WriteLine("Other players token is blocking your way out!");
                        Console.ReadKey();
                        if (diceroll == 1)
                        {
                            return;
                        }
                        if (diceroll == 6)
                        {
                            additionalroll1 = true;
                            return;
                        }
                    }
                }

                if (diceroll == 6) // if player rolled 6 (for second dice roll)
                {
                    additionalroll1 = true;
                }

                newPos = currentposition1 + diceroll; // calculate new location for token

                if (newPos == 100) // reached GOAL!
                {
                    ReachedGoal();
                    return;
                }

                if (newPos > 100) // if new location exceeding location of GOAL
                {
                    DisplayTurn();
                    Console.WriteLine("You need to roll exact number to get to the goal. Good luck on your next roll!");
                    Console.ReadKey();
                    return;
                }

                CheckIfLandedShortcuts();

                int[] currentCoord = NumToCoordinate(currentposition1); // get coordinate before the roll (OLD)
                int[] newCoord = NumToCoordinate(newPos); // get coordinate after the roll (NEW)

                CheckForTokenConflict(newCoord);

                board[currentCoord[0], currentCoord[1]] = '_'; // clear the previous position
                board[newCoord[0], newCoord[1]] = '1'; // place the token at the new position

                currentposition1 = newPos; // save the current position of token

                Console.Clear();
                Display(board);
            }

            void MoveToken2(int diceroll)
            {
                additionalroll2 = false;

                if (tokenonboard2 == false && (diceroll != 1 && diceroll != 6)) // if token is still in base, not on board
                {
                    DisplayTurn();
                    Console.WriteLine("Please roll 1 or 6 to place the token on the board!");
                    Console.ReadKey();
                    Display(board);
                    return;
                }

                if (tokenonboard2 == false && (diceroll == 1 || diceroll == 6))
                {
                    if (board[0, 0] == '_') // if (0,0) is vacant
                    {
                        DisplayTurn();
                        Console.WriteLine("Your coin got out of the base!");
                        Console.ReadKey();
                        tokenonboard2 = true;
                        currentposition2 = 1;
                        board[0, 0] = '2';
                        Display(board);
                        if (diceroll == 1)
                        {
                            return;
                        }
                        if (diceroll == 6)
                        {
                            additionalroll2 = true;
                            return;
                        }
                    }
                    else if (board[0, 0] != '_') // if (0,0) is not vacant
                    {
                        DisplayTurn();
                        Console.WriteLine("Other players token is blocking your way out!");
                        Console.ReadKey();
                        if (diceroll == 1)
                        {
                            return;
                        }
                        if (diceroll == 6)
                        {
                            additionalroll2 = true;
                            return;
                        }
                    }
                }

                if (diceroll == 6) // if player rolled 6 (for bonus dice roll)
                {
                    additionalroll2 = true;
                }

                newPos = currentposition2 + diceroll; // calculate new location for token

                if (newPos == 100) // reached GOAL!
                {
                    ReachedGoal();
                    return;
                }

                if (newPos > 100) // if new location exceeding location of GOAL
                {
                    DisplayTurn();
                    Console.WriteLine("You need to roll exact number to get to the goal. Good luck on your next roll!");
                    Console.ReadKey();
                    return;
                }

                CheckIfLandedShortcuts();

                int[] currentCoord = NumToCoordinate(currentposition2); // convert to new coordinate
                int[] newCoord = NumToCoordinate(newPos);

                CheckForTokenConflict(newCoord);

                board[currentCoord[0], currentCoord[1]] = '_'; // clear the previous position
                board[newCoord[0], newCoord[1]] = '2'; // place the token at the new position

                currentposition2 = newPos; // save the current position of token

                Console.Clear();
                Display(board);
            }

            void AdditionalRoll1()
            {
                if (additionalroll1 == true)
                {
                    DisplayTurn();
                    Console.WriteLine("You rolled 6! You get to roll one more time!");
                    Console.ReadKey();
                }
                Display(board);
            }

            void AdditionalRoll2()
            {
                if (additionalroll2 == true)
                {
                    DisplayTurn();
                    Console.WriteLine("You rolled 6! You get to roll one more time!");
                    Console.ReadKey();
                }
                Display(board);
            }

            int[] NumToCoordinate(int num)
            {
                int row = (num - 1) / 10;
                int col = (num - 1) % 10;
                return new int[] { row, col };
            }

            int CoordinateToNum(int row, int col)
            {
                int num = (row * 10) + col + 1;
                return num;
            }

            void Display(char[,] matrix)
            {
                Console.Clear(); // Clear the console before displaying the matrix

                Logo();

                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if ((i == 1 && j == 1) || (i == 2 && j == 2))
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                        }
                        else if ((i == 2 && j == 5) || (i == 4 && j == 4))
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        else if ((i == 5 && j == 5) || (i == 7 && j == 7))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        else if ((i == 6 && j == 1) || (i == 8 && j == 3))
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                        }
                        else if ((i == 7 && j == 2) || (i == 9 && j == 4))
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                        }
                        else
                        {
                            Console.ResetColor();
                        }
                        Console.Write(matrix[i, j] + " ");
                    }
                    Console.WriteLine();
                }
            }

            void ReachedGoal()
            {
                Console.Clear(); // Clear the console before displaying the win message

                if (whosturn)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(@"   _____                            _         _       _   _                 _ ");
                    Console.WriteLine(@"  / ____|                          | |       | |     | | (_)               | |");
                    Console.WriteLine(@" | |     ___  _ __   __ _ _ __ __ _| |_ _   _| | __ _| |_ _  ___  _ __  ___| |");
                    Console.WriteLine(@" | |    / _ \| '_ \ / _` | '__/ _` | __| | | | |/ _` | __| |/ _ \| '_ \/ __| |");
                    Console.WriteLine(@" | |___| (_) | | | | (_| | | | (_| | |_| |_| | | (_| | |_| | (_) | | | \__ \_|");
                    Console.WriteLine(@"  \_____\___/|_| |_|\__, |_|  \__,_|\__|\__,_|_|\__,_|\__|_|\___/|_| |_|___(_)");
                    Console.WriteLine(@" |  __ \| |          __/ |        /_ |                                        ");
                    Console.WriteLine(@" | |__) | | __ _ _  |___/__ _ __   | |                                        ");
                    Console.WriteLine(@" |  ___/| |/ _` | | | |/ _ \ '__|  | |                                        ");
                    Console.WriteLine(@" | |    | | (_| | |_| |  __/ |     | |_                                       ");
                    Console.WriteLine(@" |_|    |_|\__,_|\__, |\___|_|     |_( )                                      ");
                    Console.WriteLine(@"                  __/ |              |/                                       ");
                    Console.WriteLine(@" __     __       |___/                               _                        ");
                    Console.WriteLine(@" \ \   / /        ( )                               | |                       ");
                    Console.WriteLine(@"  \ \_/ /__  _   _|/__   _____  __      _____  _ __ | |                       ");
                    Console.WriteLine(@"   \   / _ \| | | | \ \ / / _ \ \ \ /\ / / _ \| '_ \| |                       ");
                    Console.WriteLine(@"    | | (_) | |_| |  \ V /  __/  \ V  V / (_) | | | |_|                       ");
                    Console.WriteLine(@"    |_|\___/ \__,_|   \_/ \___|   \_/\_/ \___/|_| |_(_)                  ");
                    Console.ResetColor();

                    Console.WriteLine();
                    DisplayTurn();
                    Console.WriteLine();
                    Console.WriteLine($"you took {turn1} turns to complete the game");
                    int finalScore = (int)(score1 + (4000 * Math.Pow(0.97, turn1)));
                    Console.WriteLine($"your score is {finalScore}\n");
                    Console.Write("Please enter your name: ");
                    string name = (Console.ReadLine());

                    game.AddRecord(name, finalScore, turn1);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(@"   _____                            _         _       _   _                 _ ");
                    Console.WriteLine(@"  / ____|                          | |       | |     | | (_)               | |");
                    Console.WriteLine(@" | |     ___  _ __   __ _ _ __ __ _| |_ _   _| | __ _| |_ _  ___  _ __  ___| |");
                    Console.WriteLine(@" | |    / _ \| '_ \ / _` | '__/ _` | __| | | | |/ _` | __| |/ _ \| '_ \/ __| |");
                    Console.WriteLine(@" | |___| (_) | | | | (_| | | | (_| | |_| |_| | | (_| | |_| | (_) | | | \__ \_|");
                    Console.WriteLine(@"  \_____\___/|_| |_|\__, |_|  \__,_|\__|\__,_|_|\__,_|\__|_|\___/|_| |_|___(_)");
                    Console.WriteLine(@" |  __ \| |          __/ |        |__ \                                       ");
                    Console.WriteLine(@" | |__) | | __ _ _  |___/__ _ __     ) |                                      ");
                    Console.WriteLine(@" |  ___/| |/ _` | | | |/ _ \ '__|   / /                                       ");
                    Console.WriteLine(@" | |    | | (_| | |_| |  __/ |     / /_ _                                     ");
                    Console.WriteLine(@" |_|    |_|\__,_|\__, |\___|_|    |____( )                                    ");
                    Console.WriteLine(@"                  __/ |                |/                                     ");
                    Console.WriteLine(@" __     __       |___/                               _                        ");
                    Console.WriteLine(@" \ \   / /        ( )                               | |                       ");
                    Console.WriteLine(@"  \ \_/ /__  _   _|/__   _____  __      _____  _ __ | |                       ");
                    Console.WriteLine(@"   \   / _ \| | | | \ \ / / _ \ \ \ /\ / / _ \| '_ \| |                       ");
                    Console.WriteLine(@"    | | (_) | |_| |  \ V /  __/  \ V  V / (_) | | | |_|                       ");
                    Console.WriteLine(@"    |_|\___/ \__,_|   \_/ \___|   \_/\_/ \___/|_| |_(_)                       ");
                    Console.ResetColor();

                    Console.WriteLine();
                    DisplayTurn();
                    Console.WriteLine();
                    Console.WriteLine($"you took {turn2} turns to complete the game");
                    int finalScore = (int)(score2 + (4000 * Math.Pow(0.97, turn2)));
                    Console.WriteLine($"your score is {finalScore}\n");
                    Console.Write("Please enter your name: ");
                    string name = (Console.ReadLine());

                    game.AddRecord(name, finalScore, turn2);
                }

                // player 1 RESET
                currentposition1 = -1;
                tokenonboard1 = false;
                additionalroll1 = false;
                turn1 = 0;
                score1 = 0;

                // player 2 REST
                currentposition2 = -1;
                tokenonboard2 = false;
                additionalroll2 = false;
                turn2 = 0;

                gameover = true;

                Console.ReadKey();

                Console.Clear();

                LeaderBoard(game);
            }

            void DiceRoll()
            {
                DisplayTurn();
                Random random = new Random();
                Console.WriteLine("Press Enter to roll dice");
                Console.ReadLine();
                int rollednumber = random.Next(1, 7);
                Console.WriteLine($"You rolled: {rollednumber}");
                dice = rollednumber;
            }

            void TestRoll() // to manually test if logic works
            {
                DisplayTurn();
                Console.WriteLine("TEST MODE: Type and Enter value");

                int rolledNumber;
                string input;

                while (true)
                {
                    input = Console.ReadLine();
                    if (!string.IsNullOrEmpty(input) && int.TryParse(input, out rolledNumber))
                    {
                        break;
                    }
                }

                Console.WriteLine($"You rolled: {rolledNumber}");
                dice = rolledNumber;
            }

            void DisplayTurn()
            {
                if (whosturn == true)
                    Console.Write("PLAYER 1, ");
                else
                    Console.Write("PLAYER 2, ");
            }

            void CheckForTokenConflict(int[] newCoord)
            {
                if (whosturn == true) // if player 1 caught player 2
                {
                    if (board[newCoord[0], newCoord[1]] == '2')
                    {
                        DisplayTurn();
                        Console.WriteLine("You captured Player 2's token!");
                        score1 = score1 += 100;
                        tokenonboard2 = false;
                        int[] currentCoord = NumToCoordinate(currentposition2); Console.ReadKey();
                        board[currentCoord[0], currentCoord[1]] = '_';
                        return;
                    }
                }
                else if (whosturn == false) // if player 2 caught player 1
                {
                    if (board[newCoord[0], newCoord[1]] == '1')
                    {
                        DisplayTurn();
                        Console.WriteLine("You captured Player 1's token!");
                        score2 = score2 += 100;
                        tokenonboard1 = false;
                        int[] currentCoord = NumToCoordinate(currentposition1); Console.ReadKey();
                        board[currentCoord[0], currentCoord[1]] = '_';
                        return;
                    }
                }
            }

            void CheckIfLandedShortcuts()
            {

                // (1,1) <-> (2,2)  12 <-> 23
                if (newPos == 12)
                {
                    if (board[2, 2] == '_') // if destination of the coordinate is vacant
                    {
                        newPos = 23;
                        return;
                    }
                    else
                    {
                        int[] newCoord = NumToCoordinate(newPos);
                        newPos = 23;
                        return;
                    }
                }
                if (newPos == 23)
                {
                    if (board[1, 1] == '_') // if destination of the coordinate is vacant
                    {
                        newPos = 12;
                        return;
                    }
                    else
                    {
                        int[] newCoord = NumToCoordinate(newPos);
                        newPos = 12;
                        return;
                    }
                }

                // (2,5) <-> (4,4)  26 <-> 45
                if (newPos == 26)
                {
                    if (board[4, 4] == '_') // if destination of the coordinate is vacant
                    {
                        newPos = 45;
                        return;
                    }
                    else
                    {
                        int[] newCoord = NumToCoordinate(newPos);
                        newPos = 45;
                        return;
                    }
                }
                if (newPos == 45)
                {
                    if (board[2, 5] == '_') // if destination of the coordinate is vacant
                    {
                        newPos = 26;
                        return;
                    }
                    else
                    {
                        int[] newCoord = NumToCoordinate(newPos);
                        newPos = 26;
                        return;
                    }
                }

                // (5,5) <-> (7,7)  56 <-> 78
                if (newPos == 56)
                {
                    if (board[7, 7] == '_') // if destination of the coordinate is vacant
                    {
                        newPos = 78;
                        return;
                    }
                    else
                    {
                        int[] newCoord = NumToCoordinate(newPos);
                        newPos = 78;
                        return;
                    }
                }
                if (newPos == 78)
                {
                    if (board[5, 5] == '_') // if destination of the coordinate is vacant
                    {
                        newPos = 56;
                        return;
                    }
                    else
                    {
                        int[] newCoord = NumToCoordinate(newPos);
                        newPos = 56;
                        return;
                    }
                }

                // (6,1) <-> (8,3)  62 <-> 84
                if (newPos == 62)
                {
                    if (board[8, 3] == '_') // if destination of the coordinate is vacant
                    {
                        newPos = 84;
                        return;
                    }
                    else
                    {
                        int[] newCoord = NumToCoordinate(newPos);
                        newPos = 84;
                        return;
                    }
                }
                if (newPos == 84)
                {
                    if (board[6, 1] == '_') // if destination of the coordinate is vacant
                    {
                        newPos = 62;
                        return;
                    }
                    else
                    {
                        int[] newCoord = NumToCoordinate(newPos);
                        newPos = 62;
                        return;
                    }
                }

                // (7,2) <-> (9,4)  73 <-> 95
                if (newPos == 73)
                {
                    if (board[9, 4] == '_') // if destination of the coordinate is vacant
                    {
                        newPos = 95;
                        return;
                    }
                    else
                    {
                        int[] newCoord = NumToCoordinate(newPos);
                        newPos = 95;
                        return;
                    }
                }
                if (newPos == 95)
                {
                    if (board[7, 2] == '_') // if destination of the coordinate is vacant
                    {
                        newPos = 73;
                        return;
                    }
                    else
                    {
                        int[] newCoord = NumToCoordinate(newPos);
                        newPos = 73;
                        return;
                    }
                }
            }
        }

        static void LeaderBoard(Game game)
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" _                    _            ______                     _ ");
            Console.WriteLine("| |                  | |           | ___ \\                   | |");
            Console.WriteLine("| |     ___  __ _  __| | ___ _ __  | |_/ / ___   __ _ _ __ __| |");
            Console.WriteLine("| |    / _ \\/ _` |/ _` |/ _ \\ '__| | ___ \\/ _ \\ / _` | '__/ _` |");
            Console.WriteLine("| |___|  __/ (_| | (_| |  __/ |    | |_/ / (_) | (_| | | | (_| |");
            Console.WriteLine("\\_____ /\\___|\\__,_|\\__,_|\\___|_|    \\____/ \\___/ \\__,_|_|  \\__,_|");
            Console.WriteLine("                                                               ");
            Console.WriteLine("                                                               ");
            Console.ResetColor();

            Console.WriteLine();
            List<Record> sortedRecords = game.InsertionSort();

            //display top 10 only
            //if possible, make 1st place look more 
            if (sortedRecords.Count > 0)
            {
                var firstPlace = sortedRecords[0];
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("=====================================================");
                Console.WriteLine("                 *** FIRST PLACE ***");
                Console.WriteLine($"          Score = {firstPlace.Score,4}     Name = {firstPlace.Name}");
                Console.WriteLine("=====================================================");
                Console.ResetColor();
                Console.WriteLine();
            }

            for (int i = 1; i < sortedRecords.Count && i < 10; i++)
            {
                var record = sortedRecords[i];
                Console.WriteLine($"{i + 1,2}  Score = {record.Score,4}     Name = {record.Name}");
            }
            Console.ReadKey();
        }

        static void ExplainRules()
        {
            Console.Clear();
            Logo();
            Console.WriteLine();
            Console.WriteLine("======== GAME RULES ========\n");
            Console.WriteLine("1. The objective of the game is to move your token from the start (0,0) to goal (99,99).");
            Console.WriteLine("2. Roll a 1 or 6 to place your token on the board.");
            Console.WriteLine("3. Rolling a 6 grants you an additional roll.");
            Console.WriteLine("4. Landing on colored pairs of ‘_’ can either move you ahead faster or take you back,");
            Console.WriteLine("   depending on which end of the pair you land on.");
            Console.WriteLine("5. Capture your opponent’s token to score bonus points.");
            Console.WriteLine("6. To reach the goal, you must roll the exact number needed to land on it.\n");
            Console.WriteLine("Press Enter to go back to the menu");
            Console.ReadKey();
        }

        static void Logo()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(@"   _____ _    _           _____ ______ ");
            Console.WriteLine(@"  / ____| |  | |   /\    / ____|  ____|");
            Console.WriteLine(@" | |    | |__| |  /  \  | (___ | |__   ");
            Console.WriteLine(@" | |    |  __  | / /\ \  \___ \|  __|  ");
            Console.WriteLine(@" | |____| |  | |/ ____ \ ____) | |____ ");
            Console.WriteLine(@"  \_____|_|  |_/_/    \_\_____/|______|");
            Console.WriteLine();
            Console.WriteLine();
            Console.ResetColor();
        }
    }
}