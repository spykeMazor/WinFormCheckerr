using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Ex5.UI
{
    public class GameSettings : Form
    {
        //GroupBox m_GroupBoxOfRadoiButton = new GroupBox();

        RadioButton m_RadioButtonSmallBoard = new RadioButton();
        RadioButton m_RadioButtonMediumBoard = new RadioButton();
        RadioButton m_RadioButtonLargeBoard = new RadioButton();

        TextBox m_TextboxPlayer1name = new TextBox();
        TextBox m_TextboxPlayer2name = new TextBox();

        Label m_LabelBoardSize = new Label();
        Label m_LabelPlayer1Name = new Label();
        Label m_LabelPlayers = new Label();

        CheckBox M_Player2CheckBox = new CheckBox();

        Button m_ButtonDone = new Button();

        GameBoardUI.e_BoardSize m_BoardSizeSelection;

        public GameSettings()
        {
            this.Text = ConstantsUI.k_GameSettingsHeadLine;
            this.Size = new Size(200, 200);
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitControls();
        }

        private void InitControls()
        {
            m_LabelBoardSize.Text = "Board Size:";
            m_LabelBoardSize.Location = new Point(12, 9);
            m_LabelBoardSize.Font = new Font(FontFamily.GenericSansSerif,
            11.0F, FontStyle.Bold);

            m_LabelPlayers.Text = "Players";
            m_LabelPlayers.Location = new Point(10, 67);
            m_LabelPlayers.Size = new Size(46, 17);
            m_LabelPlayers.Font = new Font(FontFamily.GenericSansSerif,
           9.0F, FontStyle.Bold);


            m_LabelPlayer1Name.Text = "Player 1: ";
            m_LabelPlayer1Name.Location = new Point(12, 90);
            m_LabelPlayer1Name.Size = new Size(50, 17);

            M_Player2CheckBox.Text = "Player 2: ";
            M_Player2CheckBox.Location = new Point(12, 120);
            M_Player2CheckBox.Size = new Size(70, 17);
            M_Player2CheckBox.CheckedChanged += new EventHandler(m_Player2CheckBox_CheckedChanged);

            m_RadioButtonSmallBoard.Text = "6x6";
            m_RadioButtonSmallBoard.Location = new Point(16,27);
            m_RadioButtonSmallBoard.Size = new Size(50, 30);
            m_RadioButtonSmallBoard.Font = new Font(FontFamily.GenericSansSerif,
            8.5F,
            FontStyle.Bold);
           
            m_RadioButtonMediumBoard.Text = "8x8";
            m_RadioButtonMediumBoard.Location = new Point(m_RadioButtonSmallBoard.Right, 27);
            m_RadioButtonMediumBoard.Size = new Size(50, 30);
            m_RadioButtonMediumBoard.Font = new Font(FontFamily.GenericSansSerif,
            8.5F,
            FontStyle.Bold);

            m_RadioButtonLargeBoard.Text = "10x10";
            m_RadioButtonLargeBoard.Location = new Point(m_RadioButtonMediumBoard.Right + 1, 27);
            m_RadioButtonLargeBoard.Size = new Size(60, 30);
            m_RadioButtonLargeBoard.Font = new Font(FontFamily.GenericSansSerif,
            8.5F,
            FontStyle.Bold);

           
            m_TextboxPlayer2name.Location = new Point(M_Player2CheckBox.Right + 3,
                M_Player2CheckBox.Bottom - 20);
            m_TextboxPlayer2name.Text = "[Computer]";
            m_TextboxPlayer2name.Enabled = false;
           
            //this.Width = m_TextboxPlayer2name.Right + 7;

            m_TextboxPlayer1name.Location = new Point(M_Player2CheckBox.Right + 4,
                m_LabelPlayer1Name.Bottom - 20);

            m_ButtonDone.Text = "DONE";
            m_ButtonDone.Location = new Point(this.ClientSize.Width - m_ButtonDone.Width - 8,
                           this.ClientSize.Height - m_ButtonDone.Height - 8);
            m_ButtonDone.Size = new Size(50, 20);
    
            this.Controls.AddRange(new Control[] {m_LabelBoardSize, m_LabelPlayers , m_RadioButtonSmallBoard, m_RadioButtonMediumBoard,
                m_RadioButtonLargeBoard, m_LabelPlayer1Name, M_Player2CheckBox, m_TextboxPlayer1name, m_TextboxPlayer2name,m_ButtonDone});
            this.m_ButtonDone.Click += new EventHandler(m_ButtonDone_Click);
        }


        //private void radioButton_CheckedChanged(object sender, EventArgs e)
        //{
        //    // Executed when any radio button is changed.
        //    // ... It is wired up to every single radio button.
        //    // Search for first radio button in GroupBox.
        //    foreach (Control control in this.Controls)
        //    {
        //        if (control is RadioButton)
        //        {
        //            RadioButton radio = control as RadioButton;
        //            if (radio.Checked)
        //            {
        //                radio.BackColor = Color.Gold;
        //            }
        //        }
        //    }

        //}


            private void m_Player2CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (M_Player2CheckBox.Checked)
            {
                m_TextboxPlayer2name.Enabled = true;
            }
            else
            {
                m_TextboxPlayer2name.Text = "[Computer]";
                m_TextboxPlayer2name.Enabled = false;

            }
        }

        void m_ButtonDone_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            if(m_RadioButtonSmallBoard.Checked)
            {
                m_BoardSizeSelection = GameBoardUI.e_BoardSize.Small;
            }
            else if(m_RadioButtonMediumBoard.Checked)
            {
                m_BoardSizeSelection = GameBoardUI.e_BoardSize.Medium;
            }
            else if(m_RadioButtonLargeBoard.Checked)
            {
                m_BoardSizeSelection = GameBoardUI.e_BoardSize.Large;
            }

            this.Close();
        }

        public string Player1Name
        {
            get { return m_TextboxPlayer1name.Text; }
            set { m_TextboxPlayer1name.Text = value; }
        }

        public string Player2Name
        {
            get { return m_TextboxPlayer2name.Text; }
            set { m_TextboxPlayer2name.Text = value; }
        }

        public GameBoardUI.e_BoardSize BoardSizeSelection
        {
            get { return m_BoardSizeSelection; }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // GameSettings
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "GameSettings";
            this.Load += new System.EventHandler(this.GameSettings_Load);
            this.ResumeLayout(false);

        }

        private void GameSettings_Load(object sender, EventArgs e)
        {

        }
    }
}
