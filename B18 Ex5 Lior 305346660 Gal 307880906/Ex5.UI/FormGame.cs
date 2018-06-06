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
    public partial class FormGame : Form
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
        private bool m_WantsToPlay = true;
        private bool m_WasMove = false;
        private bool m_CurrentPlayer = false; //// False = Player1, True = Player2
        private List<KeyValuePair<Point, Point>> m_AllTheClickableSquareReadyToMove = new List<KeyValuePair<Point, Point>>();
        private bool m_WasAttack = false;
        private bool m_HasAnotherAttack = false;
        private bool m_ChekerSelectedOnBoard = false;      
        private bool m_StartOverSameDetails = false;

        public FormGame(SettingsLogin i_FormNameLogin)
        {
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
            get { return m_WantsToPlay; }
            set { m_WantsToPlay = value; }
        }

        public bool StartOverGame
        {
            get { return m_StartOverSameDetails; }
            set { m_StartOverSameDetails = value; }
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
            initControls();
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

        private void initControls()
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
            int boardPrintLocation = getStartingLocationOfBoard(i_BoardSize);
            for (int i = 0; i < i_BoardSize; i++)
            {
                for (int j = 0; j < i_BoardSize; j++)
                {
                    m_Board.GetBoard[i, j].Size = new Size(60, 60);
                    m_Board.GetBoard[i, j].Location = new Point((i + boardPrintLocation) * 60, (j + boardPrintLocation) * 60);
                    this.Controls.Add(m_Board.GetBoard[i, j]);
                }
            }
        }

        private int getStartingLocationOfBoard(int i_BoardSize)
        {
            int boardEntryPoint;
            if(i_BoardSize == Constants.K_SmallGameBoard)
            {
                boardEntryPoint = ConstantsUI.k_SmallBoardStartingLocation;
            }
            else if (i_BoardSize == Constants.K_MediumGameBoard)
            {
                boardEntryPoint = ConstantsUI.k_MediumBoardStartingLocation;
            }
            else
            { //// i_BoardSize == Constants.K_LargeGameBoard
                boardEntryPoint = ConstantsUI.k_LargeBoardStartingLocation;
            }
            
            return boardEntryPoint;
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
                        m_Board.GetBoard[i, j].Tag = e_SquaerTag.WrongSelection;
                    }
                    else
                    {
                        m_Board.GetBoard[i, j].Tag = e_SquaerTag.WrongChecker;
                        if (m_Game.GetTestingMatrix[j, i] == (char)Checker.e_Symbol.O)
                        {
                            m_Board.GetBoard[i, j].Image = m_BlackChackerImage;
                        }
                        else if (m_Game.GetTestingMatrix[j, i] == (char)Checker.e_Symbol.X)
                        {
                            m_Board.GetBoard[i, j].Image = m_RedChackerImage;
                        }
                        else if (m_Game.GetTestingMatrix[j, i] == (char)Checker.e_Symbol.K)
                        {
                            m_Board.GetBoard[i, j].Image = m_RedQueenChackerImage;
                        }
                        else if (m_Game.GetTestingMatrix[j, i] == (char)Checker.e_Symbol.U)
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
            if (picAsSender.Tag.ToString() == e_SquaerTag.WrongSelection.ToString())
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
            else if (picAsSender.Tag.ToString() == e_SquaerTag.WrongChecker.ToString())
            {
                MessageBox.Show("Wrong Checker Selected", "Wrong", wrongInputExceptionMessageType, MessageBoxIcon.Warning);
            }
            else if (picAsSender.Image != null)
            {
                tryToMove(picAsSender);
            }
            else if (picAsSender.Tag.ToString() == e_SquaerTag.OptionalMoveTo.ToString())
            {
                afterMoving(picAsSender);
            }
        }

        private void tryToMove(PictureBoxInTheBoard i_SenderPicBoxInTheBoard)
        {
            if (i_SenderPicBoxInTheBoard.Tag.ToString() == e_SquaerTag.OptionalMoveFrom.ToString())
            {
                Image DefaultBackGroundImage = m_Board.WhiteBackGround;
                if (i_SenderPicBoxInTheBoard.BackgroundImage.Tag.ToString() == GameBoardUI.e_TypeOfBackGround.WHITE.ToString())
                {
                    i_SenderPicBoxInTheBoard.BackgroundImage = m_Board.BlueBackGround;
                    i_SenderPicBoxInTheBoard.BackgroundImage.Tag = GameBoardUI.e_TypeOfBackGround.BLUE;
                    m_ChekerSelectedOnBoard = true;
                    i_SenderPicBoxInTheBoard.Tag = e_SquaerTag.OptionalMoveFrom;
                    m_LastMoveFrom = i_SenderPicBoxInTheBoard.PointInTheBoard;
                    invokeAllTheOptionalMoveSquare(i_SenderPicBoxInTheBoard);
                }
                else if (i_SenderPicBoxInTheBoard.BackgroundImage.Tag.ToString() == GameBoardUI.e_TypeOfBackGround.BLUE.ToString())
                {
                    i_SenderPicBoxInTheBoard.BackgroundImage = DefaultBackGroundImage;
                    i_SenderPicBoxInTheBoard.BackgroundImage.Tag = GameBoardUI.e_TypeOfBackGround.WHITE;
                    m_ChekerSelectedOnBoard = false;
                    disableAllMoveToClickableSquare();
                    foreach (KeyValuePair<Point, Point> kvp in m_AllTheClickableSquareReadyToMove)
                    {
                        m_Board.GetBoard[kvp.Key.X, kvp.Key.Y].Tag = e_SquaerTag.OptionalMoveFrom;
                    }
                }
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
                    player1Moving();
                }
                else
                { //// case this is the player 2
                    player2Moving();
                }
            }
        }

        private void player1Moving()
        {
            m_Game.MoveTheCheckerOfTheCorecctPlayer(
                m_Game.Player1,
          m_Game.Player2,
          convertCheckerPositionPointToSquareOfLogic(m_LastMoveFrom),
          convertCheckerPositionPointToSquareOfLogic(m_LastMoveTo),
         ref m_WasAttack,
         ref m_HasAnotherAttack);
            if (!winnerOrDraw())
            {
                initCheckers();
                updateScore();
                enableOrDisableStartOverButton(ConstantsUI.k_Player2Turn);
                m_WasMove = false;
                if (!m_HasAnotherAttack)
                {
                    changeVisibleStatusFoeArrows(ConstantsUI.k_ArrowPlayer2);
                    this.Update();
                    if (m_Game.Player2.MachineOrNot)
                    {
                        computerMove();
                        changeVisibleStatusFoeArrows(ConstantsUI.k_ArrowPlayer1);
                    }
                    else
                    {                   
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
                    changeVisibleStatusFoeArrows(ConstantsUI.k_ArrowPlayer1);
                    invokeClickOnChecker(m_Game.AttackListOfPlayer1);
                }
            }
        }

        private void player2Moving()
        {
            m_Game.MoveTheCheckerOfTheCorecctPlayer(
                       m_Game.Player2,
                           m_Game.Player1,
                           convertCheckerPositionPointToSquareOfLogic(m_LastMoveFrom),
                           convertCheckerPositionPointToSquareOfLogic(m_LastMoveTo),
                           ref m_WasAttack,
                           ref m_HasAnotherAttack);
            if (!winnerOrDraw())
            {
                initCheckers();
                updateScore();
                enableOrDisableStartOverButton(ConstantsUI.k_Player1Turn);
                m_WasMove = false;
                if (!m_HasAnotherAttack)
                {
                    changeVisibleStatusFoeArrows(ConstantsUI.k_ArrowPlayer1);
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
                    changeVisibleStatusFoeArrows(ConstantsUI.k_ArrowPlayer2);
                    invokeClickOnChecker(m_Game.AttackListOfPlayer2);               
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
                       ref m_WasAttack,
                       ref m_HasAnotherAttack);
            if (!winnerOrDraw())
            {
                Thread.Sleep(1200);
                initCheckers();
                updateScore();
                enableOrDisableStartOverButton(ConstantsUI.k_Player1Turn);
                m_WasMove = false;
                if (!m_HasAnotherAttack)
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
                if (m_Game.AttackListOfPlayer2.Count > 0)
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
                    m_Board.GetBoard[optionalSquareToMove.X, optionalSquareToMove.Y].Tag = e_SquaerTag.OptionalMoveTo;
                }
            }
        }

        private void returnSqureToEmpty()
        {
            foreach (KeyValuePair<Point, Point> kvp in m_AllTheClickableSquareReadyToMove)
            {
                m_Board.GetBoard[kvp.Key.X, kvp.Key.Y].Tag = e_SquaerTag.WrongSelection;
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