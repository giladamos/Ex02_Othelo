using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02_Othelo
{
    public class GameRunner
    {
        public Player m_Plauer1 = null;
        public Player m_Plauer2 = null;
        public Player m_CurrentPlauer = null;
        public Board m_GameBoard = null;
        

        public GameRunner()
        {
            string player1Name = "";
            string plauer2Name = "";
            string gameMode = "";
            int boardSize = 0;
            bool isMultiplayer = false;

            Display.SetUserPrefernces(out player1Name, out plauer2Name, out boardSize, out isMultiplayer);
            m_Plauer1 = new Player(player1Name, eCoinColor.Black, false);
            m_Plauer2 = new Player(plauer2Name, eCoinColor.White, !isMultiplayer);
            m_CurrentPlauer = m_Plauer1;
            m_GameBoard = new Board(boardSize);



        }

        public void StartGame()
        {
            // While game still being played

            // Start the game by printing the initial game board
            Display.PrintCurrentBoard(m_GameBoard);
            // Play a turn 



            


        }

        // preform a turn by the current player using the display play turn method to print the turn played, we will need an Entries class to preform casting on the given entry
        private static void playTurn()
        {

        }

    }
}
