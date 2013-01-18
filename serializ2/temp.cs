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
        Conferentions conferentions = new Conferentions();
        private int idConf = 0;//индексы конференций нужно хранить на сервере, иначе будут пересечения
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
                SendTextMessage(tabName, p.Name, dialog, textBoxSend.Text);
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
                    SendTextMessage(tabName, p.Name, dialog, textBoxSend.Text);
                    textBoxSend.Clear();
                }
            };

            p.Controls.AddRange(new Control[] { dialog, textBoxSend, send, enter });
            tabsDialog.TabPages.Add(p);
        }

        private void SendTextMessage(string tabName, string id, TextBox dialog, string message)
        {

            client.sendMessage(tabName, message);
            dialog.AppendText(formateString("user", Message));
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

        private OneConferention stringToConferention()
        {
            string message = client.mess.Substring(6);//ибо первые 6 - <conf>
            String[] arr = message.Split('/');//нужно экранировать <>/
            string id = arr[0];
            string name = arr[1];
            int userCount = Int32.Parse(arr[2]);
            List<string> users = new List<string>();
            for (int i = 0; i < userCount; i++)
            {
                users.Add(arr[i + 3]);
            }
            return new OneConferention(id, name, users);
        }

        private void HandlerOnMessege(object msg)
        {
            tabsDialog.Invoke(new MethodInvoker(delegate
            {
				Message mess = (Message)msg;
                if (client.thread == null)//если сообщение не относиться к конференции(уже существующей конференции)
                {
                   
                        if (client.mess.Length > 5 && client.mess.Substring(0, 6) == "<conf>")//если приглашение
                        {
                            OneConferention conferention = stringToConferention();
                            if (conferention.id == null)
                            {
                                return; // такого тащемта быть не должно
                            }

                            if (conferentions.Contains(conferention.id))//если приглашение пришло на уже существующую конференцию
                            {
                                OneConferention conf = conferentions[conferentions.Find(conferention.id)];
                                int indexTab = findTagPage(conf.name);
                                if (indexTab == -1)
                                {
                                    createTabPage(conferention.name, null, conferention.id);//вместо null(текст внутри tabPage) по идее должна быть история конференции,
                                    //её придется передавать также, желательно с помощью функции из класса Conferentions
                                }
                                return;//конференция уже существует
                            }

                            if (MessageBox.Show("Вас пригласили в конференцию " + conferention.name + ". Желаете принять участие?", "Приглашение", MessageBoxButtons.OKCancel) == DialogResult.OK)
                            {
                                initFormDialog();
                                conferentions.Add(conferention);
                                createTabPage(conferention.name, null, conferention.id);//вместо null(текст внутри tabPage) по идее должна быть история конференции,
                                //её придется передавать также, желательно с помощью функции из класса Conferentions
                            }
                        }
                    
                    else //простое сообщение
                    {
                        initFormDialog();
                        int indexTab = findTagPage(client.from);
                        if (indexTab != -1) //если уже существует вкладка для него
                        {
                            string nameTalker = tabsDialog.TabPages[indexTab].Text;
                            ((TextBox)tabsDialog.TabPages[indexTab].Controls[0]).AppendText(nameTalker + " (" + DateTime.Now.ToString() + ")"//Controls[0] - это textBox с диалогом
                                + Environment.NewLine + client.mess + Environment.NewLine + Environment.NewLine);
                            tabsDialog.Focus();
                        }
                        else
                        {
                            createTabPage(client.from, client.from + " (" + DateTime.Now.ToString() + ")"
                                + Environment.NewLine + client.mess + Environment.NewLine + Environment.NewLine);
                            tabsDialog.Focus();
                        }
                    }
                }
                else //сообщение к конференции
                {
                    initFormDialog();
                    int indexTab = tabsDialog.TabPages.IndexOfKey(client.thread);
                    if (indexTab != -1)
                    {//TabPages[indexTab].Control[0] - textBox с диалогом
                        ((TextBox)tabsDialog.TabPages[indexTab].Controls[0]).AppendText(client.from + " (" + DateTime.Now.ToString() + ")"
                            + Environment.NewLine + client.mess + Environment.NewLine + Environment.NewLine);
                        tabsDialog.Focus();
                    }
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
                }
                createConference.Show();
                checkBoxConnected.Checked = true;
            }));
        }

        private void createConference_Click(object sender, EventArgs e)
        {
            string conferenceName = "Конференция " + idConf;
            idConf++;//костыль, id надо хранить на серваке, либо при отсылке приглашений сравнивать на наличие id у клиентов, но это плохой план
            string createrJid = login;
            /*for (int i = 0; i < listBoxUsers.SelectedItems.Count; i++)
                users.Add(listBoxUsers.SelectedItems[i].ToString());*/

            FormCreateConferention fCreate = new FormCreateConferention(listBoxUsers.Items);
            fCreate.ShowDialog();

            List<string> users = fCreate.selectedUsers;
            if (users.Count == 0) return;
            initFormDialog();
            OneConferention conf = new OneConferention(idConf.ToString(), conferenceName, createrJid, new List<string>(users));
            conferentions.Add(conf);
            client.sendInviteConferention(users, conf.toString());
            //можно еще добавить отправку всем "Конференция создана"

            createTabPage(conferenceName, null, idConf.ToString());
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
                string connserver = ( server == "gmail.com" ) ? "talk.google.com" : (( server == "haupc") ? "192.168.1.4" : server );

                login = loginBox.Text;
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
            createConference.Hide();
            listBoxUsers.Items.Clear();
            checkBoxConnected.Checked = false;

        }

        private void Add_Users_Click(object sender, EventArgs e)
        {
            FormAddUser add_us = new FormAddUser();//никак не работает
            add_us.Show();//ничего не делает
        }


    }
}
