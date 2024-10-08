﻿using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02_Othelo
{
    



    public class Board
    {
        private int m_Dimension = 0;
        private eCoinColors[,] m_State = null;
        

        private static readonly (int, int)[] sr_directions =
    {
        (-1, 0), // Up
        ( 1, 0), // Down
        ( 0, -1), // Left
        ( 0, 1), // Right
        (-1, -1), // Up-Left
        (-1, 1), // Up-Right
        ( 1, -1), // Down-Left
        ( 1, 1)  // Down-Right
    };

        public Board(int i_BoardSize)
        {
            m_Dimension = i_BoardSize;
            m_State = new eCoinColors[m_Dimension, m_Dimension];

            // init board with middle tokens
            m_State[(m_Dimension / 2) - 1, (m_Dimension / 2) - 1] = eCoinColors.White;
            m_State[(m_Dimension / 2), (m_Dimension / 2)] = eCoinColors.White;
            m_State[(m_Dimension / 2) - 1, (m_Dimension / 2)] = eCoinColors.Black;
            m_State[(m_Dimension / 2), (m_Dimension / 2) - 1] = eCoinColors.Black;
        }

        public eCoinColors[,] State
        {
            get
            {
                return m_State;
            }
        }

        public int Dimension
        {
            get
            {
                return m_Dimension;
            }

        }

        public void updatePlayerValidMoves(Player i_player)
        {
            // Clear the playe current legal moves dictionary
            i_player.LegalMoves.Clear();

            // Iterate over every cell on the boardfor 
            for (int row = 0; row < this.Dimension; row++)
            {
                for (int column = 0; column < this.Dimension; column++)
                {
                    // If in the current cell there is no coin we check if we can place a coin there
                    if (this.State[row, column] == eCoinColors.Empty)
                    {
                        int numOfFlippedCoins = getFlippedCoinsCount((row, column), i_player);
                        if (numOfFlippedCoins > 0)
                        {
                            i_player.LegalMoves[(row, column)] = numOfFlippedCoins;
                        }
                    }
                }
            }
        }

        // Count the number of flipped coins for a given move defined by a cell and a Plauer
        public int getFlippedCoinsCount((int i_row, int i_column) i_boardCell, Player i_player)
        {
            int totalFlipsCount = 0;
            // we iterate over every direction possible and sum the total number of coins to be flipped
            foreach (var direction in sr_directions)
            {
                totalFlipsCount += getFlippedCoinsCountInDirection(i_boardCell, i_player, direction.Item1, direction.Item2);
            }

            return totalFlipsCount;
        }

        private int getFlippedCoinsCountInDirection((int i_row, int i_column) i_boardCell, Player i_player, int i_rowDirection, int i_columnDirection)
        {
            // define the current cell by going one cell in the given direction
            int currentRow = i_boardCell.i_row + i_rowDirection;
            int currentColumn = i_boardCell.i_column + i_columnDirection;
            int flippedCoinsCount = 0;
            eCoinColors currentCoinColor = eCoinColors.Empty;
            

            // while we still inside the board check if the current cell contains an opponent coin
            while (currentRow >= 0 && currentRow < this.Dimension && currentColumn >= 0 && currentColumn < this.Dimension)
            {
                currentCoinColor = this.State[currentRow, currentColumn];

                // If an empty cell encountered(contains an Empty coin) then no flip is possible in the given direction
                if (currentCoinColor == eCoinColors.Empty)
                {
                    flippedCoinsCount = 0;
                    break;
                }
                // If we encounter our own coin we stop iterating
                if (currentCoinColor == i_player.CoinColor)
                {
                    break;
                }

                flippedCoinsCount++;
                currentRow += i_rowDirection;
                currentColumn += i_columnDirection;
            }

            // we are flipping the opponents coins only if t we trap them from both ends
            flippedCoinsCount = currentCoinColor == i_player.CoinColor ? flippedCoinsCount : 0;
            
          
            return flippedCoinsCount;
        }

        public void UpdateBoard(Player i_player, (int, int) i_boardCell)
        {
            // Place the player's coin on the board
            this.State[i_boardCell.Item1, i_boardCell.Item2] = i_player.CoinColor;

            // Flip the opponent's coins
            foreach (var direction in sr_directions)
            {
                FlipCoinsInDirection(i_player, i_boardCell, direction.Item1, direction.Item2);
            }
        }

        public void FlipCoinsInDirection(Player i_player, (int, int) i_boardCell, int i_rowDirection, int i_columnDirection)
        {
            int row = i_boardCell.Item1 + i_rowDirection;
            int col = i_boardCell.Item2 + i_columnDirection;
            List<(int, int)> coinsToFlip = new List<(int, int)>();

            while (row >= 0 && row < this.Dimension && col >= 0 && col < this.Dimension)
            {
                eCoinColors currentColor = this.State[row, col];
                if (currentColor == eCoinColors.Empty)
                {
                    break; // No coins to flip in this direction
                }
                if (currentColor == i_player.CoinColor)
                {
                    // We've found the player's coin on the other end; flip all coins in between
                    foreach (var pos in coinsToFlip)
                    {
                        this.State[pos.Item1, pos.Item2] = i_player.CoinColor;
                    }
                    break;
                }
                coinsToFlip.Add((row, col));
                row += i_rowDirection;
                col += i_columnDirection;
            }
        }
    }
}
