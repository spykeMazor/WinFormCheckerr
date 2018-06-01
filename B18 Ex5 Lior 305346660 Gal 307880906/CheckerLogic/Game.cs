using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using static System.Math;
namespace CheckerLogic
{
    public class Game
    {
        private Player m_Player1;

        private Player m_Player2;

        private readonly LinkedList<KeyValuePair<string, string>> r_MovesListOfPlayer1 = new LinkedList<KeyValuePair<string, string>>();
                                                                  
        private readonly LinkedList<KeyValuePair<string, string>> r_MovesListOfPlayer2 = new LinkedList<KeyValuePair<string, string>>();
                                                                  
        private LinkedList<KeyValuePair<string, string>> m_AttackListOfPlayer1 = new LinkedList<KeyValuePair<string, string>>();
                                                                  
        private LinkedList<KeyValuePair<string, string>> m_AttackListOfPlayer2 = new LinkedList<KeyValuePair<string, string>>();
                                                                  
        private int m_BoardSize;

        private char[,] m_MatrixBoardForTesting;

        public Game(Player i_Player1, Player i_Player2, int i_BoardSize)
        {
            m_Player1 = i_Player1;
            m_Player2 = i_Player2;
            m_BoardSize = i_BoardSize;
            m_MatrixBoardForTesting = new char[m_BoardSize, m_BoardSize];
            CreateMatrixBoardForTesting();
        }

        public static bool ListContainTwoStringsInOneNode(LinkedList<KeyValuePair<string, string>> i_ListOfTwoStrings, string i_String1, string i_String2)
        {
            bool contain = false;
            if (i_ListOfTwoStrings.Contains(new KeyValuePair<string, string>(i_String1, i_String2)))
            {
                contain = true;
            }

            return contain;
        }

        public char[,] GetTestingMatrix
        {
            get { return m_MatrixBoardForTesting; }
        }

        public int BoradSize
        {
            get { return m_BoardSize; }
        }

        public LinkedList<KeyValuePair<string, string>> MoveListOfPlayer1
        {
            get { return  r_MovesListOfPlayer1; }
        }

        public LinkedList<KeyValuePair<string, string>> MoveListOfPlayer2
        {
            get { return  r_MovesListOfPlayer2; }
        }

        public LinkedList<KeyValuePair<string, string>> AttackListOfPlayer1
        {
            get { return  m_AttackListOfPlayer1; }
        }

        public LinkedList<KeyValuePair<string, string>> AttackListOfPlayer2
        {
            get { return  m_AttackListOfPlayer2; }
        }

        public void MoveTheCheckerOfTheCorecctPlayer(Player i_CorrectPlayer, Player i_SecondPlayer, string i_InputMoveFrom, string i_InputMoveTo,ref bool i_AttackFlag, ref bool i_AttackMoreFlag)
        {
            i_CorrectPlayer.MoveChecker(i_InputMoveFrom, i_InputMoveTo);
            i_AttackFlag = ConfirmAttack(i_InputMoveFrom, i_InputMoveTo, i_CorrectPlayer, this);
            UpdateTestingMatrix(i_InputMoveFrom, i_InputMoveTo, i_AttackFlag);
            UpdateMoveList(i_CorrectPlayer.GetUpOrDown);
            UpdateMoveList(i_SecondPlayer.GetUpOrDown);
            if (i_AttackFlag)
            {
                if (i_CorrectPlayer.GetUpOrDown == Player.e_LocationOfThePlayer.UP)
                {
                    i_AttackMoreFlag = CanAttackMore(i_InputMoveTo, m_AttackListOfPlayer1);
                    updateAttackListForOneMoreAttack(i_InputMoveTo, ref m_AttackListOfPlayer1);
                }
                else
                {
                    i_AttackMoreFlag = CanAttackMore(i_InputMoveTo, m_AttackListOfPlayer2);
                    updateAttackListForOneMoreAttack(i_InputMoveTo, ref m_AttackListOfPlayer2);
                }
            }
        }

