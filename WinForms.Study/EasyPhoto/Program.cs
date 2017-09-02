using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace EasyPhoto
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if ((args != null) && (args.Length > 0))
            {
                string filePath = "";
                for (int i = 0; i < args.Length; i++)
                {
                    filePath += " " + args[i];
                }

                MainForm.DefaultPath = filePath.Trim();
                //MessageBox.Show(MainForm.DefaultPath,"");
            }


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
