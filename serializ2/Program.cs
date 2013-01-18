using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using agsXMPP;

namespace serializ2
{
    static class Program
    {
        
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);   
#if !DEBUG
            Application.Run(new FormFirst());
#endif
#if DEBUG 
            Application.Run(new FormFirst(0));
#endif
        }
        internal static string jid;
        internal static XmppClientConnection xmpp;
    }
}
