using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using agsXMPP;
using agsXMPP.protocol.client;
using System.Data.SQLite;


namespace EnterpriseMICApplicationDemo {

    public partial class FormJabberStart : Form {
        FormDialog formDialog;
        TabControl tabsDialog;
        XmppClientConnection xmpp;
        private Jid mainJid;// текущий jid
        private string nickname = "we";
        private List<string> onlineUsersWait = new List<string>();//хранятся контакты, которые в онлайне на момент загрузки списка контактов; затем отмечаются в списке как онлайн и удаляются
        private List<ListViewItem> offlineUsers = new List<ListViewItem>();
        private List<agsXMPP.protocol.client.Message> waitList = new List<agsXMPP.protocol.client.Message>();//хранит сообщения, которые пришли, но еще не прочитаны пользователем(хранятся при условии, что formDialog закрыта)
        bool offlineContactsHidden = false;//скрыты ли контакты, которые не в сети
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;


        public FormJabberStart() {
            InitializeComponent();
            loginBox.Text = "user@haupc";//"haudvd@gmail.com";
            passwordBox.Text = "321";//"haukot1994";
            this.Paint += new PaintEventHandler(FormJabberStart_Paint);
        }
#if DEBUG
        public FormJabberStart(int i) {
            InitializeComponent();
            switch ( i )//это свитч 
            {
                case 1:
				Settings.nickname = "user2";
                    loginBox.Text = "user2@haupc";//"khelek@jabber.ru";
                    passwordBox.Text = "321";//"haukot1994";
                    break;
                case 2:
				Settings.nickname = "admin";
                    loginBox.Text = "admin@haupc";//"khelek@jabberd.eu";
                    passwordBox.Text = "haukot";//"abe2b33519";
                    break;
                case 3:
				Settings.nickname = "user";
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
        private void initSettings(string ip, string nickname, string jid) {
            Settings.serverIp = ip;
            Settings.nickname = nickname;
            Settings.jid = jid;
        }

        private void FormJabberStart_Paint(object sender, PaintEventArgs e) {
            Graphics g = e.Graphics;
            DrawRectangle(g, 0, 0, this.Width, this.Height);
        }

        private void DrawRectangle(Graphics g, int x, int y, int widht, int height) {
            Rectangle rec = new Rectangle(x, y, widht, height);
            if ( ( widht != 0 ) && ( height != 0 ) ) {
                System.Drawing.Drawing2D.LinearGradientBrush gradient;
                gradient = new System.Drawing.Drawing2D.LinearGradientBrush(rec, Color.FromArgb(251, 188, 59), Color.White, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);

                g.FillRectangle(gradient, rec);
                return;
            }
            Brush brush = new SolidBrush(Color.FromArgb(251, 188, 59));
            g.FillRectangle(brush, rec);
        }

        /// <summary>
        /// создает вкладку с диалогом на FormDialog
        /// </summary>
        /// <param name="tabName">название вкладки(ник собеседника)</param>
        /// <param name="key">скрытый параметр - jid собеседника</param>
        /// <param name="insideText">текст, добавляющийся на вкладку сразу после её создания</param>
        private void createTabPage(string tabName, string key = "", string insideText = null) {
            formDialog.createTabPage(this, tabName, key, insideText);
        }

        /// <summary>
        /// Отправляет сообщение к собеседнику с заданным jid и выводит его в textbox с чатом, а также добавляет в историю(в бд)
        /// </summary>
        /// <param name="toJid">jid получателя</param>
        /// <param name="dialog">textbox с чатом</param>
        /// <param name="message">сообщение</param>
        public void SendTextMessage(Jid toJid, RichTextBox dialog, string message) {
            agsXMPP.protocol.client.Message msg = new agsXMPP.protocol.client.Message(toJid, MessageType.chat, message);
            xmpp.Send(msg);
            addMessToDialog(nickname, message, dialog, Settings.myColor);
            //addMessageToHistoryDB(mainJid.Bare, mainJid.Resource, toJid.Bare, toJid.Resource, message);
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

        /// <summary>
        /// Находит в коллекции открытых вкладок с диалогами ту вкладку, jid собеседника которой совпадает с аргументом
        /// </summary>
        /// <param name="jid"></param>
        /// <returns>Возвращает индекс найденной вкладки</returns>
        private int findTagPage(string jid) {
            for ( int i = 0; i < tabsDialog.TabPages.Count; i++ ) {
                if ( jid == tabsDialog.TabPages[i].Name ) {
                    return i;
                }
            }
            return -1;
        }

        private string formateString(string from, string mess) {
            return ( from + " (" + DateTime.Now.ToString() + ")"//Controls[0] - это textBox с диалогом
                            + Environment.NewLine + mess + Environment.NewLine + Environment.NewLine );
        }

        private void addMessToDialog(string from, string mess, RichTextBox dialog, Color color) {
            dialog.SelectionColor = color;
            dialog.AppendText( from + " (" + DateTime.Now.ToString() + ")");
            dialog.SelectionColor = Color.Black;
            dialog.AppendText(Environment.NewLine + mess + Environment.NewLine + Environment.NewLine );
        }

        /// <summary>
        /// Получает из списка контактов тот элемент, в котором jid совпадает с аргументом
        /// </summary>
        /// <param name="jid"></param>
        /// <returns>Возвращает элемент списка</returns>
        private ListViewItem getListViewItem(string jid) {
            foreach ( ListViewItem lvi in listUsers.Items ) {
                if ( jid.ToLower() == lvi.Name.ToLower() )
                    return lvi;
            }
            return null;
        }

        /// <summary>
        /// Создает элемент списка контактов
        /// </summary>
        /// <param name="jid">jid контакта</param>
        /// <param name="name">ник контакта</param>
        /// <param name="color">цвет отображения контакта в списке</param>
        /// <returns></returns>
        private ListViewItem createListViewItem(string jid, string name, Color color) {
            ListViewItem item = new ListViewItem();
            item.Name = jid;
            item.Text = name;
            item.ForeColor = color;            
            return item;
        }

        /// <summary>
        /// Событие, вызывающееся при получении сообщения
        /// </summary>
        /// <param name="o"></param>
        /// <param name="msg"></param>
        private void HandlerOnMessage(object o, agsXMPP.protocol.client.Message msg) {
            BeginInvoke(new MethodInvoker(delegate {
                string from = msg.From.Bare;
                switch ( msg.Type ) {
                    case MessageType.chat: //простое сообщение		
                        int indexTab = findTagPage(from);
                        if ( indexTab != -1 ) {//если уже существует вкладка для него 
                            addMessToDialog(from, msg.Body, (RichTextBox)tabsDialog.TabPages[indexTab].Controls[0], Settings.youColor);//добаляем в диалог
                            //( (TextBox)tabsDialog.TabPages[indexTab].Controls[0] ).AppendText(formateString(from, msg.Body));
                        } else {
                            waitList.Add(msg);//добавляем в лист ожидания
                        }
                        if ( !( formDialog.Focused && tabsDialog.SelectedIndex == indexTab ) ) {
                            getListViewItem(from).BackColor = Color.Orange;//если на форме с диалогом нет фокуса или открыта не та вкладка, к которой пришло сообщение, выделяем в списке контактов контакт отправителя
                        }
                        // addMessageToHistoryDB(msg.From.Bare, msg.From.Resource, msg.To.Bare, msg.To.Resource, msg.Body);//записываем сообщение в бд(история)
                        break;
                    case MessageType.groupchat:
                        //конференции сами ловят сообщения в своей форме
                        break;
                    case MessageType.error:
                        //вообще не ловятся
                        break;
                }
                if ( msg.FirstChild.FirstChild.TagName == "invite" ) {
                    DialogResult res =  MessageBox.Show("Вам пришло приглашение в конференцию " + msg.From.Resource + ". Желаете присоединиться?", "caption", MessageBoxButtons.OKCancel );
                	if (res == DialogResult.OK)
					{
						( new FormConferention(msg.From.Bare) ).Show();
					}
				}
            }));
        }

        /// <summary>
        /// Событие, вызыващееся при получении присутствия
        /// </summary>
        /// <param name="o"></param>
        /// <param name="presence"></param>
        private void HandlerOnPresence(object o, Presence presence) {
            BeginInvoke(new MethodInvoker(delegate {
                ListViewItem it = getListViewItem(presence.From.Bare);
                //если контакт не онлайн
                if ( presence.Type.ToString() == "unavailable" || presence.Type.ToString() == "error" ) {
                    if ( it == null ) {
                        // и его нет в списке контактов, то удаляем его из списка onlineUsers
                        onlineUsersWait.Remove(presence.From.Bare);
                    } else if ( !offlineContactsHidden ) {
                        //а если есть в списке контактов, и контакты не в сети не надо скрывать, то выделяем его красным
                        it.ForeColor = Color.Red;
                    } else {
                        //а если же пользователь не желает зрить контакты, с коими общаться не можно, то заносим ирода в список пользователей недоступных
                        ListViewItem offlineUser = getListViewItem(presence.From.Bare);
                        offlineUser.ForeColor = Color.Red;
                        offlineUsers.Add(offlineUser);
                    }
                }
                if ( presence.Type.ToString() == "available" ) {
                    if ( it != null ) {
                        //если пользователь есть в списке контактов
                        it.ForeColor = Color.Black;
                    } else if ( offlineContactsHidden ) {
                        //если нужно скрывать оффлайн контакты, то ищем в списке оффлайн юзеров 
                        ListViewItem listItem = offlineUsers.Find(item => item.Name == presence.From.Bare);
                        if ( listItem == null ) {
                            //здесь попадаться здесь не должен _никто_(по идее), но попадаются какие-то левые 
                        } else {
                            //и добавляем в список контактов
                            listItem.ForeColor = Color.Black;
                            listUsers.Items.Add(listItem);
                            offlineUsers.Remove(listItem);
                        }
                    } else {
                        //если же пользователя нет в списке контактов, и оффлановые контакты не скрыты, то это значит, что список контактов еще не загрузился, и добавляем контакт в список ожидания
                        onlineUsersWait.Add(presence.From.Bare);
                    }
                }
            }));
        }

        /// <summary>
        /// Происходит при удачной авторизации
        /// </summary>
        /// <param name="o"></param>
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
                Settings.xmpp = xmpp;
            }));
        }

