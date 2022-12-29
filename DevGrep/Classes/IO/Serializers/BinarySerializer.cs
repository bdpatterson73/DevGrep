#region Usings

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using DevGrep.Classes.DataTypes.ExtensionMethods;
using DevGrep.Classes.IO.Serializers.Interfaces;

#endregion

namespace DevGrep.Classes.IO.Serializers
{
    /// <summary>
    /// Binary serializer
    /// </summary>
    internal class BinarySerializer : ISerializer<byte[]>
    {
        #region ISerializer<byte[]> Members

        /// <summary>
        /// Serializes the object
        /// </summary>
        /// <param name="Object">Object to serialize</param>
        /// <returns>The serialized object</returns>
        public byte[] Serialize(object Object)
        {
            Object.ThrowIfNull("Object");
            using (var Stream = new MemoryStream())
            {
                var Formatter = new BinaryFormatter();
                Formatter.Serialize(Stream, Object);
                return Stream.ToArray();
            }
        }

        /// <summary>
        /// Deserializes the data
        /// </summary>
        /// <param name="ObjectType">Object type</param>
        /// <param name="Data">Data to deserialize</param>
        /// <returns>The resulting object</returns>
        public object Deserialize(byte[] Data, Type ObjectType)
        {
            if (Data == null)
                return null;
            using (var Stream = new MemoryStream(Data))
            {
                var Formatter = new BinaryFormatter();
                return Formatter.Deserialize(Stream);
            }
        }

        #endregion
    }
}