        public int Player1Score()
        {
            int scoreForKings = 0;
            int distance;
            foreach (Checker checker in m_Player1.ListOfCheckers)
            {
                if (checker.SymbolOfChecker == Checker.Symbol.U)
                {
                    scoreForKings += 4;
                }
            }

            distance = m_Player1.ListOfCheckers.Count - m_Player2.ListOfCheckers.Count;
            if (distance < 0)
            {
                distance = 0;
            }

            return scoreForKings + distance;
        }

        public int Player2Score()
        {
            int scoreForKings = 0;
            int distance = 0;
            foreach (Checker checker in m_Player2.ListOfCheckers)
            {
                if (checker.SymbolOfChecker == Checker.Symbol.K)
                {
                    scoreForKings += 4;
                }
            }

            distance = m_Player2.ListOfCheckers.Count - m_Player1.ListOfCheckers.Count;

            if (distance < 0)
            {
                distance = 0;
            }

            return scoreForKings + distance;
        }

        public void ComputerPlaying()
        {


        }

        public KeyValuePair<string, string> SelectRandomMove()
        {
            Random rand = new Random();
            KeyValuePair<string, string> selectedMove = new KeyValuePair<string, string>();
            UpdateMoveListOfPlayer2();
            if ( m_AttackListOfPlayer2.Count > 0)
            {
                int counter = 0;
                int numberOfMoves =  m_AttackListOfPlayer2.Count;
                int randomNumber = rand.Next(numberOfMoves);
                foreach (KeyValuePair<string, string> move in  m_AttackListOfPlayer2)
                {
                    if (counter == randomNumber)
                    {
                        selectedMove = move;
                    }

                    counter++;
                }
            }
            else
            {
                int counter = 0;
                int numberOfMoves =  r_MovesListOfPlayer2.Count;
                int randomNumber = rand.Next(numberOfMoves);
                foreach (KeyValuePair<string, string> move in  r_MovesListOfPlayer2)
                {
                    if (counter == randomNumber)
                    {
                        selectedMove = move;
                    }

                    counter++;
                }
            }

            return selectedMove;
        }

        private void UpdateTestingMatrix(string i_InputMoveFrom, string i_InputMoveTo, bool i_AttackFlag)
        {
            PointOfPosition moveFromPoint = new PointOfPosition();
            PointOfPosition moveToPoint = new PointOfPosition();
            moveFromPoint = Position.ConvertSqureToPoint(i_InputMoveFrom);
            moveToPoint = Position.ConvertSqureToPoint(i_InputMoveTo);
            if (m_MatrixBoardForTesting[moveFromPoint.Y, moveFromPoint.X] == (char)Checker.Symbol.O && moveToPoint.Y == m_BoardSize - 1)
            {
                m_MatrixBoardForTesting[moveToPoint.Y, moveToPoint.X] = (char)Checker.Symbol.U;
                m_MatrixBoardForTesting[moveFromPoint.Y, moveFromPoint.X] = '\0';
            }
            else if (m_MatrixBoardForTesting[moveFromPoint.Y, moveFromPoint.X] == (char)Checker.Symbol.X && moveToPoint.Y == 0)
            {
                m_MatrixBoardForTesting[moveToPoint.Y, moveToPoint.X] = (char)Checker.Symbol.K;
                m_MatrixBoardForTesting[moveFromPoint.Y, moveFromPoint.X] = '\0';
            }
            else
            {
                m_MatrixBoardForTesting[moveToPoint.Y, moveToPoint.X] = m_MatrixBoardForTesting[moveFromPoint.Y, moveFromPoint.X];
                m_MatrixBoardForTesting[moveFromPoint.Y, moveFromPoint.X] = '\0';
            }

            if (i_AttackFlag)
            {
                UpdadeAfterAttacking(moveFromPoint, moveToPoint);
            }
        }

