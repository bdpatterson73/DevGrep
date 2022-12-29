// Copyright 2013 Brian David Patterson <pattersonbriandavid@gmail.com>

namespace BLS.JSON
{
    /// <summary>
    ///     Specifies null value handling options for the <see cref="JsonSerializer" />.
    /// </summary>
    /// <example>
    ///     <code lang="cs" source="..\Src\BLS.JSON.Tests\Documentation\SerializationTests.cs"
    ///         region="ReducingSerializedJsonSizeNullValueHandlingObject" title="NullValueHandling Class" />
    ///     <code lang="cs" source="..\Src\BLS.JSON.Tests\Documentation\SerializationTests.cs"
    ///         region="ReducingSerializedJsonSizeNullValueHandlingExample" title="NullValueHandling Ignore Example" />
    /// </example>
    public enum NullValueHandling
    {
        /// <summary>
        ///     Include null values when serializing and deserializing objects.
        /// </summary>
        Include = 0,

        /// <summary>
        ///     Ignore null values when serializing and deserializing objects.
        /// </summary>
        Ignore = 1
    }
}