// Copyright 2013 Brian David Patterson <pattersonbriandavid@gmail.com>

using System;
using BLS.JSON.Utilities;

namespace BLS.JSON.Bson
{
    /// <summary>
    ///     Represents a BSON Oid (object id).
    /// </summary>
    public class BsonObjectId
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BsonObjectId" /> class.
        /// </summary>
        /// <param name="value">The Oid value.</param>
        public BsonObjectId(byte[] value)
        {
            ValidationUtils.ArgumentNotNull(value, "value");
            if (value.Length != 12)
                throw new ArgumentException("An ObjectId must be 12 bytes", "value");

            Value = value;
        }

        /// <summary>
        ///     Gets or sets the value of the Oid.
        /// </summary>
        /// <value>The value of the Oid.</value>
        public byte[] Value { get; private set; }
    }
}