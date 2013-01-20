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


namespace serializ2 {

    delegate void ClientEvent(object o);
    public partial class FormFirst : Form {
        FormDialog formDialog;
        TabControl tabsDialog;
        XmppClientConnection xmpp;
        private Jid mainJid;// текущий логин пользователя(имеется ввиду jid на самом деле - xxx@yyy.zz)
        private List<string> onlineUsers = new List<string>();


        public FormFirst() {
            InitializeComponent();
        }
#if DEBUG
        public FormFirst(int i) {
            InitializeComponent();
            switch ( i )//это свитч 
            {
                case 1:
                    loginBox.Text = "user2@haupc";//"khelek@jabber.ru";
                    passwordBox.Text = "321";//"haukot1994";
                    break;
                case 2:
                    loginBox.Text = "admin@haupc";//"khelek@jabberd.eu";
                    passwordBox.Text = "haukot";//"abe2b33519";
                    break;
                case 3:
                    loginBox.Text = "user@haupc";//"haudvd@gmail.com";
                    passwordBox.Text = "321";//"haukot1994";
                    break;
                case 4:
                    loginBox.Text = "khelek@jabber.ru";
                    passwordBox.Text = "haukot1994";
                    break;
                case 5:
                    loginBox.Text = "khelek@jabberd.eu";
                    passwordBox.Text = "abe2b33519";
                    break;
            }
        }
#endif

        private void createTabPage(string tabName, string key = "", string insideText = null) {
            TabPage p = new TabPage(tabName);
            p.Name = key;
            TextBox dialog = new TextBox();
            dialog.Location = tabsDialog.Location;
            dialog.Size = new System.Drawing.Size(450, 170);
            dialog.Multiline = true;
            dialog.ScrollBars = ScrollBars.Vertical;
            dialog.ReadOnly = true;
            dialog.BackColor = Color.White;
            if ( insideText != null ) {
                dialog.AppendText(insideText);
            }
            TextBox textBoxSend = new TextBox();
            textBoxSend.Location = new Point(dialog.Location.X, dialog.Location.Y + dialog.Height);
            textBoxSend.Size = new System.Drawing.Size(240, 45);
            textBoxSend.Multiline = true;
            Button send = new Button();
            send.Text = "Послать";
            send.Location = new Point(dialog.Location.X + dialog.Width - 80, dialog.Location.Y + dialog.Height + 10);
            send.Click += delegate(object s, EventArgs a) {
                SendTextMessage(tabName, dialog, textBoxSend.Text);
                textBoxSend.Clear();
            };
            CheckBox enter = new CheckBox();
            enter.Text = "Send if Enter";
            enter.Location = new Point(textBoxSend.Location.X + textBoxSend.Width + 10, dialog.Location.Y + dialog.Height + 5);
            enter.Checked = true;

            textBoxSend.KeyDown += delegate(object o, KeyEventArgs k) {
                if ( k.KeyCode == Keys.Enter && enter.Checked ) {
                    SendTextMessage(key, dialog, textBoxSend.Text);
                    textBoxSend.Clear();
                }
            };
            p.Controls.AddRange(new Control[] { dialog, textBoxSend, send, enter });
            tabsDialog.TabPages.Add(p);
        }

        private void SendTextMessage(string toJid, TextBox dialog, string message) {
            agsXMPP.protocol.client.Message msg = new agsXMPP.protocol.client.Message(new Jid(toJid), MessageType.chat, message);
            xmpp.Send(msg);
            dialog.AppendText(formateString("user", message));
        }

        private int findTagPage(string name) {
            for ( int i = 0; i < tabsDialog.TabPages.Count; i++ ) {
                if ( name == tabsDialog.TabPages[i].Name ) {
                    return i;
                }
            }
            return -1;
        }

        private string formateString(string from, string mess) {
            return ( from + " (" + DateTime.Now.ToString() + ")"//Controls[0] - это textBox с диалогом
                            + Environment.NewLine + mess + Environment.NewLine + Environment.NewLine );
        }

