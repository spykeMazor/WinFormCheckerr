using System;
using System.Collections.Generic;
using System.Text;

namespace CheckerLogic
{
    public class Checker
    {
        public enum Symbol
        {
            O = 'O',
            U = 'U',
            X = 'X',
            K = 'K'
        }

        private Position m_PositionOfTheChecker;
        private Symbol m_SymbolOfChecker;

        public Checker(Position i_Position, Symbol i_SymbolOfChecker) /// Cto'r of Checker
        {
            PositionOfTheChecker = new Position(i_Position.SquareInTheBoard);
            m_SymbolOfChecker = i_SymbolOfChecker;
        }

        public Position PositintOfTheChecker
        {
            get
            {
                return m_PositionOfTheChecker;
            }
        }

        public void SetPositionOfTheChecker(string i_NewSquareInTheBoard)
        {
            m_PositionOfTheChecker.SetPosition(i_NewSquareInTheBoard);
        }

        public Symbol SymbolOfChecker
        {
            get
            {
                return m_SymbolOfChecker;
            }

            set
            {
                m_SymbolOfChecker = value;
            }
        }

        public Position PositionOfTheChecker
        {
            get
            {
                return m_PositionOfTheChecker;
            }

            set
            {
                m_PositionOfTheChecker = value;
            }
        }

        public void MoveChecker(string i_NewPos)
        { 
            SetPositionOfTheChecker(i_NewPos);
        }
    }
}