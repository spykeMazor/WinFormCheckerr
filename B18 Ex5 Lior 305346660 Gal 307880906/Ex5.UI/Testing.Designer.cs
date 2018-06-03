using System.Windows.Forms;
using System.Drawing;

namespace Ex5.UI
{
    partial class Testing
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
            this.comboBoxBackground = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            //// 
            //// comboBoxBackground
            //// 
            this.comboBoxBackground.BackColor = Color.Blue;
            this.comboBoxBackground.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBoxBackground.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, (byte)177);
            this.comboBoxBackground.ForeColor = Color.Yellow;
            this.comboBoxBackground.FormattingEnabled = true;
            this.comboBoxBackground.ItemHeight = 13;
            this.comboBoxBackground.Items.AddRange(new object[] {
            "Blue ",
            "Heart",
            "Green",
            "Purple",
            "Damka3D"});
            this.comboBoxBackground.Location = new Point(457, 131);
            this.comboBoxBackground.Name = "comboBoxBackground";
            this.comboBoxBackground.Size = new Size(143, 21);
            this.comboBoxBackground.TabIndex = 0;
            this.comboBoxBackground.Text = "BACKGROUNDS";
            this.comboBoxBackground.SelectedIndexChanged += new System.EventHandler(this.comboBoxBackground_SelectedIndexChanged);
            //// 
            //// Testing
            //// 
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new Size(645, 627);
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