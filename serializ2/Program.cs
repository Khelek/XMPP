using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using agsXMPP;

namespace EnterpriseMICApplicationDemo
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
            //( new FormJabberStart(1) ).Show();
            ( new FormJabberStart(2) ).Show();
            Application.Run(new FormJabberStart(3));
#endif
        }
    }
}
