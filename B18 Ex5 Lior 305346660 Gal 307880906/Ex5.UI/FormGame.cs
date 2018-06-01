using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Windows.Forms;
using System.Drawing;
using System.Xml;
using CheckerLogic;
using System.Threading;

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
        private bool wantsToPlay = true;
        private bool m_WasMove = false;
        private bool m_CurrentPlayer = false; //// False = Player1, True = Player2
        List<KeyValuePair<Point, Point>> m_AllTheClickableSquareReadyToMove = new List<KeyValuePair<Point, Point>>();
        private bool m_wasAttack = false;
        private bool m_hasAnotherAttack = false;
        private Label headLine;
        private Label labelScore1;
        private Label labelScore2;
        private SplitContainer splitContainer1;
        private Label labelPlayer1Name;
        private Label labelPlayer2Name;
        private Button buttonQuit;
        private Button buttonStartOver;
        public void FormGameStart()
        {
            m_FormNameLogin.ShowDialog();
            if (m_FormNameLogin.DialogResult == DialogResult.OK)
            {
                loadCheckersPics();
                InitializeComponent();
            }
            else
            {
                wantsToPlay = false;
            }
        }

        public bool WantsToPlay
        {
            get { return wantsToPlay; }
        }

        private void loadCheckersPics()
        {
            ////m_BlackChackerImage = Image.FromFile(@"C:\black_soldier.png").GetThumbnailImage(55, 55, null, IntPtr.Zero);
            ////m_BlackChackerImage.Tag = e_TypeUICheckers.BlackChecker;
            ////m_RedChackerImage = Image.FromFile(@"C:\red_soldier.png").GetThumbnailImage(55, 55, null, IntPtr.Zero);
            ////m_RedChackerImage.Tag = e_TypeUICheckers.RedChecker;
            ////m_BlackQueenChackerImage = Image.FromFile(@"C:\black_king.png").GetThumbnailImage(55, 55, null, IntPtr.Zero);
            ////m_BlackQueenChackerImage.Tag = e_TypeUICheckers.BlackQueen;
            ////m_RedQueenChackerImage = Image.FromFile(@"C:\red_queen.png").GetThumbnailImage(55, 55, null, IntPtr.Zero);
            ////m_RedQueenChackerImage.Tag = e_TypeUICheckers.RedQueen;
            m_BlackChackerImage = Properties.Resources.black_soldier.GetThumbnailImage(55, 55, null, IntPtr.Zero);
            m_BlackChackerImage.Tag = e_TypeUICheckers.BlackChecker;
            m_RedChackerImage = Properties.Resources.red_soldier.GetThumbnailImage(55, 55, null, IntPtr.Zero);
            m_RedChackerImage.Tag = e_TypeUICheckers.RedChecker;
            m_BlackQueenChackerImage = Properties.Resources.black_king.GetThumbnailImage(55, 55, null, IntPtr.Zero);
            m_BlackQueenChackerImage.Tag = e_TypeUICheckers.BlackQueen;
            m_RedQueenChackerImage = Properties.Resources.red_queen.GetThumbnailImage(55, 55, null, IntPtr.Zero);
            m_RedQueenChackerImage.Tag = e_TypeUICheckers.RedQueen;
        }



        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
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
            int boardSizeAsInt = (int)i_BoardSize;
            Player Player1 = new Player(player1Name, boardSizeAsInt, 1, false);
            Player Player2 = new Player(player2Name, boardSizeAsInt, 2, !m_FormNameLogin.ComputerOrNot);
            Player1.InitAllTheCheckersOfOnePlayer(boardSizeAsInt, Player.e_LocationOfThePlayer.UP);
            Player2.InitAllTheCheckersOfOnePlayer(boardSizeAsInt, Player.e_LocationOfThePlayer.DOWN);
            m_Game = new Game(Player1, Player2, boardSizeAsInt);
           
            m_Board = new GameBoardUI(i_BoardSize);
            invokeTheBoard();
            initCheckers();
            for (int i = 0; i < boardSizeAsInt; i++)
            {
                for (int j = 0; j < boardSizeAsInt; j++)
                {
                    m_Board.GetBoard[i, j].Size = new Size(60, 60);
                    m_Board.GetBoard[i, j].Location = new Point((i + 1) * 60, (j + 1) * 60);
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
        //// Convert from "PositionPoint"  to Drawing.Point obj

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
                afterMoving(picAsSender);
            }
        }

        private void afterMoving(PictureBoxInTheBoard i_CurrentPicBoxThatMoveTo)
        {
           
            if (i_CurrentPicBoxThatMoveTo != null)
            {
                m_LastMoveTo = i_CurrentPicBoxThatMoveTo.PointInTheBoard;
            }
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
                 ref m_wasAttack, ref m_hasAnotherAttack);
                    initCheckers();
                    this.labelScore1.Text = m_Game.Player1Score().ToString();
                    this.labelScore2.Text = m_Game.Player2Score().ToString();
                    this.Update();
                    m_WasMove = false;
                    if (!m_hasAnotherAttack)
                    {
                        if (m_Game.Player2.MachineOrNot)
                        { 
                            computerMove();
                        }
                        else
                        {
                            m_CurrentPlayer = true;////---> now player2 turn
                            if (m_Game.AttackListOfPlayer2.Count > 0)
                            {
                                invokeClickOnChecker(m_Game.AttackListOfPlayer2);
                            }
                            else
                            {
                                invokeClickOnChecker(m_Game.MoveListOfPlayer2);
                            }
                        }
                    }
                    else
                    {////--->Combo one more attack -> turn stay in Player1 
                        invokeClickOnChecker(m_Game.AttackListOfPlayer1);
                    }
                }
                else
                { 
                        m_Game.MoveTheCheckerOfTheCorecctPlayer(m_Game.Player2,
                            m_Game.Player1,
                            convertCheckerPositionPointToSquareOfLogic(m_LastMoveFrom),
                            convertCheckerPositionPointToSquareOfLogic(m_LastMoveTo),
                            ref m_wasAttack, ref m_hasAnotherAttack);
                        initCheckers();
                    m_WasMove = false;
                    if (!m_hasAnotherAttack)
                    {
                        m_CurrentPlayer = false;////---> now player1 turn
                        if (m_Game.AttackListOfPlayer1.Count > 0)
                        {
                            invokeClickOnChecker(m_Game.AttackListOfPlayer1);
                        }
                        else
                        {
                            invokeClickOnChecker(m_Game.MoveListOfPlayer1);
                        }
                    }
                    else
                    {////--->Combo one more attack -> turn stay in Player2                 
                        invokeClickOnChecker(m_Game.AttackListOfPlayer2);

                    }
                }
            
            }
        }

        private void computerMove()
        { /// computer moving managment
            
            KeyValuePair<string, string> computerNextMove = new KeyValuePair<string, string>();
            computerNextMove = m_Game.SelectRandomMove();

            m_Game.MoveTheCheckerOfTheCorecctPlayer(m_Game.Player2,
                       m_Game.Player1,
                     computerNextMove.Key,
                       computerNextMove.Value,
                       ref m_wasAttack, ref m_hasAnotherAttack);
            Thread.Sleep(1000);
            initCheckers();
            this.labelScore1.Text = m_Game.Player1Score().ToString();
            this.labelScore2.Text = m_Game.Player2Score().ToString();
            this.Update();
            m_WasMove = false;
            if (!m_hasAnotherAttack)
            {
                m_CurrentPlayer = false;////---> now player1 turn
                if (m_Game.AttackListOfPlayer1.Count > 0)
                {
                    invokeClickOnChecker(m_Game.AttackListOfPlayer1);
                }
                else
                {
                    invokeClickOnChecker(m_Game.MoveListOfPlayer1);
                }
            }
            else
            {
                computerMove();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGame));
            this.headLine = new System.Windows.Forms.Label();
            this.labelScore1 = new System.Windows.Forms.Label();
            this.labelScore2 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.labelPlayer1Name = new System.Windows.Forms.Label();
            this.labelPlayer2Name = new System.Windows.Forms.Label();
            this.buttonQuit = new System.Windows.Forms.Button();
            this.buttonStartOver = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // headLine
            // 
            this.headLine.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.headLine.AutoSize = true;
            this.headLine.BackColor = System.Drawing.Color.Yellow;
            this.headLine.Font = new System.Drawing.Font("MV Boli", 19F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(177)), true);
            this.headLine.ForeColor = System.Drawing.Color.DodgerBlue;
            this.headLine.Location = new System.Drawing.Point(298, 9);
            this.headLine.Name = "headLine";
            this.headLine.Size = new System.Drawing.Size(469, 43);
            this.headLine.TabIndex = 1;
            this.headLine.Text = "Gal & Lior Checkers Game";
            this.headLine.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelScore1
            // 
            this.labelScore1.AutoSize = true;
            this.labelScore1.BackColor = System.Drawing.Color.Transparent;
            this.labelScore1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelScore1.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.labelScore1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.labelScore1.Location = new System.Drawing.Point(100, 110);
            this.labelScore1.Name = "labelScore1";
            this.labelScore1.Size = new System.Drawing.Size(36, 37);
            this.labelScore1.TabIndex = 3;
            this.labelScore1.Text = "0";
            this.labelScore1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelScore2
            // 
            this.labelScore2.AutoSize = true;
            this.labelScore2.BackColor = System.Drawing.Color.Transparent;
            this.labelScore2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelScore2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelScore2.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.labelScore2.Location = new System.Drawing.Point(100, 110);
            this.labelScore2.Name = "labelScore2";
            this.labelScore2.Size = new System.Drawing.Size(36, 37);
            this.labelScore2.TabIndex = 4;
            this.labelScore2.Text = "0";
            this.labelScore2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.Location = new System.Drawing.Point(688, 106);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 

            ////this.splitContainer1.Panel1.BackgroundImage = Image.FromFile(@"C:\black_soldier.PNG");
            this.splitContainer1.Panel1.BackgroundImage = Properties.Resources.black_soldier;

            this.splitContainer1.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.splitContainer1.Panel1.Controls.Add(this.labelScore1);
            this.splitContainer1.Panel1.Controls.Add(this.labelPlayer1Name);
            // 
            // splitContainer1.Panel2
            // 
            ////this.splitContainer1.Panel2.BackgroundImage = Image.FromFile(@"C:\red_soldier.PNG");
            this.splitContainer1.Panel2.BackgroundImage = Properties.Resources.red_soldier; 

            this.splitContainer1.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.splitContainer1.Panel2.Controls.Add(this.labelScore2);
            this.splitContainer1.Panel2.Controls.Add(this.labelPlayer2Name);
            this.splitContainer1.Size = new System.Drawing.Size(218, 354);
            this.splitContainer1.SplitterDistance = 177;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 3;
            // 
            // labelPlayer1Name
            // 
            this.labelPlayer1Name.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelPlayer1Name.BackColor = System.Drawing.Color.Transparent;
            this.labelPlayer1Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelPlayer1Name.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.labelPlayer1Name.Location = new System.Drawing.Point(61, 55);
            this.labelPlayer1Name.Name = "labelPlayer1Name";
            this.labelPlayer1Name.Size = new System.Drawing.Size(101, 40);
            this.labelPlayer1Name.TabIndex = 2;
            this.labelPlayer1Name.Text = m_FormNameLogin.Player1Name;
            this.labelPlayer1Name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelPlayer2Name
            // 
            this.labelPlayer2Name.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelPlayer2Name.BackColor = System.Drawing.Color.Transparent;
            this.labelPlayer2Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelPlayer2Name.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelPlayer2Name.Location = new System.Drawing.Point(70, 57);
            this.labelPlayer2Name.Name = "labelPlayer2Name";
            this.labelPlayer2Name.Size = new System.Drawing.Size(69, 40);
            this.labelPlayer2Name.TabIndex = 3;
            this.labelPlayer2Name.Text = m_FormNameLogin.Player2Name;
            this.labelPlayer2Name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonQuit
            // 
            this.buttonQuit.AutoSize = true;
            this.buttonQuit.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonQuit.Cursor = System.Windows.Forms.Cursors.Help;
            this.buttonQuit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonQuit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.buttonQuit.Location = new System.Drawing.Point(832, 608);
            this.buttonQuit.Name = "buttonQuit";
            this.buttonQuit.Size = new System.Drawing.Size(121, 33);
            this.buttonQuit.TabIndex = 4;
            this.buttonQuit.Text = "QUIT";
            this.buttonQuit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonQuit.UseVisualStyleBackColor = false;
            this.buttonQuit.Click += new System.EventHandler(this.buttonQuit_Click);
            // 
            // buttonStartOver
            // 
            this.buttonStartOver.AutoSize = true;
            this.buttonStartOver.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonStartOver.Cursor = System.Windows.Forms.Cursors.Help;
            this.buttonStartOver.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonStartOver.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.buttonStartOver.Location = new System.Drawing.Point(685, 608);
            this.buttonStartOver.Name = "buttonStartOver";
            this.buttonStartOver.Size = new System.Drawing.Size(131, 33);
            this.buttonStartOver.TabIndex = 5;
            this.buttonStartOver.Text = "START OVER";
            this.buttonStartOver.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonStartOver.UseVisualStyleBackColor = false;
            this.buttonStartOver.Click += new System.EventHandler(this.buttonStartOver_Click);
            // 
            // FormGame
            // 
            this.BackColor = System.Drawing.Color.Black;
            ////this.BackgroundImage = Image.FromFile(@"C:\damka3d.jpg");
            this.BackgroundImage = Properties.Resources.damka3d;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(978, 694);
            this.Controls.Add(this.buttonStartOver);
            this.Controls.Add(this.buttonQuit);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.headLine);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormGame";
            this.Text = "Gal & Lior Checker Game";
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void buttonQuit_Click(object sender, EventArgs e)
        {
            VerifyForm quitForm = new VerifyForm(ConstantsUI.k_QuitMessage);
            quitForm.ShowDialog();
            if (quitForm.DialogResult == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void buttonStartOver_Click(object sender, EventArgs e)
        {
            VerifyForm startOver = new VerifyForm(ConstantsUI.k_StartOverMessage);
            startOver.ShowDialog();
            if (startOver.DialogResult == DialogResult.Yes)
            {
                this.InitializeComponent();
            }
        }
    }
}