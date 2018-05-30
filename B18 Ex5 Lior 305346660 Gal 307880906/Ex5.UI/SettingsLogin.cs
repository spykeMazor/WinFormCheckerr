using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Ex5.UI
{
    public partial class SettingsLogin : Form
    {
        GameBoardUI.e_BoardSize m_BoardSizeSelection;

        public SettingsLogin()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
        }

        private void checkBoxPlayer2_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxPlayer2.Checked)
            {
                textBoxPlayer2Name.Clear();
                textBoxPlayer2Name.BackColor = System.Drawing.SystemColors.Window;
                textBoxPlayer2Name.Enabled = true;
                textBoxPlayer2Name.TabStop = true;
            }
            else
            {
                textBoxPlayer2Name.Text = "[Computer]";
                textBoxPlayer2Name.BackColor = System.Drawing.SystemColors.AppWorkspace;
                textBoxPlayer2Name.Enabled = false;
                textBoxPlayer2Name.TabStop = false;
            }
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            bool userChoosedSize = true ;
            LogInNameExceptionForm logInNameException;
            LogInBoardSizeExceptionForm logInBoardSizeException;
            if ((Player1Name.Length > 0) && (Player2Name.Length > 0))
            {
                if (boardGameSmallSize.Checked)
                {
                    m_BoardSizeSelection = GameBoardUI.e_BoardSize.Small;
                }
                else if (boardGameMediumSize.Checked)
                {
                    m_BoardSizeSelection = GameBoardUI.e_BoardSize.Medium;
                }
                else if (boardGameLargeSize.Checked)
                {
                    m_BoardSizeSelection = GameBoardUI.e_BoardSize.Large;
                }
                else
                {
                    userChoosedSize = false;
                }

                if (userChoosedSize)
                {
                    this.DialogResult = DialogResult.OK;

                }
                else
                {
                    logInBoardSizeException = new LogInBoardSizeExceptionForm();
                    logInBoardSizeException.ShowDialog();
                }
            }
            else
            {
                logInNameException = new LogInNameExceptionForm();
                logInNameException.ShowDialog();
            }          
        }

        public string Player1Name
        {
            get { return textBoxPlayer1Name.Text; }
            set { textBoxPlayer1Name.Text = value; }
        }

        public string Player2Name
        {
            get { return textBoxPlayer2Name.Text; }
            set { textBoxPlayer2Name.Text = value; }
        }

        public GameBoardUI.e_BoardSize BoardSizeSelection
        {
            get { return m_BoardSizeSelection; }
        }
    }
}
