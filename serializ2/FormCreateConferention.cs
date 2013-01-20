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

namespace serializ2
{
    public partial class FormCreateConferention : Form
    {
        List<string> selectedUsers = new List<string>();
        XmppClientConnection mainXmpp;
        Jid mainJid;

        public FormCreateConferention(XmppClientConnection xmpp, Jid jid, List<string> users)
        {
            InitializeComponent();
            for (int i = 0; i < users.Count; i++)
            {
                CheckBox c = new CheckBox();
                ListViewItem item = new ListViewItem(users[i], 0);
                item.Checked = true;//"true" for debug
                listViewUsers.Items.Add(item);
            }
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listViewUsers.Items.Count; i++ )
            {
                if (listViewUsers.Items[i].Checked)
                {
                    selectedUsers.Add(listViewUsers.Items[i].Text);
                }
            }
			FormConferention fConf = new FormConferention(mainXmpp, mainJid, "khelek", "conf@conference.haupc", selectedUsers);
			fConf.Show();
			this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        
		
    }
}