        private ListViewItem getListViewItem(string jid) {
            foreach ( ListViewItem lvi in listUsers.Items ) {
                if ( jid.ToLower() == lvi.Name.ToLower() )
                    return lvi;
            }
            return null;
        }

        private ListViewItem createListViewItem(string jid, string name, Color color) {
            ListViewItem item = new ListViewItem();
            item.Name = jid;
            item.Text = name;
            item.ForeColor = color;
            return item;
        }

        private void HandlerOnMessage(object o, agsXMPP.protocol.client.Message msg) {
            BeginInvoke(new MethodInvoker(delegate {
                string from = msg.From.Bare.ToString();
                int indexTab;
                switch ( msg.Type ) {
                    case MessageType.chat: //простое сообщение			
                        initFormDialog();
                        indexTab = findTagPage(from);
                        if ( indexTab != -1 ) {//если уже существует вкладка для него 
                            ( (TextBox)tabsDialog.TabPages[indexTab].Controls[0] ).AppendText(formateString(from, msg.Body));
                        } else {
                            createTabPage(from, formateString(from, msg.Body));
                        }
                        tabsDialog.Focus();
                        break;
                    case MessageType.groupchat:
                        //конференции сами ловят сообщения в своей форме
                        break;
                    case MessageType.error:
                        //вообще не ловятся
                        break;
                }
            }));
        }

        private void HandlerOnPresence(object o, Presence presence) {
            BeginInvoke(new MethodInvoker(delegate {
                ListViewItem it = getListViewItem(presence.From.Bare);
                if ( presence.Type.ToString() == "unavailable" || presence.Type.ToString() == "error" ) {//client.presence == "unavailable") 
                    if ( it != null )
                        it.ForeColor = Color.Red;
                    else {
                        onlineUsers.Remove(presence.From.Bare);
                    }
                }
                if ( presence.Type.ToString() == "available" ) {
                    if ( it != null )
                        it.ForeColor = Color.Black;
                    else {
                        onlineUsers.Add(presence.From.Bare);
                    }
                }
            }));
        }

