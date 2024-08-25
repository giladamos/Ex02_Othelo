using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02_Othelo
{
    public enum eCoinColor
    {
        Black = -1,
        White = 1,
    }

    public class Player
    {
        private string m_Name;
        private int m_Score;
        private bool m_IsBot;
        private eCoinColor m_CoinColor;


        public Player(string i_name, eCoinColor i_color, bool i_isBot)
        {
            m_Name = i_name;
            m_Score = 0;
            m_CoinColor = i_color;
            m_IsBot = i_isBot;
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
        
        public eCoinColor CoinColor
        {
            get
            {
                return m_CoinColor;
            }
        }
    
    }

}