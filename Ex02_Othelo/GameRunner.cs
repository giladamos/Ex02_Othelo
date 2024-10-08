﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02_Othelo
{
    public class GameRunner
    {
        public Player m_Player1 = null;
        public Player m_Player2 = null;
        public Player m_CurrentPlayer = null;
        public Board m_GameBoard = null;
        private bool m_GameIsRunning = false;


        public GameRunner()
        {
            string player1Name = "";
            string player2Name = "";
            int boardSize = 0;
            bool isMultiplayer = false;


            Display.SetUserPrefernces(out player1Name, out player2Name, out boardSize, out isMultiplayer);
            m_Player1 = new Player(player1Name, eCoinColors.Black, false);
            m_Player2 = new Player(player2Name, eCoinColors.White, !isMultiplayer);
            m_CurrentPlayer = m_Player1;
            m_GameBoard = new Board(boardSize);
            m_GameIsRunning = true;



        }

        public void RunGame()
        {
            // Start the game by printing the initial game board 
            Display.PrintCurrentBoard(m_GameBoard);
            
            m_Player1.UpdateValidMoves(m_GameBoard);
            
            m_Player2.UpdateValidMoves(m_GameBoard);
            
            while (!isGameOver()) 
            {
                // if the current player doesnt have valid moves we switch to the other player
                m_CurrentPlayer.UpdateValidMoves(m_GameBoard);
                
                if (!m_CurrentPlayer.HasValidMove())
                {
                    Console.WriteLine($"{m_CurrentPlayer.Name} has no valid moves and must pass.");
                    switchPlayer();
                    
                    continue;
                }
                else
                {
                    processPlayerMove(m_CurrentPlayer, m_GameBoard);
                    
                    switchPlayer();
                }
            }

            EndGame();
        }

        // preform a turn by the current player using the display play turn method to print the turn played, we will need an Entries class to preform casting on the given entry
        private void processPlayerMove(Player i_currentPlayer, Board i_gameBoard)
        {

            // take the player turn from user and verify its ui logic:
            // if current player is bot we do ai move

            (int, int) move;
            

            if (i_currentPlayer.IsBot)
            {
                Console.WriteLine($"Current player: {i_currentPlayer.Name}");
                move = i_currentPlayer.GetAiMove();
                System.Threading.Thread.Sleep(2000);
            }
            else
            {
                string playerMoveInput = "";
                do
                {
                    playerMoveInput = Display.GetPlayerMove(i_currentPlayer.Name, i_gameBoard.Dimension);

                    if (playerMoveInput == "Q")
                    {
                        m_GameIsRunning = false;
                        return; // Exit the method in order to end the game
                    }

                    move = parseMove(playerMoveInput);

                    if (!i_currentPlayer.LegalMoves.ContainsKey(move))
                    {
                        Console.WriteLine("Invalid move!, make sure your move follows the game logic and try again:");
                    }

                }
                while (!i_currentPlayer.LegalMoves.ContainsKey(move));
            }
            
            // once we got a valid move we clear the console and preform the move
            Ex02.ConsoleUtils.Screen.Clear();

            i_gameBoard.UpdateBoard(i_currentPlayer, move);
            
            // Print the updated board and 
            Display.PrintCurrentBoard(i_gameBoard);
        }



        private void switchPlayer()
        {
            // If the current player is player 1 we switch to plauer2 and vice versa, we use the fact that names are unique to compare between players
            m_CurrentPlayer = m_CurrentPlayer.Name.Equals(m_Player1.Name) ? m_Player2 : m_Player1;
        }

        private (int, int) parseMove(string i_input)
        {
            char columnChar = i_input[0];
            int column = columnChar - 'A';
            int row = i_input[1] - '0' - 1;
           
            return (row, column);
        }

        private bool isGameOver()
        {
            // Game is over if the board is full, or neither player has a valid move
            bool boardIsFull = !m_GameBoard.State.Cast<eCoinColors>().Any(cell => cell == eCoinColors.Empty);
            bool noValidMoves = !m_Player1.HasValidMove() && !m_Player2.HasValidMove();

            return boardIsFull || noValidMoves || !m_GameIsRunning; // The gameIsRunning flag will be false if "Q" was pressed
        }

        private void EndGame()
        {
            Console.WriteLine("Game over!");
            // Calculate scores, determine the winner, etc.
            int player1Score = m_Player1.CalculateScore(m_GameBoard);
            int player2Score = m_Player2.CalculateScore(m_GameBoard);

            Console.WriteLine($"{m_Player1.Name} (X) Score: {player1Score}");
            Console.WriteLine($"{m_Player2.Name} (O) Score: {player2Score}");

            if (player1Score > player2Score)
            {
                Console.WriteLine($"{m_Player1.Name} wins!");
            }
            else if (player2Score > player1Score)
            {
                Console.WriteLine($"{m_Player2.Name} wins!");
            }
            else
            {
                Console.WriteLine("It's a draw!");
            }
        }
    }
}
