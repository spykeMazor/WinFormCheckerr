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
        private string m_WhatToDoTitle;

        public VerifyForm(string i_WhatToDo, string i_WhatToDoTitle)
        {
            WhatToPrint = i_WhatToDo;
            WhatToPrintTitle = i_WhatToDoTitle;
            InitializeComponent();
        }

        public string WhatToPrint
        {
            get { return m_WhatToDo; }
            set { m_WhatToDo = value; }
        }

        public string WhatToPrintTitle
        {
            get { return m_WhatToDoTitle; }
            set { m_WhatToDoTitle = value; }
        }
    }
}
