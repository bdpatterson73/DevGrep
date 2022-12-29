// Copyright 2013 Brian David Patterson <pattersonbriandavid@gmail.com>

namespace BLS.JSON.Schema
{
    /// <summary>
    ///     Specifies undefined schema Id handling options for the <see cref="JsonSchemaGenerator" />.
    /// </summary>
    public enum UndefinedSchemaIdHandling
    {
        /// <summary>
        ///     Do not infer a schema Id.
        /// </summary>
        None = 0,

        /// <summary>
        ///     Use the .NET type name as the schema Id.
        /// </summary>
        UseTypeName = 1,

        /// <summary>
        ///     Use the assembly qualified .NET type name as the schema Id.
        /// </summary>
        UseAssemblyQualifiedName = 2,
    }
}