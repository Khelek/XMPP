using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using agsXMPP;
using agsXMPP.Collections;
using agsXMPP.protocol;
using agsXMPP.protocol.client;
using agsXMPP.protocol.x.muc;
using agsXMPP.protocol.x.muc.iq;
using agsXMPP.protocol.x.data;

namespace EnterpriseMICApplicationDemo
{
    public partial class FormConferention : Form
    {
        private Jid roomJid;
        private string mainJid;
        private string nickname;
        private string roomName;
        private string roomDesc;
        private string password = "";
        private string savingHistory;
        private string persistRoom;
        private XmppClientConnection xmpp;
        private MucManager muc;

        public FormConferention(string _roomJid)
        {
            InitializeComponent();
            roomJid = new Jid(_roomJid);
            xmpp = Settings.xmpp;
            mainJid = Settings.jid;
            nickname = Settings.nickname;
            muc = new MucManager(xmpp);
            muc.JoinRoom(roomJid, nickname);			
            xmpp.MesagageGrabber.Add(roomJid, new BareJidComparer(), new MessageCB(MessageCallback), null);
            xmpp.PresenceGrabber.Add(roomJid, new BareJidComparer(), new PresenceCB(PresenceCallback), null);
        }

        public FormConferention(string _roomJid, string _roomName, bool _savingHistory, bool _persistRoom, string _roomDesc = "", List<string> users = null )
        {
            InitializeComponent();
            roomJid = new Jid(_roomJid);
            roomName = _roomName;
            this.Text = _roomName;
            roomDesc = _roomDesc;
            mainJid = Settings.jid;
            nickname = Settings.nickname;			
            xmpp = Settings.xmpp;
            muc = new MucManager(xmpp);
            savingHistory = _savingHistory ? "1" : "0";
            persistRoom = _persistRoom ? "1" : "0";
            //muc.AcceptDefaultConfiguration(roomJid, new IqCB(OnGetFieldsResult));
	        muc.CreateReservedRoom(roomJid);
            muc.GrantOwnershipPrivileges(roomJid, new Jid(mainJid));
            muc.JoinRoom(roomJid, nickname);
            initMucConfig();
            xmpp.MesagageGrabber.Add(roomJid, new BareJidComparer(), new MessageCB(MessageCallback), null);
            xmpp.PresenceGrabber.Add(roomJid, new BareJidComparer(), new PresenceCB(PresenceCallback), null);
            muc.Invite(users.ConvertAll<Jid>(
                delegate(string jid)
                {
                    return new Jid(jid);
                }
            ).ToArray(), roomJid, "Вы приглашены в конференцию Конф.");
        }

        private void initMucConfig()
        {
            muc.RequestConfigurationForm(roomJid, new IqCB(ReceiveFormConfiguration));
        }

        private void addFieldInDataIQ(agsXMPP.protocol.x.data.Data data, string fieldname, string value) {
            Field field = new Field();					
            field.Var = fieldname;
            agsXMPP.Xml.Dom.Element e = new agsXMPP.Xml.Dom.Element("value", value);
            field.AddChild(e);
            data.AddChild(field);
        }

        private void ReceiveFormConfiguration(object sender, IQ iq, object obj)
        {
            agsXMPP.protocol.x.muc.iq.owner.OwnerIq oIq = new agsXMPP.protocol.x.muc.iq.owner.OwnerIq();
            oIq.Type = IqType.get;
            oIq.To = roomJid;
            Settings.xmpp.IqGrabber.SendIq(oIq, new IqCB(OnRequestConfiguration), null);
        }

