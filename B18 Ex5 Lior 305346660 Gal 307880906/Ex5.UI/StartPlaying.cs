using System;
using System.Collections.Generic;
using System.Text;

namespace Ex5.UI
{
    public class StartPlaying
    {
        public void StartCheckersGame(SettingsLogin i_FormNameLogin)
        {
            FormGame form = new FormGame();
            //SettingsLogin formNameLogin = new SettingsLogin();
            //formNameLogin.ShowDialog();
            form.FormGameStart(i_FormNameLogin);
            if (form.WantsToPlay)
            {
                form.ShowDialog();
                ////if (!form.StartOverGame)
                ////{
                ////    formNameLogin = new SettingsLogin();
                ////    formNameLogin.ShowDialog();
                ////}

                ////form = new FormGame();
                ////form.FormGameStart(formNameLogin);
            }
        }       
    }
}
