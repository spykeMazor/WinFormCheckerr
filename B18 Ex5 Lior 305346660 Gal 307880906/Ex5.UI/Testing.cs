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
        }

        private void comboBoxBackground_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Text.CompareTo("Blue") == 0)
            {
                this.BackgroundImage = Properties.Resources.blue_Background;
            }
            else if (this.Text.CompareTo("Purple") == 0)
            {
                this.BackgroundImage = Properties.Resources.purple_Background;
            }
            else if(this.Text.CompareTo("Heart") == 0)
            {
                this.BackgroundImage = Properties.Resources.heart_Background;
            }
            else if (this.Text.CompareTo("Green") == 0)
            {
                this.BackgroundImage = Properties.Resources.green_Background;
            }
            else
            {
                this.BackgroundImage = Properties.Resources.damka3d;
            }
        }
    }
}