        private void OnRequestConfiguration(object sender, IQ iq, object obj)
        {
            agsXMPP.protocol.x.muc.iq.owner.OwnerIq oIq = new agsXMPP.protocol.x.muc.iq.owner.OwnerIq();
            oIq.Type = IqType.set;
            oIq.To = iq.From;
            agsXMPP.protocol.x.data.Data data = new agsXMPP.protocol.x.data.Data(XDataFormType.submit);

            addFieldInDataIQ(data, "FORM_TYPE", "http://jabber.org/protocol/muc#roomconfig");
            addFieldInDataIQ(data, "muc#roomconfig_roomname", roomName);
            addFieldInDataIQ(data, "muc#roomconfig_roomdesc", roomDesc);
            addFieldInDataIQ(data, "muc#roomconfig_persistentroom", persistRoom);
            addFieldInDataIQ(data, "muc#roomconfig_publicroom", "1");
            addFieldInDataIQ(data, "public_list", "1");
            addFieldInDataIQ(data, "muc#roomconfig_passwordprotectedroom", (password == "") ? "0" : "1");
            addFieldInDataIQ(data, "muc#roomconfig_roomsecret", "");
            addFieldInDataIQ(data, "muc#roomconfig_maxusers", "1000");
            addFieldInDataIQ(data, "muc#roomconfig_whois", "moderators");
            addFieldInDataIQ(data, "muc#roomconfig_membersonly", "0");
            addFieldInDataIQ(data, "muc#roomconfig_moderatedroom", "1");
            addFieldInDataIQ(data, "members_by_default", "0");
            addFieldInDataIQ(data, "muc#roomconfig_membersonly", "0");
            addFieldInDataIQ(data, "muc#roomconfig_membersonly", "0");
            addFieldInDataIQ(data, "muc#roomconfig_changesubject", "1");
            addFieldInDataIQ(data, "allow_private_messages", "1");
            addFieldInDataIQ(data, "allow_private_messages_from_visitors", "anyone");
            addFieldInDataIQ(data, "allow_query_users", "1");
            addFieldInDataIQ(data, "muc#roomconfig_allowinvites", "0");
            addFieldInDataIQ(data, "muc#roomconfig_allowvisitorstatus", "1");
            addFieldInDataIQ(data, "muc#roomconfig_allowvisitornickchange", "0");
            addFieldInDataIQ(data, "muc#roomconfig_allowvoicerequests", "1");
            addFieldInDataIQ(data, "muc#roomconfig_voicerequestmininterval", "1800");
            addFieldInDataIQ(data, "muc#roomconfig_captcha_whitelist", "");
            addFieldInDataIQ(data, "muc#roomconfig_enablelogging", savingHistory);

            oIq.Query.AddChild(data);
            Settings.xmpp.IqGrabber.SendIq(oIq, new IqCB(OnGetFieldsResult), null);
        }

        public void OnGetFieldsResult(object sender, IQ iq, object data)
        {
            //
        }

