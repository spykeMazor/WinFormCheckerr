using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Windows.Forms;
using System.Drawing;
using System.Xml;
using CheckerLogic;

namespace Ex5.UI
{
    public class FormGame : Form
    {
        public enum e_TypeUICheckers
        {
            BlackChecker,
            BlackQueen,
            RedChecker,
            RedQueen

        }

        SettingsLogin m_FormNameLogin = new SettingsLogin();
        Image m_BlackChackerImage;
        Image m_RedChackerImage;
        Image m_BlackQueenChackerImage;
        Image m_RedQueenChackerImage;
        GameBoardUI m_Board;
        CheckerLogic.Game m_Game;
        Point m_LastMoveFrom = new Point();
        Point m_LastMoveTo = new Point();
        private bool m_WasMove = false;
        private bool m_CurrentPlayer = false; //// False = Player1, True = Player2
        List<KeyValuePair<Point, Point>> m_AllTheClickableSquareReadyToMove = new List<KeyValuePair<Point, Point>>();
        private bool m_wasAttack = false;
        public FormGame()
        {
            loadCheckersPics();
            this.BackColor = Color.Azure;
            this.Text = "Gal & Lior Checker Game";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.Size = new Size(1000, 800);
            this.BackgroundImage = Image.FromFile(@"C:\black_marble.JPG");
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.BackColor = Color.Black;

        }



        private void loadCheckersPics()
        {
            m_BlackChackerImage = Image.FromFile(@"C:\black_soldier.png").GetThumbnailImage(55, 55, null, IntPtr.Zero);
            m_BlackChackerImage.Tag = e_TypeUICheckers.BlackChecker;
            m_RedChackerImage = Image.FromFile(@"C:\red_soldier.png").GetThumbnailImage(55, 55, null, IntPtr.Zero);
            m_RedChackerImage.Tag = e_TypeUICheckers.RedChecker;
            m_BlackQueenChackerImage = Image.FromFile(@"C:\black_king.png").GetThumbnailImage(55, 55, null, IntPtr.Zero);
            m_BlackQueenChackerImage.Tag = e_TypeUICheckers.BlackQueen;
            m_RedQueenChackerImage = Image.FromFile(@"C:\red_queen.png").GetThumbnailImage(55, 55, null, IntPtr.Zero);
            m_RedQueenChackerImage.Tag = e_TypeUICheckers.RedQueen;
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
            Player1.InitAllTheCheckersOfOnePlayer((int)i_BoardSize, Player.e_LocationOfThePlayer.UP);
            Player2.InitAllTheCheckersOfOnePlayer((int)i_BoardSize, Player.e_LocationOfThePlayer.DOWN);
            m_Game = new Game(Player1, Player2, (int)i_BoardSize);
            m_Board = new GameBoardUI(i_BoardSize);
            invokeTheBoard();
            initCheckers();
            for (int i = 0; i < (int)i_BoardSize; i++)
            {
                for (int j = 0; j < (int)i_BoardSize; j++)
                {
                    m_Board.GetBoard[i, j].Size = new Size(60, 60);
                    m_Board.GetBoard[i, j].Location = new Point((i) * 60, (j + 1) * 60);
                    this.Controls.Add(m_Board.GetBoard[i, j]);
                }
            }

            m_Game.UpdateMoveList(Player.e_LocationOfThePlayer.UP);
            invokeClickOnChecker(m_Game.MoveListOfPlayer1);

        }

        private void invokeTheBoard()
        {
            for (int i = 0; i < m_Game.BoradSize; i++)
            {
                for (int j = 0; j < m_Game.BoradSize; j++)
                {
                    if (m_Board.GetBoard[i, j].BackgroundImage.Tag.ToString() == GameBoardUI.e_TypeOfBackGround.WHITE.ToString())
                    {
                        m_Board.GetBoard[i, j].Click += new EventHandler(PictureBoxInTheBoard_Click);
                        m_Board.GetBoard[i, j].Enabled = false;
                    }
                }
            }
        }
        private void initCheckers()
        {

            for (int i = 0; i < m_Game.BoradSize; i++)
            {
                for (int j = 0; j < m_Game.BoradSize; j++)
                {
                    if (m_Game.GetTestingMatrix[j, i] == '\0')
                    {
                        m_Board.GetBoard[i, j].Image = null;
                    }
                    else
                    {
                        if (m_Game.GetTestingMatrix[j, i] == (char)Checker.Symbol.O)
                        {
                            // Image checkerImg = m_BlackChackerImage;
                            m_Board.GetBoard[i, j].Image = m_BlackChackerImage;
                        }
                        else if (m_Game.GetTestingMatrix[j, i] == (char)Checker.Symbol.X)
                        {
                            //Image checkerImg = m_RedChackerImage;
                            m_Board.GetBoard[i, j].Image = m_RedChackerImage;
                        }
                    }

                }
            }
        }

