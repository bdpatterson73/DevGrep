// Copyright 2013 Brian David Patterson <pattersonbriandavid@gmail.com>

namespace BLS.JSON
{
    /// <summary>
    ///     Specifies how floating point numbers, e.g. 1.0 and 9.9, are parsed when reading JSON text.
    /// </summary>
    public enum FloatParseHandling
    {
        /// <summary>
        ///     Floating point numbers are parsed to <see cref="Double" />.
        /// </summary>
        Double,

        /// <summary>
        ///     Floating point numbers are parsed to <see cref="Decimal" />.
        /// </summary>
        Decimal
    }
}