        private void textBoxMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && checkIfEnter.Checked)
            {
                SendConfMessage(textBoxSend.Text);
            }
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            SendConfMessage(textBoxSend.Text);
        }

        private void SendConfMessage(string message)
        {
            if (message.Length > 0)
            {
                agsXMPP.protocol.client.Message msg = new agsXMPP.protocol.client.Message();
                msg.Type = MessageType.groupchat;
                msg.To = roomJid;
                msg.Body = message;
                xmpp.Send(msg);
                textBoxSend.Clear();
            }
        }

        private void FormConferention_Load(object sender, EventArgs e)
        {
        }

        private void FormConferention_FormClosed(object sender, FormClosedEventArgs e)
        {
            muc.LeaveRoom(roomJid, nickname);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="msg"></param>
        /// <param name="data"></param>
        private void MessageCallback(object sender, agsXMPP.protocol.client.Message msg, object data)
        {
            BeginInvoke(new MethodInvoker(delegate {
            if (msg.Type == MessageType.groupchat)
                IncomingMessage(msg);
			}));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="pres"></param>
        /// <param name="data"></param>
        private void PresenceCallback(object sender, agsXMPP.protocol.client.Presence pres, object data)
        {
            BeginInvoke(new MethodInvoker(delegate {
	            string user = findListBoxItem(pres.From.Resource);
	            if (user != null)
	            {
	                if (pres.Type == PresenceType.unavailable)
	                {
	                    listBoxConfUsers.Items.Remove(user);
	                }
	                else
	                {
	                    //хз что делает
	
	                    //int imageIdx = Util.GetRosterImageIndex(pres);
	                    //lvi.ImageIndex = imageIdx;
	                    //lvi.SubItems[1].Text = ( pres.Status == null ? "" : pres.Status );
	                    //User u = pres.SelectSingleElement(typeof(User)) as User;
	                    //if ( u != null ) {
	                    //    lvi.SubItems[2].Text = u.Item.Affiliation.ToString();
	                    //    lvi.SubItems[3].Text = u.Item.Role.ToString();
	                    //}
	                }
	            }
	            else
	            {
	                if (pres.Type != PresenceType.unavailable)
	                {
	                    listBoxConfUsers.Items.Add(pres.From.Resource);
	                }
	            }
			}));
        }

        private string findListBoxItem(string jid)
        {
            foreach (string i in listBoxConfUsers.Items)
            {
                if (jid.ToLower() == i.ToLower())
                    return i;
            }
            return null;
        }

        private void IncomingMessage(agsXMPP.protocol.client.Message msg)
        {
            if (msg.Type == MessageType.error)
            {
                //Handle errors here
                // we dont handle them in this example
                return;
            }
            if (msg.Subject != null)
            {
                txtSubject.Text = msg.Subject;

                rtfChat.SelectionColor = Color.DarkGreen;
                // The Nickname of the sender is in GroupChat in the Resource of the Jid
                rtfChat.AppendText(msg.From.Resource + " изменил тему: ");
                rtfChat.SelectionColor = Color.Black;
                rtfChat.AppendText(msg.Subject);
                rtfChat.AppendText("\r\n");
            }
            else
            {
                if (msg.Body == null)
                    return;

                rtfChat.SelectionColor = Color.Blue;//(msg.Nickname.InnerXml == nickname) ? Color.Green : Color.Red;
                // The Nickname of the sender is in GroupChat in the Resource of the Jid
                rtfChat.AppendText(msg.From.Resource + " : ");
                rtfChat.SelectionColor = Color.Black;
                rtfChat.AppendText(msg.Body);
                rtfChat.AppendText("\r\n");
            }
        }

        private void cmdSend_Click(object sender, EventArgs e)
        {
            // Make sure that the users send no empty messages
            if (textBoxSend.Text.Length > 0)
            {
                agsXMPP.protocol.client.Message msg = new agsXMPP.protocol.client.Message();
                msg.Type = MessageType.groupchat;
                msg.To = roomJid;
                msg.Body = textBoxSend.Text;
                xmpp.Send(msg);
                textBoxSend.Text = "";
            }
        }

        /// <summary>
        /// Changing the subject in a chatroom
        /// in MUC rooms this could return an error when you are a normal user and not allowed
        /// to change the subject.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdChangeSubject_Click(object sender, EventArgs e)
        {
            agsXMPP.protocol.client.Message msg = new agsXMPP.protocol.client.Message();
            msg.Type = MessageType.groupchat;
            msg.To = roomJid;
            msg.Subject = txtSubject.Text;
            xmpp.Send(msg);
        }

        private void buttonDescr_Click(object sender, EventArgs e) {	
								
        }
		
		private void getInfoTheRoom() {
			requestOnGetInfoTheRoom (delegate (object sender, IQ iq, object data) {				
				string descr = iq.Query.LastNode.ChildNodes.Item(2).ChildNodes.Item(1).Value;	
				string name = iq.Query.FirstChild.Attribute("name");
			});			
		}
				
		private void requestOnGetInfoTheRoom(agsXMPP.IqCB handl) {
			BeginInvoke( new MethodInvoker(delegate {
				IQ iq = new IQ(IqType.get, new Jid(mainJid), roomJid);
				agsXMPP.protocol.iq.disco.DiscoInfo info = new agsXMPP.protocol.iq.disco.DiscoInfo();
				iq.Query = info;
				iq.Id = "id_" + mainJid + "_" + roomJid.Bare + "_" + (Settings.requestId++);
	            xmpp.IqGrabber.SendIq(iq, handl, null);					
			}));
		}
    }
}
