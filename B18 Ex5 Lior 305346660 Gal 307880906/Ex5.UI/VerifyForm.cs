using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Ex5.UI
{
    public partial class VerifyForm : Form
    {
        private string m_WhatToDo;
        public VerifyForm(string i_WhatToDo)
        {
            WhatToPrint = i_WhatToDo;
            InitializeComponent();
        }

        public string WhatToPrint
        {
            get { return m_WhatToDo; }
            set { m_WhatToDo = value; }
        }
    }
}
