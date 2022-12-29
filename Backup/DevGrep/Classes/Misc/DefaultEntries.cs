namespace DevGrep.Classes.Misc
{
    /// <summary>
    /// Summary description for DefaultEntries.
    /// </summary>
    public class DefaultEntries
    {
        private DefaultEntries()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static void CreateDefaultSettings()
        {
            Win32Registry.HKCUWriteKey(@"Software\DevGrep\Pref\", "Editor", "DevGrep");
            Win32Registry.HKCUWriteKey(@"Software\DevGrep\Pref\", "ExternalEditor", "");
            Win32Registry.HKCUWriteKey(@"Software\DevGrep\Pref\", "ConfirmEach", "FALSE");
            Win32Registry.HKCUWriteKey(@"Software\DevGrep\Pref\", "ReplaceOriginal", "TRUE");
            Win32Registry.HKCUWriteKey(@"Software\DevGrep\Pref\", "ConfirmFileReplace", "FALSE");
            Win32Registry.HKCUWriteKey(@"Software\DevGrep\Pref\", "CmdLine", "%F");

        }
    }
}