        private void UpdadeAfterAttacking(PointOfPosition i_InputMoveFromPoint, PointOfPosition i_InputMoveToPoint)
        {
            if (i_InputMoveFromPoint.X + 2 == i_InputMoveToPoint.X && i_InputMoveFromPoint.Y + 2 == i_InputMoveToPoint.Y)
            { ////Down Right Attack

                RemoveChecker(new PointOfPosition(i_InputMoveFromPoint.X + 1, i_InputMoveFromPoint.Y + 1));
            }
            else if (i_InputMoveFromPoint.X - 2 == i_InputMoveToPoint.X && i_InputMoveFromPoint.Y + 2 == i_InputMoveToPoint.Y)
            { ////Down Left Attack
                RemoveChecker(new PointOfPosition(i_InputMoveFromPoint.X - 1, i_InputMoveFromPoint.Y + 1));
            }
            else if (i_InputMoveFromPoint.X - 2 == i_InputMoveToPoint.X && i_InputMoveFromPoint.Y - 2 == i_InputMoveToPoint.Y)
            { ////Up Left Attack
                RemoveChecker(new PointOfPosition(i_InputMoveFromPoint.X - 1, i_InputMoveFromPoint.Y - 1));
            }
            else if (i_InputMoveFromPoint.X + 2 == i_InputMoveToPoint.X && i_InputMoveFromPoint.Y - 2 == i_InputMoveToPoint.Y)
            { ////Up Right Attack
                RemoveChecker(new PointOfPosition(i_InputMoveFromPoint.X + 1, i_InputMoveFromPoint.Y - 1));
            }
        }

        private void RemoveChecker(PointOfPosition i_CheckerSquarePoint)
        {
            char colorOfTheChecker = m_MatrixBoardForTesting[i_CheckerSquarePoint.Y, i_CheckerSquarePoint.X];
            string squareOfThePoint = Position.ConvertPointToSquare(i_CheckerSquarePoint);
            if (colorOfTheChecker == (char)Checker.Symbol.O || colorOfTheChecker == (char)Checker.Symbol.U)
            {
                foreach (Checker checker in m_Player1.ListOfCheckers)
                {
                    if (checker.PositintOfTheChecker.SquareInTheBoard.Equals(squareOfThePoint))
                    {
                        m_Player1.ListOfCheckers.Remove(checker);
                        break;
                    }
                }
            }

            if (colorOfTheChecker == (char)Checker.Symbol.X || colorOfTheChecker == (char)Checker.Symbol.K)
            {
                foreach (Checker checker in m_Player2.ListOfCheckers)
                {
                    if (checker.PositintOfTheChecker.SquareInTheBoard.Equals(squareOfThePoint))
                    {
                        m_Player2.ListOfCheckers.Remove(checker);
                        break;
                    }
                }
            }

            m_MatrixBoardForTesting[i_CheckerSquarePoint.Y, i_CheckerSquarePoint.X] = '\0';
        }

        public Player Player1
        {
            get { return m_Player1; }
        }

        public Player Player2
        {
            get { return m_Player2; }
        }

        private void CreateMatrixBoardForTesting()
        {
            foreach (Checker checker in m_Player1.ListOfCheckers)
            {
                m_MatrixBoardForTesting[checker.PositintOfTheChecker.Coordinate.Y, checker.PositintOfTheChecker.Coordinate.X] = (char)checker.SymbolOfChecker;
            }

            foreach (Checker checker in m_Player2.ListOfCheckers)
            {
                m_MatrixBoardForTesting[checker.PositintOfTheChecker.Coordinate.Y, checker.PositintOfTheChecker.Coordinate.X] = (char)checker.SymbolOfChecker;
            }
        }

        public bool IllegalMove(Player i_CorrectPlayer, Player i_SecondPlayer, string i_MoveFrom, string i_MoveTo)
        {
            bool llegalMove = true;
            if (!LliegalInputByBoardSize(i_MoveFrom, i_MoveTo))
            {
                llegalMove = false;
            }
            else if (!OptionMove(i_MoveFrom, i_MoveTo, i_CorrectPlayer.GetUpOrDown))
            {
                llegalMove = false;
            }

            return llegalMove;
        }

