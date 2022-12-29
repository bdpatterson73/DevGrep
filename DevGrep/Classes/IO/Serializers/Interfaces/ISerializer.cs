#region Usings

using System;

#endregion

namespace DevGrep.Classes.IO.Serializers.Interfaces
{
    /// <summary>
    /// Serializer interface
    /// </summary>
    /// <typeparam name="T">Type that the object is serialized to/from</typeparam>
    internal interface ISerializer<T>
    {
        /// <summary>
        /// Serializes the object
        /// </summary>
        /// <param name="Object">Object to serialize</param>
        /// <returns>The serialized object</returns>
        T Serialize(object Object);

        /// <summary>
        /// Deserializes the data
        /// </summary>
        /// <param name="ObjectType">Object type</param>
        /// <param name="Data">Data to deserialize</param>
        /// <returns>The resulting object</returns>
        object Deserialize(T Data, Type ObjectType);
    }
}