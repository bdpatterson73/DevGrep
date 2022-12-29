﻿// Copyright 2013 Brian David Patterson <pattersonbriandavid@gmail.com>

using System;
using System.Globalization;
using BLS.JSON.Bson;
using BLS.JSON.Utilities;

namespace BLS.JSON.Converters
{
    /// <summary>
    ///     Converts a <see cref="BsonObjectId" /> to and from JSON and BSON.
    /// </summary>
    public class BsonObjectIdConverter : JsonConverter
    {
        /// <summary>
        ///     Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">
        ///     The <see cref="JsonWriter" /> to write to.
        /// </param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            BsonObjectId objectId = (BsonObjectId) value;

            BsonWriter bsonWriter = writer as BsonWriter;
            if (bsonWriter != null)
            {
                bsonWriter.WriteObjectId(objectId.Value);
            }
            else
            {
                writer.WriteValue(objectId.Value);
            }
        }

        /// <summary>
        ///     Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">
        ///     The <see cref="JsonReader" /> to read from.
        /// </param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
                                        JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.Bytes)
                throw new JsonSerializationException(
                    "Expected Bytes but got {0}.".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));

            byte[] value = (byte[]) reader.Value;

            return new BsonObjectId(value);
        }

        /// <summary>
        ///     Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        ///     <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof (BsonObjectId));
        }
    }
}