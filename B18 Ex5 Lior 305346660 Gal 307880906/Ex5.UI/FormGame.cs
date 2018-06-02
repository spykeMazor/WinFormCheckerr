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

        public enum e_SquaerTag
        {
            WorngMove,
            BlueTag,
            OptionalMoveFrom,
            OptionalMoveTo

        }
        SettingsLogin m_FormNameLogin;

        
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
        private bool startOverSameDetails = false;
        
        ////private ComboBox comboBoxBackground;
        private Button buttonStartOver;

        /// <summary>
        private PictureBox arrowPictureBoxPlayer1;
        private PictureBox arrowPictureBoxPlayer2;
        ////private Image m_MyBackground ;

        LogInExceptionForm wongInputException = new LogInExceptionForm("Worng Move", "Worng Move");

        private ComboBox comboBoxBackground;

        private Image m_MyBackground;
        private Label labelBackground;

        public void FormGameStart(SettingsLogin i_FormNameLogin)
        {
            ////m_FormNameLogin.ShowDialog();
            GetNameLogin = i_FormNameLogin;
            if (GetNameLogin.DialogResult == DialogResult.OK)
            {
                loadCheckersPics();
                InitializeComponent();
            }
            else
            {
                WantsToPlay = false;
            }
        }

        public bool WantsToPlay
        {
            get { return wantsToPlay; }
            set { wantsToPlay = value; }
        }

        public bool StartOverGame
        {
            get { return startOverSameDetails; }
            set { startOverSameDetails = value; }
        }

        private void loadCheckersPics()
        {
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

        public Image UpdateBackground
        {
            get { return m_MyBackground; }
            set { m_MyBackground = value; }
        }
        ////public Image UpdateBackground
        ////{
        ////    get { return m_MyBackground; }
        ////    set { m_MyBackground = value; }
        ////}

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
                        m_Board.GetBoard[i, j].Tag = e_SquaerTag.WorngMove;
                        //m_Board.GetBoard[i, j].Enabled = false;
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
                        else if (m_Game.GetTestingMatrix[j, i] == (char)Checker.Symbol.K)
                        {
                            //Image checkerImg = m_RedChackerImage;
                            m_Board.GetBoard[i, j].Image = m_RedQueenChackerImage;
                        }
                        else if (m_Game.GetTestingMatrix[j, i] == (char)Checker.Symbol.U)
                        {
                            //Image checkerImg = m_RedChackerImage;
                            m_Board.GetBoard[i, j].Image = m_BlackQueenChackerImage;
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
                   // m_Board.GetBoard[PointMoveFrom.X, PointMoveFrom.Y].Enabled = true;
                    m_Board.GetBoard[PointMoveFrom.X, PointMoveFrom.Y].Tag = e_SquaerTag.OptionalMoveFrom;
                }

            }
        }

        public void PictureBoxInTheBoard_Click(object sender, EventArgs e)
        {
            PictureBoxInTheBoard picAsSender = sender as PictureBoxInTheBoard;
            if(picAsSender.Tag.ToString() == e_SquaerTag.WorngMove.ToString())
            {
                wongInputException.ShowDialog();
            }
            else if(picAsSender.Image != null)
             {
                if (picAsSender.Tag.ToString() == e_SquaerTag.OptionalMoveFrom.ToString())
                {
                    Image DefaultBackGroundImage = m_Board.WhiteBackGround;
                    if (picAsSender.BackgroundImage.Tag.ToString() == GameBoardUI.e_TypeOfBackGround.WHITE.ToString())
                    {
                        picAsSender.BackgroundImage = m_Board.BlueBackGround;
                        picAsSender.BackgroundImage.Tag = GameBoardUI.e_TypeOfBackGround.BLUE;
                        picAsSender.Tag = e_SquaerTag.BlueTag;
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
                            // m_Board.GetBoard[kvp.Key.X, kvp.Key.Y].Enabled = true;
                            m_Board.GetBoard[kvp.Key.X, kvp.Key.Y].Tag = e_SquaerTag.OptionalMoveFrom;
                        }
                    }
                }
            }
            else if(picAsSender.Tag.ToString() == e_SquaerTag.OptionalMoveTo.ToString())
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
                { //// case this is the player 1
                    this.arrowPictureBoxPlayer1.Visible = false;
                    this.arrowPictureBoxPlayer2.Visible = true;
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
                    { ////--->Combo one more attack -> turn stay in Player1 
                        invokeClickOnChecker(m_Game.AttackListOfPlayer1);
                    }

                    this.arrowPictureBoxPlayer2.Visible = false;
                    this.arrowPictureBoxPlayer1.Visible = true;
                }
                else
                { //// case this is the player 2
                    this.arrowPictureBoxPlayer2.Visible = false;
                    this.arrowPictureBoxPlayer1.Visible = true;
                    m_Game.MoveTheCheckerOfTheCorecctPlayer(m_Game.Player2,
                            m_Game.Player1,
                            convertCheckerPositionPointToSquareOfLogic(m_LastMoveFrom),
                            convertCheckerPositionPointToSquareOfLogic(m_LastMoveTo),
                            ref m_wasAttack, ref m_hasAnotherAttack);
                        initCheckers();
                    m_WasMove = false;
                    if (!m_hasAnotherAttack)
                    {
                        m_CurrentPlayer = false; ////---> now player1 turn
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
                    { ////--->Combo one more attack -> turn stay in Player2                 
                        invokeClickOnChecker(m_Game.AttackListOfPlayer2);
                    }
                     
                    this.arrowPictureBoxPlayer1.Visible = false;
                    this.arrowPictureBoxPlayer2.Visible = true;
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
            Thread.Sleep(2000);
            initCheckers();
            this.labelScore1.Text = m_Game.Player1Score().ToString();
            this.labelScore2.Text = m_Game.Player2Score().ToString();
            this.Update();
            m_WasMove = false;
            if (!m_hasAnotherAttack)
            {
                m_CurrentPlayer = false; ////---> now player1 turn
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
                    //m_Board.GetBoard[kpv.Key.X, kpv.Key.Y].Enabled = false;
                    m_Board.GetBoard[kpv.Key.X, kpv.Key.Y].Tag = e_SquaerTag.WorngMove;
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


        ///////////////////////////
  
        private void enableMoveToSquare(LinkedList<KeyValuePair<string, string>> i_MoveListOfTheCurrentPlayer, Point i_BlueSquare)
        {
            foreach (KeyValuePair<string, string> kvp in i_MoveListOfTheCurrentPlayer)
            {
                if (i_BlueSquare == converCheckerPositionToPoint(kvp.Key))
                {
                    Point optionalSquareToMove = new Point();
                    optionalSquareToMove = converCheckerPositionToPoint(kvp.Value);
                    //m_Board.GetBoard[optionalSquareToMove.X, optionalSquareToMove.Y].Enabled = true;
                    m_Board.GetBoard[optionalSquareToMove.X, optionalSquareToMove.Y].Tag = e_SquaerTag.OptionalMoveTo;
                }
                //else
                //{

                //}
            }
        }
        
        private void returnSqureToEmpty()
        {
            foreach (KeyValuePair<Point, Point> kvp in m_AllTheClickableSquareReadyToMove)
            {
                //m_Board.GetBoard[kvp.Key.X, kvp.Key.Y].Enabled = false;
                m_Board.GetBoard[kvp.Key.X, kvp.Key.Y].Tag = e_SquaerTag.WorngMove;
               // m_Board.GetBoard[kvp.Value.X, kvp.Value.Y].Enabled = false;
                m_Board.GetBoard[kvp.Value.X, kvp.Value.Y].Tag = e_SquaerTag.WorngMove;
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
                //m_Board.GetBoard[kvp.Value.X, kvp.Value.Y].Enabled = false;
                m_Board.GetBoard[kvp.Value.X, kvp.Value.Y].Tag = e_SquaerTag.WorngMove;
            }
        }

        private void comboBoxBackground_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBoxBackground.Text.CompareTo("Damka3D") == 0)
            {
                UpdateBackground = Properties.Resources.damka3d;
                this.headLine.ForeColor = System.Drawing.Color.Yellow;

            }
            else if (this.comboBoxBackground.Text.CompareTo("Purple") == 0)
            {
                UpdateBackground = Properties.Resources.purple_Background;
            }
            else if (this.comboBoxBackground.Text.CompareTo("Heart") == 0)
            {
                UpdateBackground = Properties.Resources.heart_Background;
            }
            else if (this.comboBoxBackground.Text.CompareTo("Green") == 0)
            {
                UpdateBackground = Properties.Resources.green_Background;
            }
            else
            {
                UpdateBackground = Properties.Resources.blue_Background;
            }

            this.BackgroundImage = UpdateBackground;
        }


        //private void checkerMove(Point i_MoveFrom,Point i_MoveTo, Image i_CheckerType)
        //{
        //    PictureBox tempPicBox = new PictureBox();
        //    tempPicBox.Image = i_CheckerType;

        //}

        //private void timer_Tick(object sender, EventArgs e)
        //{
        //    PictureBox picAsSender = sender as PictureBox;

        //}


        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGame));
            this.headLine = new Label();
            this.labelScore1 = new Label();
            this.labelScore2 = new Label();
            this.splitContainer1 = new SplitContainer();
            this.labelPlayer1Name = new Label();
            this.labelPlayer2Name = new Label();
            this.buttonQuit = new Button();
            this.buttonStartOver = new Button();
            this.labelBackground = new Label();
            this.comboBoxBackground = new System.Windows.Forms.ComboBox();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // headLine
            // 
            this.headLine.Anchor = AnchorStyles.Top;
            this.headLine.AutoSize = true;
            this.headLine.BackColor = System.Drawing.Color.Transparent;
            this.headLine.Font = new System.Drawing.Font("MV Boli", 19F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(177)), true);
            this.headLine.ForeColor = System.Drawing.Color.Yellow;
            this.headLine.Location = new System.Drawing.Point(320, 9);
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
            this.splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 

            ////this.splitContainer1.Panel1.BackgroundImage = Image.FromFile(@"C:\black_soldier.PNG");
            this.splitContainer1.Panel1.BackgroundImage = Properties.Resources.black_soldier;

            this.splitContainer1.Panel1.BackgroundImageLayout = ImageLayout.Stretch;
            this.splitContainer1.Panel1.Controls.Add(this.labelScore1);
            this.splitContainer1.Panel1.Controls.Add(this.labelPlayer1Name);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackgroundImage = Properties.Resources.red_soldier;

            this.splitContainer1.Panel2.BackgroundImageLayout = ImageLayout.Stretch;
            this.splitContainer1.Panel2.Controls.Add(this.labelScore2);
            this.splitContainer1.Panel2.Controls.Add(this.labelPlayer2Name);
            this.splitContainer1.Size = new System.Drawing.Size(218, 354);
            this.splitContainer1.SplitterDistance = 177;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 3;
            // 
            // labelPlayer1Name
            // 
            this.labelPlayer1Name.Anchor = AnchorStyles.None;
            this.labelPlayer1Name.BackColor = System.Drawing.Color.Transparent;
            this.labelPlayer1Name.Text = m_FormNameLogin.Player1Name;
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
            this.labelPlayer2Name.Anchor = AnchorStyles.None;
            this.labelPlayer2Name.BackColor = System.Drawing.Color.Transparent;
            this.labelPlayer2Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelPlayer2Name.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelPlayer2Name.Location = new System.Drawing.Point(61, 55);
            this.labelPlayer2Name.Name = "labelPlayer2Name";
            this.labelPlayer2Name.Size = new System.Drawing.Size(101, 40);
            this.labelPlayer2Name.TabIndex = 3;
            this.labelPlayer2Name.Text = m_FormNameLogin.Player2Name;
            this.labelPlayer2Name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonQuit
            // 
            this.buttonQuit.AutoSize = true;
            this.buttonQuit.BackColor = System.Drawing.SystemColors.HotTrack;
            this.buttonQuit.Cursor = Cursors.Help;
            this.buttonQuit.FlatStyle = FlatStyle.Popup;
            this.buttonQuit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.buttonQuit.Location = new System.Drawing.Point(832, 608);
            this.buttonQuit.Name = "buttonQuit";
            this.buttonQuit.Size = new System.Drawing.Size(121, 33);
            this.buttonQuit.TabIndex = 4;
            this.buttonQuit.Text = "QUIT";
            this.buttonQuit.TextImageRelation = TextImageRelation.ImageAboveText;
            this.buttonQuit.UseVisualStyleBackColor = false;
            this.buttonQuit.Click += new System.EventHandler(this.buttonQuit_Click);
            // 
            // arrowPictureBoxPlayer1
            // 
            this.arrowPictureBoxPlayer1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.arrowPictureBoxPlayer1)).BeginInit();

            this.arrowPictureBoxPlayer1.Image = Properties.Resources.arrow_PngYellow;
            this.arrowPictureBoxPlayer1.BackColor = System.Drawing.Color.Transparent;
            this.arrowPictureBoxPlayer1.Location = new System.Drawing.Point(900, 163);
            this.arrowPictureBoxPlayer1.Name = "arrowPictureBox1";
            this.arrowPictureBoxPlayer1.Size = new System.Drawing.Size(64, 64);
            this.arrowPictureBoxPlayer1.SizeMode = PictureBoxSizeMode.StretchImage;
            this.arrowPictureBoxPlayer1.TabIndex = 1;
            this.arrowPictureBoxPlayer1.TabStop = false;
            this.arrowPictureBoxPlayer1.Visible = true;
            this.Controls.Add(this.arrowPictureBoxPlayer1);
            ((System.ComponentModel.ISupportInitialize)(this.arrowPictureBoxPlayer1)).EndInit();

            // 
            // arrowPictureBoxPlayer2
            // 
            this.arrowPictureBoxPlayer2 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.arrowPictureBoxPlayer1)).BeginInit();

            this.arrowPictureBoxPlayer2.Image = Properties.Resources.arrow_PngYellow;
            this.arrowPictureBoxPlayer2.BackColor = System.Drawing.Color.Transparent;
            this.arrowPictureBoxPlayer2.Location = new System.Drawing.Point(900, 340);
            this.arrowPictureBoxPlayer2.Name = "arrowPictureBox2";
            this.arrowPictureBoxPlayer2.Size = new System.Drawing.Size(64, 64);
            this.arrowPictureBoxPlayer2.SizeMode = PictureBoxSizeMode.StretchImage;
            this.arrowPictureBoxPlayer2.TabIndex = 1;
            this.arrowPictureBoxPlayer2.TabStop = false;
            this.arrowPictureBoxPlayer2.Visible = false;
            this.Controls.Add(this.arrowPictureBoxPlayer2);
            ((System.ComponentModel.ISupportInitialize)(this.arrowPictureBoxPlayer2)).EndInit();


            // 
            // buttonStartOver
            // 
            this.buttonStartOver.AutoSize = true;
            this.buttonStartOver.BackColor = System.Drawing.SystemColors.HotTrack;
            this.buttonStartOver.Cursor = Cursors.Help;
            this.buttonStartOver.FlatStyle = FlatStyle.Popup;
            this.buttonStartOver.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.buttonStartOver.Location = new System.Drawing.Point(685, 608);
            this.buttonStartOver.Name = "buttonStartOver";
            this.buttonStartOver.Size = new System.Drawing.Size(131, 33);
            this.buttonStartOver.TabIndex = 5;
            this.buttonStartOver.Text = "START OVER";
            this.buttonStartOver.TextImageRelation = TextImageRelation.ImageAboveText;
            this.buttonStartOver.UseVisualStyleBackColor = false;
            this.buttonStartOver.Click += new System.EventHandler(this.buttonStartOver_Click);

            ////////////////////////////////////////////////////////////////////////////
            // 
            // comboBoxBackground
            // 
            this.comboBoxBackground.BackColor = System.Drawing.Color.Blue;
            this.comboBoxBackground.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBoxBackground.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBackground.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.comboBoxBackground.ForeColor = System.Drawing.Color.Yellow;
            this.comboBoxBackground.ItemHeight = 13;
            this.comboBoxBackground.TabStop = false;
            this.comboBoxBackground.Items.AddRange(new object[] {
            "Blue",
            "Heart",
            "Green",
            "Purple",
            "Damka3D"});
            this.comboBoxBackground.Location = new System.Drawing.Point(800, 500);
            this.comboBoxBackground.Name = "comboBoxBackground";
            this.comboBoxBackground.Size = new System.Drawing.Size(143, 21);
            //this.comboBoxBackground.TabIndex = 0;
            this.comboBoxBackground.Text = "Green";
            this.comboBoxBackground.SelectedIndexChanged += new System.EventHandler(this.comboBoxBackground_SelectedIndexChanged);
            // 
            // labelBackground
            // 
            this.labelBackground.AutoSize = true;
            this.labelBackground.BackColor = System.Drawing.Color.Transparent;
            this.labelBackground.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelBackground.ForeColor = System.Drawing.Color.Yellow;
            this.labelBackground.BackColor = System.Drawing.Color.Blue;
            this.labelBackground.Location = new System.Drawing.Point(680, 502);
            this.labelBackground.Name = "labelBackground";
            this.labelBackground.Size = new System.Drawing.Size(115, 15);
            this.labelBackground.TabIndex = 1;
            this.labelBackground.Text = "BACKGROUNDS:";
            //////////////////////////////////////////////////////////////////////
            //  
            // FormGame
            // 
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImage = Properties.Resources.green_Background;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(978, 694);
            this.Controls.Add(this.buttonStartOver);
            this.Controls.Add(this.buttonQuit);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.headLine);
            this.Controls.Add(this.comboBoxBackground);
            this.Controls.Add(this.labelBackground);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.Name = "FormGame";
            this.Text = "Gal & Lior Checker Game";
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            this.Icon = Properties.Resources.damka_Icon;
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void buttonQuit_Click(object sender, EventArgs e)
        {
            VerifyForm quitForm = new VerifyForm(ConstantsUI.k_QuitMessage, ConstantsUI.k_QuitMessageTitle);
            quitForm.ShowDialog();
            if (quitForm.DialogResult == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void buttonStartOver_Click(object sender, EventArgs e)
        {
            VerifyForm startOver = new VerifyForm(ConstantsUI.k_StartOverMessage, ConstantsUI.k_StartOverMessageTtle);
            startOver.ShowDialog();
            if (startOver.DialogResult == DialogResult.Yes)
            {
                ////////////////////////loadCheckersPics();
                ////////////////////////InitializeComponent();
                ////////////////////////InitControls();
                StartOverGame = true;
                this.Close();
            }
        }
    }
}