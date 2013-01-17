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
    public partial class FormCreateConferention : Form
    {
        List<string> _selectedUsers = new List<string>();
        public List<string> selectedUsers
        {
            get { return _selectedUsers; }
        }
        public FormCreateConferention(ListBox.ObjectCollection users)
        {
            InitializeComponent();
            for (int i = 0; i < users.Count; i++)
            {
                CheckBox c = new CheckBox();
                c.Name = users[i].ToString();
                ListViewItem item = new ListViewItem(c.Name,0);
                item.Checked = false;
                listViewUsers.Items.Add(item);
            }
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listViewUsers.Items.Count; i++ )
            {
                if (listViewUsers.Items[i].Checked)
                {
                    _selectedUsers.Add(listViewUsers.Items[i].Text);
                }
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        
    }
}