        private string convertCheckerPositionPointToSquareOfLogic(Point i_CheckerPoint)
        {
            PointOfPosition tempPointPosition = new PointOfPosition();
            tempPointPosition.X = i_CheckerPoint.X;
            tempPointPosition.Y = i_CheckerPoint.Y;
            return Position.ConvertPointToSquare(tempPointPosition);
        }

        private Point converCheckerPositionToPoint(string i_CheckerPosition)
        {
            PointOfPosition tempPointPosition = new PointOfPosition();
            tempPointPosition = Position.ConvertSqureToPoint(i_CheckerPosition);
            Point checkerPointInTheUIBoard = new Point(tempPointPosition.X, tempPointPosition.Y);
            return checkerPointInTheUIBoard;
        }

        private void invokeClickOnChecker(LinkedList<KeyValuePair<string, string>> i_MoveList)
        {

            m_AllTheClickableSquareReadyToMove.Clear();
            Point PointMoveFrom = new Point();
            Point PointMoveTo = new Point();
            Point lastPointMoveFrom = new Point(-1, -1);

            foreach (KeyValuePair<string, string> kvp in i_MoveList)
            {
                PointMoveFrom = converCheckerPositionToPoint(kvp.Key);
                PointMoveTo = converCheckerPositionToPoint(kvp.Value);
                m_AllTheClickableSquareReadyToMove.Add(new KeyValuePair<Point, Point>(PointMoveFrom, PointMoveTo));
                if (lastPointMoveFrom != converCheckerPositionToPoint(kvp.Key))
                {
                    lastPointMoveFrom = converCheckerPositionToPoint(kvp.Key);
                    m_Board.GetBoard[PointMoveFrom.X, PointMoveFrom.Y].Enabled = true;
                }

            }
        }

        public void PictureBoxInTheBoard_Click(object sender, EventArgs e)
        {
            PictureBoxInTheBoard picAsSender = sender as PictureBoxInTheBoard;
            if (picAsSender.Image != null)
            {
                Image DefaultBackGroundImage = m_Board.WhiteBackGround;
                if (picAsSender.BackgroundImage.Tag.ToString() == GameBoardUI.e_TypeOfBackGround.WHITE.ToString())
                {
                    picAsSender.BackgroundImage = m_Board.BlueBackGround;
                    picAsSender.BackgroundImage.Tag = GameBoardUI.e_TypeOfBackGround.BLUE;
                    m_LastMoveFrom = picAsSender.PointInTheBoard;
                    invokeAllTheOptionalMoveSquare(picAsSender);
                }
                else
                {
                    picAsSender.BackgroundImage = DefaultBackGroundImage;
                    picAsSender.BackgroundImage.Tag = GameBoardUI.e_TypeOfBackGround.WHITE;
                    disableAllMoveToClickableSquare();
                    foreach (KeyValuePair<Point, Point> kvp in m_AllTheClickableSquareReadyToMove)
                    {
                        m_Board.GetBoard[kvp.Key.X, kvp.Key.Y].Enabled = true;
                    }
                }
            }

            else
            {
                //PictureBoxInTheBoard picAsSender = sender as PictureBoxInTheBoard;
                afterMoving(picAsSender);
            }
        }

        private void afterMoving(PictureBoxInTheBoard i_CurrentPicBoxThatMoveTo)
        {
            m_LastMoveTo = i_CurrentPicBoxThatMoveTo.PointInTheBoard;
            m_WasMove = true;

            returnSqureToEmpty();
            if (m_WasMove)
            {
                if (!m_CurrentPlayer)
                {
                    m_Game.MoveTheCheckerOfTheCorecctPlayer(m_Game.Player1,
                  m_Game.Player2,
                  convertCheckerPositionPointToSquareOfLogic(m_LastMoveFrom),
                  convertCheckerPositionPointToSquareOfLogic(m_LastMoveTo),
                 ref m_wasAttack);
                    initCheckers();
                    m_WasMove = false;
                    if (!m_wasAttack)
                    {
                        m_CurrentPlayer = true;////---> now player2 turn
                        invokeClickOnChecker(m_Game.MoveListOfPlayer2);
                    }
                    else
                    {////--->was attack turn stay in Player1
                        invokeClickOnChecker(m_Game.MoveListOfPlayer1);
                    }
                }
                else
                {
                    m_Game.MoveTheCheckerOfTheCorecctPlayer(m_Game.Player2,
                        m_Game.Player1,
                        convertCheckerPositionPointToSquareOfLogic(m_LastMoveFrom),
                        convertCheckerPositionPointToSquareOfLogic(m_LastMoveTo),
                        ref m_wasAttack);
                    initCheckers();
                    m_WasMove = false;
                    if (!m_wasAttack)
                    {
                        m_CurrentPlayer = false;////---> now player1 turn
                        invokeClickOnChecker(m_Game.MoveListOfPlayer1);
                    }
                    else
                    {////--->was attack turn stay in Player2
                        invokeClickOnChecker(m_Game.MoveListOfPlayer2);
                    }
                }
            }
        }

