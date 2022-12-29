using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using DevGrep.Classes;
using DevGrep.Classes.Config;
using DevGrep.Classes.Features;

namespace DevGrep
{
    /// Class Program
    /// </summary>
    internal static class Program
    {
        internal static INIConfig ConfigFile;

        internal static string APP_NAME = "DevGrep";
        internal static string COMPANY_NAME = "Borderline Software, Inc.";
        internal static DevGrepLicense dgl;
        internal static FeatureStorage featureStorage;
        internal static Assembly helpFiles;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="args">The args.</param>
        [STAThread]
        private static void Main(string[] args)
        {
            dgl = new DevGrepLicense();
            helpFiles = DevGrepResources.Resources.AssemblyReference();
            featureStorage = new FeatureStorage();
            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
            ApplicationData ad = new ApplicationData();

            ConfigFile = new INIConfig(COMPANY_NAME, APP_NAME);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            Application.Run(new frmMain(args));
           //Application.Run(new formMainSearch(args));
        }

        static void Application_ApplicationExit(object sender, EventArgs e)
        {

        }

        internal static bool FeatureLicensedMsg(string featureName)
        {
            if (!dgl.IsLicensed)
            {
                MessageBox.Show(
                    featureName + " is only available in the licensed version of DevGrep. To purchase" +
                    Environment.NewLine
                    + "a license please visit: " + Environment.NewLine +
                    "http://www.borderlinesoftware.com", "Licensed Feature", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            return true;
        }

        internal static bool FeatureLicensedMsg(string featureName, int maxRuns)
        {
            if (maxRuns>0)
            {
                
                MessageBox.Show(
                    featureName + " can only be run "+ maxRuns+" more time(s) in this trial" +
                    Environment.NewLine
                    + "version of DevGrep. To purchase a license please visit: " + Environment.NewLine +
                    "http://www.borderlinesoftware.com", "Licensed Feature", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return true;
            }
            return false;
        }
    }
}
