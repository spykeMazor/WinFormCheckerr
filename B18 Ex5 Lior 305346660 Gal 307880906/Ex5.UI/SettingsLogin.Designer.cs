﻿using System.Drawing;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsLogin));
            this.textBoxPlayer1Name = new System.Windows.Forms.TextBox();
            this.boardGameSmallSize = new System.Windows.Forms.RadioButton();
            this.buttonDone = new System.Windows.Forms.Button();
            this.checkBoxPlayer2 = new System.Windows.Forms.CheckBox();
            this.boardGameMediumSize = new System.Windows.Forms.RadioButton();
            this.boardGameLargeSize = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBoxPlayer2Name = new System.Windows.Forms.TextBox();
            this.labelBoardSize = new System.Windows.Forms.Label();
            this.labelPlayers = new System.Windows.Forms.Label();
            this.labelPlayer1Name = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
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
            this.boardGameSmallSize.ForeColor = System.Drawing.SystemColors.Info;
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
            this.buttonDone.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonDone.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonDone.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.buttonDone.Location = new System.Drawing.Point(226, 385);
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
            this.checkBoxPlayer2.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxPlayer2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.checkBoxPlayer2.ForeColor = System.Drawing.SystemColors.Info;
            this.checkBoxPlayer2.Location = new System.Drawing.Point(28, 321);
            this.checkBoxPlayer2.Name = "checkBoxPlayer2";
            this.checkBoxPlayer2.Size = new System.Drawing.Size(141, 33);
            this.checkBoxPlayer2.TabIndex = 4;
            this.checkBoxPlayer2.Text = "Player 2:";
            this.checkBoxPlayer2.UseVisualStyleBackColor = false;
            this.checkBoxPlayer2.CheckedChanged += new System.EventHandler(this.checkBoxPlayer2_CheckedChanged);
            // 
            // boardGameMediumSize
            // 
            this.boardGameMediumSize.AutoSize = true;
            this.boardGameMediumSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.boardGameMediumSize.ForeColor = System.Drawing.SystemColors.Info;
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
            this.boardGameLargeSize.ForeColor = System.Drawing.SystemColors.Info;
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
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.boardGameLargeSize);
            this.panel1.Controls.Add(this.boardGameMediumSize);
            this.panel1.Controls.Add(this.boardGameSmallSize);
            this.panel1.Location = new System.Drawing.Point(241, 44);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(185, 162);
            this.panel1.TabIndex = 12;
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
            this.textBoxPlayer2Name.Text = "[Computer]";
            // 
            // labelBoardSize
            // 
            this.labelBoardSize.AutoSize = true;
            this.labelBoardSize.BackColor = System.Drawing.Color.Transparent;
            this.labelBoardSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelBoardSize.ForeColor = System.Drawing.SystemColors.Info;
            this.labelBoardSize.Location = new System.Drawing.Point(45, 44);
            this.labelBoardSize.Name = "labelBoardSize";
            this.labelBoardSize.Size = new System.Drawing.Size(180, 36);
            this.labelBoardSize.TabIndex = 14;
            this.labelBoardSize.Text = "Board Size:";
            // 
            // labelPlayers
            // 
            this.labelPlayers.AutoSize = true;
            this.labelPlayers.BackColor = System.Drawing.Color.Transparent;
            this.labelPlayers.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelPlayers.ForeColor = System.Drawing.SystemColors.Info;
            this.labelPlayers.Location = new System.Drawing.Point(45, 210);
            this.labelPlayers.Name = "labelPlayers";
            this.labelPlayers.Size = new System.Drawing.Size(130, 36);
            this.labelPlayers.TabIndex = 15;
            this.labelPlayers.Text = "Players:";
            // 
            // labelPlayer1Name
            // 
            this.labelPlayer1Name.AutoSize = true;
            this.labelPlayer1Name.BackColor = System.Drawing.Color.Transparent;
            this.labelPlayer1Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelPlayer1Name.ForeColor = System.Drawing.SystemColors.Info;
            this.labelPlayer1Name.Location = new System.Drawing.Point(54, 264);
            this.labelPlayer1Name.Name = "labelPlayer1Name";
            this.labelPlayer1Name.Size = new System.Drawing.Size(115, 29);
            this.labelPlayer1Name.TabIndex = 16;
            this.labelPlayer1Name.Text = "Player 1:";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.buttonCancel.Location = new System.Drawing.Point(374, 385);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(125, 35);
            this.buttonCancel.TabIndex = 17;
            this.buttonCancel.Text = "CANCEL";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // SettingsLogin
            // 
            this.AcceptButton = this.buttonDone;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.BackgroundImage = Image.FromFile(@"C:\black_marble.JPG");
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(511, 445);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.labelPlayer1Name);
            this.Controls.Add(this.labelPlayers);
            this.Controls.Add(this.labelBoardSize);
            this.Controls.Add(this.textBoxPlayer2Name);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.checkBoxPlayer2);
            this.Controls.Add(this.buttonDone);
            this.Controls.Add(this.textBoxPlayer1Name);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            this.Name = "SettingsLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game Settings";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBoxPlayer1Name;
        private System.Windows.Forms.RadioButton boardGameSmallSize;
        private System.Windows.Forms.Button buttonDone;
        private System.Windows.Forms.CheckBox checkBoxPlayer2;
        private System.Windows.Forms.RadioButton boardGameMediumSize;
        private System.Windows.Forms.RadioButton boardGameLargeSize;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBoxPlayer2Name;
        private System.Windows.Forms.Label labelBoardSize;
        private System.Windows.Forms.Label labelPlayers;
        private System.Windows.Forms.Label labelPlayer1Name;
        private System.Windows.Forms.Button buttonCancel;
    }
}