        /// <summary>
        /// Происходит при начале получения списка контактов
        /// </summary>
        /// <param name="o"></param>
        private void HandlerOnRosterStart(object o) {
            BeginInvoke(new MethodInvoker(delegate() {
                listUsers.BeginUpdate();
            }));
        }

        /// <summary>
        /// Происходит, когда весь список контактов получен
        /// </summary>
        /// <param name="o"></param>
        private void HandlerOnRosterEnd(object o) {
            BeginInvoke(new MethodInvoker(delegate() {
                //добавляем контакты из списка ожидания
                for ( int i = onlineUsersWait.Count - 1; i >= 0; i-- ) {
                    ListViewItem it = getListViewItem(onlineUsersWait[i]);
                    if ( it != null ) {
                        it.ForeColor = Color.Black;
                        onlineUsersWait.RemoveAt(i);
                    }
                }
                listUsers.EndUpdate();
            }));
        }

        /// <summary>
        /// Происходит при получении одного элемента списка контактов
        /// </summary>
        /// <param name="o"></param>
        /// <param name="item"></param>
        private void HandlerOnRosterItem(object o, agsXMPP.protocol.iq.roster.RosterItem item) {
            BeginInvoke(new MethodInvoker(delegate {
                string itemJid = item.Jid.Bare;
                if ( item.Subscription != agsXMPP.protocol.iq.roster.SubscriptionType.remove ) {
                    //если этот элемент списка не пришел с меткой на удаление, то добавляем его
                    string nodeText = item.Name != null ? item.Name : itemJid;
                    listUsers.Items.Add(createListViewItem(itemJid, nodeText, Color.Red));
                } else {
                    //иначе удаляем
                    listUsers.Items.Remove(getListViewItem(itemJid));
                }
            }));
        }

