using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using CheckerLogic;

namespace Ex5.UI
{
    public class PictureBoxInTheBoard : PictureBox
    {
        private Point m_PointInTheBoard = new Point();

        public Point PointInTheBoard
        {
            get { return m_PointInTheBoard; }
            set { m_PointInTheBoard = value; }
        }
    }
}
