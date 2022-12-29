// Copyright 2013 Brian David Patterson <pattersonbriandavid@gmail.com>

using System;

namespace BLS.JSON
{
    /// <summary>
    ///     Instructs the <see cref="JsonSerializer" /> not to serialize the public field or public read/write property value.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public sealed class JsonIgnoreAttribute : Attribute
    {
    }
}