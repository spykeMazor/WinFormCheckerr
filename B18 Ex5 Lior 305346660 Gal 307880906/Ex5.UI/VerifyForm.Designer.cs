using System.Windows.Forms;

namespace Ex5.UI
{
    public partial class VerifyForm
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
            ////string outPutMessage = string.Format(ConstantsUI.k_QuestionToDo, WhatToDo);
            this.labelQuestion = new Label();
            this.buttonYes = new Button();
            this.buttonNo = new Button();
            this.pictureBoxIcon = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).BeginInit();
            this.SuspendLayout();
            //// 
            //// labelQuestion
            //// 
            this.labelQuestion.AutoSize = true;
            this.labelQuestion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (byte)177);
            this.labelQuestion.Location = new System.Drawing.Point(80, 25);
            this.labelQuestion.Name = "labelQuestion";
            this.labelQuestion.Size = new System.Drawing.Size(220, 72);
            this.labelQuestion.TabIndex = 0;
            this.labelQuestion.Text = WhatToPrint;
            this.labelQuestion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //// 
            //// buttonYes
            //// 
            this.buttonYes.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.buttonYes.DialogResult = DialogResult.Yes;
            this.buttonYes.FlatStyle = FlatStyle.Popup;
            this.buttonYes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (byte)177);
            this.buttonYes.Location = new System.Drawing.Point(175, 112);
            this.buttonYes.Name = "buttonYes";
            this.buttonYes.Size = new System.Drawing.Size(99, 34);
            this.buttonYes.TabIndex = 1;
            this.buttonYes.Text = "YES";
            this.buttonYes.UseVisualStyleBackColor = false;
            //// 
            //// buttonNo
            //// 
            this.buttonNo.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.buttonNo.DialogResult = DialogResult.No;
            this.buttonNo.FlatStyle = FlatStyle.Popup;
            this.buttonNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (byte)177);
            this.buttonNo.Location = new System.Drawing.Point(40, 112);
            this.buttonNo.Name = "buttonNo";
            this.buttonNo.Size = new System.Drawing.Size(99, 34);
            this.buttonNo.TabIndex = 2;
            this.buttonNo.Text = "NO";
            this.buttonNo.UseVisualStyleBackColor = false;
            //// 
            //// pictureBoxIcon
            //// 
            this.pictureBoxIcon.Image = Properties.Resources.question_Png;
            this.pictureBoxIcon.InitialImage = Properties.Resources.question_Png;
            this.pictureBoxIcon.Location = new System.Drawing.Point(12, 25);
            this.pictureBoxIcon.Name = "pictureBoxIcon";
            this.pictureBoxIcon.Size = new System.Drawing.Size(62, 62);
            this.pictureBoxIcon.SizeMode = PictureBoxSizeMode.StretchImage;
            this.pictureBoxIcon.TabIndex = 2;
            this.pictureBoxIcon.TabStop = false;
            //// 
            //// VerifyForm
            //// 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(320, 170);
            this.Controls.Add(this.buttonNo);
            this.Controls.Add(this.buttonYes);
            this.Controls.Add(this.labelQuestion);
            this.Controls.Add(this.pictureBoxIcon);
            this.Icon = Properties.Resources.question_Icon;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).EndInit();
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.Name = "VerifyForm";
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = WhatToPrintTitle;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
        private PictureBox pictureBoxIcon;
        private Label labelQuestion;
        private Button buttonYes;
        private Button buttonNo;
    }
}