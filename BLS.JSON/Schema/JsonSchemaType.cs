// Copyright 2013 Brian David Patterson <pattersonbriandavid@gmail.com>

using System;

namespace BLS.JSON.Schema
{
    /// <summary>
    ///     The value types allowed by the <see cref="JsonSchema" />.
    /// </summary>
    [Flags]
    public enum JsonSchemaType
    {
        /// <summary>
        ///     No type specified.
        /// </summary>
        None = 0,

        /// <summary>
        ///     String type.
        /// </summary>
        String = 1,

        /// <summary>
        ///     Float type.
        /// </summary>
        Float = 2,

        /// <summary>
        ///     Integer type.
        /// </summary>
        Integer = 4,

        /// <summary>
        ///     Boolean type.
        /// </summary>
        Boolean = 8,

        /// <summary>
        ///     Object type.
        /// </summary>
        Object = 16,

        /// <summary>
        ///     Array type.
        /// </summary>
        Array = 32,

        /// <summary>
        ///     Null type.
        /// </summary>
        Null = 64,

        /// <summary>
        ///     Any type.
        /// </summary>
        Any = String | Float | Integer | Boolean | Object | Array | Null
    }
}