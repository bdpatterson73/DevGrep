﻿// Copyright 2013 Brian David Patterson <pattersonbriandavid@gmail.com>

using System;

namespace BLS.JSON.Converters
{
    /// <summary>
    ///     Provides a base class for converting a <see cref="DateTime" /> to and from JSON.
    /// </summary>
    public abstract class DateTimeConverterBase : JsonConverter
    {
        /// <summary>
        ///     Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        ///     <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof (DateTime) || objectType == typeof (DateTime?))
                return true;
#if !NET20
            if (objectType == typeof (DateTimeOffset) || objectType == typeof (DateTimeOffset?))
                return true;
#endif

            return false;
        }
    }
}