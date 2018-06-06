using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;

namespace Ex5.UI
{
    public class Program
    {
        public static void Main()
        {
            StartPlaying checkersGame = new StartPlaying();
            checkersGame.StartCheckersGame();
        }
    }
}