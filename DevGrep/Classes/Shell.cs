using System.Runtime.InteropServices;

namespace DevGrep

{
    [StructLayout(LayoutKind.Sequential)]
    public class SHELLEXECUTEINFO

    {
        public int cbSize;
        public int fMask;
        public int hwnd;
        public string lpVerb;
        public string lpFile;
        public string lpParameters;
        public string lpDirectory;
        public int nShow;
        public int hInstApp;
        public int lpIDList;
        public string lpClass;
        public int hkeyClass;
        public int dwHotKey;
        public int hIcon;
        public int hProcess;

    }

    public class Shell

    {
        public const int SEE_MASK_INVOKEIDLIST = 0xc;

        [DllImport("shell32.dll")]
        public static extern bool ShellExecuteEx([In, Out] SHELLEXECUTEINFO
            execInfo);

    }
}