        private void HandlerOnLogin(object o) {
            BeginInvoke(new MethodInvoker(delegate() {
                setStatus("Онлайн");
                if ( formDialog == null ) {
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

        private void HandlerOnRosterStart(object o) {
            BeginInvoke(new MethodInvoker(delegate() {
                listUsers.BeginUpdate();
            }));
        }

        private void HandlerOnRosterEnd(object o) {
            BeginInvoke(new MethodInvoker(delegate() {
                for ( int i = onlineUsers.Count - 1; i >= 0; i-- ) {
                    ListViewItem it = getListViewItem(onlineUsers[i]);
                    if ( it != null ) {
                        it.ForeColor = Color.Black;
                        onlineUsers.RemoveAt(i);
                    }
                }
                listUsers.EndUpdate();
            }));
        }

        private void HandlerOnRosterItem(object o, agsXMPP.protocol.iq.roster.RosterItem item) {
            BeginInvoke(new MethodInvoker(delegate {
                string itemJid = item.Jid.Bare;
                if ( item.Subscription != agsXMPP.protocol.iq.roster.SubscriptionType.remove ) {
                    string nodeText = item.Name != null ? item.Name : itemJid;
                    listUsers.Items.Add(createListViewItem(itemJid, nodeText, Color.Red));
                } else {
                    listUsers.Items.Remove(getListViewItem(itemJid));
                }
            }));
        }

        private void setStatus(string status) {
            Presence p = new Presence(ShowType.chat, status);
            xmpp.Send(p);
            textBoxStatus.Clear();
        }

        private void textBoxStatus_KeyDown(object sender, KeyEventArgs e) {
            if ( e.KeyCode == Keys.Enter ) {
                setStatus(textBoxStatus.Text);
            }
        }

        private void button4_Click(object sender, EventArgs e) {
            setStatus(textBoxStatus.Text);
        }

        private void initFormDialog() {
            if ( !formDialog.Visible ) {
                formDialog.Show();
            } else {
                formDialog.Focus();
            }
        }

        private void listUsers_MouseDoubleClick(object sender, MouseEventArgs e) {
            if ( listUsers.SelectedItems.Count > 0 && listUsers.SelectedItems[0] != null ) {
                initFormDialog();
                string name = listUsers.SelectedItems[0].Text;
                string jid = listUsers.SelectedItems[0].Name;
                int index = findTagPage(jid);
                if ( index == -1 ) {
                    createTabPage(name, jid);
                } else {
                    tabsDialog.TabPages[index].Focus();
                }
            }
        }

        private void login_MouseClick(object sender, MouseEventArgs e) {
            //login.Clear();
        }

        private void password_MouseClick(object sender, MouseEventArgs e) {
            //password.Clear();
        }

        private void connect(string server, string connserver, string username, string password) {
            xmpp = new XmppClientConnection();
            xmpp.Server = server;//"jabberd.eu";
            xmpp.ConnectServer = connserver;// "jabberd.eu";
            if ( server == "haupc" || server == "192.168.1.4" ) {
                xmpp.Port = 5222;
            }
            xmpp.Username = username;// "khelek";
            xmpp.Password = password;//"abe2b33519";
            //xmpp.UseSSL = true;
            xmpp.UseStartTLS = true;

            xmpp.Open();
            xmpp.OnRosterStart += HandlerOnRosterStart;
            xmpp.OnRosterEnd += HandlerOnRosterEnd;
            xmpp.OnRosterItem += HandlerOnRosterItem;
            xmpp.OnMessage += HandlerOnMessage;
            xmpp.OnLogin += HandlerOnLogin;
            xmpp.OnPresence += HandlerOnPresence;
        }

        private void Login_Button_Click(object sender, EventArgs e) {
            try {
                string nickname = loginBox.Text.Substring(0, loginBox.Text.IndexOf("@"));
                string server = loginBox.Text.Substring(loginBox.Text.IndexOf("@") + 1);
                string connserver = ( server == "gmail.com" ) ? "talk.google.com" : ( ( server == "haupc" ) ? "192.168.1.3" : server );
                mainJid = new Jid(loginBox.Text);
                //khelek@jabberd.eu
                //abe2b33519
                Exit.Show();
                loginBox.Hide();
                passwordBox.Hide();
                Login_Button.Hide();
                connect(server, connserver, nickname, passwordBox.Text);
                //Add_Users.Hide();
            } catch {
                MessageBox.Show("Неверный логин или пароль, попробуйте еще раз");
                loginBox.Show();
                passwordBox.Show();
                Login_Button.Show();
                passwordBox.Clear();
            }
        }

        private void Exit_Click(object sender, EventArgs e) {
            xmpp.Close();
            Exit.Hide();
            loginBox.Show();
            passwordBox.Show();
            Login_Button.Show();
            //Add_Users.Show();
            buttonCreateConf.Hide();
            buttonJoinConf.Hide();
            listUsers.Items.Clear();
            checkBoxConnected.Checked = false;
        }

        private void Add_Users_Click(object sender, EventArgs e) {
            FormAddUser add_us = new FormAddUser();//никак не работает
            add_us.Show();//ничего не делает
        }

        private void createConference_Click(object sender, EventArgs e) {
            List<string> users = new List<string>();
            foreach ( var it in listUsers.Items )
                users.Add(it.ToString());
            FormCreateConferention fCreate = new FormCreateConferention(xmpp, mainJid, users);
            fCreate.Show();
        }

        private void buttonJoinConf_Click(object sender, EventArgs e) {
            FormJoinConferention fJoinConf = new FormJoinConferention(xmpp, mainJid);
            fJoinConf.Show();
        }

        private void FormFirst_FormClosing(object sender, FormClosingEventArgs e) {
            if ( xmpp != null )
                xmpp.Close();
        }

    }
}
