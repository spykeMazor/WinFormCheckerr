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

        PictureBox[,] m_ButtonMatrixGameBoard;
        CheckerLogic.Game Game;
        public GameBoardUI(e_BoardSize i_BoardSize)
        {
            int boardSizeAsInt = (int)i_BoardSize;
            m_ButtonMatrixGameBoard = new PictureBox[boardSizeAsInt, boardSizeAsInt];
            CreateBoard(boardSizeAsInt);
        }

        public PictureBox[,] GetBoard
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
                    m_ButtonMatrixGameBoard[i, j] = new PictureBox();
                    if ((j % 2 == 0 && i % 2 == 0) || (i % 2 != 0 && j % 2 != 0))
                    {
                        m_ButtonMatrixGameBoard[i, j].BackgroundImage = Image.FromFile(@"C:\Users\galma\Desktop\B18 Ex5 Lior 305346660 Gal 307880906\brown_square.PNG");
                    }
                    ////else if (i % 2 != 0 && j % 2 != 0)
                    ////{
                    ////    m_ButtonMatrixGameBoard[i, j].BackgroundImage = Image.FromFile(@"C:\Users\galma\Desktop\B18 Ex5 Lior 305346660 Gal 307880906\brown_square.PNG");
                    ////}
                    else
                    {
                        m_ButtonMatrixGameBoard[i, j].BackgroundImage = Image.FromFile(@"C:\Users\galma\Desktop\B18 Ex5 Lior 305346660 Gal 307880906\whitesquare.PNG");
                    }
                    m_ButtonMatrixGameBoard[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                    m_ButtonMatrixGameBoard[i, j].BackColor = Color.Black;
                   // m_ButtonMatrixGameBoard[i, j].MouseEnter += new EventHandler(panel1_MouseEnter);

                    //m_PictureBoxGameBoard[i, j].BackColor = Color.Transparent;
                }
            }

            //public void panel1_MouseEnter(object sender, EventArgs e)
            //{
            //    // Update the mouse event label to indicate the MouseEnter event occurred.
            //    var senderButton = (Button)sender;
            //    senderButton.Width += 30;
            //}

            //for (int i = 0; i < i_BoardSize; i++)
            //{
            //    for (int j = 0; j < i_BoardSize; j++)
            //    {
            //        m_ButtonMatrixGameBoard[i, j].Size = new Size(60, 60);
            //        m_ButtonMatrixGameBoard[i, j].Location = new Point((i) * 60, (j + 1) * 60);
            //        //this.Controls.Add(m_ButtonMatrixGameBoard[i, j]);
            //    }

            //}
        }
    }
}