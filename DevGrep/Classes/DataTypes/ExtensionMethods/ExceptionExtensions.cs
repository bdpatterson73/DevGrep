#region Usings

using System;
using System.Text;

#endregion

namespace DevGrep.Classes.DataTypes.ExtensionMethods
{
    /// <summary>
    /// Class for housing exception specific extensions
    /// </summary>
    internal static class ExceptionExtensions
    {
        #region ToString

        /// <summary>
        /// Converts the exception to a string and appends the specified prefix/suffix (used for logging)
        /// </summary>
        /// <param name="Exception">Exception to convert</param>
        /// <param name="Prefix">Prefix</param>
        /// <param name="Suffix">Suffix</param>
        /// <returns>The exception as a string</returns>
        public static string ToString(this Exception Exception, string Prefix, string Suffix = "")
        {
            if (Exception == null)
                return "";
            var Builder = new StringBuilder();
            Builder.AppendLine(Prefix);
            try
            {
                Builder.AppendLineFormat("Exception: {0}", Exception.Message);
                Builder.AppendLineFormat("Exception Type: {0}", Exception.GetType().FullName);
                if (Exception.Data != null)
                {
                    foreach (object Object in Exception.Data)
                        Builder.AppendLineFormat("Data: {0}:{1}", Object, Exception.Data[Object]);
                }
                Builder.AppendLineFormat("StackTrace: {0}", Exception.StackTrace);
                Builder.AppendLineFormat("Source: {0}", Exception.Source);
                Builder.AppendLineFormat("TargetSite: {0}", Exception.TargetSite);
                Builder.Append(Exception.InnerException.ToString(Prefix, Suffix));
            }
            catch
            {
            }
            Builder.AppendLine(Suffix);
            return Builder.ToString();
        }

        #endregion
    }
}