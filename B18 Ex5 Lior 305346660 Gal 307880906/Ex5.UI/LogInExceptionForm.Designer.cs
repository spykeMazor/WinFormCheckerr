namespace Ex5.UI
{
    partial class LogInExceptionForm
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
            this.components = new System.ComponentModel.Container();
            this.textBoxMessage = new System.Windows.Forms.TextBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxMessage
            // 
            this.textBoxMessage.AllowDrop = true;
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
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            this.errorProvider1.Tag = "huio\\";
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
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxMessage;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}