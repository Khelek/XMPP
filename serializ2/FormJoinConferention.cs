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

namespace serializ2 {
    public partial class FormJoinConferention : Form {
        XmppClientConnection mainXmpp;
        Jid mainJid;

        public FormJoinConferention( XmppClientConnection xmpp, Jid jid) {
            InitializeComponent();
            mainXmpp = xmpp;
            mainJid = jid;
        }

        private void buttonCreate_Click(object sender, EventArgs e) {
            FormConferention conf = new FormConferention(mainXmpp, mainJid, "khelek", textBoxConfName.Text, new List<string>() {"admin@haupc"});
            conf.Show();
            this.Close();
        }

    }
}
