using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02_Othelo
{
    public class Program
    {
        public static void Main()
        {
            Board board = new Board(8);
            eCoinColor[,] boardState = board.State;
            int boardSize = 8;
            // Print column labels
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

                Console.WriteLine("|");
                Console.Write("  ");
                Console.WriteLine(new string('=', boardSize * 4 + 1));

            }

        }

    }
}
