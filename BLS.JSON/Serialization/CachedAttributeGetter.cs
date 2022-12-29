// Copyright 2013 Brian David Patterson <pattersonbriandavid@gmail.com>

using System;
using BLS.JSON.Utilities;

namespace BLS.JSON.Serialization
{
    internal static class CachedAttributeGetter<T> where T : Attribute
    {
        private static readonly ThreadSafeStore<object, T> TypeAttributeCache =
            new ThreadSafeStore<object, T>(JsonTypeReflector.GetAttribute<T>);

        public static T GetAttribute(object type)
        {
            return TypeAttributeCache.Get(type);
        }
    }
}