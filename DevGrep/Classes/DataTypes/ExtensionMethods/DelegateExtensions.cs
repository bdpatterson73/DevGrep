#region Usings

using System;
using System.Threading;

#endregion

namespace DevGrep.Classes.DataTypes.ExtensionMethods
{
    /// <summary>
    /// Extensions for Func, Action, and EventHandler
    /// </summary>
    internal static class DelegateExtensions
    {
        #region Functions

        #region Async

        /// <summary>
        /// Runs an action async
        /// </summary>
        /// <param name="Action">Action to run</param>
        public static void Async(this Action Action)
        {
            new Thread(Action.Invoke).Start();
        }

        #endregion

        #region Raise

        /// <summary>
        /// Safely calls the specified action
        /// </summary>
        /// <typeparam name="T">The type of the event args</typeparam>
        /// <param name="Delegate">The delegate</param>
        /// <param name="EventArgs">The event args</param>
        public static void Raise<T>(this Action<T> Delegate, T EventArgs)
        {
            if (Delegate != null)
                Delegate(EventArgs);
        }

        /// <summary>
        /// Safely raises the event
        /// </summary>
        /// <typeparam name="T">The type of the event args</typeparam>
        /// <param name="Delegate">The delegate</param>
        /// <param name="Sender">The sender</param>
        /// <param name="EventArg">The event args</param>
        public static void Raise<T>(this EventHandler<T> Delegate, object Sender, T EventArg)
            where T : EventArgs
        {
            if (Delegate != null)
                Delegate(Sender, EventArg);
        }

        /// <summary>
        /// Safely calls the Func
        /// </summary>
        /// <typeparam name="T1">The event arg type</typeparam>
        /// <typeparam name="T2">The return type</typeparam>
        /// <param name="Delegate">The delegate</param>
        /// <param name="EventArgs">The event args</param>
        /// <returns>The value returned by the function</returns>
        public static T2 Raise<T1, T2>(this Func<T1, T2> Delegate, T1 EventArgs)
        {
            if (Delegate != null)
                return Delegate(EventArgs);
            return default(T2);
        }

        #endregion

        #endregion
    }
}