// Copyright 2013 Brian David Patterson <pattersonbriandavid@gmail.com>

using System;

namespace BLS.JSON
{
    /// <summary>
    ///     Instructs the <see cref="JsonSerializer" /> to use the specified constructor when deserializing that object.
    /// </summary>
    [AttributeUsage(AttributeTargets.Constructor, AllowMultiple = false)]
    public sealed class JsonConstructorAttribute : Attribute
    {
    }
}