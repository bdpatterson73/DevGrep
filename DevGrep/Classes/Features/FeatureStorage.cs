using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace DevGrep.Classes.Features
{
    internal class FeatureStorage
    {
        private string DUPLICATE_FILE_SCANNER = @"9AD5BEFB-1BA5-4BEE-8250-B5CFA8720B4B";
        private int DUPLICATE_FILE_SCANNER_MAX_RUNS = 10;
        private string VISUALIZE = @"1F80EF25-D2BA-40FC-A388-D211F5A73F41";
        private int VISUALIZE_MAX_RUNS = 10;

        internal FeatureStorage()
        {
            InitValues();
        }

        internal string RootKey
        {
            get { return @"Software\" + Program.APP_NAME + @"\Version\" + VersionMajorMinor + @"\Feature\"; }
        }

        internal string VersionMajorMinor
        {
            get
            {
                Assembly assem = Assembly.GetExecutingAssembly();
                AssemblyName assemName = assem.GetName();
                Version ver = assemName.Version;
                return ver.Major + "." + ver.Minor;
            }
        }


        private void InitValues()
        {
            // Attempt to load our values.. If they don't exist - then create them.
            LoadFromRegistry();
        }

        private bool ReadSettings()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(RootKey);
            if (key != null)
            {
                _dupFileScannerRuns = (int)key.GetValue(DUPLICATE_FILE_SCANNER, DUPLICATE_FILE_SCANNER_MAX_RUNS);
                _visualizeRuns = (int)key.GetValue(VISUALIZE, VISUALIZE_MAX_RUNS);
                return true;
            }
            return false;
        }
        private void LoadFromRegistry()
        {
           if (!ReadSettings())
            {
                // Create the defaults.
                CreateDefaults();
                ReadSettings();
            }
        }

        private void CreateDefaults()
        {
            RegistryKey key2 = Registry.CurrentUser.CreateSubKey(RootKey);
            key2.SetValue(DUPLICATE_FILE_SCANNER, DUPLICATE_FILE_SCANNER_MAX_RUNS);
            key2.SetValue(VISUALIZE, VISUALIZE_MAX_RUNS);
        }

        internal void SaveSettings()
        {
            RegistryKey key2 = Registry.CurrentUser.CreateSubKey(RootKey);
            key2.SetValue(DUPLICATE_FILE_SCANNER, _dupFileScannerRuns);
            key2.SetValue(VISUALIZE, _visualizeRuns);
        }

        private int _visualizeRuns;
        internal int VisualizerRuns
        {
            get { return _visualizeRuns; }

        }

        internal void ReportVisualizerRun()
        {
            _visualizeRuns--;
            if (_visualizeRuns < 0)
                _visualizeRuns = 0;
            SaveSettings();

        }

        private int _dupFileScannerRuns;
        internal int DuplicateFileScannerRuns
        {
            get { return _dupFileScannerRuns; }
            
        }

        internal void ReportDuplicateFileScannerRun()
        {
            _dupFileScannerRuns--;
            if (_dupFileScannerRuns < 0)
                _dupFileScannerRuns = 0;
            SaveSettings();
        }

    }
}