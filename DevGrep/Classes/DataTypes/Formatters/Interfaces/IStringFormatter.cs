#region Usings

#endregion

namespace DevGrep.Classes.DataTypes.Formatters.Interfaces
{
    /// <summary>
    /// String formatter
    /// </summary>
    internal interface IStringFormatter
    {
        #region Functions

        /// <summary>
        /// Formats the string based on the pattern
        /// </summary>
        /// <param name="Input">Input string</param>
        /// <param name="FormatPattern">Format pattern</param>
        /// <returns>The formatted string</returns>
        string Format(string Input, string FormatPattern);

        #endregion
    }
}