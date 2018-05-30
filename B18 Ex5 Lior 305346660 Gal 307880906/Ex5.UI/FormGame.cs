using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Xml;
using CheckerLogic;

namespace Ex5.UI
{
    public class FormGame : Form
    {
        ////GameSettings m_FormNameLogin = new GameSettings();
        SettingsLogin m_FormNameLogin = new SettingsLogin();
        GameBoardUI m_Board;
        CheckerLogic.Game Game;
        public FormGame()
        {
            this.BackColor = Color.Azure;
            this.Text = "Gal & Lior Checker Game";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.Size = new Size(1000, 800);
            this.BackgroundImage = Image.FromFile(@"C:\Users\galma\Documents\GitHub\WinFormCheckerr\B18 Ex5 Lior 305346660 Gal 307880906\black_marble.JPG");
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.BackColor = Color.Black;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            m_FormNameLogin.ShowDialog();
            InitControls();
        }

        public SettingsLogin GetNameLogin
        {
            get { return m_FormNameLogin; }
            set { m_FormNameLogin = value; }
        }

        private void InitControls()
        {
            GameBoardUI.e_BoardSize i_BoardSize = m_FormNameLogin.BoardSizeSelection;
            string player1Name = m_FormNameLogin.Player1Name;
            string player2Name = m_FormNameLogin.Player2Name;
            Player Player1 = new Player(player1Name, (int)i_BoardSize, 1, false);
            Player Player2 = new Player(player2Name, (int)i_BoardSize, 2, false);
            Player1.InitAllTheCheckersOfOnePlayer((int)i_BoardSize, Player.e_LocationOfThePlayer.DOWN);
            Player2.InitAllTheCheckersOfOnePlayer((int)i_BoardSize, Player.e_LocationOfThePlayer.UP);
            Game = new Game(Player1, Player2, (int)i_BoardSize);
            m_Board = new GameBoardUI(i_BoardSize);
            initCheckers(Player1, Player2, Game);
            for (int i = 0; i < (int)i_BoardSize; i++)
            {
                for (int j = 0; j < (int)i_BoardSize; j++)
                {
                    m_Board.GetBoard[i, j].Size = new Size(60, 60);
                    m_Board.GetBoard[i, j].Location = new Point((i) * 60, (j + 1) * 60);
                    this.Controls.Add(m_Board.GetBoard[i, j]);
                }
            }
        }


        private void initCheckers(Player i_Player1, Player i_Player2, Game i_Game)
        {
            //foreach (Checker checker in i_Player1.ListOfCheckers)
            //{
            //    int X = checker.PositintOfTheChecker.Coordinate.X;
            //    int Y = checker.PositintOfTheChecker.Coordinate.Y;
            //    Image checkerImg = Image.FromFile(@"C:\Users\ulmer\Desktop\extractione");
            //    m_Board.GetBoard[Y, X].Image = checkerImg;
            //}

            for(int i = 0; i < i_Game.BoradSize; i++ )
            {
                for(int j = 0; j < i_Game.BoradSize; j++ )
                {
                    if(i_Game.GetTestingMatrix[j,i] == (char)Checker.Symbol.O)
                    {
                        Image checkerImg = Image.FromFile(@"C:\Users\galma\Documents\GitHub\WinFormCheckerr\B18 Ex5 Lior 305346660 Gal 307880906\black_soldier.png").GetThumbnailImage(55,55,null, IntPtr.Zero);
                        m_Board.GetBoard[i, j].Image = checkerImg;
                        //m_Board.GetBoard[i, j].ImageAlign = ContentAlignment.MiddleCenter;
                        //m_Board.GetBoard[i, j].
                    }
                    else if (i_Game.GetTestingMatrix[j, i] == (char)Checker.Symbol.X)
                    {
                        Image checkerImg = Image.FromFile(@"C:\Users\galma\Documents\GitHub\WinFormCheckerr\B18 Ex5 Lior 305346660 Gal 307880906\red_soldier.png").GetThumbnailImage(55, 55, null, IntPtr.Zero);
                        m_Board.GetBoard[i, j].Image = checkerImg;
                        //m_Board.GetBoard[i, j].ImageAlign = ContentAlignment.MiddleCenter;
                    }
                }
            }
        }

        ////private void InitializeComponent()
        ////{
        ////    this.SuspendLayout();
        ////    // 
        ////    // FormGame
        ////    // 
        ////    this.AutoScroll = true;
        ////    this.ClientSize = new System.Drawing.Size(816, 509);
        ////    this.HelpButton = true;
        ////    this.Name = "FormGame";
        ////    this.Load += new System.EventHandler(this.FormGame_Load);
        ////    this.Click += new System.EventHandler(this.FormGame_Load);
        ////    this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormGame_MouseDown);
        ////    this.ResumeLayout(false);

        ////}

        private void FormGame_Load(object sender, EventArgs e)
        {

        }

        private void FormGame_MouseDown(object sender, MouseEventArgs e)
        {

        }

        ////private void InitializeComponent()
        ////{
        ////    this.SuspendLayout();
        ////    // 
        ////    // FormGame
        ////    // 
        ////    this.BackgroundImage = global::Ex5.UI.Properties.Resources.black_marble;
        ////    this.ClientSize = new System.Drawing.Size(861, 540);
        ////    this.Name = "FormGame";
        ////    this.ResumeLayout(false);

        ////}
    }
}