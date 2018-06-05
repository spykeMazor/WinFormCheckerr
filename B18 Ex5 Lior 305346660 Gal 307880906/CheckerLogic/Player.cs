using System;
using System.Collections.Generic;
using System.Text;

namespace CheckerLogic
{
    public class Player
    {
        public enum e_LocationOfThePlayer
        {
            UP,
            DOWN
        }

        public enum e_SymbolOfThePlayer
        {
            O = 'O',
            X = 'X'
        }

        private readonly string r_PlayerName;

        private int m_NumberOfCheckers;

        private e_LocationOfThePlayer m_UpOrDown;

        private LinkedList<Checker> m_ListOfAllTheChecker = new LinkedList<Checker>();

        private bool m_MachineOrNot = false; //// if the player computer or not...

        private int m_BoardSize;

        public Player(string i_PlayerName, int i_BoardSize, int i_FirstOrSecondPlayer, bool i_ComputerFlag)
        {
            m_BoardSize = i_BoardSize;
            m_MachineOrNot = i_ComputerFlag;
            r_PlayerName = i_PlayerName;
            m_NumberOfCheckers = NumberOfCheckersByBoardSize(i_BoardSize);
            if (i_FirstOrSecondPlayer == 1)
            {
                m_UpOrDown = e_LocationOfThePlayer.UP;
            }
            else
            {
                m_UpOrDown = e_LocationOfThePlayer.DOWN;
            }
        }

        public static string ConvertSizeOfTheBoardToSquare(int i_SizeOfTheBoard)
        {
            string square = null;
            if (i_SizeOfTheBoard == Constants.K_SmallGameBoard)
            {
                square = "Ff";
            }
            else if (i_SizeOfTheBoard == Constants.K_MediumGameBoard)
            {
                square = "Hh";
            }
            else if (i_SizeOfTheBoard == Constants.K_LargeGameBoard)
            {
                square = "Jj";
            }

            return square;
        }

        public e_LocationOfThePlayer GetUpOrDown
        {
            get { return m_UpOrDown; }
        }

        public e_SymbolOfThePlayer Gete_SymbolOfThePlayer
        {
            get
            {
                e_SymbolOfThePlayer color;
                if (m_UpOrDown == e_LocationOfThePlayer.UP)
                {
                    color = e_SymbolOfThePlayer.O;
                }
                else
                {
                    color = e_SymbolOfThePlayer.X;
                }

                return color;
            }
        }

        public string GetName
        {
            get { return r_PlayerName; }
        }

        public bool MachineOrNot
        {
            get { return m_MachineOrNot; }
        }

        public LinkedList<Checker> ListOfCheckers
        {
            get { return m_ListOfAllTheChecker; }
        }

        private int NumberOfCheckersByBoardSize(int i_BoardSize)
        {
            int numberOfCheckers = 0;

            if (i_BoardSize == Constants.K_SmallGameBoard)
            {
                numberOfCheckers = Constants.K_SmallBoardNumberOfPlayers;
            }

            if (i_BoardSize == Constants.K_MediumGameBoard)
            {
                numberOfCheckers = Constants.K_MediumBoardNumberOfPlayers;
            }

            if (i_BoardSize == Constants.K_LargeGameBoard)
            {
                numberOfCheckers = Constants.K_LargeBoardNumberOfPlayers;
            }

            return numberOfCheckers;
        }

        public void InitAllTheCheckersOfOnePlayer(int i_SizeOfBoard, e_LocationOfThePlayer i_Location)
        {
            string limitSquare = ConvertSizeOfTheBoardToSquare(i_SizeOfBoard);
            char[] limitSqureAsTwoChars = { limitSquare[0], limitSquare[1] };
            ///------------Init all the checkers of the upper player----////
            if (i_Location == e_LocationOfThePlayer.UP)
            {
                m_ListOfAllTheChecker.Clear();
                char[] squreIterator = { 'B', 'a' };
                ////string squreIterator = "aA";
                for (int i = 0; i < m_NumberOfCheckers; i++)
                {
                    m_ListOfAllTheChecker.AddLast(new Checker(new Position(new string(squreIterator)), Checker.e_Symbol.O)); ////Create new checker and push hit to the end of the list
                    if (squreIterator[0] == limitSqureAsTwoChars[0])
                    { ////End of the line
                        squreIterator[0] = 'A';
                        squreIterator[1]++;
                    }
                    else if (squreIterator[0] == limitSqureAsTwoChars[0] - 1)
                    { ////End of the line
                        squreIterator[0] = 'B';
                        squreIterator[1]++;
                    }
                    else
                    {
                        squreIterator[0]++;
                        squreIterator[0]++;
                    }
                }
            }
            ////------------Init all the checkers of the downer player----////
            if (i_Location == e_LocationOfThePlayer.DOWN)
            {
                m_ListOfAllTheChecker.Clear();
                char startSmallLetter = StartSquareSmallLetter(i_SizeOfBoard);
                char startBigLetter;
                if (i_SizeOfBoard == 8)
                {
                    startBigLetter = 'A';
                }
                else
                {
                    startBigLetter = 'B';
                }

                char[] squreIterator = { startBigLetter, startSmallLetter };
                ////string squreIterator = "aA";
                for (int i = 0; i < m_NumberOfCheckers; i++)
                {
                    m_ListOfAllTheChecker.AddLast(new Checker(new Position(new string(squreIterator)), Checker.e_Symbol.X)); ////Create new checker and push him to the end of the list
                    if (squreIterator[0] == limitSqureAsTwoChars[0] - 1)
                    { ////End of the line 
                        squreIterator[0] = 'B';
                        squreIterator[1]++;
                    }
                    else if (squreIterator[0] == limitSqureAsTwoChars[0])
                    { ////End of the line
                        squreIterator[0] = 'A';
                        squreIterator[1]++;
                    }
                    else
                    {
                        squreIterator[0]++;
                        squreIterator[0]++;
                    }
                }
            }
        }

        public int NumberOfCheckers
        {
            get { return m_ListOfAllTheChecker.Count; }
        }

        private char StartSquareSmallLetter(int i_BoardSize)
        {
            char startSmallLetter = 'a'; ////Just for assign

            if (i_BoardSize == Constants.K_SmallGameBoard)
            {
                startSmallLetter = 'e';
            }
            else if (i_BoardSize == Constants.K_MediumGameBoard)
            {
                startSmallLetter = 'f';
            }
            else if (i_BoardSize == Constants.K_LargeGameBoard)
            {
                startSmallLetter = 'g';
            }

            return startSmallLetter;
        }

        public void MoveChecker(string i_MoveFrom, string i_MoveTo)
        {
            foreach (Checker checker in m_ListOfAllTheChecker)
            {
                if (checker.PositintOfTheChecker.SquareInTheBoard.Equals(i_MoveFrom))
                {
                    checker.SetPositionOfTheChecker(i_MoveTo);
                    if (checker.SymbolOfChecker == Checker.e_Symbol.O &&
                        checker.PositintOfTheChecker.Coordinate.Y == m_BoardSize - 1)
                    {
                        checker.SymbolOfChecker = Checker.e_Symbol.U;
                    }
                    else if (checker.SymbolOfChecker == Checker.e_Symbol.X &&
                        checker.PositintOfTheChecker.Coordinate.Y == 0)
                    {
                        checker.SymbolOfChecker = Checker.e_Symbol.K;
                    }

                    break;
                }
            }
        }
    }
}