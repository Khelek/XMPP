using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using agsXMPP;

namespace EnterpriseMICApplicationDemo {
    class Settings {
        public static string jid;
        public static XmppClientConnection xmpp;
        public static string nickname = "haukot";
        public static string serverIp = "192.168.1.4";
        public static string server = "haupc";
        public static System.Drawing.Color myColor = System.Drawing.Color.Green;
        public static System.Drawing.Color youColor = System.Drawing.Color.Red;
		public static int requestId = 0;
    }
}
