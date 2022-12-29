using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevGrep.Classes;

namespace DevGrep.SubApps.DupFileScan.Classes.Prefs
{
    internal class DuplicateFileScanPrefs
    {

        //     txtSearchLocation.Text = Win32Registry.HKCUReadKey(@"Software\DevGrep\Pref\", "LastSearchLocation");
        //Win32Registry.HKCUWriteKey(@"Software\DevGrep\Pref\", "LastSearchLocation", txtSearchLocation.Text);
        internal DuplicateFileScanPrefs()
        {
            InitValues();
        }

        internal void RefreshValues()
        {
            InitValues();
        }
        private void InitValues()
        {
            _lastDuplicateScan = Win32Registry.HKCUReadKey(@"Software\DevGrep\Pref\", "LastDupScanLocation");
            _lastDuplicateScanComparison = Win32Registry.HKCUReadKey(@"Software\DevGrep\Pref\", "LastDupScanComparisonLocation");
        }

        private string _lastDuplicateScan;
        internal string LastDuplicateScan
        {
            get { return _lastDuplicateScan; }
            set 
            { 
                _lastDuplicateScan = value;
                Win32Registry.HKCUWriteKey(@"Software\DevGrep\Pref\", "LastDupScanLocation", _lastDuplicateScan);

            }
        }

        private string _lastDuplicateScanComparison;
        internal string LastDuplicateScanComparison
        {
            get { return _lastDuplicateScanComparison; }
            set
            {
                _lastDuplicateScanComparison = value;
                Win32Registry.HKCUWriteKey(@"Software\DevGrep\Pref\", "LastDupScanComparisonLocation", _lastDuplicateScanComparison);

            }
        }
    }
}
