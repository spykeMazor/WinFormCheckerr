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
        public LogInExceptionForm()
        {
            this.textBoxMessage = new System.Windows.Forms.TextBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxMessage
            // 
            this.textBoxMessage.BackColor = System.Drawing.Color.DarkGray;
            this.textBoxMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.textBoxMessage.ForeColor = System.Drawing.SystemColors.MenuText;
            this.textBoxMessage.HideSelection = false;
            this.textBoxMessage.Location = new System.Drawing.Point(47, 49);
            this.textBoxMessage.Multiline = true;
            this.textBoxMessage.Name = "textBoxMessage";
            this.textBoxMessage.ReadOnly = true;
            this.textBoxMessage.Size = new System.Drawing.Size(340, 82);
            this.textBoxMessage.TabIndex = 0;
            this.textBoxMessage.Text = "Player\'s name must include\r\nat least 1 character.";
            this.textBoxMessage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxMessage.UseWaitCursor = true;
            // 
            // buttonOk
            // 
            this.buttonOk.AutoSize = true;
            this.buttonOk.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.buttonOk.Location = new System.Drawing.Point(156, 160);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(106, 38);
            this.buttonOk.TabIndex = 1;
            this.buttonOk.Text = "OK";
            this.buttonOk.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.buttonOk.UseVisualStyleBackColor = false;
            this.buttonOk.Click += new System.EventHandler(this.LogInExceptionForm_Load);
            // 
            // LogInExceptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(439, 250);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.textBoxMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "LogInExceptionForm";
            this.ShowInTaskbar = false;
            this.Text = "LogIn Exception";
            this.UseWaitCursor = true;
            this.Load += new System.EventHandler(this.LogInExceptionForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    

        private void LogInExceptionForm_Load(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