        private bool OptionMove(string i_MoveFrom, string i_MoveTo, Player.e_LocationOfThePlayer i_UpOrDownPlayer)
        {
            bool optionalMove = false;
            if (i_UpOrDownPlayer == Player.e_LocationOfThePlayer.UP)
            {
                if (ListContainTwoStringsInOneNode( r_MovesListOfPlayer1, i_MoveFrom, i_MoveTo))
                {
                    optionalMove = true;
                }
            }
            else
            {
                if (ListContainTwoStringsInOneNode( r_MovesListOfPlayer2, i_MoveFrom, i_MoveTo))
                {
                    optionalMove = true;
                }
            }

            return optionalMove;
        }

        private bool LliegalInputByBoardSize(string i_MoveFrom, string i_MoveTo)
        {
            bool lligalMove = true;
            string limitSquare = Player.ConvertSizeOfTheBoardToSquare(m_BoardSize);
            char[] limitSqureAsTwoChars = { limitSquare[0], limitSquare[1] };

            if (i_MoveFrom[0] < 'A' || i_MoveFrom[0] > limitSqureAsTwoChars[0])
            {
                lligalMove = false;
            }
            else if (i_MoveFrom[1] < 'a' || i_MoveFrom[1] > limitSqureAsTwoChars[1])
            {
                lligalMove = false;
            }
            else if (i_MoveTo[0] < 'A' || i_MoveTo[0] > limitSqureAsTwoChars[0])
            {
                lligalMove = false;
            }
            else if (i_MoveTo[1] < 'a' || i_MoveTo[1] > limitSqureAsTwoChars[1])
            {
                lligalMove = false;
            }

            return lligalMove;
        }

        public void UpdateMoveList(Player.e_LocationOfThePlayer i_UpOrDownPlayer)
        {
            if (i_UpOrDownPlayer == Player.e_LocationOfThePlayer.UP)
            {
                UpdateMoveListOfPlayer1();
            }
            else
            {
                UpdateMoveListOfPlayer2();
            }
        }

        public void UpdateMoveListOfPlayer1()
        {
             r_MovesListOfPlayer1.Clear();
             m_AttackListOfPlayer1.Clear();
            foreach (Checker checker in m_Player1.ListOfCheckers)
            {
                int xCoordinate = checker.PositintOfTheChecker.Coordinate.X;
                int yCoordinate = checker.PositintOfTheChecker.Coordinate.Y;
                char SymbolOfChecker = (char)checker.SymbolOfChecker;
                Check4OptionalMoves( r_MovesListOfPlayer1, xCoordinate, yCoordinate, SymbolOfChecker);
                Check4OptionalAttacks( r_MovesListOfPlayer1,  m_AttackListOfPlayer1, xCoordinate, yCoordinate, SymbolOfChecker);
            }
        }

