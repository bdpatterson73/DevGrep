// Copyright 2013 Brian David Patterson <pattersonbriandavid@gmail.com>

using System;

namespace BLS.JSON
{
    /// <summary>
    ///     Specifies reference handling options for the <see cref="JsonSerializer" />.
    ///     Note that references cannot be preserved when a value is set via a non-default constructor such as types that implement ISerializable.
    /// </summary>
    /// <example>
    ///     <code lang="cs" source="..\Src\BLS.JSON.Tests\Documentation\SerializationTests.cs"
    ///         region="PreservingObjectReferencesOn" title="Preserve Object References" />
    /// </example>
    [Flags]
    public enum PreserveReferencesHandling
    {
        /// <summary>
        ///     Do not preserve references when serializing types.
        /// </summary>
        None = 0,

        /// <summary>
        ///     Preserve references when serializing into a JSON object structure.
        /// </summary>
        Objects = 1,

        /// <summary>
        ///     Preserve references when serializing into a JSON array structure.
        /// </summary>
        Arrays = 2,

        /// <summary>
        ///     Preserve references when serializing.
        /// </summary>
        All = Objects | Arrays
    }
}