        private void invokeAllTheOptionalMoveSquare(PictureBoxInTheBoard i_PicAsSender)
        {
            Point blueSquare = new Point();           
            blueSquare.X = i_PicAsSender.PointInTheBoard.X;
            blueSquare.Y = i_PicAsSender.PointInTheBoard.Y;

            foreach (KeyValuePair<Point, Point> kpv in m_AllTheClickableSquareReadyToMove)
            {
                if (kpv.Key != i_PicAsSender.PointInTheBoard)
                {
                    m_Board.GetBoard[kpv.Key.X, kpv.Key.Y].Enabled = false;
                }
            }

            if (i_PicAsSender.Image.Tag.ToString() == e_TypeUICheckers.BlackChecker.ToString() ||
                i_PicAsSender.Image.Tag.ToString() == e_TypeUICheckers.BlackQueen.ToString())
            {
                if (m_Game.AttackListOfPlayer1.Count > 0)
                {
                    enableMoveToSquare(m_Game.AttackListOfPlayer1, blueSquare);
                }
                else
                {
                    enableMoveToSquare(m_Game.MoveListOfPlayer1, blueSquare);
                }
            }
            else
            {
                if (m_Game.AttackListOfPlayer1.Count > 0)
                {
                    enableMoveToSquare(m_Game.AttackListOfPlayer2, blueSquare);
                }
                else
                {
                    enableMoveToSquare(m_Game.MoveListOfPlayer2, blueSquare);
                }
            }
        }

        private void enableMoveToSquare(LinkedList<KeyValuePair<string, string>> i_MoveListOfTheCurrentPlayer, Point i_BlueSquare)
        {
            

            foreach (KeyValuePair<string, string> kvp in i_MoveListOfTheCurrentPlayer)
            {
                if (i_BlueSquare == converCheckerPositionToPoint(kvp.Key))
                {
                    Point optionalSquareToMove = new Point();
                    optionalSquareToMove = converCheckerPositionToPoint(kvp.Value);
                    m_Board.GetBoard[optionalSquareToMove.X, optionalSquareToMove.Y].Enabled = true;
                }
                else
                {

                }
            }
        }
        
        private void returnSqureToEmpty()
        {
            foreach (KeyValuePair<Point, Point> kvp in m_AllTheClickableSquareReadyToMove)
            {
                m_Board.GetBoard[kvp.Key.X, kvp.Key.Y].Enabled = false;
                m_Board.GetBoard[kvp.Value.X, kvp.Value.Y].Enabled = false;
                if (m_Board.GetBoard[kvp.Key.X, kvp.Key.Y].BackgroundImage.Tag.ToString() == GameBoardUI.e_TypeOfBackGround.BLUE.ToString())
                {
                    m_Board.GetBoard[kvp.Key.X, kvp.Key.Y].BackgroundImage = m_Board.WhiteBackGround;
                    m_Board.GetBoard[kvp.Key.X, kvp.Key.Y].BackgroundImage.Tag = GameBoardUI.e_TypeOfBackGround.WHITE;
                }
            }
        }

        private void disableAllMoveToClickableSquare()
        {
            foreach (KeyValuePair<Point, Point> kvp in m_AllTheClickableSquareReadyToMove)
            {
                m_Board.GetBoard[kvp.Value.X, kvp.Value.Y].Enabled = false;
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FormGame
            // 
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(816, 509);
            this.HelpButton = true;
            this.Name = "FormGame";
            this.Load += new System.EventHandler(this.FormGame_Load);
            this.Click += new System.EventHandler(this.FormGame_Load);
            this.ResumeLayout(false);

        }

        private void FormGame_Load(object sender, EventArgs e)
        {

        }

        


    }
}