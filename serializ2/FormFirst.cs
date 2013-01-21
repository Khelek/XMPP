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
using System.Data.SQLite;


namespace serializ2 {

    public partial class FormFirst : Form {
        FormDialog formDialog;
        TabControl tabsDialog;
        XmppClientConnection xmpp;
        private Jid mainJid;// текущий логин пользователя(имеется ввиду jid на самом деле - xxx@yyy.zz)
        private string nickname = "user";
        private List<string> onlineUsers = new List<string>();//хранятся контакты, которые в онлайне на момент загрузки списка контактов; затем отмечаются в списке как онлайн и удаляются
        private List<ListViewItem> offlineUsers = new List<ListViewItem>();
        bool offlineContactsHidden = false;
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;


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
            //dialog.Anchor = (AnchorStyles)(AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            if ( insideText != null ) {
                dialog.AppendText(insideText);
            }
            TextBox textBoxSend = new TextBox();
            textBoxSend.Location = new Point(dialog.Location.X, dialog.Location.Y + dialog.Height);
            textBoxSend.Size = new System.Drawing.Size(240, 45);
            textBoxSend.Multiline = true;
            //textBoxSend.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Button send = new Button();
            send.Text = "Послать";
            send.Location = new Point(dialog.Location.X + dialog.Width - 80, dialog.Location.Y + dialog.Height + 10);
            //send.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            send.Click += delegate(object s, EventArgs a) {
                SendTextMessage(tabName, dialog, textBoxSend.Text);
                textBoxSend.Clear();
            };
            CheckBox enter = new CheckBox();
            enter.Text = "Send if Enter";
            enter.Location = new Point(textBoxSend.Location.X + textBoxSend.Width + 10, dialog.Location.Y + dialog.Height + 5);
            enter.Checked = true;
            //enter.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

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
            dialog.AppendText(formateString(nickname, message));
            addMessageToDB(mainJid.Bare, toJid, message);
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
                string from = msg.From.Bare;
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
                        indexTab = findTagPage(from);
                        if ( !( formDialog.Focused && tabsDialog.SelectedIndex == indexTab ) ) {
                            getListViewItem(from).BackColor = Color.Orange;
                        }
                        addMessageToDB(msg.From.Bare, msg.To.Bare, msg.Body);
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
                    if ( it == null ) {
                        onlineUsers.Remove(presence.From.Bare);
                    } else if ( !offlineContactsHidden ) {
                        it.ForeColor = Color.Red;
                    } else {
                        ListViewItem offlineUser = getListViewItem(presence.From.Bare);
                        offlineUser.ForeColor = Color.Red;
                        offlineUsers.Add(offlineUser);
                    }
                }
                if ( presence.Type.ToString() == "available" ) {
                    if ( it != null ) {
                        it.ForeColor = Color.Black;
                    } else if ( offlineContactsHidden ) {
                        ListViewItem listItem = offlineUsers.Find(item => item.Name == presence.From.Bare);
                        if ( listItem == null ) {
                            //здесь попадаются какие-то левые
                        } else {
                            listItem.ForeColor = Color.Black;
                            listUsers.Items.Add(listItem);
                            offlineUsers.Remove(listItem);
                        }
                    } else {
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
                    tabsDialog.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                    tabsDialog.SelectedIndexChanged += tabsDialogSelectedIndexChangedOrFormDialogFocused;
                    formDialog.Controls.AddRange(new Control[] { tabsDialog });
                    formDialog.GotFocus += tabsDialogSelectedIndexChangedOrFormDialogFocused;
                    formDialog.FormClosing += formDialog_FormClosing;
                    formDialog.Show();//this is
                    formDialog.Hide();//КОООСТЫЫЫ
                    formDialog.Close();//ЫЫЫЫЫЫЫЫЫЫЛЬ
                }
                buttonCreateConf.Show();
                buttonJoinConf.Show();
                buttonShowHideContacts.Show();
                checkBoxConnected.Checked = true;
            }));
        }

        void formDialog_FormClosing(object sender, FormClosingEventArgs e) {
            tabsDialog.TabPages.Clear();
        }

        private void tabsDialogSelectedIndexChangedOrFormDialogFocused(object sender, EventArgs e) {//длинное мнемоническое имя
            if ( tabsDialog.SelectedTab == null )
                return;
            ListViewItem it = getListViewItem(tabsDialog.SelectedTab.Name);
            if ( it == null ) {
                return;
            }
            if ( it.BackColor != Color.Orange ) {
                return;
            }
            it.BackColor = Color.Transparent;
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
                    createTabPage(name, jid, getHistoryFromDB(jid));
                    tabsDialog.SelectedIndex = tabsDialog.TabPages.Count - 1;
                } else {
                    tabsDialog.SelectedIndex = index;
                }
            }
        }

        private void login_MouseClick(object sender, MouseEventArgs e) {
            //login.Clear();
        }

        private void password_MouseClick(object sender, MouseEventArgs e) {
            //password.Clear();
        }

        private string getHistoryFromDB(string jidTo) {
            string res = "";
            int limit = 2;
            Stack<string> lastMessages = new Stack<string>();            
            sql_cmd.CommandText = @"SELECT * FROM chat_history WHERE user_from ='" + jidTo + "' OR user_to ='" + jidTo + "' ORDER BY date_time DESC LIMIT " + limit;
            using ( SQLiteDataReader reader = sql_cmd.ExecuteReader() ) {
                if ( reader.HasRows ) {
                    for ( int i = 0; i < limit && reader.Read(); i++ ) {
                        string from = reader.GetValue(0).ToString();
                        if ( from == mainJid.Bare )
                            from = nickname;
                        lastMessages.Push(Environment.NewLine + from + "(" + reader.GetValue(2) + ")" + Environment.NewLine + reader.GetValue(3) + Environment.NewLine);
                    }
                }
            }
            while ( lastMessages.Count != 0 ) {
                res += lastMessages.Pop();
            }
            if (res != "")
                res += "_________________________________________________________________" + Environment.NewLine + Environment.NewLine;
            return res;
        }

        private void addMessageToDB(string from, string to, string message) {
            sql_cmd.CommandText = @"INSERT INTO chat_history (user_from,user_to,date_time,message) VALUES('" + from + "','" + to + "','" + DateTime.Now.ToString() + "', '" + message + "');";
            int res = sql_cmd.ExecuteNonQuery();
        }

        private void connectDb() {
            sql_con = new SQLiteConnection(@"Data Source=d:\programming\git\xmpp\serializ2\db\xmpp_db;New=False;Version=3");
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
        }

        private void connectXmpp(string server, string connserver, string username, string password) {
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
                Exit.Show();
                loginBox.Hide();
                passwordBox.Hide();
                Login_Button.Hide();
                connectXmpp(server, connserver, nickname, passwordBox.Text);
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
            listUsers.Items.Clear();
            Exit.Hide();
            loginBox.Show();
            passwordBox.Show();
            Login_Button.Show();
            //Add_Users.Show();
            buttonCreateConf.Hide();
            buttonJoinConf.Hide();
            buttonShowHideContacts.Hide();
            checkBoxConnected.Checked = false;
        }

        private void Add_Users_Click(object sender, EventArgs e) {
            FormAddUser add_us = new FormAddUser();//никак не работает
            add_us.Show();//ничего не делает
        }

        private void createConference_Click(object sender, EventArgs e) {
            List<string> users = new List<string>();
            foreach ( ListViewItem it in listUsers.Items )
                users.Add(it.Text);
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

        private void buttonShowHideContacts_Click(object sender, EventArgs e) {
            listUsers.BeginUpdate();
            if ( offlineContactsHidden ) {
                for ( int i = offlineUsers.Count - 1; i >= 0; i-- ) {
                    listUsers.Items.Add(offlineUsers[i]);
                    offlineUsers.RemoveAt(i);
                }
                offlineContactsHidden = false;
            } else {
                for ( int i = listUsers.Items.Count - 1; i >= 0; i-- ) {
                    if ( listUsers.Items[i].ForeColor == Color.Red ) {
                        offlineUsers.Add(listUsers.Items[i]);
                        listUsers.Items.RemoveAt(i);
                    }
                }
                offlineContactsHidden = true;
            }
            listUsers.EndUpdate();
        }

        private void listUsers_KeyDown(object sender, KeyEventArgs e) {
            if ( e.KeyCode == Keys.Enter ) {
                listUsers_MouseDoubleClick(sender, null);
            }
        }

        private void FormFirst_Load(object sender, EventArgs e) {
            connectDb();
        }



    }
}
