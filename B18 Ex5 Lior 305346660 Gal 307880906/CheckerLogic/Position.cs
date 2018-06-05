using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CheckerLogic
{
    public struct PointOfPosition
    {
        private int m_X;

        private int m_Y;

        public PointOfPosition(int i_X, int i_Y)
        {
            m_X = i_X;
            m_Y = i_Y;
        }

        public int X
        {
            get { return m_X; }
            set
            {
                m_X = value;
            }
        }

        public int Y
        {
            get { return m_Y; }
            set
            {
                m_Y = value;
            }
        }
    }

    public class Position
    {
        private PointOfPosition m_Coord;
        private string m_SquareInTheBoard;

        public Position(string i_StartSquarePosition) //// Cto'r of Position
        {
            m_SquareInTheBoard = i_StartSquarePosition;
            m_Coord = ConvertSqureToPoint(i_StartSquarePosition);
        }

        public static PointOfPosition ConvertSqureToPoint(string i_Square)
        {
            char[] coords = { i_Square[0], i_Square[1] };
            PointOfPosition point = new PointOfPosition(coords[0] - 'A', coords[1] - 'a');
            return point;
        }

        public static string ConvertPointToSquare(PointOfPosition i_Point)
        {
            char[] coords = new char[2];
            coords[0] = (char)(i_Point.X + 'A');
            coords[1] = (char)(i_Point.Y + 'a');
            string square = new string(coords);
            return square;
        }

        public string SquareInTheBoard
        {
            get { return m_SquareInTheBoard; }
        }

        public PointOfPosition Coordinate
        {
            get { return m_Coord; }
        }

        public void SetPosition(string i_NewSquare)
        {
            m_Coord = ConvertSqureToPoint(i_NewSquare);
            m_SquareInTheBoard = i_NewSquare;
        }
    }
}
