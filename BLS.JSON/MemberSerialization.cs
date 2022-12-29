// Copyright 2013 Brian David Patterson <pattersonbriandavid@gmail.com>

using System;
using System.Runtime.Serialization;
using BLS.JSON.Serialization;

namespace BLS.JSON
{
    /// <summary>
    ///     Specifies the member serialization options for the <see cref="JsonSerializer" />.
    /// </summary>
    public enum MemberSerialization
    {
        /// <summary>
        ///     All public members are serialized by default. Members can be excluded using <see cref="JsonIgnoreAttribute" /> or
        ///     <see
        ///         cref="NonSerializedAttribute" />
        ///     .
        ///     This is the default member serialization mode.
        /// </summary>
        OptOut,

        /// <summary>
        ///     Only members must be marked with <see cref="JsonPropertyAttribute" /> or <see cref="DataMemberAttribute" /> are serialized.
        ///     This member serialization mode can also be set by marking the class with <see cref="DataContractAttribute" />.
        /// </summary>
        OptIn,

        /// <summary>
        ///     All public and private fields are serialized. Members can be excluded using <see cref="JsonIgnoreAttribute" /> or
        ///     <see
        ///         cref="NonSerializedAttribute" />
        ///     .
        ///     This member serialization mode can also be set by marking the class with <see cref="SerializableAttribute" />
        ///     and setting IgnoreSerializableAttribute on <see cref="DefaultContractResolver" /> to false.
        /// </summary>
        Fields
    }
}