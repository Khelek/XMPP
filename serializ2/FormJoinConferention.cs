using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace serializ2 {
    public partial class FormJoinConferention : Form {
        public FormJoinConferention() {
            InitializeComponent();
        }

        private void buttonCreate_Click(object sender, EventArgs e) {
            FormConferention conf = new FormConferention(textBoxConfName.Text, new List<string>() {"admin@haupc"});
            conf.Show();
            this.Close();
        }

    }
}