        public void UpdateMoveListOfPlayer2()
        {
             r_MovesListOfPlayer2.Clear();
             m_AttackListOfPlayer2.Clear();
            foreach (Checker checker in m_Player2.ListOfCheckers)
            {
                int xCoordinate = checker.PositintOfTheChecker.Coordinate.X;
                int yCoordinate = checker.PositintOfTheChecker.Coordinate.Y;
                char SymbolOfChecker = (char)checker.SymbolOfChecker;
                Check4OptionalMoves( r_MovesListOfPlayer2, xCoordinate, yCoordinate, SymbolOfChecker);
                Check4OptionalAttacks( r_MovesListOfPlayer2,  m_AttackListOfPlayer2, xCoordinate, yCoordinate, SymbolOfChecker);

            }
        }
        private void Check4OptionalMoves(LinkedList<KeyValuePair<string, string>> r_MovesListOfPlayer, int i_XCoordinate, int i_YCoordinate, char i_SymbolOfChecker)
        {
            PointOfPosition checkerPoint = new PointOfPosition(i_XCoordinate, i_YCoordinate);
            PointOfPosition leftUp = new PointOfPosition(i_XCoordinate - 1, i_YCoordinate - 1);
            PointOfPosition rightUp = new PointOfPosition(i_XCoordinate + 1, i_YCoordinate - 1);
            PointOfPosition leftDown = new PointOfPosition(i_XCoordinate - 1, i_YCoordinate + 1);
            PointOfPosition rightDown = new PointOfPosition(i_XCoordinate + 1, i_YCoordinate + 1);
            char SymbolOfChecker = i_SymbolOfChecker;
            if (SymbolOfChecker == (char)Checker.Symbol.X || SymbolOfChecker == (char)Checker.Symbol.K ||
                SymbolOfChecker == (char)Checker.Symbol.U)
            {
                if ((leftUp.X >= 0 && leftUp.Y >= 0) && (leftUp.X < m_BoardSize && leftUp.Y < m_BoardSize))
                {
                    if (m_MatrixBoardForTesting[leftUp.Y, leftUp.X] == '\0')
                    {
                        KeyValuePair<string, string> tempKVP = new KeyValuePair<string, string>(Position.ConvertPointToSquare(checkerPoint), Position.ConvertPointToSquare(leftUp));
                        r_MovesListOfPlayer.AddLast(tempKVP);
                    }
                }

                if ((rightUp.X >= 0 && rightUp.Y >= 0) && (rightUp.X < m_BoardSize && rightUp.Y < m_BoardSize))
                {
                    if (m_MatrixBoardForTesting[rightUp.Y, rightUp.X] == '\0')
                    {
                        KeyValuePair<string, string> tempKVP = new KeyValuePair<string, string>(Position.ConvertPointToSquare(checkerPoint), Position.ConvertPointToSquare(rightUp));
                        r_MovesListOfPlayer.AddLast(tempKVP);
                    }
                }
            }

            if (SymbolOfChecker == (char)Checker.Symbol.O || SymbolOfChecker == (char)Checker.Symbol.K ||
               SymbolOfChecker == (char)Checker.Symbol.U)
            {
                if ((leftDown.X >= 0 && leftDown.Y >= 0) && (leftDown.X < m_BoardSize && leftDown.Y < m_BoardSize))
                {
                    if (m_MatrixBoardForTesting[leftDown.Y, leftDown.X] == '\0')
                    {
                        KeyValuePair<string, string> tempKVP = new KeyValuePair<string, string>(Position.ConvertPointToSquare(checkerPoint), Position.ConvertPointToSquare(leftDown));
                        r_MovesListOfPlayer.AddLast(tempKVP);
                    }
                }

                if ((rightDown.X >= 0 && rightDown.Y >= 0) && (rightDown.X < m_BoardSize && rightDown.Y < m_BoardSize))
                {
                    if (m_MatrixBoardForTesting[rightDown.Y, rightDown.X] == '\0')
                    {
                        KeyValuePair<string, string> tempKVP = new KeyValuePair<string, string>(Position.ConvertPointToSquare(checkerPoint), Position.ConvertPointToSquare(rightDown));
                        r_MovesListOfPlayer.AddLast(tempKVP);
                    }
                }
            }
        }

