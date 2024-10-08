﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02_Othelo
{

    public class Player
    {
        private string m_Name;
        private int m_Score;
        private bool m_IsBot;
        private eCoinColors m_CoinColor;
        private Dictionary<(int, int), int> m_ValidMoves;  


        public Player(string i_name, eCoinColors i_color, bool i_isBot)
        {
            m_Name = i_name;
            m_Score = 0;
            m_CoinColor = i_color;
            m_IsBot = i_isBot;
            m_ValidMoves = new Dictionary<(int, int), int> ();
        }

        public string Name
        {
            get
            {
                return m_Name;
            }
        }

        public int Score
        {
            get
            {
                return m_Score;

            }
        }

        public bool IsBot
        {
            get
            {
                return m_IsBot;
            }
        }
        
        public eCoinColors CoinColor
        {
            get
            {
                return m_CoinColor;
            }
        }
        public Dictionary<(int, int), int> LegalMoves
        {
            get
            {
                return m_ValidMoves;
            }
        }

        public void UpdateValidMoves(Board i_board)
        {
            i_board.updatePlayerValidMoves(this);
        }

        public bool HasValidMove()
        {
            return this.LegalMoves.Count > 0;
        }

        // ai move, the computer choose the best available move in a greedy way
        public (int, int) GetAiMove()
        {
            var greedyMove = this.m_ValidMoves.OrderByDescending(m => m.Value).First().Key;
            
            return greedyMove;
        }

        public int CalculateScore(Board i_gameBoard)
        {
            int score = 0;
            for (int row = 0; row < i_gameBoard.Dimension; row++)
            {
                for (int col = 0; col < i_gameBoard.Dimension; col++)
                {
                    if (i_gameBoard.State[row, col] == this.CoinColor)
                    {
                        score++;
                    }
                }
            }
            return score;
        }
    }
}