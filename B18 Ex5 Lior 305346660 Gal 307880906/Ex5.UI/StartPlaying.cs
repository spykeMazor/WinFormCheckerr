using System;
using System.Collections.Generic;
using System.Text;

namespace Ex5.UI
{
    public class StartPlaying
    {
        public void StartCheckersGame()
        {
            FormGame form;
            SettingsLogin formNameLogin = new SettingsLogin();
            formNameLogin.ShowDialog();
            form = new FormGame(formNameLogin);
            while (form.WantsToPlay)
            {
                form.ShowDialog();
                if (!form.StartOverGame)
                {
                    formNameLogin = new SettingsLogin();
                    formNameLogin.ShowDialog();
                }

                form = new FormGame(formNameLogin);
            }
        }       
    }
}
