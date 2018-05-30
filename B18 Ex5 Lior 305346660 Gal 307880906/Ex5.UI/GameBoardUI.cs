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
        PictureBoxInTheBoard[,] m_ButtonMatrixGameBoard;
        Image m_WhiteBackGround = Image.FromFile(@"C:\whitesquare.PNG");
        Image m_BrownBackGround = Image.FromFile(@"C:\brown_square.PNG");
        Image m_BlueBackGround = Image.FromFile(@"C:\blue.JPG");

        LinkedList<PictureBoxInTheBoard> r_Player1CheckersListOnTheBoard = new LinkedList<PictureBoxInTheBoard>();
        LinkedList<PictureBoxInTheBoard> r_Player2CheckersListOnTheBoard = new LinkedList<PictureBoxInTheBoard>();
        //CheckerLogic.Game Game;
        public GameBoardUI(e_BoardSize i_BoardSize)
        {
            int boardSizeAsInt = (int)i_BoardSize;
            m_ButtonMatrixGameBoard = new PictureBoxInTheBoard[boardSizeAsInt, boardSizeAsInt];
            CreateBoard(boardSizeAsInt);
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
        //public LinkedList<PictureBoxInTheBoard> Player1CheckersListOnTheBoard
        //{
        //    get { return r_Player1CheckersListOnTheBoard; }
        //}

        //public LinkedList<PictureBoxInTheBoard> Player2CheckersListOnTheBoard
        //{
        //    get { return r_Player2CheckersListOnTheBoard; }
        //}

        public PictureBoxInTheBoard[,] GetBoard
        {
            get { return m_ButtonMatrixGameBoard; }
            set { m_ButtonMatrixGameBoard = value; }
        }

        private void CreateBoard(int i_BoardSize)
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
                        //m_ButtonMatrixGameBoard[i, j].Click += new EventHandler(PictureBoxInTheBoard_Click);
                        //m_ButtonMatrixGameBoard[i, j].MouseClick += new MouseEventHandler(PictureBoxInTheBoard_MouseClick);
                    }
                    m_ButtonMatrixGameBoard[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                    m_ButtonMatrixGameBoard[i, j].BackColor = Color.Black;
                   // m_ButtonMatrixGameBoard[i, j].MouseEnter += new EventHandler(panel1_MouseEnter);

                  
                }
            }

        }

       
    }
}