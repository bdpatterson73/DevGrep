// Copyright 2013 Brian David Patterson <pattersonbriandavid@gmail.com>

namespace BLS.JSON
{
    /// <summary>
    ///     Specifies how strings are escaped when writing JSON text.
    /// </summary>
    public enum StringEscapeHandling
    {
        /// <summary>
        ///     Only control characters (e.g. newline) are escaped.
        /// </summary>
        Default,

        /// <summary>
        ///     All non-ASCII and control characters (e.g. newline) are escaped.
        /// </summary>
        EscapeNonAscii,

        /// <summary>
        ///     HTML (&lt;, &gt;, &amp;, &apos;, &quot;) and control characters (e.g. newline) are escaped.
        /// </summary>
        EscapeHtml
    }
}