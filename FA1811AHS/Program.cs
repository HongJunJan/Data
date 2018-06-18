using System;
using System.Windows.Forms;

namespace FA1811AHS
{
    internal static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormStart());
            //Application.Run(new LaserPrinting());
            //Application.Run(new LaserSet());
            //Application.Run(new PartNoSet());
            //Application.Run(new TraySet());
            //Application.Run(new FirstStepApp());
            //Application.Run(new PartNoWritePLC());
            //Application.Run(new AccountSet());
            //Application.Run(new TraySearch());
            //Application.Run(new RecChangeReport());
            //Application.Run(new RecErrorReport());
        }
    }
}