        private void Check4OptionalAttacks(
         LinkedList<KeyValuePair<string, string>> r_MovesListOfPlayer,
         LinkedList<KeyValuePair<string, string>> m_AttackListOfPlayer,
        int i_XCoordinate,
        int i_YCoordinate,
        char i_SymbolOfChecker)
        {
            PointOfPosition checkerPoint = new PointOfPosition(i_XCoordinate, i_YCoordinate);
            PointOfPosition leftUp = new PointOfPosition(i_XCoordinate - 1, i_YCoordinate - 1);
            PointOfPosition rightUp = new PointOfPosition(i_XCoordinate + 1, i_YCoordinate - 1);
            PointOfPosition leftDown = new PointOfPosition(i_XCoordinate - 1, i_YCoordinate + 1);
            PointOfPosition rightDown = new PointOfPosition(i_XCoordinate + 1, i_YCoordinate + 1);
            PointOfPosition leftUpAfterAttack = new PointOfPosition(i_XCoordinate - 2, i_YCoordinate - 2);
            PointOfPosition rightUpAfterAttack = new PointOfPosition(i_XCoordinate + 2, i_YCoordinate - 2);
            PointOfPosition leftDownAfterAttack = new PointOfPosition(i_XCoordinate - 2, i_YCoordinate + 2);
            PointOfPosition rightDownAfterAttack = new PointOfPosition(i_XCoordinate + 2, i_YCoordinate + 2);
            char SymbolOfChecker = i_SymbolOfChecker;
            //Player.e_LocationOfThePlayer location;
            //if(SymbolOfChecker == (char)Checker.Symbol.U || SymbolOfChecker ==)
            if (SymbolOfChecker == (char)Checker.Symbol.X || SymbolOfChecker == (char)Checker.Symbol.K ||
                SymbolOfChecker == (char)Checker.Symbol.U)
            {
                if ((leftUpAfterAttack.X >= 0 && leftUpAfterAttack.Y >= 0) &&
                    (leftUpAfterAttack.X < m_BoardSize && leftUpAfterAttack.Y < m_BoardSize))
                {
                    if (m_MatrixBoardForTesting[leftUpAfterAttack.Y, leftUpAfterAttack.X] == '\0' &&
                        m_MatrixBoardForTesting[leftUp.Y, leftUp.X] != SymbolOfChecker &&
                        m_MatrixBoardForTesting[leftUp.Y, leftUp.X] != '\0')
                    {
                        KeyValuePair<string, string> tempKVP = new KeyValuePair<string, string>(Position.ConvertPointToSquare(checkerPoint), Position.ConvertPointToSquare(leftUpAfterAttack));
                        r_MovesListOfPlayer.AddLast(tempKVP);
                        m_AttackListOfPlayer.AddLast(tempKVP);
                    }
                }

                if ((rightUpAfterAttack.X >= 0 && rightUpAfterAttack.Y >= 0) &&
                    (rightUpAfterAttack.X < m_BoardSize && rightUpAfterAttack.Y < m_BoardSize))
                {
                    if (m_MatrixBoardForTesting[rightUpAfterAttack.Y, rightUpAfterAttack.X] == '\0' &&
                        m_MatrixBoardForTesting[rightUp.Y, rightUp.X] != SymbolOfChecker &&
                        m_MatrixBoardForTesting[rightUp.Y, rightUp.X] != '\0')
                    {
                        KeyValuePair<string, string> tempKVP = new KeyValuePair<string, string>(Position.ConvertPointToSquare(checkerPoint), Position.ConvertPointToSquare(rightUpAfterAttack));
                        r_MovesListOfPlayer.AddLast(tempKVP);
                        m_AttackListOfPlayer.AddLast(tempKVP);
                    }
                }
            }

            if (SymbolOfChecker == (char)Checker.Symbol.O || SymbolOfChecker == (char)Checker.Symbol.K ||
               SymbolOfChecker == (char)Checker.Symbol.U)
            {
                if ((leftDownAfterAttack.X >= 0 && leftDownAfterAttack.Y >= 0) &&
                    (leftDownAfterAttack.X < m_BoardSize && leftDownAfterAttack.Y < m_BoardSize))
                {
                    if (m_MatrixBoardForTesting[leftDownAfterAttack.Y, leftDownAfterAttack.X] == '\0' &&
                        m_MatrixBoardForTesting[leftDown.Y, leftDown.X] != SymbolOfChecker &&
                        m_MatrixBoardForTesting[leftDown.Y, leftDown.X] != '\0')
                    {
                        KeyValuePair<string, string> tempKVP = new KeyValuePair<string, string>(Position.ConvertPointToSquare(checkerPoint), Position.ConvertPointToSquare(leftDownAfterAttack));
                        r_MovesListOfPlayer.AddLast(tempKVP);
                        m_AttackListOfPlayer.AddLast(tempKVP);
                    }
                }

                if ((rightDownAfterAttack.X >= 0 && rightDownAfterAttack.Y >= 0) &&
                    (rightDownAfterAttack.X < m_BoardSize && rightDownAfterAttack.Y < m_BoardSize))
                {
                    if (m_MatrixBoardForTesting[rightDownAfterAttack.Y, rightDownAfterAttack.X] == '\0' &&
                        m_MatrixBoardForTesting[rightDown.Y, rightDown.X] != SymbolOfChecker &&
                        m_MatrixBoardForTesting[rightDown.Y, rightDown.X] != '\0')
                    {
                        KeyValuePair<string, string> tempKVP = new KeyValuePair<string, string>(Position.ConvertPointToSquare(checkerPoint), Position.ConvertPointToSquare(rightDownAfterAttack));
                        r_MovesListOfPlayer.AddLast(tempKVP);
                        m_AttackListOfPlayer.AddLast(tempKVP);
                    }
                }
            }
        }

