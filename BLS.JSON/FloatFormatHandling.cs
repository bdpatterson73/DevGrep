// Copyright 2013 Brian David Patterson <pattersonbriandavid@gmail.com>

namespace BLS.JSON
{
    /// <summary>
    ///     Specifies float format handling options when writing special floating point numbers, e.g.
    ///     <see
    ///         cref="F:System.Double.NaN" />
    ///     ,
    ///     <see cref="F:System.Double.PositiveInfinity" /> and <see cref="F:System.Double.NegativeInfinity" /> with
    ///     <see
    ///         cref="JsonWriter" />
    ///     .
    /// </summary>
    public enum FloatFormatHandling
    {
        /// <summary>
        ///     Write special floating point values as strings in JSON, e.g. "NaN", "Infinity", "-Infinity".
        /// </summary>
        String,

        /// <summary>
        ///     Write special floating point values as symbols in JSON, e.g. NaN, Infinity, -Infinity.
        ///     Note that this will produce non-valid JSON.
        /// </summary>
        Symbol,

        /// <summary>
        ///     Write special floating point values as the property's default value in JSON, e.g. 0.0 for a
        ///     <see
        ///         cref="System.Double" />
        ///     property, null for a <see cref="System.Nullable{Double}" /> property.
        /// </summary>
        DefaultValue
    }
}