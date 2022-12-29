// Copyright 2013 Brian David Patterson <pattersonbriandavid@gmail.com>

using System;

namespace BLS.JSON.Serialization
{
    /// <summary>
    ///     Used by <see cref="JsonSerializer" /> to resolves a <see cref="JsonContract" /> for a given <see cref="Type" />.
    /// </summary>
    /// <example>
    ///     <code lang="cs" source="..\Src\BLS.JSON.Tests\Documentation\SerializationTests.cs"
    ///         region="ReducingSerializedJsonSizeContractResolverObject" title="IContractResolver Class" />
    ///     <code lang="cs" source="..\Src\BLS.JSON.Tests\Documentation\SerializationTests.cs"
    ///         region="ReducingSerializedJsonSizeContractResolverExample" title="IContractResolver Example" />
    /// </example>
    public interface IContractResolver
    {
        /// <summary>
        ///     Resolves the contract for a given type.
        /// </summary>
        /// <param name="type">The type to resolve a contract for.</param>
        /// <returns>The contract for a given type.</returns>
        JsonContract ResolveContract(Type type);
    }
}