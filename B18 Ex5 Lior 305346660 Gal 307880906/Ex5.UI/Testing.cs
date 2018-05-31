using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Ex5.UI
{
    public partial class Testing : Form
    {
        public Testing()
        {
            InitializeComponent();
            Button b = new Button();
            this.Controls.Add(b);
            b.Click += new EventHandler(button_Click);

        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void button_Click(object sender, EventArgs e)
        {
            Button senderButton = sender as Button;
            senderButton.Location = new Point(senderButton.Location.X + 20, senderButton.Location.Y + 20);


        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
