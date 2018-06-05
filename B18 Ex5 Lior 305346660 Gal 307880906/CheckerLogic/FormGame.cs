using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Windows.Forms;
using System.Drawing;
using System.Xml;
using System.Threading;
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

        public enum e_SquaerTag
        {
            WrongMove,
            WrongChecker,
            WrongSelection,
            OptionalMoveFrom,
            OptionalMoveTo
        }

        private SettingsLogin m_FormNameLogin;
        private Image m_BlackChackerImage;
        private Image m_RedChackerImage;
        private Image m_BlackQueenChackerImage;
        private Image m_RedQueenChackerImage;
        private GameBoardUI m_Board;
        private CheckerLogic.Game m_Game;
        private Point m_LastMoveFrom = new Point();
        private Point m_LastMoveTo = new Point();
        private bool wantsToPlay = true;
        private bool m_WasMove = false;
        private bool m_CurrentPlayer = false; //// False = Player1, True = Player2
        private List<KeyValuePair<Point, Point>> m_AllTheClickableSquareReadyToMove = new List<KeyValuePair<Point, Point>>();
        private bool m_wasAttack = false;
        private bool m_hasAnotherAttack = false;
        private bool m_ChekerSelectedOnBoard = false;
        private Label headLine;
        private Label labelScore1;
        private Label labelScore2;
        private Label labelTotalScore1;
        private Label labelTotalScore2;
        private SplitContainer splitContainerNameAndCurrentScore;
        private SplitContainer splitContainerTotalScore;
        private Label labelPlayer1Name;
        private Label labelPlayer2Name;
        private Button buttonExit;
        private bool startOverSameDetails = false;       
        ////private ComboBox comboBoxBackground;
        private Button buttonStartOver;
        private PictureBox arrowPictureBoxPlayer1;
        private PictureBox arrowPictureBoxPlayer2;
        ////private Image m_MyBackground ;
        private LogInExceptionForm wongInputException = new LogInExceptionForm("Worng Move", "Worng Move");
        private ComboBox comboBoxBackground;
        private Image m_MyBackground;
        private Label labelBackground;
        private Label labelTotalScoreTitle;

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
            locateBoardOnTheForm(boardSizeAsInt);
            startGame(m_Game);
        }

        private void startGame(Game i_CurrentGame)
        {
            invokeTheBoard();
            initCheckers();
            m_Game.UpdateMoveList(Player.e_LocationOfThePlayer.UP);
            invokeClickOnChecker(m_Game.MoveListOfPlayer1);
        }

        private void locateBoardOnTheForm(int i_BoardSize)
        {
            for (int i = 0; i < i_BoardSize; i++)
            {
                for (int j = 0; j < i_BoardSize; j++)
                {
                    m_Board.GetBoard[i, j].Size = new Size(60, 60);
                    m_Board.GetBoard[i, j].Location = new Point((i + 1) * 60, (j + 1) * 60);
                    this.Controls.Add(m_Board.GetBoard[i, j]);
                }
            }
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
                        m_Board.GetBoard[i, j].Tag = e_SquaerTag.WrongSelection;  
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
                        m_Board.GetBoard[i, j].Tag = e_SquaerTag.WrongChecker;
                        if (m_Game.GetTestingMatrix[j, i] == (char)Checker.Symbol.O)
                        {
                            m_Board.GetBoard[i, j].Image = m_BlackChackerImage;
                        }
                        else if (m_Game.GetTestingMatrix[j, i] == (char)Checker.Symbol.X)
                        {
                            m_Board.GetBoard[i, j].Image = m_RedChackerImage;
                        }
                        else if (m_Game.GetTestingMatrix[j, i] == (char)Checker.Symbol.K)
                        {
                            m_Board.GetBoard[i, j].Image = m_RedQueenChackerImage;
                        }
                        else if (m_Game.GetTestingMatrix[j, i] == (char)Checker.Symbol.U)
                        {
                            m_Board.GetBoard[i, j].Image = m_BlackQueenChackerImage;
                        }
                    }
                }
            }
        }

        //// Convert from "PositionPoint" to Drawing.Point obj
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

        private bool winnerOrDraw()
        {
            bool winnerOrDrawFlag = false;
            Player tempPlayer = m_Game.TheWinnerPlayerIs();
            DialogResult gameEndedAndWantsToStartOver;
            string resultMessage = ConstantsUI.k_EmptyString;
            if (tempPlayer != null)
            { //// There is a WINNER in the game
                initCheckers();
                winnerOrDrawFlag = true;
                resultMessage = tempPlayer.GetName + ConstantsUI.k_WinnerMessage;
            }
            else if (m_Game.ThereIsDraw())
            { //// There is a DRAW in the game
                initCheckers();
                winnerOrDrawFlag = true;
                resultMessage = tempPlayer.GetName + ConstantsUI.k_DrawMessage;
            }
            else
            {
                winnerOrDrawFlag = false;
            }

            if (winnerOrDrawFlag)
            {
                gameEndedAndWantsToStartOver = MessageBox.Show(resultMessage, ConstantsUI.k_DamkaTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (gameEndedAndWantsToStartOver == DialogResult.No)
                {
                    this.Close();
                }
                else              
                { //// Player wants to start over
                    GetNameLogin.Player1TotalScoreCounter += m_Game.Player1Score();
                    GetNameLogin.Player2TotalScoreCounter += m_Game.Player2Score();
                    this.Update();
                    StartOverGame = true;
                    this.Close();
                }
            }

            return winnerOrDrawFlag;
        }

        private void invokeClickOnChecker(LinkedList<KeyValuePair<string, string>> i_MoveList)
        {
            m_ChekerSelectedOnBoard = false;
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
                    m_Board.GetBoard[PointMoveFrom.X, PointMoveFrom.Y].Tag = e_SquaerTag.OptionalMoveFrom;
                }
            }          
        }

        public void PictureBoxInTheBoard_Click(object sender, EventArgs e)
        {
            MessageBoxButtons wrongInputExceptionMessageType = MessageBoxButtons.OK;
            PictureBoxInTheBoard picAsSender = sender as PictureBoxInTheBoard;
            if(picAsSender.Tag.ToString() == e_SquaerTag.WrongSelection.ToString())
            {
                if (m_ChekerSelectedOnBoard)
                {
                    MessageBox.Show("Wrong Move", "Wrong", wrongInputExceptionMessageType, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Wrong Selection", "Wrong", wrongInputExceptionMessageType, MessageBoxIcon.Warning);
                }
            }  
            else if(picAsSender.Tag.ToString() == e_SquaerTag.WrongChecker.ToString())
            {
                MessageBox.Show("Wrong Checker Selected", "Wrong", wrongInputExceptionMessageType, MessageBoxIcon.Warning);
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
                        m_ChekerSelectedOnBoard = true;
                        picAsSender.Tag = e_SquaerTag.OptionalMoveFrom;
                        m_LastMoveFrom = picAsSender.PointInTheBoard;
                        invokeAllTheOptionalMoveSquare(picAsSender);
                    }
                    else if(picAsSender.BackgroundImage.Tag.ToString() == GameBoardUI.e_TypeOfBackGround.BLUE.ToString())
                    {
                        picAsSender.BackgroundImage = DefaultBackGroundImage;
                        picAsSender.BackgroundImage.Tag = GameBoardUI.e_TypeOfBackGround.WHITE;
                        m_ChekerSelectedOnBoard = false;
                        disableAllMoveToClickableSquare();
                        foreach (KeyValuePair<Point, Point> kvp in m_AllTheClickableSquareReadyToMove)
                        {
                            //// m_Board.GetBoard[kvp.Key.X, kvp.Key.Y].Enabled = true;
                            
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
                    changeVisibleStatusFoeArrows(ConstantsUI.k_ArrowPlayer2);
                    m_Game.MoveTheCheckerOfTheCorecctPlayer(
                        m_Game.Player1,
                  m_Game.Player2,
                  convertCheckerPositionPointToSquareOfLogic(m_LastMoveFrom),
                  convertCheckerPositionPointToSquareOfLogic(m_LastMoveTo),
                 ref m_wasAttack, 
                 ref m_hasAnotherAttack);
                    if (!winnerOrDraw())
                    {
                        initCheckers();
                        updateScore();
                        enableOrDisableStartOverButton(ConstantsUI.k_Player2Turn);
                        m_WasMove = false;
                        if (!m_hasAnotherAttack)
                        {
                            if (m_Game.Player2.MachineOrNot)
                            {
                                computerMove();
                                changeVisibleStatusFoeArrows(ConstantsUI.k_ArrowPlayer1);
                            }
                            else
                            {
                                changeVisibleStatusFoeArrows(ConstantsUI.k_ArrowPlayer2);
                                m_CurrentPlayer = true; ////---> now player2 turn
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

                    }
                }
                else
                { //// case this is the player 2
                    ////changeVisibleStatusFoeArrows(ConstantsUI.k_ArrowPlayer1);
                    m_Game.MoveTheCheckerOfTheCorecctPlayer(
                        m_Game.Player2,
                            m_Game.Player1,
                            convertCheckerPositionPointToSquareOfLogic(m_LastMoveFrom),
                            convertCheckerPositionPointToSquareOfLogic(m_LastMoveTo),
                            ref m_wasAttack,
                            ref m_hasAnotherAttack);
                    if (!winnerOrDraw())
                    {
                        initCheckers();
                        updateScore();
                        enableOrDisableStartOverButton(ConstantsUI.k_Player1Turn);
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

                        changeVisibleStatusFoeArrows(ConstantsUI.k_ArrowPlayer1);
                    }
                }
            }
        }

        private void changeVisibleStatusFoeArrows(int i_CurrentVisible)
        {
            if (i_CurrentVisible == ConstantsUI.k_ArrowPlayer1)
            { ////make arrow 1 visible
                this.arrowPictureBoxPlayer2.Visible = false;
                this.arrowPictureBoxPlayer1.Visible = true;
            }
            else
            { ////i_CurrentVisible = ConstantsUI.k_ArrowPlayer2 ----> make arrow 2 visible
                this.arrowPictureBoxPlayer1.Visible = false;
                this.arrowPictureBoxPlayer2.Visible = true;
            }
        }

        private void enableOrDisableStartOverButton(int i_CurrentPlayerTurn)
        {
            if (i_CurrentPlayerTurn == ConstantsUI.k_Player1Turn)
            {
                if (m_Game.Player1Score() < m_Game.Player2Score())
                {
                    this.buttonStartOver.Enabled = true;
                }
                else
                {
                    this.buttonStartOver.Enabled = false;
                }
            }
            else
            {
                if (m_Game.Player1Score() > m_Game.Player2Score())
                {
                    this.buttonStartOver.Enabled = true;
                }
                else
                {
                    this.buttonStartOver.Enabled = false;
                }
            }

            this.Update();
        }

        private void updateScore()
        {
            this.labelScore1.Text = m_Game.Player1Score().ToString();
            this.labelScore2.Text = m_Game.Player2Score().ToString();
            if (m_Game.Player1Score() > 9)
            {
                this.labelScore1.Location = new Point(75, 110);
            }
            else
            {
                this.labelScore1.Location = new Point(80, 110);
            }

            if (m_Game.Player2Score() > 9)
            {
                this.labelScore2.Location = new Point(75, 110);
            }
            else
            {
                this.labelScore2.Location = new Point(80, 110);
            }

            this.Update();
        }

        private void computerMove()
        { //// computer moving managment
            
            KeyValuePair<string, string> computerNextMove = new KeyValuePair<string, string>();
            computerNextMove = m_Game.SelectRandomMove();
            m_Game.MoveTheCheckerOfTheCorecctPlayer(
                m_Game.Player2,
                       m_Game.Player1,
                     computerNextMove.Key,
                       computerNextMove.Value,
                       ref m_wasAttack,
                       ref m_hasAnotherAttack);
            if (!winnerOrDraw())
            {
                Thread.Sleep(2000);
                initCheckers();
                updateScore();
                enableOrDisableStartOverButton(ConstantsUI.k_Player1Turn);
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
                    ////m_Board.GetBoard[kpv.Key.X, kpv.Key.Y].Enabled = false;
                    m_Board.GetBoard[kpv.Key.X, kpv.Key.Y].Tag = e_SquaerTag.WrongChecker;
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
                    ////m_Board.GetBoard[optionalSquareToMove.X, optionalSquareToMove.Y].Enabled = true;
                    m_Board.GetBoard[optionalSquareToMove.X, optionalSquareToMove.Y].Tag = e_SquaerTag.OptionalMoveTo;
                }       
            }
        }
        
        private void returnSqureToEmpty()
        {
            foreach (KeyValuePair<Point, Point> kvp in m_AllTheClickableSquareReadyToMove)
            {
                ////m_Board.GetBoard[kvp.Key.X, kvp.Key.Y].Enabled = false;
                m_Board.GetBoard[kvp.Key.X, kvp.Key.Y].Tag = e_SquaerTag.WrongSelection;
               //// m_Board.GetBoard[kvp.Value.X, kvp.Value.Y].Enabled = false;
                m_Board.GetBoard[kvp.Value.X, kvp.Value.Y].Tag = e_SquaerTag.WrongSelection;
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
                ////m_Board.GetBoard[kvp.Value.X, kvp.Value.Y].Enabled = false;
                m_Board.GetBoard[kvp.Value.X, kvp.Value.Y].Tag = e_SquaerTag.WrongSelection;
            }
        }

        private void comboBoxBackground_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBoxBackground.Text.CompareTo("Damka3D") == 0)
            {
                UpdateBackground = Properties.Resources.damka3d;
                paintFontsOnBackgroundWithSuitableColor(Color.Red);
            }
            else if (this.comboBoxBackground.Text.CompareTo("Purple") == 0)
            {
                UpdateBackground = Properties.Resources.purple_Background;
                paintFontsOnBackgroundWithSuitableColor(Color.Yellow);
            }
            else if (this.comboBoxBackground.Text.CompareTo("Heart") == 0)
            {
                UpdateBackground = Properties.Resources.heart_Background;
                paintFontsOnBackgroundWithSuitableColor(Color.Yellow);
            }
            else if (this.comboBoxBackground.Text.CompareTo("Green") == 0)
            {
                UpdateBackground = Properties.Resources.green_Background;
                paintFontsOnBackgroundWithSuitableColor(Color.Black);
            }
            else
            {
                UpdateBackground = Properties.Resources.blue_Background;
                paintFontsOnBackgroundWithSuitableColor(Color.Yellow);
            }

            this.BackgroundImage = UpdateBackground;
        }

        private void paintFontsOnBackgroundWithSuitableColor(Color i_SuitableColor)
        {
            this.labelBackground.ForeColor = i_SuitableColor;
            this.labelTotalScoreTitle.ForeColor = i_SuitableColor;
            this.headLine.ForeColor = i_SuitableColor;
        }
        ////private void checkerMove(Point i_MoveFrom,Point i_MoveTo, Image i_CheckerType)
        ////{
        ////    PictureBox tempPicBox = new PictureBox();
        ////    tempPicBox.Image = i_CheckerType;

        ////}

        ////private void timer_Tick(object sender, EventArgs e)
        ////{
        ////    PictureBox picAsSender = sender as PictureBox;

        ////}

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGame));
            this.headLine = new Label();
            this.labelScore1 = new Label();
            this.labelScore2 = new Label();
            this.labelTotalScore1 = new Label();
            this.labelTotalScore2 = new Label();
            this.splitContainerNameAndCurrentScore = new SplitContainer();
            this.splitContainerTotalScore = new SplitContainer();
            this.labelPlayer1Name = new Label();
            this.labelPlayer2Name = new Label();
            this.buttonExit = new Button();
            this.buttonStartOver = new Button();
            this.labelBackground = new Label();
            this.labelTotalScoreTitle = new Label();
            this.comboBoxBackground = new System.Windows.Forms.ComboBox();
            this.splitContainerNameAndCurrentScore.Panel1.SuspendLayout();
            this.splitContainerNameAndCurrentScore.Panel2.SuspendLayout();
            this.splitContainerNameAndCurrentScore.SuspendLayout();
            this.splitContainerTotalScore.Panel1.SuspendLayout();
            this.splitContainerTotalScore.Panel2.SuspendLayout();
            this.splitContainerTotalScore.SuspendLayout();
            this.SuspendLayout();
            //// 
            //// headLine
            //// 
            this.headLine.Anchor = AnchorStyles.Top;
            this.headLine.AutoSize = true;
            this.headLine.BackColor = Color.Transparent;
            this.headLine.Font = new Font("MV Boli", 19F, (FontStyle)(FontStyle.Bold | FontStyle.Italic | FontStyle.Underline), GraphicsUnit.Point, (byte)177, true);
            this.headLine.ForeColor = Color.Yellow;
            this.headLine.Location = new Point(320, 9);
            this.headLine.Name = "headLine";
            this.headLine.Size = new Size(469, 43);
            this.headLine.TabIndex = 1;
            this.headLine.Text = "Gal & Lior Checkers Game";
            this.headLine.TextAlign = ContentAlignment.TopCenter;
            //// 
            //// labelScore1
            //// 
            this.labelScore1.AutoSize = true;
            this.labelScore1.BackColor = Color.Transparent;
            this.labelScore1.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Bold, GraphicsUnit.Point, (byte)177);
            this.labelScore1.ForeColor = SystemColors.HighlightText;
            this.labelScore1.ImageAlign = ContentAlignment.TopCenter;
            this.labelScore1.Location = new Point(80, 110);
            this.labelScore1.Name = "labelScore1";
            this.labelScore1.Size = new Size(36, 37);
            this.labelScore1.TabIndex = 3;
            this.labelScore1.Text = "0";
            this.labelScore1.TextAlign = ContentAlignment.TopCenter;
            //// 
            //// labelScore2
            //// 
            this.labelScore2.AutoSize = true;
            this.labelScore2.BackColor = Color.Transparent;
            this.labelScore2.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Bold, GraphicsUnit.Point, (byte)177);
            this.labelScore2.ForeColor = SystemColors.ActiveCaptionText;
            this.labelScore2.ImageAlign = ContentAlignment.TopCenter;
            this.labelScore2.Location = new Point(80, 110);
            this.labelScore2.Name = "labelScore2";
            this.labelScore2.Size = new Size(36, 37);
            this.labelScore2.TabIndex = 4;
            this.labelScore2.Text = "0";
            this.labelScore2.TextAlign = ContentAlignment.TopCenter;
            //// 
            //// splitContainerNameAndCurrentScore
            //// 
            this.splitContainerNameAndCurrentScore.BackColor = Color.Transparent;
            this.splitContainerNameAndCurrentScore.Location = new Point(712, 106);
            this.splitContainerNameAndCurrentScore.Name = "splitContainerNameAndCurrentScore";
            this.splitContainerNameAndCurrentScore.Orientation = Orientation.Horizontal;
            //// 
            //// splitContainerNameAndCurrentScore.Panel1
            //// 
            this.splitContainerNameAndCurrentScore.Panel1.BackgroundImage = Properties.Resources.black_soldier;
            this.splitContainerNameAndCurrentScore.Panel1.BackgroundImageLayout = ImageLayout.Stretch;
            this.splitContainerNameAndCurrentScore.Panel1.Controls.Add(this.labelScore1);
            this.splitContainerNameAndCurrentScore.Panel1.Controls.Add(this.labelPlayer1Name);
            //// 
            //// splitContainerNameAndCurrentScore.Panel2
            //// 
            this.splitContainerNameAndCurrentScore.Panel2.BackgroundImage = Properties.Resources.red_soldier;
            this.splitContainerNameAndCurrentScore.Panel2.BackgroundImageLayout = ImageLayout.Stretch;
            this.splitContainerNameAndCurrentScore.Panel2.Controls.Add(this.labelScore2);
            this.splitContainerNameAndCurrentScore.Panel2.Controls.Add(this.labelPlayer2Name);
            this.splitContainerNameAndCurrentScore.Size = new Size(177, 354);
            this.splitContainerNameAndCurrentScore.SplitterDistance = 177;
            this.splitContainerNameAndCurrentScore.SplitterWidth = 1;
            this.splitContainerNameAndCurrentScore.TabIndex = 3;
            //// 
            //// labelPlayer1Name
            //// 
            this.labelPlayer1Name.Anchor = AnchorStyles.None;
            this.labelPlayer1Name.BackColor = Color.Transparent;
            this.labelPlayer1Name.Text = m_FormNameLogin.Player1Name;
            this.labelPlayer1Name.Font = new Font("Microsoft Sans Serif", 15F, FontStyle.Bold, GraphicsUnit.Point, (byte)177);
            this.labelPlayer1Name.ForeColor = SystemColors.HighlightText;
            this.labelPlayer1Name.Location = new Point(40, 55);
            this.labelPlayer1Name.Name = "labelPlayer1Name";
            this.labelPlayer1Name.Size = new Size(101, 40);
            this.labelPlayer1Name.TabIndex = 2;
            this.labelPlayer1Name.Text = m_FormNameLogin.Player1Name;
            this.labelPlayer1Name.TextAlign = ContentAlignment.MiddleCenter;
            //// 
            //// labelPlayer2Name
            //// 
            this.labelPlayer2Name.Anchor = AnchorStyles.None;
            this.labelPlayer2Name.BackColor = Color.Transparent;
            this.labelPlayer2Name.Font = new Font("Microsoft Sans Serif", 15F, FontStyle.Bold, GraphicsUnit.Point, (byte)177);
            this.labelPlayer2Name.ForeColor = SystemColors.ActiveCaptionText;
            this.labelPlayer2Name.Location = new Point(40, 55);
            this.labelPlayer2Name.Name = "labelPlayer2Name";
            this.labelPlayer2Name.Size = new Size(101, 40);
            this.labelPlayer2Name.TabIndex = 3;
            this.labelPlayer2Name.Text = m_FormNameLogin.Player2Name;
            this.labelPlayer2Name.TextAlign = ContentAlignment.MiddleCenter;
            //// 
            //// splitContainerTotalScore
            //// 
            this.splitContainerTotalScore.BackColor = Color.Transparent;
            this.splitContainerTotalScore.Location = new Point(745, 540);
            this.splitContainerTotalScore.Name = "splitContainerTotalScore";
            this.splitContainerTotalScore.Orientation = Orientation.Vertical;
            //// 
            //// splitContainerTotalScore.Panel1
            //// 
            this.splitContainerTotalScore.Panel1.BackgroundImage = Properties.Resources.black_soldier;
            this.splitContainerTotalScore.Panel1.BackgroundImageLayout = ImageLayout.Stretch;
            this.splitContainerTotalScore.Panel1.Controls.Add(this.labelTotalScore1);
            //// 
            //// splitContainerTotalScore.Panel2
            //// 
            this.splitContainerTotalScore.Panel2.BackgroundImage = Properties.Resources.red_soldier;
            this.splitContainerTotalScore.Panel2.BackgroundImageLayout = ImageLayout.Stretch;
            this.splitContainerTotalScore.Panel2.Controls.Add(this.labelTotalScore2);
            this.splitContainerTotalScore.Size = new Size(120, 60);
            this.splitContainerTotalScore.SplitterDistance = 60;
            this.splitContainerTotalScore.SplitterWidth = 1;
            this.splitContainerTotalScore.TabIndex = 3;
            //// 
            //// labelTotalScore1
            //// 
            this.labelTotalScore1.AutoSize = true;
            this.labelTotalScore1.BackColor = Color.Transparent;
            this.labelTotalScore1.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Bold, GraphicsUnit.Point, (byte)177);
            this.labelTotalScore1.ForeColor = SystemColors.HighlightText;
            this.labelTotalScore1.ImageAlign = ContentAlignment.TopCenter;
            this.labelTotalScore1.Location = new Point(15, 15);
            this.labelTotalScore1.Name = "labelTotalScore1";
            this.labelTotalScore1.Size = new Size(17, 17);
            this.labelTotalScore1.TabIndex = 3;
            this.labelTotalScore1.Text = GetNameLogin.Player1TotalScoreCounter.ToString(); 
            this.labelTotalScore1.TextAlign = ContentAlignment.TopCenter;
            //// 
            //// labelTotalScore2
            //// 
            this.labelTotalScore2.AutoSize = true;
            this.labelTotalScore2.BackColor = Color.Transparent;
            this.labelTotalScore2.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Bold, GraphicsUnit.Point, (byte)177);
            this.labelTotalScore2.ForeColor = SystemColors.ActiveCaptionText;
            this.labelTotalScore2.ImageAlign = ContentAlignment.TopCenter;
            this.labelTotalScore2.Location = new Point(15, 15);
            this.labelTotalScore2.Name = "labelTotalScore2";
            this.labelTotalScore2.Size = new Size(17, 17);
            this.labelTotalScore2.TabIndex = 4;
            this.labelTotalScore2.Text = GetNameLogin.Player2TotalScoreCounter.ToString(); 
            this.labelTotalScore2.TextAlign = ContentAlignment.TopCenter;
            //// 
            //// labelTotalScoreTitle
            //// 
            this.labelTotalScoreTitle.AutoSize = true;
            this.labelTotalScoreTitle.BackColor = Color.Transparent;
            this.labelTotalScoreTitle.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Bold, GraphicsUnit.Point, (byte)177);
            this.labelTotalScoreTitle.ForeColor = Color.Yellow;
            this.labelTotalScoreTitle.Location = new Point(685, 554);
            this.labelTotalScoreTitle.Name = "labelTotalScore";
            this.labelTotalScoreTitle.Size = new Size(115, 15);
            this.labelTotalScoreTitle.TabIndex = 1;
            this.labelTotalScoreTitle.Text = "Total\nScore:";
            //// 
            //// buttonExit
            //// 
            this.buttonExit.AutoSize = true;
            this.buttonExit.BackColor = SystemColors.HotTrack;
            this.buttonExit.Cursor = Cursors.Help;
            this.buttonExit.FlatStyle = FlatStyle.Popup;
            this.buttonExit.Font = new Font("Microsoft Sans Serif", 8F, FontStyle.Bold, GraphicsUnit.Point, (byte)177);
            this.buttonExit.Location = new Point(832, 608);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new Size(121, 33);
            this.buttonExit.TabIndex = 4;
            this.buttonExit.Text = "EXIT";
            this.buttonExit.TextImageRelation = TextImageRelation.ImageAboveText;
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            //// 
            //// arrowPictureBoxPlayer1
            //// 
            this.arrowPictureBoxPlayer1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)this.arrowPictureBoxPlayer1).BeginInit();
            this.arrowPictureBoxPlayer1.Image = Properties.Resources.arrow_PngYellow;
            this.arrowPictureBoxPlayer1.BackColor = Color.Transparent;
            this.arrowPictureBoxPlayer1.Location = new Point(900, 163);
            this.arrowPictureBoxPlayer1.Name = "arrowPictureBox1";
            this.arrowPictureBoxPlayer1.Size = new Size(64, 64);
            this.arrowPictureBoxPlayer1.SizeMode = PictureBoxSizeMode.StretchImage;
            this.arrowPictureBoxPlayer1.TabIndex = 1;
            this.arrowPictureBoxPlayer1.TabStop = false;
            this.arrowPictureBoxPlayer1.Visible = true;
            this.Controls.Add(this.arrowPictureBoxPlayer1);
            ((System.ComponentModel.ISupportInitialize)this.arrowPictureBoxPlayer1).EndInit();
            //// 
            //// arrowPictureBoxPlayer2
            //// 
            this.arrowPictureBoxPlayer2 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)this.arrowPictureBoxPlayer1).BeginInit();
            this.arrowPictureBoxPlayer2.Image = Properties.Resources.arrow_PngYellow;
            this.arrowPictureBoxPlayer2.BackColor = Color.Transparent;
            this.arrowPictureBoxPlayer2.Location = new Point(900, 340);
            this.arrowPictureBoxPlayer2.Name = "arrowPictureBox2";
            this.arrowPictureBoxPlayer2.Size = new Size(64, 64);
            this.arrowPictureBoxPlayer2.SizeMode = PictureBoxSizeMode.StretchImage;
            this.arrowPictureBoxPlayer2.TabIndex = 1;
            this.arrowPictureBoxPlayer2.TabStop = false;
            this.arrowPictureBoxPlayer2.Visible = false;
            this.Controls.Add(this.arrowPictureBoxPlayer2);
            ((System.ComponentModel.ISupportInitialize)this.arrowPictureBoxPlayer2).EndInit();
            //// 
            //// buttonStartOver
            //// 
            this.buttonStartOver.AutoSize = true;
            this.buttonStartOver.BackColor = SystemColors.HotTrack;
            this.buttonStartOver.Cursor = Cursors.Help;
            this.buttonStartOver.FlatStyle = FlatStyle.Popup;
            this.buttonStartOver.Font = new Font("Microsoft Sans Serif", 8F, FontStyle.Bold, GraphicsUnit.Point, (byte)177);
            this.buttonStartOver.Location = new Point(685, 608);
            this.buttonStartOver.Name = "buttonStartOver";
            this.buttonStartOver.Size = new Size(131, 33);
            this.buttonStartOver.TabIndex = 5;
            this.buttonStartOver.Enabled = false;
            this.buttonStartOver.Text = "START OVER";
            this.buttonStartOver.TextImageRelation = TextImageRelation.ImageAboveText;
            this.buttonStartOver.UseVisualStyleBackColor = false;
            this.buttonStartOver.Click += new System.EventHandler(this.buttonStartOver_Click);

            ////////////////////////////////////////////////////////////////////////////
            //// 
            //// comboBoxBackground
            //// 
            this.comboBoxBackground.BackColor = Color.Blue;
            this.comboBoxBackground.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBoxBackground.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBackground.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, (byte)177);
            this.comboBoxBackground.ForeColor = Color.Yellow;
            this.comboBoxBackground.ItemHeight = 13;
            this.comboBoxBackground.TabStop = false;
            this.comboBoxBackground.Items.AddRange(new object[] 
            {
            "Blue",
            "Heart",
            "Green",
            "Purple",
            "Damka3D"
            });

            this.comboBoxBackground.Location = new Point(805, 500);
            this.comboBoxBackground.Name = "comboBoxBackground";
            this.comboBoxBackground.Size = new Size(143, 21);
            this.comboBoxBackground.Text = "Blue";
            this.comboBoxBackground.SelectedIndexChanged += new System.EventHandler(this.comboBoxBackground_SelectedIndexChanged);
            //// 
            //// labelBackground
            //// 
            this.labelBackground.AutoSize = true;
            this.labelBackground.BackColor = Color.Transparent;
            this.labelBackground.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold, GraphicsUnit.Point, (byte)177);
            this.labelBackground.ForeColor = Color.Yellow;
            this.labelBackground.Location = new Point(685, 502);
            this.labelBackground.Name = "labelBackground";
            this.labelBackground.Size = new Size(115, 15);
            this.labelBackground.TabIndex = 1;
            this.labelBackground.Text = "BACKGROUNDS:";
            ////  
            //// FormGame
            //// 
            this.BackColor = Color.Black;
            this.BackgroundImage = Properties.Resources.blue_Background;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.ClientSize = new Size(978, 694);
            this.Controls.Add(this.buttonStartOver);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.splitContainerNameAndCurrentScore);
            this.Controls.Add(this.splitContainerTotalScore);
            this.Controls.Add(this.headLine);
            this.Controls.Add(this.comboBoxBackground);
            this.Controls.Add(this.labelBackground);
            this.Controls.Add(this.labelTotalScoreTitle);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.Name = "FormGame";
            this.Text = "Gal & Lior Checker Game";
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            this.Icon = Properties.Resources.damka_Icon;
            this.splitContainerNameAndCurrentScore.Panel1.ResumeLayout(false);
            this.splitContainerNameAndCurrentScore.Panel1.PerformLayout();
            this.splitContainerNameAndCurrentScore.Panel2.ResumeLayout(false);
            this.splitContainerNameAndCurrentScore.Panel2.PerformLayout();
            this.splitContainerNameAndCurrentScore.ResumeLayout(false);
            this.splitContainerTotalScore.Panel1.ResumeLayout(false);
            this.splitContainerTotalScore.Panel1.PerformLayout();
            this.splitContainerTotalScore.Panel2.ResumeLayout(false);
            this.splitContainerTotalScore.Panel2.PerformLayout();
            this.splitContainerTotalScore.ResumeLayout(false);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            VerifyForm quitForm = new VerifyForm(ConstantsUI.k_QuitMessage, ConstantsUI.k_QuitMessageTitle);
            quitForm.ShowDialog();
            if (quitForm.DialogResult == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private string getCurrentWinnerCaseQuiter()
        {
            string winnerResult;
            if (m_Game.Player1Score() < m_Game.Player2Score())
            {
                winnerResult = m_FormNameLogin.Player2Name;
            }
            else
            {
                winnerResult = m_FormNameLogin.Player1Name;
            }

            return winnerResult;
        }

        private void buttonStartOver_Click(object sender, EventArgs e)
        {
            string resultMessage;
            VerifyForm startOver = new VerifyForm(ConstantsUI.k_StartOverMessage, ConstantsUI.k_StartOverMessageTtle);
            startOver.ShowDialog();
            if (startOver.DialogResult == DialogResult.Yes)
            {
                GetNameLogin.Player1TotalScoreCounter += m_Game.Player1Score();
                GetNameLogin.Player2TotalScoreCounter += m_Game.Player2Score();
                resultMessage = getCurrentWinnerCaseQuiter() + ConstantsUI.k_WinnerMessage;
                MessageBox.Show(resultMessage, ConstantsUI.k_DamkaTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                StartOverGame = true;
                this.Close();
            }
        }
    }
}