        public Player TheWinnerPlayerIs()
        {
            Player winner;
            UpdateMoveList(Player.e_LocationOfThePlayer.UP);
            UpdateMoveList(Player.e_LocationOfThePlayer.DOWN);
            if (MoveListOfPlayer1.Count == 0 || Player1.ListOfCheckers.Count == 0)
            {
                winner = m_Player2;
            }
            else if (MoveListOfPlayer2.Count == 0 || Player2.ListOfCheckers.Count == 0)
            {
                winner = m_Player1;
            }
            else
            {
                winner = null;
            }

            return winner;
        }

        public bool ThereIsDraw()
        {
            bool draw = false;
            if (MoveListOfPlayer2.Count == 0 && MoveListOfPlayer1.Count == 0)
            {
                draw = true;
            }

            return draw;
        }

        ////MOVES AND ATTACKS CATEGORY
        private void updateAttackListForOneMoreAttack(string i_MoveFrom, ref LinkedList<KeyValuePair<string, string>> i_AttackingListOfTheCorrectPlayer)
        {
            LinkedList<KeyValuePair<string, string>> updatedAttackList = new LinkedList<KeyValuePair<string, string>>();
            foreach (KeyValuePair<string, string> kvp in i_AttackingListOfTheCorrectPlayer)
            {
                if (kvp.Key.Equals(i_MoveFrom))
                {
                    updatedAttackList.AddLast(kvp);
                }
            }

            i_AttackingListOfTheCorrectPlayer.Clear();
            foreach (KeyValuePair<string, string> kvp in updatedAttackList)
            {
                i_AttackingListOfTheCorrectPlayer.AddLast(kvp);

            }
        }

        public bool CanAttackMore(string i_MoveFrom, LinkedList<KeyValuePair<string, string>> i_AttackingListOfTheCorrectPlayer)
        {
            bool anotherAttack = false;
            foreach (KeyValuePair<string, string> kvp in i_AttackingListOfTheCorrectPlayer)
            {
                if (kvp.Key.Equals(i_MoveFrom))
                {
                    anotherAttack = true;
                    break;
                }
            }

            return anotherAttack;
        }

        public bool ConfirmAnotherAttack(string i_MoveFrom, string i_MoveTo, LinkedList<KeyValuePair<string, string>> i_AttackingListOfTheCorrectPlayer)
        {
            bool llegalInput = false;
            if (ListContainTwoStringsInOneNode(i_AttackingListOfTheCorrectPlayer, i_MoveFrom, i_MoveTo))
            {
                llegalInput = true;
            }

            return llegalInput;
        }

        public bool ConfirmAttack(string i_FromMove, string i_ToMove, Player i_CorrectPlayer, Game i_CorrectGame)
        {
            bool attacking = false;
            KeyValuePair<string, string> attackMove = new KeyValuePair<string, string>(i_FromMove, i_ToMove);
            if (i_CorrectPlayer.GetUpOrDown == Player.e_LocationOfThePlayer.UP)
            {
                if (i_CorrectGame.AttackListOfPlayer1.Contains(attackMove))
                {
                    attacking = true;
                }
            }
            else
            {
                if (i_CorrectGame.AttackListOfPlayer2.Contains(attackMove))
                {
                    attacking = true;
                }
            }

            return attacking;
        }
    }
    //// class game
}

////nameSpace