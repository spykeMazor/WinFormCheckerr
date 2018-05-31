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
            FormGame form = new FormGame();
            form.FormGameStart();
            while (form.WantsToPlay)
            {
                form.ShowDialog();
                form.FormGameStart();
            }
            //GameSettings f = new GameSettings();
            //f.ShowDialog();
        }

    }
}
