using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using agsXMPP;
using agsXMPP.protocol.client;


namespace serializ2
{

    delegate void ClientEvent(object o);
    public partial class FormFirst : Form
    {
        ClientSide client;
        FormDialog formDialog;
        TabControl tabsDialog;
        private string login;// текущий логин пользователя(имеется ввиду jid на самом деле - xxx@yyy.zz)

        public FormFirst()
        {
            InitializeComponent();
        }
#if DEBUG
        public FormFirst(int i)
        {
            InitializeComponent();
            switch (i)//этот свитч 
            {
                case 0:
                    loginBox.Text = "user@haupc";//"misha@192.168.43.3";//
                    passwordBox.Text = "321";//"9ca82e3206caf6d7075c050fe78edc806efb1446";// 
                    break;
                case 3:
                    loginBox.Text = "khelek@jabber.ru";//"misha@192.168.43.3";//
                    passwordBox.Text = "haukot1994";//"9ca82e3206caf6d7075c050fe78edc806efb1446";// 
                    break;
                case 1:
                    loginBox.Text = "khelek@jabberd.eu";
                    passwordBox.Text = "abe2b33519";
                    break;
                case 2:
                    loginBox.Text = "haudvd@gmail.com";
                    passwordBox.Text = "haukot1994";
                    break;
            }
        }
#endif
		
        private void createTabPage(string tabName, string insideText = null, string key = "")
        {
            TabPage p = new TabPage(tabName);
            p.Name = key;
            TextBox dialog = new TextBox();
            dialog.Location = tabsDialog.Location;
            dialog.Size = new System.Drawing.Size(450, 170);
            dialog.Multiline = true;
            dialog.ScrollBars = ScrollBars.Vertical;
            dialog.ReadOnly = true;
            dialog.BackColor = Color.White;
            if (insideText != null)
            {
                dialog.AppendText(insideText);
            }
            TextBox textBoxSend = new TextBox();
            textBoxSend.Location = new Point(dialog.Location.X, dialog.Location.Y + dialog.Height);
            textBoxSend.Size = new System.Drawing.Size(240, 45);
            textBoxSend.Multiline = true;
            Button send = new Button();
            send.Text = "Послать";
            send.Location = new Point(dialog.Location.X + dialog.Width - 80, dialog.Location.Y + dialog.Height + 10);
            send.Click += delegate(object s, EventArgs a)
            {
                SendTextMessage(tabName, dialog, textBoxSend.Text);
                textBoxSend.Clear();
            };
            CheckBox enter = new CheckBox();
            enter.Text = "Send if Enter";
            enter.Location = new Point(textBoxSend.Location.X + textBoxSend.Width + 10, dialog.Location.Y + dialog.Height + 5);
            enter.Checked = true;

            textBoxSend.KeyDown += delegate(object o, KeyEventArgs k)
            {
                if (k.KeyCode == Keys.Enter && enter.Checked)
                {
                    SendTextMessage(tabName, dialog, textBoxSend.Text);
                    textBoxSend.Clear();
                }
            };
            p.Controls.AddRange(new Control[] { dialog, textBoxSend, send, enter });
            tabsDialog.TabPages.Add(p);
        }

        private void SendTextMessage(string toJid, TextBox dialog, string message)
        {
            client.sendMessage(toJid, message, MessageType.chat);
            dialog.AppendText(formateString("user", message));
        }

        private int findTagPage(string name)
        {
            for (int i = 0; i < tabsDialog.TabPages.Count; i++)
            {
                if (name == tabsDialog.TabPages[i].Text)
                {
                    return i;
                }
            }
            return -1;
        }
		
		private string formateString(string from, string mess) {
			return (from + " (" + DateTime.Now.ToString() + ")"//Controls[0] - это textBox с диалогом
                            + Environment.NewLine + mess + Environment.NewLine + Environment.NewLine);
		}

        private void HandlerOnMessege(object msg)
        {
            tabsDialog.Invoke(new MethodInvoker(delegate
            {
			agsXMPP.protocol.client.Message mess = (agsXMPP.protocol.client.Message)msg;
			string from = mess.From.Bare.ToString();
			int indexTab;
                switch(mess.Type) {
			case MessageType.chat: //простое сообщение			
                initFormDialog();
                indexTab = findTagPage(from);
                if (indexTab != -1) //если уже существует вкладка для него
                {
                    ((TextBox)tabsDialog.TabPages[indexTab].Controls[0]).AppendText(formateString(from, mess.Body));
                }
				else {				
                	createTabPage(from, formateString(from, mess.Body));
				}
                tabsDialog.Focus();
			    break;   
			case MessageType.groupchat:				
               	//конференции сами ловят сообщения в своей форме
				break;
			}
            }));
        }

