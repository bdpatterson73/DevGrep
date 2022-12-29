#region Usings

using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;
using DevGrep.Classes.DataTypes.ExtensionMethods;
using DevGrep.Classes.IO.Serializers.Interfaces;
using SmartAssembly.Attributes;

#endregion

namespace DevGrep.Classes.IO.Serializers
{
    /// <summary>
    /// JSON serializer
    /// </summary>
    [DoNotObfuscateType]
    internal class JSONSerializer : ISerializer<string>
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="EncodingUsing">Encoding that the serializer should use (defaults to ASCII)</param>
        public JSONSerializer(Encoding EncodingUsing = null)
        {
            this.EncodingUsing = EncodingUsing.NullCheck(new ASCIIEncoding());
        }

        #endregion

        #region Properties

        /// <summary>
        /// Encoding that the serializer should use
        /// </summary>
        public virtual Encoding EncodingUsing { get; set; }

        #endregion

        #region Functions

        /// <summary>
        /// Serializes the object
        /// </summary>
        /// <param name="Object">Object to serialize</param>
        /// <returns>The serialized object</returns>
        public string Serialize(object Object)
        {
            Object.ThrowIfNull("Object");
            using (var Stream = new MemoryStream())
            {
                var Serializer = new DataContractJsonSerializer(Object.GetType());
                Serializer.WriteObject(Stream, Object);
                Stream.Flush();
                return EncodingUsing.GetString(Stream.GetBuffer(), 0, (int) Stream.Position);
            }
        }

        /// <summary>
        /// Deserializes the data
        /// </summary>
        /// <param name="ObjectType">Object type</param>
        /// <param name="Data">Data to deserialize</param>
        /// <returns>The resulting object</returns>
        public object Deserialize(string Data, Type ObjectType)
        {
            if (Data.IsNullOrEmpty())
                return null;
            try
            {
                using (var Stream = new MemoryStream(EncodingUsing.GetBytes(Data)))
                {
                    var Serializer = new DataContractJsonSerializer(ObjectType);
                    return Serializer.ReadObject(Stream);
                }
            }
            catch (XmlException)
            {
                
                
            }
            return null;
        }

        #endregion
    }
}