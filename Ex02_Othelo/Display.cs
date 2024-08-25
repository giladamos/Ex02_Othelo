using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02_Othelo
{
    internal class Display
    {
        // setup the game display by recieving from user mode, usernames and board size
        public static void SetUserPrefernces(out string io_player1Name, out string io_player2Name, out int io_boardSize, out bool io_isMultiplayer)
        {
            Console.WriteLine("Welcome to the Othelo game"); // welcome the user
            io_player1Name = getPlayerName(); // get the first player name

            string chosenMode = getGameMode(); // get the game mode
            if (chosenMode == "1")
            {
                io_isMultiplayer = false;
                io_player2Name = "Computer";
            }
            else // if its multiplayer mode get the second player name, make sure names are unique
            {
                io_isMultiplayer = true;
                io_player2Name = getPlayerName();
                
                while (io_player2Name.Equals(io_player1Name))
                {
                    Console.WriteLine("Invalid input! player names must be unique");
                    io_player2Name = getPlayerName();
                }
            }

            io_boardSize = getBoardSize();
        }
 
        // get the player name from the user and make sure it is valid(not blank)
        private static string getPlayerName()
        {
            Console.WriteLine("Please enter player name:");
            string enteredName = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(enteredName) || enteredName.Equals("Computer", StringComparison.OrdinalIgnoreCase)) 
            {
                if (string.IsNullOrWhiteSpace(enteredName))
                {
                    Console.WriteLine("Invalid input! user name must consist of at least one non-whitespace char. please try again:");
                    
                }
                else
                {
                    Console.WriteLine("Invalid input! 'Computer' is a saved keuword and cannot be used(not case sensitive). please try again:");
                }
               
                enteredName = Console.ReadLine();
            }

            return enteredName;
        }
        
        // get the board game size form the user 
        private static int getBoardSize()
        {
            Console.WriteLine("Select board size:");
            Console.WriteLine("Enter 6 for 6x6 game board.");
            Console.WriteLine("Enter 8 for 8x8 game board.");
            string chosenSize = Console.ReadLine();

            while (chosenSize != "6" && chosenSize != "8") // the user must enter either 6 or 8
            {
                Console.WriteLine("Invalid input! you must enter either 6 or 8. please try again:");
                chosenSize = Console.ReadLine();
            }

            return int.Parse(chosenSize);
        }

        // get the game mode from the user 
        private static string getGameMode()
        {
            Console.WriteLine("Select game mode:");
            Console.WriteLine("Enter 1 for Single Plauer mode.");
            Console.WriteLine("Enter 2 for  Multiplayer mode.");
            string chosenMode = Console.ReadLine();

            while (chosenMode != "1" && chosenMode != "2")
            {
                Console.WriteLine("Invalid input! you must enter either 1 or 2. please try again:");
                chosenMode = Console.ReadLine();
            }

            return chosenMode;
        }

        public static string GetPlayerTurn(string i_currentPlayerName, int i_boardDimension)
        {
            string boardEntry = "";
            do
            {
                Console.WriteLine($"Current player: {i_currentPlayerName}");
                Console.WriteLine("Please enter a board entry to place your next coin (e.g., 'A1', 'B3') or enter 'Q' to quit:");
                boardEntry = Console.ReadLine();

                if (!isValidEntry(boardEntry, i_boardDimension))
                {
                    Console.WriteLine("Invalid input!,Please enter a valid board position in the format LetterNumber (e.g., 'A1', 'B3').");
                }
            }
            while (!isValidEntry(boardEntry, i_boardDimension));
            
            return boardEntry;


        }
        
        // Check the that the given entry is in a valid format
        private static bool isValidEntry(string i_boardEntry, int i_boardDimension)
        {
            bool isValid = false;
            
            if (i_boardEntry == "Q")
            {
                isValid = true;
            }

            if (i_boardEntry.Length == 2)
            {
                char column = i_boardEntry[0];
                char row = i_boardEntry[1];
                isValid = (column >= 'A' && column <= 'A' + (i_boardDimension - 1) && row >= '1' && row <= '1' + (i_boardDimension - 1));
            }
            
            return isValid;
        }

        // printing the board after the last play, or the inital board at first
        public static void PrintCurrentBoard(Board i_board)
        {
            eCoinColor[,] boardState = i_board.State;
            int boardSize = i_board.Dimension;

            // Print column labels
            Console.Write("  ");

            for (char column = 'A'; column < 'A' + boardSize; column++)
            {
                Console.Write($"  {column} ");
            }

            Console.WriteLine();

            // Print top border
            Console.Write("  ");
            Console.WriteLine(new string('=', boardSize * 4 + 1));

            for (int i = 0; i < boardSize; i++)
            {
                // Print row label
                Console.Write($"{i + 1} ");

                // Print the game board content according to the boardState matrix
                for (int j = 0; j < boardSize; j++)
                {
                    char currentCoin;
                    switch (boardState[i, j])
                    {
                        case eCoinColor.White:
                            currentCoin = 'O';
                            break;

                        case eCoinColor.Black:
                            currentCoin = 'X';
                            break;

                        default:
                            currentCoin = ' ';
                            break;
                    }

                    Console.Write($"| {currentCoin} ");    
                }
                
                // Close the row with '|' and move to the next line
                Console.WriteLine("|");
                
                Console.Write("  ");
                // Print row divider
                Console.WriteLine(new string('=', boardSize * 4 + 1));
            }

        }
    }
}
