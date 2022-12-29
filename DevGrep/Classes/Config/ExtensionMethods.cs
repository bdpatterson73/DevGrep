using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevGrep.Classes.Config
{
    internal static class ExtensionMethods
    {
        public static void InvokeIfRequired(this Control c, Action<Control> action)
        {
            if (c.InvokeRequired)
            {
                c.Invoke(new Action(() => action(c)));
            }
            else
            {
                action(c);
            }
        }

        public static void InvokeIfRequired(this StatusStrip c, Action<StatusStrip> action)
        {
            if (c.InvokeRequired)
            {
                c.Invoke(new Action(() => action(c)));
            }
            else
            {
                action(c);
            }
        }

        public static void InvokeIfRequired(this ListView c, Action<ListView> action)
        {
            if (c.InvokeRequired)
            {
                c.Invoke(new Action(() => action(c)));
            }
            else
            {
                action(c);
            }
        }

        public static void InvokeIfRequired(this RichTextBox c, Action<RichTextBox> action)
        {
            if (c.InvokeRequired)
            {
                c.Invoke(new Action(() => action(c)));
            }
            else
            {
                action(c);
            }
        }

    }
}
