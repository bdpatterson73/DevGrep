using System;
using System.IO;
using System.Windows.Forms;
using UpdateCheck.Properties;

namespace UpdateCheck
{
    internal static class Program
    {
        internal static bool _DEBUG = false;
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static int Main(string[] args)
        {
            if (args.Length > 0)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                ParseArgs(args);
                var fTry = new Form1(args);
                if (fTry.AreUpdatesAvailable(true))
                {
                    Application.Run(new Form1(args));
                }
                else
                {
                    return 7;
                    if (args.Length == 2 && args[1].ToUpper() == "-V")
                    {
                       //TODO this doesn't work since some UI elements haven't been inited. 
                        //MessageBox.Show(Resources.Form1_StartCheckForUpdate_);
                    }
                }
            }
            return 0;
        }

        internal static void ParseArgs(string[] args)
        {
            foreach (string s in args)
            {
                switch (s.ToUpper())
                {
                    case "-D":
                        _DEBUG = true;
                        break;
                }
            }
        }

        internal static void LogUpdateCheck(string message)
        {
            File.AppendAllText("UpdateCheck.log",DateTime.Now.ToString() + " " + message + Environment.NewLine);
        }
    }
}