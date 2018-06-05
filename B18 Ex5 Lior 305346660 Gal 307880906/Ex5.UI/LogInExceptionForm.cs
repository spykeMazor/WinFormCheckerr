using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Ex5.UI
{
    public partial class LogInExceptionForm : Form
    {
        private string m_ExceptionType;
        private string m_ExceptionTitle;

        public LogInExceptionForm(string i_ExceptionType, string i_ExceptionTitle)
        {
            WhatIsExceptionType = i_ExceptionType;
            WhatIsExceptionTitle = i_ExceptionTitle;
            InitializeComponent();
        }

        public string WhatIsExceptionType
        {
            get { return m_ExceptionType; }
            set { m_ExceptionType = value; }
        }

        public string WhatIsExceptionTitle
        {
            get { return m_ExceptionTitle; }
            set { m_ExceptionTitle = value; }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
        
        private void InitializeComponent()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(LogInExceptionForm));
            this.buttonOK = new Button();
            this.errorMessage = new TextBox();
            this.pictureBoxIcon = new PictureBox();
            ((ISupportInitialize)this.pictureBoxIcon).BeginInit();
            this.SuspendLayout();
            //// 
            //// buttonOK
            //// 
            this.buttonOK.AutoSize = true;
            this.buttonOK.BackColor = SystemColors.MenuBar;
            this.buttonOK.BackgroundImageLayout = ImageLayout.Center;
            this.buttonOK.DialogResult = DialogResult.Cancel;
            this.buttonOK.FlatAppearance.BorderColor = Color.Black;
            this.buttonOK.FlatAppearance.BorderSize = 5;
            this.buttonOK.FlatAppearance.MouseDownBackColor = Color.Transparent;
            this.buttonOK.FlatAppearance.MouseOverBackColor = Color.Transparent;
            this.buttonOK.FlatStyle = FlatStyle.Popup;
            this.buttonOK.Location = new Point(84, 66);
            this.buttonOK.Margin = new Padding(2);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new Size(75, 23);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = false;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            //// 
            //// errorMessage
            //// 
            this.errorMessage.BackColor = SystemColors.ActiveBorder;
            this.errorMessage.BorderStyle = BorderStyle.None;
            this.errorMessage.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, (byte)177);
            this.errorMessage.Location = new Point(66, 9);
            this.errorMessage.Margin = new Padding(2);
            this.errorMessage.Multiline = true;
            this.errorMessage.Name = "errorMessage";
            this.errorMessage.ReadOnly = true;
            this.errorMessage.Size = new Size(175, 48);
            this.errorMessage.TabIndex = 1;
            this.errorMessage.Text = WhatIsExceptionType;
            this.errorMessage.TextAlign = HorizontalAlignment.Center;
            //// 
            //// pictureBoxIcon
            //// 
            this.pictureBoxIcon.Image = Properties.Resources.Warning_Png;
            this.pictureBoxIcon.InitialImage = Properties.Resources.Warning_Png;
            this.pictureBoxIcon.Location = new Point(12, 12);
            this.pictureBoxIcon.Name = "pictureBoxIcon";
            this.pictureBoxIcon.Size = new Size(45, 45);
            this.pictureBoxIcon.SizeMode = PictureBoxSizeMode.StretchImage;
            this.pictureBoxIcon.TabIndex = 2;
            this.pictureBoxIcon.TabStop = false;
            //// 
            //// LogInExceptionForm
            //// 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = SystemColors.ActiveBorder;
            this.ClientSize = new Size(250, 110);
            this.Controls.Add(this.pictureBoxIcon);
            this.Controls.Add(this.errorMessage);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.Icon = Properties.Resources.Warning_Icon;
            this.Margin = new Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.Name = "LogInExceptionForm";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = WhatIsExceptionTitle;
            ((ISupportInitialize)this.pictureBoxIcon).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private Button buttonOK;
        private PictureBox pictureBoxIcon;
        private TextBox errorMessage;
    }
}
