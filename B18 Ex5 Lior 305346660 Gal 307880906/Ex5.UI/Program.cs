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
            SettingsLogin formNameLogin = new SettingsLogin();
            formNameLogin.ShowDialog();
            form.FormGameStart(formNameLogin);
            while (form.WantsToPlay)
            {
                form.ShowDialog();
                if (!form.StartOverGame)
                {
                    formNameLogin = new SettingsLogin();
                    formNameLogin.ShowDialog();
                }

                form = new FormGame();
                form.FormGameStart(formNameLogin);
            }
        }
    }
}