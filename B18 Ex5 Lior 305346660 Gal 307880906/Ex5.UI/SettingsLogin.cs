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
        private int m_Player1TotalScore;
        private int m_Player2TotalScore;

        public SettingsLogin()
        {
            Player1TotalScoreCounter = 0;
            Player2TotalScoreCounter = 0;
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
        }

        private void checkBoxPlayer2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPlayer2.Checked)
            {
                textBoxPlayer2Name.Clear();
                textBoxPlayer2Name.BackColor = System.Drawing.SystemColors.Window;
                textBoxPlayer2Name.Enabled = true;
                textBoxPlayer2Name.TabStop = true;
            }
            else
            {
                textBoxPlayer2Name.Text = ConstantsUI.k_ComputerNameTextBox;
                textBoxPlayer2Name.BackColor = System.Drawing.SystemColors.AppWorkspace;
                textBoxPlayer2Name.Enabled = false;
                textBoxPlayer2Name.TabStop = false;
            }
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            bool userChoosedSize = true;
            LogInExceptionForm logInException;
     
            if ((Player1Name.Length > 0) && (Player2Name.Length > 0) && (Player1Name.Length < 15) && (Player2Name.Length < 15))
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
                    //MessageBox.Show(ConstantsUI.k_BoardSizeLogInException, "Board size error", checkrhis, MessageBoxIcon.Information);
                    logInException = new LogInExceptionForm(ConstantsUI.k_BoardSizeLogInException, ConstantsUI.k_BoardSizeLogInExceptionTitle);
                    logInException.ShowDialog();
                }
            }
            else
            {
                logInException = new LogInExceptionForm(ConstantsUI.k_NameLogInException, ConstantsUI.k_NameLogInExceptionTitle);
                logInException.ShowDialog();
            }
        }

        public string Player1Name
        {
            get { return textBoxPlayer1Name.Text; }
            set { textBoxPlayer1Name.Text = value; }
        }

        public string Player2Name
        {
            get
            {
                string player2Name;
                if (textBoxPlayer2Name.Text.CompareTo(ConstantsUI.k_ComputerNameTextBox) == 0)
                {
                    player2Name = ConstantsUI.k_ComputerPlayerName;
                }
                else
                {
                    player2Name = textBoxPlayer2Name.Text;
                }

                return player2Name;
            }
            set { textBoxPlayer2Name.Text = value; }
        }

        public int Player1TotalScoreCounter
        {
            get { return m_Player1TotalScore; }
            set { m_Player1TotalScore = value; }
        }

        public int Player2TotalScoreCounter
        {
            get { return m_Player2TotalScore; }
            set { m_Player2TotalScore = value; }
        }

        public GameBoardUI.e_BoardSize BoardSizeSelection
        {
            get { return m_BoardSizeSelection; }
        }

        public bool ComputerOrNot
        {
            get {return checkBoxPlayer2.Checked; }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            VerifyForm quitForm = new VerifyForm(ConstantsUI.k_QuitMessage, ConstantsUI.k_QuitMessageTitle);
            quitForm.ShowDialog();
            if (quitForm.DialogResult == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
