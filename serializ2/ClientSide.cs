using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using agsXMPP;
using agsXMPP.protocol.client;

namespace serializ2
{
    class ClientSide
    {
        private string _mess = "";
        private string _thread = "";
        private string _from = "";
        private string _presence = "";
        private string _talker = "";
        private XmppClientConnection client;
        public event ClientEvent Receive;
        public event ClientEvent getPresence;
        public event ClientEvent onLoginEvent;

        public string mess
        {
            get { return _mess; }
        }

        public string thread
        {
            get { return _thread; }
        }

        public string from
        {
            get { return _from; }
        }

        public string presence
        {
            get { return _presence; }
        }

        public string talker
        {
            get { return _talker; }
        }

        public void CloseConnection()
        {
            client.Close();
        }

        public ClientSide(string server, string connserver, string username, string password)
        {
            client = connect(server, connserver, username, password);
        }

        public void sendMessage(string jid, string message, string thread = null)
        {
            Message msg = new Message(new Jid(jid), MessageType.chat, message);
            if (thread != null)
                msg.Thread = thread;
            client.Send(msg);
        }

        public void sendInviteConferention(List<string> jidUsers, string dataOfConferention)
        {
            /*using (var stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, conferention);
                byte[] b = stream.ToArray();
                message = BitConverter.ToString(b);
            }*/

            for (int i = 0; i < jidUsers.Count; i++)
            {
                sendMessage(jidUsers[i], "<conf>" + dataOfConferention);
            }
        }

        private void onMessage(object o, Message msg)
        {
            _thread = msg.Thread;
            _mess = msg.Body;
            _from = msg.From.Bare.ToString();
            if (Receive != null)
                Receive(msg);
        }

        public void setStatus(string status)
        {
            Presence p = new Presence();
            p.Show = ShowType.NONE;
            p.Status = status;
            client.Send(p);
        }

        private void onLogin(object o)
        {
            setStatus("онлайн");
            if (onLoginEvent != null)
                onLoginEvent(null);
        }

        private void onPresence(object o, Presence pres)
        {
            //unavailable
            _presence = pres.Type.ToString();
            _talker = pres.From.Bare;
            if (getPresence != null)
                getPresence(pres);

        }

        private XmppClientConnection connect(string server, string connserver, string username, string password)
        {
            XmppClientConnection xmpp = new XmppClientConnection();
            xmpp.Server = server;//"jabberd.eu";
            xmpp.ConnectServer = connserver;// "jabberd.eu";
            if ( server == "haupc" || server == "192.168.1.4" ) {
                xmpp.Port = 5222;
            }
            xmpp.Username = username;// "khelek";
            xmpp.Password = password;//"abe2b33519";
            //xmpp.UseSSL = true;
        	xmpp.UseStartTLS = true;

            xmpp.Open();//"khelek@jabberd.eu";
            xmpp.OnMessage += onMessage;
            xmpp.OnLogin += onLogin;
            xmpp.OnPresence += onPresence;

            return xmpp;
        }

    }
}
