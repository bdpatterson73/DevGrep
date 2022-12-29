// Copyright 2013 Brian David Patterson <pattersonbriandavid@gmail.com>

using System;

namespace BLS.JSON.Serialization
{
    /// <summary>
    ///     When applied to a method, specifies that the method is called when an error occurs serializing an object.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public sealed class OnErrorAttribute : Attribute
    {
    }
}