        private void HandlerOnPresence(object pres)
        {
            listBoxUsers.Invoke(new MethodInvoker(delegate
            {
				Presence presence = (Presence)pres;
                if (presence.Type.ToString() == "unavailable")//client.presence == "unavailable")
                {
                    listBoxUsers.Items.Remove(presence.From.Bare);
                }
                else
                {
                    listBoxUsers.Items.Add(presence.From.Bare);
                }
            }));
        }

        private void HandlerOnLogin(object nil)
        {
            checkBoxConnected.Invoke(new MethodInvoker(delegate()
            {
                if (formDialog == null)
                {
                    formDialog = new FormDialog();
                    tabsDialog = new TabControl();
                    tabsDialog.Size = new System.Drawing.Size(500, 260);
                    tabsDialog.Location = new Point(10, 10);
                    formDialog.Controls.AddRange(new Control[] { tabsDialog });
                    formDialog.Show();//this is
                    formDialog.Hide();//КОООСТЫЫЫЫЫЛЬ
					formDialog.Close();
                }
                buttonCreateConf.Show();
                buttonJoinConf.Show();
                checkBoxConnected.Checked = true;
            }));
        }

        private void createConference_Click(object sender, EventArgs e)
        {            
            List<string> users = new List<string>();
            foreach(var it in listBoxUsers.Items)
                users.Add(it.ToString());
            FormCreateConferention fCreate = new FormCreateConferention(users);
            fCreate.Show();
        }

        private void textBoxStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                client.setStatus(textBoxStatus.Text);
                textBoxStatus.Clear();
            }
        }

        private void initFormDialog()
        {
            if (!formDialog.Visible)
            {
                formDialog.Show();
            }
            else
            {
                formDialog.Focus();
            }
        }

        private void listBox2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBoxUsers.SelectedItem != null)
            {
                initFormDialog();
                string tabName = listBoxUsers.SelectedItem.ToString();
                int index = findTagPage(tabName);
                if (index == -1)
                {
                    createTabPage(tabName);
                }
                else
                {
                    tabsDialog.TabPages[index].Focus();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            client.setStatus(textBoxStatus.Text);
        }

        private void login_MouseClick(object sender, MouseEventArgs e)
        {
            //login.Clear();
        }

        private void password_MouseClick(object sender, MouseEventArgs e)
        {
            //password.Clear();
        }

        private void Login_Button_Click(object sender, EventArgs e)
        {
            try
            {
                string nickname = loginBox.Text.Substring(0, loginBox.Text.IndexOf("@"));
                string server = loginBox.Text.Substring(loginBox.Text.IndexOf("@") + 1);
                string connserver = ( server == "gmail.com" ) ? "talk.google.com" : (( server == "haupc") ? "172.25.76.57" : server );
                Program.jid = loginBox.Text;
                //khelek@jabberd.eu
                //abe2b33519
                Exit.Show();
                loginBox.Hide();
                passwordBox.Hide();
                Login_Button.Hide();
                //Add_Users.Hide();
                client = new ClientSide(server, connserver, nickname, passwordBox.Text);
                client.Receive += new ClientEvent(HandlerOnMessege);
                client.getPresence += new ClientEvent(HandlerOnPresence);
                client.onLoginEvent += new ClientEvent(HandlerOnLogin);
            }
            catch
            {
                MessageBox.Show("Неверный логин или пароль, попробуйте еще раз");
                loginBox.Show();
                passwordBox.Show();
                Login_Button.Show();
                passwordBox.Clear();
            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            client.CloseConnection();
            Exit.Hide();
            loginBox.Show();
            passwordBox.Show();
            Login_Button.Show();
            //Add_Users.Show();
            buttonCreateConf.Hide();
            buttonJoinConf.Hide();
            listBoxUsers.Items.Clear();
            checkBoxConnected.Checked = false;

        }

        private void Add_Users_Click(object sender, EventArgs e)
        {
            FormAddUser add_us = new FormAddUser();//никак не работает
            add_us.Show();//ничего не делает
        }

        private void buttonJoinConf_Click(object sender, EventArgs e) {
            FormJoinConferention fJoinConf = new FormJoinConferention();
            fJoinConf.Show();
        }


    }
}
