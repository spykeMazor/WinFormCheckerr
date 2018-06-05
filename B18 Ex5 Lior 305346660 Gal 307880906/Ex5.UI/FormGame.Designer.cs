using System.Windows.Forms;
using System.Drawing;

namespace Ex5.UI
{
    public partial class FormGame
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGame));
            this.headLine = new Label();
            this.labelScore1 = new Label();
            this.labelScore2 = new Label();
            this.labelTotalScore1 = new Label();
            this.labelTotalScore2 = new Label();
            this.splitContainerNameAndCurrentScore = new SplitContainer();
            this.splitContainerTotalScore = new SplitContainer();
            this.labelPlayer1Name = new Label();
            this.labelPlayer2Name = new Label();
            this.buttonExit = new Button();
            this.buttonStartOver = new Button();
            this.labelBackground = new Label();
            this.labelTotalScoreTitle = new Label();
            this.comboBoxBackground = new System.Windows.Forms.ComboBox();
            this.splitContainerNameAndCurrentScore.Panel1.SuspendLayout();
            this.splitContainerNameAndCurrentScore.Panel2.SuspendLayout();
            this.splitContainerNameAndCurrentScore.SuspendLayout();
            this.splitContainerTotalScore.Panel1.SuspendLayout();
            this.splitContainerTotalScore.Panel2.SuspendLayout();
            this.splitContainerTotalScore.SuspendLayout();
            this.SuspendLayout();
            //// 
            //// headLine
            //// 
            this.headLine.Anchor = AnchorStyles.Top;
            this.headLine.AutoSize = true;
            this.headLine.BackColor = Color.Transparent;
            this.headLine.Font = new Font("MV Boli", 19F, (FontStyle)(FontStyle.Bold | FontStyle.Italic | FontStyle.Underline), GraphicsUnit.Point, (byte)177, true);
            this.headLine.ForeColor = Color.Yellow;
            this.headLine.Location = new Point(320, 9);
            this.headLine.Name = "headLine";
            this.headLine.Size = new Size(469, 43);
            this.headLine.TabIndex = 1;
            this.headLine.Text = "Gal & Lior Checkers Game";
            this.headLine.TextAlign = ContentAlignment.TopCenter;
            //// 
            //// labelScore1
            //// 
            this.labelScore1.AutoSize = true;
            this.labelScore1.BackColor = Color.Transparent;
            this.labelScore1.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Bold, GraphicsUnit.Point, (byte)177);
            this.labelScore1.ForeColor = SystemColors.HighlightText;
            this.labelScore1.ImageAlign = ContentAlignment.TopCenter;
            this.labelScore1.Location = new Point(80, 110);
            this.labelScore1.Name = "labelScore1";
            this.labelScore1.Size = new Size(36, 37);
            this.labelScore1.TabIndex = 3;
            this.labelScore1.Text = "0";
            this.labelScore1.TextAlign = ContentAlignment.TopCenter;
            //// 
            //// labelScore2
            //// 
            this.labelScore2.AutoSize = true;
            this.labelScore2.BackColor = Color.Transparent;
            this.labelScore2.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Bold, GraphicsUnit.Point, (byte)177);
            this.labelScore2.ForeColor = SystemColors.ActiveCaptionText;
            this.labelScore2.ImageAlign = ContentAlignment.TopCenter;
            this.labelScore2.Location = new Point(80, 110);
            this.labelScore2.Name = "labelScore2";
            this.labelScore2.Size = new Size(36, 37);
            this.labelScore2.TabIndex = 4;
            this.labelScore2.Text = "0";
            this.labelScore2.TextAlign = ContentAlignment.TopCenter;
            //// 
            //// splitContainerNameAndCurrentScore
            //// 
            this.splitContainerNameAndCurrentScore.BackColor = Color.Transparent;
            this.splitContainerNameAndCurrentScore.Location = new Point(712, 106);
            this.splitContainerNameAndCurrentScore.Name = "splitContainerNameAndCurrentScore";
            this.splitContainerNameAndCurrentScore.Orientation = Orientation.Horizontal;
            //// 
            //// splitContainerNameAndCurrentScore.Panel1
            //// 
            this.splitContainerNameAndCurrentScore.Panel1.BackgroundImage = Properties.Resources.black_soldier;
            this.splitContainerNameAndCurrentScore.Panel1.BackgroundImageLayout = ImageLayout.Stretch;
            this.splitContainerNameAndCurrentScore.Panel1.Controls.Add(this.labelScore1);
            this.splitContainerNameAndCurrentScore.Panel1.Controls.Add(this.labelPlayer1Name);
            //// 
            //// splitContainerNameAndCurrentScore.Panel2
            //// 
            this.splitContainerNameAndCurrentScore.Panel2.BackgroundImage = Properties.Resources.red_soldier;
            this.splitContainerNameAndCurrentScore.Panel2.BackgroundImageLayout = ImageLayout.Stretch;
            this.splitContainerNameAndCurrentScore.Panel2.Controls.Add(this.labelScore2);
            this.splitContainerNameAndCurrentScore.Panel2.Controls.Add(this.labelPlayer2Name);
            this.splitContainerNameAndCurrentScore.Size = new Size(177, 354);
            this.splitContainerNameAndCurrentScore.SplitterDistance = 177;
            this.splitContainerNameAndCurrentScore.SplitterWidth = 1;
            this.splitContainerNameAndCurrentScore.TabIndex = 3;
            //// 
            //// labelPlayer1Name
            //// 
            this.labelPlayer1Name.Anchor = AnchorStyles.None;
            this.labelPlayer1Name.BackColor = Color.Transparent;
            this.labelPlayer1Name.Text = m_FormNameLogin.Player1Name;
            this.labelPlayer1Name.Font = new Font("Microsoft Sans Serif", 15F, FontStyle.Bold, GraphicsUnit.Point, (byte)177);
            this.labelPlayer1Name.ForeColor = SystemColors.HighlightText;
            this.labelPlayer1Name.Location = new Point(40, 55);
            this.labelPlayer1Name.Name = "labelPlayer1Name";
            this.labelPlayer1Name.Size = new Size(101, 40);
            this.labelPlayer1Name.TabIndex = 2;
            this.labelPlayer1Name.Text = m_FormNameLogin.Player1Name;
            this.labelPlayer1Name.TextAlign = ContentAlignment.MiddleCenter;
            //// 
            //// labelPlayer2Name
            //// 
            this.labelPlayer2Name.Anchor = AnchorStyles.None;
            this.labelPlayer2Name.BackColor = Color.Transparent;
            this.labelPlayer2Name.Font = new Font("Microsoft Sans Serif", 15F, FontStyle.Bold, GraphicsUnit.Point, (byte)177);
            this.labelPlayer2Name.ForeColor = SystemColors.ActiveCaptionText;
            this.labelPlayer2Name.Location = new Point(40, 55);
            this.labelPlayer2Name.Name = "labelPlayer2Name";
            this.labelPlayer2Name.Size = new Size(101, 40);
            this.labelPlayer2Name.TabIndex = 3;
            this.labelPlayer2Name.Text = m_FormNameLogin.Player2Name;
            this.labelPlayer2Name.TextAlign = ContentAlignment.MiddleCenter;
            //// 
            //// splitContainerTotalScore
            //// 
            this.splitContainerTotalScore.BackColor = Color.Transparent;
            this.splitContainerTotalScore.Location = new Point(745, 540);
            this.splitContainerTotalScore.Name = "splitContainerTotalScore";
            this.splitContainerTotalScore.Orientation = Orientation.Vertical;
            //// 
            //// splitContainerTotalScore.Panel1
            //// 
            this.splitContainerTotalScore.Panel1.BackgroundImage = Properties.Resources.black_soldier;
            this.splitContainerTotalScore.Panel1.BackgroundImageLayout = ImageLayout.Stretch;
            this.splitContainerTotalScore.Panel1.Controls.Add(this.labelTotalScore1);
            //// 
            //// splitContainerTotalScore.Panel2
            //// 
            this.splitContainerTotalScore.Panel2.BackgroundImage = Properties.Resources.red_soldier;
            this.splitContainerTotalScore.Panel2.BackgroundImageLayout = ImageLayout.Stretch;
            this.splitContainerTotalScore.Panel2.Controls.Add(this.labelTotalScore2);
            this.splitContainerTotalScore.Size = new Size(120, 60);
            this.splitContainerTotalScore.SplitterDistance = 60;
            this.splitContainerTotalScore.SplitterWidth = 1;
            this.splitContainerTotalScore.TabIndex = 3;
            //// 
            //// labelTotalScore1
            //// 
            this.labelTotalScore1.AutoSize = true;
            this.labelTotalScore1.BackColor = Color.Transparent;
            this.labelTotalScore1.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Bold, GraphicsUnit.Point, (byte)177);
            this.labelTotalScore1.ForeColor = SystemColors.HighlightText;
            this.labelTotalScore1.ImageAlign = ContentAlignment.TopCenter;
            this.labelTotalScore1.Location = new Point(15, 15);
            this.labelTotalScore1.Name = "labelTotalScore1";
            this.labelTotalScore1.Size = new Size(17, 17);
            this.labelTotalScore1.TabIndex = 3;
            this.labelTotalScore1.Text = GetNameLogin.Player1TotalScoreCounter.ToString();
            this.labelTotalScore1.TextAlign = ContentAlignment.TopCenter;
            //// 
            //// labelTotalScore2
            //// 
            this.labelTotalScore2.AutoSize = true;
            this.labelTotalScore2.BackColor = Color.Transparent;
            this.labelTotalScore2.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Bold, GraphicsUnit.Point, (byte)177);
            this.labelTotalScore2.ForeColor = SystemColors.ActiveCaptionText;
            this.labelTotalScore2.ImageAlign = ContentAlignment.TopCenter;
            this.labelTotalScore2.Location = new Point(15, 15);
            this.labelTotalScore2.Name = "labelTotalScore2";
            this.labelTotalScore2.Size = new Size(17, 17);
            this.labelTotalScore2.TabIndex = 4;
            this.labelTotalScore2.Text = GetNameLogin.Player2TotalScoreCounter.ToString();
            this.labelTotalScore2.TextAlign = ContentAlignment.TopCenter;
            //// 
            //// labelTotalScoreTitle
            //// 
            this.labelTotalScoreTitle.AutoSize = true;
            this.labelTotalScoreTitle.BackColor = Color.Transparent;
            this.labelTotalScoreTitle.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Bold, GraphicsUnit.Point, (byte)177);
            this.labelTotalScoreTitle.ForeColor = Color.Yellow;
            this.labelTotalScoreTitle.Location = new Point(685, 554);
            this.labelTotalScoreTitle.Name = "labelTotalScore";
            this.labelTotalScoreTitle.Size = new Size(115, 15);
            this.labelTotalScoreTitle.TabIndex = 1;
            this.labelTotalScoreTitle.Text = "Total\nScore:";
            //// 
            //// buttonExit
            //// 
            this.buttonExit.AutoSize = true;
            this.buttonExit.BackColor = SystemColors.HotTrack;
            this.buttonExit.Cursor = Cursors.Help;
            this.buttonExit.FlatStyle = FlatStyle.Popup;
            this.buttonExit.Font = new Font("Microsoft Sans Serif", 8F, FontStyle.Bold, GraphicsUnit.Point, (byte)177);
            this.buttonExit.Location = new Point(832, 608);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new Size(121, 33);
            this.buttonExit.TabIndex = 4;
            this.buttonExit.Text = "EXIT";
            this.buttonExit.TextImageRelation = TextImageRelation.ImageAboveText;
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            //// 
            //// arrowPictureBoxPlayer1
            //// 
            this.arrowPictureBoxPlayer1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)this.arrowPictureBoxPlayer1).BeginInit();
            this.arrowPictureBoxPlayer1.Image = Properties.Resources.arrow_PngYellow;
            this.arrowPictureBoxPlayer1.BackColor = Color.Transparent;
            this.arrowPictureBoxPlayer1.Location = new Point(900, 163);
            this.arrowPictureBoxPlayer1.Name = "arrowPictureBox1";
            this.arrowPictureBoxPlayer1.Size = new Size(64, 64);
            this.arrowPictureBoxPlayer1.SizeMode = PictureBoxSizeMode.StretchImage;
            this.arrowPictureBoxPlayer1.TabIndex = 1;
            this.arrowPictureBoxPlayer1.TabStop = false;
            this.arrowPictureBoxPlayer1.Visible = true;
            this.Controls.Add(this.arrowPictureBoxPlayer1);
            ((System.ComponentModel.ISupportInitialize)this.arrowPictureBoxPlayer1).EndInit();
            //// 
            //// arrowPictureBoxPlayer2
            //// 
            this.arrowPictureBoxPlayer2 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)this.arrowPictureBoxPlayer1).BeginInit();
            this.arrowPictureBoxPlayer2.Image = Properties.Resources.arrow_PngYellow;
            this.arrowPictureBoxPlayer2.BackColor = Color.Transparent;
            this.arrowPictureBoxPlayer2.Location = new Point(900, 340);
            this.arrowPictureBoxPlayer2.Name = "arrowPictureBox2";
            this.arrowPictureBoxPlayer2.Size = new Size(64, 64);
            this.arrowPictureBoxPlayer2.SizeMode = PictureBoxSizeMode.StretchImage;
            this.arrowPictureBoxPlayer2.TabIndex = 1;
            this.arrowPictureBoxPlayer2.TabStop = false;
            this.arrowPictureBoxPlayer2.Visible = false;
            this.Controls.Add(this.arrowPictureBoxPlayer2);
            ((System.ComponentModel.ISupportInitialize)this.arrowPictureBoxPlayer2).EndInit();
            //// 
            //// buttonStartOver
            //// 
            this.buttonStartOver.AutoSize = true;
            this.buttonStartOver.BackColor = SystemColors.HotTrack;
            this.buttonStartOver.Cursor = Cursors.Help;
            this.buttonStartOver.FlatStyle = FlatStyle.Popup;
            this.buttonStartOver.Font = new Font("Microsoft Sans Serif", 8F, FontStyle.Bold, GraphicsUnit.Point, (byte)177);
            this.buttonStartOver.Location = new Point(685, 608);
            this.buttonStartOver.Name = "buttonStartOver";
            this.buttonStartOver.Size = new Size(131, 33);
            this.buttonStartOver.TabIndex = 5;
            this.buttonStartOver.Enabled = false;
            this.buttonStartOver.Text = "START OVER";
            this.buttonStartOver.TextImageRelation = TextImageRelation.ImageAboveText;
            this.buttonStartOver.UseVisualStyleBackColor = false;
            this.buttonStartOver.Click += new System.EventHandler(this.buttonStartOver_Click);
            //// 
            //// comboBoxBackground
            //// 
            this.comboBoxBackground.BackColor = Color.Blue;
            this.comboBoxBackground.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBoxBackground.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBackground.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, (byte)177);
            this.comboBoxBackground.ForeColor = Color.Yellow;
            this.comboBoxBackground.ItemHeight = 13;
            this.comboBoxBackground.TabStop = false;
            this.comboBoxBackground.Items.AddRange(new object[]
            {
            "Blue",
            "Heart",
            "Green",
            "Purple",
            "Damka3D"
            });

            this.comboBoxBackground.Location = new Point(805, 500);
            this.comboBoxBackground.Name = "comboBoxBackground";
            this.comboBoxBackground.Size = new Size(143, 21);
            this.comboBoxBackground.Text = "Blue";
            this.comboBoxBackground.SelectedIndexChanged += new System.EventHandler(this.comboBoxBackground_SelectedIndexChanged);
            //// 
            //// labelBackground
            //// 
            this.labelBackground.AutoSize = true;
            this.labelBackground.BackColor = Color.Transparent;
            this.labelBackground.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold, GraphicsUnit.Point, (byte)177);
            this.labelBackground.ForeColor = Color.Yellow;
            this.labelBackground.Location = new Point(685, 502);
            this.labelBackground.Name = "labelBackground";
            this.labelBackground.Size = new Size(115, 15);
            this.labelBackground.TabIndex = 1;
            this.labelBackground.Text = "BACKGROUNDS:";
            ////  
            //// FormGame
            //// 
            this.BackColor = Color.Black;
            this.BackgroundImage = Properties.Resources.blue_Background;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.ClientSize = new Size(978, 694);
            this.Controls.Add(this.buttonStartOver);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.splitContainerNameAndCurrentScore);
            this.Controls.Add(this.splitContainerTotalScore);
            this.Controls.Add(this.headLine);
            this.Controls.Add(this.comboBoxBackground);
            this.Controls.Add(this.labelBackground);
            this.Controls.Add(this.labelTotalScoreTitle);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.Name = "FormGame";
            this.Text = "Gal & Lior Checker Game";
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            this.Icon = Properties.Resources.damka_Icon;
            this.splitContainerNameAndCurrentScore.Panel1.ResumeLayout(false);
            this.splitContainerNameAndCurrentScore.Panel1.PerformLayout();
            this.splitContainerNameAndCurrentScore.Panel2.ResumeLayout(false);
            this.splitContainerNameAndCurrentScore.Panel2.PerformLayout();
            this.splitContainerNameAndCurrentScore.ResumeLayout(false);
            this.splitContainerTotalScore.Panel1.ResumeLayout(false);
            this.splitContainerTotalScore.Panel1.PerformLayout();
            this.splitContainerTotalScore.Panel2.ResumeLayout(false);
            this.splitContainerTotalScore.Panel2.PerformLayout();
            this.splitContainerTotalScore.ResumeLayout(false);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private Label headLine;
        private Label labelScore1;
        private Label labelScore2;
        private Label labelTotalScore1;
        private Label labelTotalScore2;
        private SplitContainer splitContainerNameAndCurrentScore;
        private SplitContainer splitContainerTotalScore;
        private Label labelPlayer1Name;
        private Label labelPlayer2Name;
        private Button buttonExit;
        private Button buttonStartOver;
        private PictureBox arrowPictureBoxPlayer1;
        private PictureBox arrowPictureBoxPlayer2;
        private LogInExceptionForm wongInputException = new LogInExceptionForm("Worng Move", "Worng Move");
        private ComboBox comboBoxBackground;
        private Image m_MyBackground;
        private Label labelBackground;
        private Label labelTotalScoreTitle;
    }
    #endregion
}