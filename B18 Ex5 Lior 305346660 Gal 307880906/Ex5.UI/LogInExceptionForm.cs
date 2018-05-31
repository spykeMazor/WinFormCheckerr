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

        public LogInExceptionForm(string i_ExceptionType)
        {
            WhatIsExceptionType = i_ExceptionType;
            InitializeComponent();
        }

        public string WhatIsExceptionType
        {
            get { return m_ExceptionType; }
            set { m_ExceptionType = value; }
        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void InitializeComponent()
        {
            this.buttonOK = new System.Windows.Forms.Button();
            this.errorPlayerNameMessage = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.AutoSize = true;
            this.buttonOK.BackColor = System.Drawing.SystemColors.MenuBar;
            this.buttonOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonOK.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.buttonOK.FlatAppearance.BorderSize = 5;
            this.buttonOK.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonOK.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonOK.Location = new System.Drawing.Point(125, 97);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(113, 35);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = false;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // errorPlayerNameMessage
            // 
            this.errorPlayerNameMessage.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.errorPlayerNameMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.errorPlayerNameMessage.Dock = System.Windows.Forms.DockStyle.Top;
            this.errorPlayerNameMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.errorPlayerNameMessage.Location = new System.Drawing.Point(0, 0);
            this.errorPlayerNameMessage.Multiline = true;
            this.errorPlayerNameMessage.Name = "errorPlayerNameMessage";
            this.errorPlayerNameMessage.ReadOnly = true;
            this.errorPlayerNameMessage.Size = new System.Drawing.Size(362, 91);
            this.errorPlayerNameMessage.TabIndex = 1;
            this.errorPlayerNameMessage.Text = WhatIsExceptionType;
            this.errorPlayerNameMessage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // LogInExceptionForm
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(362, 154);
            this.Controls.Add(this.errorPlayerNameMessage);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LogInExceptionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "LogIn Exception";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.TextBox errorPlayerNameMessage;
    }
}
