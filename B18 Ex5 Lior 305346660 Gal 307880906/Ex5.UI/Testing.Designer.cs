using System.Windows.Forms;

namespace Ex5.UI
{
    partial class Testing
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
            this.comboBoxBackground = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // comboBoxBackground
            // 
            this.comboBoxBackground.BackColor = System.Drawing.Color.Blue;
            this.comboBoxBackground.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBoxBackground.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.comboBoxBackground.ForeColor = System.Drawing.Color.Yellow;
            this.comboBoxBackground.FormattingEnabled = true;
            this.comboBoxBackground.ItemHeight = 13;
            this.comboBoxBackground.Items.AddRange(new object[] {
            "Blue ",
            "Heart",
            "Green",
            "Purple",
            "Damka3D"});
            this.comboBoxBackground.Location = new System.Drawing.Point(457, 131);
            this.comboBoxBackground.Name = "comboBoxBackground";
            this.comboBoxBackground.Size = new System.Drawing.Size(143, 21);
            this.comboBoxBackground.TabIndex = 0;
            this.comboBoxBackground.Text = "BACKGROUNDS";
            this.comboBoxBackground.SelectedIndexChanged += new System.EventHandler(this.comboBoxBackground_SelectedIndexChanged);
            // 
            // Testing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 627);
            this.Controls.Add(this.comboBoxBackground);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Testing";
            this.Text = "Testing";
            this.ResumeLayout(false);

        }

        #endregion

        private ComboBox comboBoxBackground;
    }
}