        /// <summary>
        /// Происходит при ошибке авторизации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandlerOnAuthError(object sender, agsXMPP.Xml.Dom.Element e) {
            MessageBox.Show("Ошибка авторизации. При частом её повторении помолитесь и попробуйте еще разок.");
            Exit_Click(null, null);
        }

        private void setStatus(string status) {
            Presence p = new Presence(ShowType.chat, status);
            xmpp.Send(p);
            textBoxStatus.Clear();
        }

        private void initFormDialog() {
            if ( !formDialog.Visible ) {
                formDialog.Show();
            } else {
                formDialog.Focus();
            }
        }

        private void createTableInDB(string tablename) {
            sql_cmd.CommandText = @"CREATE TABLE IF NOT EXISTS" + tablename + @" 
                    (
                            id INTEGER PRIMARY KEY AUTOINCREMENT,
                            from_jid TEXT,
                            data_time TEXT,
                            message TEXT
                    )";
            int res = sql_cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Получает из бд историю переписки с контактом
        /// </summary>
        /// <param name="jidTo">jid контакта</param>
        /// <returns></returns>
        private string getHistoryFromDB(string jidTo) {
            string res = "";
            int limit = 2;
            Stack<string> lastMessages = new Stack<string>();
            sql_cmd.CommandText = @"SELECT * FROM chat_history WHERE user_from_jid ='" + jidTo + "' OR user_to_jid ='" + jidTo + "' ORDER BY date_time DESC LIMIT " + limit;
            using ( SQLiteDataReader reader = sql_cmd.ExecuteReader() ) {
                if ( reader.HasRows ) {
                    for ( int i = 0; i < limit && reader.Read(); i++ ) {
                        //    0        1       2      3         4        5
                        //from_jid from_name to_jid to_name data_time message
                        lastMessages.Push(Environment.NewLine + reader.GetValue(1) + "(" + reader.GetValue(4) + ")" + Environment.NewLine + reader.GetValue(5) + Environment.NewLine);
                    }
                }
            }
            while ( lastMessages.Count != 0 ) {
                res += lastMessages.Pop();
            }
            if ( res != "" )
                res += "_________________________________________________________________" + Environment.NewLine + Environment.NewLine;
            return res;
        }

        private void addMessageToHistoryDB(string fromJid, string fromName, string toJid, string toName, string message) {
            sql_cmd.CommandText = @"INSERT INTO chat_history (user_from_jid, user_from_name, user_to_jid, user_to_name, date_time,message)
                                    VALUES('" + fromJid + "','" + fromName + "','" + toJid + "','" + toName + "','" + DateTime.Now.ToString() + "', '" + message + "');";
            int res = sql_cmd.ExecuteNonQuery();
        }

        private void connectDb() {
            sql_con = new SQLiteConnection(@"Data Source=d:\programming\git\MIC_Enterprise\EnterpriseMICApplicationDemo\Jabber\db\xmpp_db;New=False;Version=3");
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
        }

        private void connectXmpp(string server, string connserver, string username, string password) {
            xmpp = new XmppClientConnection();
            xmpp.Server = server;//"jabberd.eu";
            xmpp.ConnectServer = connserver;// "jabberd.eu";
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
            xmpp.OnAuthError += HandlerOnAuthError;
        }

        private void listUsers_KeyDown(object sender, KeyEventArgs e) {
            if ( e.KeyCode == Keys.Enter ) {
                listUsers_MouseDoubleClick(sender, null);
            }
        }

        private void textBoxStatus_KeyDown(object sender, KeyEventArgs e) {
            if ( e.KeyCode == Keys.Enter ) {
                setStatus(textBoxStatus.Text);
            }
        }

        private void buttonLogin_Click(object sender, EventArgs e) {
            try {
                string nickname = loginBox.Text.Substring(0, loginBox.Text.IndexOf("@"));
                string server = loginBox.Text.Substring(loginBox.Text.IndexOf("@") + 1);
                Settings.jid = loginBox.Text;
                string connserver = ( server == "gmail.com" ) ? "talk.google.com" : ( ( server == "haupc" ) ? Settings.serverIp : server );
                mainJid = new Jid(loginBox.Text);
                Exit.Show();
                loginBox.Hide();
                passwordBox.Hide();
                buttonLogin.Hide();
                listUsers.TileSize = new Size(listUsers.Width, (int)(listUsers.Font.Size + listUsers.Font.Height/1.4));
                connectXmpp(server, connserver, nickname, passwordBox.Text);
                //Add_Users.Hide();
            } catch {
                MessageBox.Show("Неверный логин или пароль, попробуйте еще раз");
                loginBox.Show();
                passwordBox.Show();
                buttonLogin.Show();
                passwordBox.Clear();
            }
        }

        private void Exit_Click(object sender, EventArgs e) {
            xmpp.Close();
            listUsers.Items.Clear();
            Exit.Hide();
            loginBox.Show();
            passwordBox.Show();
            buttonLogin.Show();
            //Add_Users.Show();
            buttonCreateConf.Hide();
            buttonJoinConf.Hide();
            buttonShowHideContacts.Hide();
            checkBoxConnected.Checked = false;
        }

        /// <summary>
        /// Событие нажатия на кнопку "показать/скрыть оффлайн контакты". Показывает или скрывает контакты.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        private void Add_Users_Click(object sender, EventArgs e) {
            FormAddUser add_us = new FormAddUser();//никак не работает
            add_us.Show();//ничего не делает
        }

        private void createConference_Click(object sender, EventArgs e) {
            List<string> users = new List<string>();
            foreach ( ListViewItem it in listUsers.Items )
                users.Add(it.Text);
            FormCreateConferention fCreate = new FormCreateConferention(users);
            fCreate.Show();
        }

        private void buttonJoinConf_Click(object sender, EventArgs e) {
            FormJoinConferention fJoinConf = new FormJoinConferention();
            fJoinConf.Show();
        }

        private void buttonSettings_Click(object sender, EventArgs e) {
            ( new FormSettings() ).Show();
        }

        private void button4_Click(object sender, EventArgs e) {
            setStatus(textBoxStatus.Text);
        }

        private void listUsers_MouseDoubleClick(object sender, MouseEventArgs e) {
            if ( listUsers.SelectedItems.Count > 0 && listUsers.SelectedItems[0] != null ) {
                initFormDialog();
                string name = listUsers.SelectedItems[0].Text;
                string jid = listUsers.SelectedItems[0].Name;
                int indexTab = findTagPage(jid);
                List <agsXMPP.protocol.client.Message> msgsWait = waitList.FindAll(item => item.From.Bare == jid);//получает все сообщения из списка ожидающих сообщений с нужным jid
                if ( indexTab == -1 ) {
                    createTabPage(name, jid);//, getHistoryFromDB(jid));
                    tabsDialog.SelectedIndex = tabsDialog.TabPages.Count - 1;
                } else {
                    tabsDialog.SelectedIndex = indexTab;
                }
                indexTab = tabsDialog.SelectedIndex;
                //добавляем сообщения из ожидания в диалог
                for (int i = msgsWait.Count - 1; i >= 0; i-- ) {
                    agsXMPP.protocol.client.Message msgWait = msgsWait[i];
                    ( (TextBox)tabsDialog.TabPages[indexTab].Controls[0] ).AppendText(formateString(msgWait.From.Bare, msgWait.Body));
                    waitList.Remove(msgWait);
                }
            }
        }

        private void login_MouseClick(object sender, MouseEventArgs e) {
        }

        private void password_MouseClick(object sender, MouseEventArgs e) {
        }

        private void FormJabberStart_Load(object sender, EventArgs e) {
           // connectDb();
        }

        private void FormJabberStart_FormClosing(object sender, FormClosingEventArgs e) {
            if ( xmpp != null )
                xmpp.Close();
        }

        private void formDialog_FormClosing(object sender, FormClosingEventArgs e) {
            tabsDialog.TabPages.Clear();
        }


    }
}
