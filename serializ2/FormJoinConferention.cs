using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using agsXMPP;
using agsXMPP.protocol.client;

namespace EnterpriseMICApplicationDemo {
    public partial class FormJoinConferention : Form {

        public FormJoinConferention() {
            InitializeComponent();
        }		

        private void buttonCreate_Click(object sender, EventArgs e) {
            ( new FormConferention(textBoxConfName.Text.Trim() + "@conference." + Settings.server) ).Show();
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e) {
            this.Close();
        }

    }
}
