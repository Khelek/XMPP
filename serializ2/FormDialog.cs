using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace serializ2
{
    public partial class FormDialog : Form
    {
        public TabControl conversation;
        public FormDialog()
        {
            InitializeComponent();
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}
