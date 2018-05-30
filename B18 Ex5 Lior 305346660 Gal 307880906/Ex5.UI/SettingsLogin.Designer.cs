namespace Ex5.UI
{
    partial class SettingsLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxBoardSize = new System.Windows.Forms.TextBox();
            this.Player1NameBox = new System.Windows.Forms.TextBox();
            this.textBoxPlayer1Name = new System.Windows.Forms.TextBox();
            this.boardGameSmallSize = new System.Windows.Forms.RadioButton();
            this.buttonDone = new System.Windows.Forms.Button();
            this.checkBoxPlayer2 = new System.Windows.Forms.CheckBox();
            this.boardGameMediumSize = new System.Windows.Forms.RadioButton();
            this.boardGameLargeSize = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBoxPlayers = new System.Windows.Forms.TextBox();
            this.textBoxPlayer2Name = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxBoardSize
            // 
            this.textBoxBoardSize.BackColor = System.Drawing.Color.SteelBlue;
            this.textBoxBoardSize.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxBoardSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.textBoxBoardSize.Location = new System.Drawing.Point(51, 46);
            this.textBoxBoardSize.Name = "textBoxBoardSize";
            this.textBoxBoardSize.Size = new System.Drawing.Size(197, 34);
            this.textBoxBoardSize.TabIndex = 0;
            this.textBoxBoardSize.TabStop = false;
            this.textBoxBoardSize.Text = "Board Size:";
            // 
            // Player1NameBox
            // 
            this.Player1NameBox.BackColor = System.Drawing.Color.SteelBlue;
            this.Player1NameBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Player1NameBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.Player1NameBox.Location = new System.Drawing.Point(51, 265);
            this.Player1NameBox.Name = "Player1NameBox";
            this.Player1NameBox.Size = new System.Drawing.Size(100, 28);
            this.Player1NameBox.TabIndex = 2;
            this.Player1NameBox.TabStop = false;
            this.Player1NameBox.Text = "Player 1:";
            // 
            // textBoxPlayer1Name
            // 
            this.textBoxPlayer1Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.textBoxPlayer1Name.Location = new System.Drawing.Point(197, 258);
            this.textBoxPlayer1Name.Name = "textBoxPlayer1Name";
            this.textBoxPlayer1Name.Size = new System.Drawing.Size(229, 35);
            this.textBoxPlayer1Name.TabIndex = 0;
            // 
            // boardGameSmallSize
            // 
            this.boardGameSmallSize.AutoSize = true;
            this.boardGameSmallSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.boardGameSmallSize.Location = new System.Drawing.Point(26, 12);
            this.boardGameSmallSize.Name = "boardGameSmallSize";
            this.boardGameSmallSize.Size = new System.Drawing.Size(84, 33);
            this.boardGameSmallSize.TabIndex = 0;
            this.boardGameSmallSize.TabStop = true;
            this.boardGameSmallSize.Text = "6X6";
            this.boardGameSmallSize.UseVisualStyleBackColor = true;
            // 
            // buttonDone
            // 
            this.buttonDone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDone.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonDone.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonDone.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.buttonDone.Location = new System.Drawing.Point(339, 385);
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.Size = new System.Drawing.Size(125, 35);
            this.buttonDone.TabIndex = 6;
            this.buttonDone.Text = "DONE";
            this.buttonDone.UseVisualStyleBackColor = false;
            this.buttonDone.Click += new System.EventHandler(this.buttonDone_Click);
            // 
            // checkBoxPlayer2
            // 
            this.checkBoxPlayer2.AutoSize = true;
            this.checkBoxPlayer2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.checkBoxPlayer2.Location = new System.Drawing.Point(28, 321);
            this.checkBoxPlayer2.Name = "checkBoxPlayer2";
            this.checkBoxPlayer2.Size = new System.Drawing.Size(132, 33);
            this.checkBoxPlayer2.TabIndex = 4;
            this.checkBoxPlayer2.Text = "Player 2:";
            this.checkBoxPlayer2.UseVisualStyleBackColor = true;
            this.checkBoxPlayer2.CheckedChanged += new System.EventHandler(this.checkBoxPlayer2_CheckedChanged);
            // 
            // boardGameMediumSize
            // 
            this.boardGameMediumSize.AutoSize = true;
            this.boardGameMediumSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.boardGameMediumSize.Location = new System.Drawing.Point(26, 62);
            this.boardGameMediumSize.Name = "boardGameMediumSize";
            this.boardGameMediumSize.Size = new System.Drawing.Size(84, 33);
            this.boardGameMediumSize.TabIndex = 1;
            this.boardGameMediumSize.TabStop = true;
            this.boardGameMediumSize.Text = "8X8";
            this.boardGameMediumSize.UseVisualStyleBackColor = true;
            // 
            // boardGameLargeSize
            // 
            this.boardGameLargeSize.AutoSize = true;
            this.boardGameLargeSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.boardGameLargeSize.Location = new System.Drawing.Point(26, 112);
            this.boardGameLargeSize.Name = "boardGameLargeSize";
            this.boardGameLargeSize.Size = new System.Drawing.Size(112, 33);
            this.boardGameLargeSize.TabIndex = 2;
            this.boardGameLargeSize.TabStop = true;
            this.boardGameLargeSize.Text = "10X10";
            this.boardGameLargeSize.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.boardGameLargeSize);
            this.panel1.Controls.Add(this.boardGameMediumSize);
            this.panel1.Controls.Add(this.boardGameSmallSize);
            this.panel1.Location = new System.Drawing.Point(241, 44);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(185, 162);
            this.panel1.TabIndex = 12;
            // 
            // textBoxPlayers
            // 
            this.textBoxPlayers.BackColor = System.Drawing.Color.SteelBlue;
            this.textBoxPlayers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxPlayers.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.textBoxPlayers.Location = new System.Drawing.Point(51, 212);
            this.textBoxPlayers.Name = "textBoxPlayers";
            this.textBoxPlayers.Size = new System.Drawing.Size(197, 34);
            this.textBoxPlayers.TabIndex = 13;
            this.textBoxPlayers.TabStop = false;
            this.textBoxPlayers.Text = "Players:";
            // 
            // textBoxPlayer2Name
            // 
            this.textBoxPlayer2Name.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.textBoxPlayer2Name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPlayer2Name.Enabled = false;
            this.textBoxPlayer2Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.textBoxPlayer2Name.Location = new System.Drawing.Point(197, 321);
            this.textBoxPlayer2Name.Name = "textBoxPlayer2Name";
            this.textBoxPlayer2Name.Size = new System.Drawing.Size(229, 35);
            this.textBoxPlayer2Name.TabIndex = 5;
            this.textBoxPlayer2Name.Text = "[Compter]";
            // 
            // SettingsLogin
            // 
            this.AcceptButton = this.buttonDone;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(511, 445);
            this.Controls.Add(this.textBoxPlayer2Name);
            this.Controls.Add(this.textBoxPlayers);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.checkBoxPlayer2);
            this.Controls.Add(this.buttonDone);
            this.Controls.Add(this.textBoxPlayer1Name);
            this.Controls.Add(this.Player1NameBox);
            this.Controls.Add(this.textBoxBoardSize);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game Settings";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxBoardSize;
        private System.Windows.Forms.TextBox Player1NameBox;
        private System.Windows.Forms.TextBox textBoxPlayer1Name;
        private System.Windows.Forms.RadioButton boardGameSmallSize;
        private System.Windows.Forms.Button buttonDone;
        private System.Windows.Forms.CheckBox checkBoxPlayer2;
        private System.Windows.Forms.RadioButton boardGameMediumSize;
        private System.Windows.Forms.RadioButton boardGameLargeSize;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBoxPlayers;
        private System.Windows.Forms.TextBox textBoxPlayer2Name;
    }
}