using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02_Othelo
{
    public class Board
    {
        private int m_Dimension = 0;
        private eCoinColor[,] m_State = null;
        private bool m_HasMoreSpace = true;

        public Board(int i_BoardSize)
        {
            m_Dimension = i_BoardSize;
            m_State = new eCoinColor[m_Dimension, m_Dimension];

            // init board with middle tokens
            m_State[(m_Dimension / 2) - 1, (m_Dimension / 2) - 1] = eCoinColor.White;
            m_State[(m_Dimension / 2), (m_Dimension / 2)] = eCoinColor.White;
            m_State[(m_Dimension / 2) - 1, (m_Dimension / 2)] = eCoinColor.Black;
            m_State[(m_Dimension / 2), (m_Dimension / 2) - 1] = eCoinColor.Black;
        }

        public eCoinColor[,] State
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
         
    }
}
