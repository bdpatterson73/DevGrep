using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace DevGrep.Classes
{
    public static class Win32API
    {
        [DllImport("User32.Dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("shell32.dll")]
        public static extern IntPtr ShellExecute(
            IntPtr hwnd,
            string lpOperation,
            string lpFile,
            string lpParameters,
            string lpDirectory,
            int nShowCmd);
    }
}
