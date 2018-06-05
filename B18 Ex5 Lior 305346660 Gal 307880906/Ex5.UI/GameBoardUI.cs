using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using CheckerLogic;

namespace Ex5.UI
{
    public class GameBoardUI 
    {
        public enum e_BoardSize
        {
            Small = CheckerLogic.Constants.K_SmallGameBoard,
            Medium = CheckerLogic.Constants.K_MediumGameBoard,
            Large = CheckerLogic.Constants.K_LargeGameBoard
        }

        public enum e_TypeOfBackGround
        {
            BROWN,
            WHITE,
            BLUE
        }

        private PictureBoxInTheBoard[,] m_ButtonMatrixGameBoard;
        private Image m_WhiteBackGround = Properties.Resources.whitesquare;
        private Image m_BrownBackGround = Properties.Resources.brown_square;
        private Image m_BlueBackGround = Properties.Resources.blue;
        private LinkedList<PictureBoxInTheBoard> r_Player1CheckersListOnTheBoard = new LinkedList<PictureBoxInTheBoard>();
        private LinkedList<PictureBoxInTheBoard> r_Player2CheckersListOnTheBoard = new LinkedList<PictureBoxInTheBoard>();

        public GameBoardUI(e_BoardSize i_BoardSize)
        {
            int boardSizeAsInt = (int)i_BoardSize;
            m_ButtonMatrixGameBoard = new PictureBoxInTheBoard[boardSizeAsInt, boardSizeAsInt];
            createBoard(boardSizeAsInt);
        }

        public Image WhiteBackGround
        {
            get { return m_WhiteBackGround; }
        }

        public Image BrownBackGround
        {
            get { return m_BrownBackGround; }
        }

        public Image BlueBackGround
        {
            get { return m_BlueBackGround; }
        }
     
        public PictureBoxInTheBoard[,] GetBoard
        {
            get { return m_ButtonMatrixGameBoard; }
            set { m_ButtonMatrixGameBoard = value; }
        }

        private void createBoard(int i_BoardSize)
        {
            for (int i = 0; i < i_BoardSize; i++)
            {
                for (int j = 0; j < i_BoardSize; j++)
                {
                    m_ButtonMatrixGameBoard[i, j] = new PictureBoxInTheBoard();
                    m_ButtonMatrixGameBoard[i, j].PointInTheBoard = new Point(i, j);
                    if ((j % 2 == 0 && i % 2 == 0) || (i % 2 != 0 && j % 2 != 0))
                    {
                        m_ButtonMatrixGameBoard[i, j].BackgroundImage = m_BrownBackGround;
                        m_ButtonMatrixGameBoard[i, j].BackgroundImage.Tag = e_TypeOfBackGround.BROWN;
                    }
                    else
                    {              
                        m_ButtonMatrixGameBoard[i, j].BackgroundImage = m_WhiteBackGround;
                        m_ButtonMatrixGameBoard[i, j].BackgroundImage.Tag = e_TypeOfBackGround.WHITE;
                    }

                    m_ButtonMatrixGameBoard[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                    m_ButtonMatrixGameBoard[i, j].BackColor = Color.Black;
                }
            }
        }      
    }
}