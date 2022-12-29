#region Usings

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Soap;
using System.Security;
using System.Text;
using DevGrep.Classes.DataTypes.ExtensionMethods;
using DevGrep.Classes.IO.Serializers.Interfaces;

#endregion

namespace DevGrep.Classes.IO.Serializers
{
    /// <summary>
    /// SOAP serializer
    /// </summary>
    internal class SOAPSerializer : ISerializer<string>
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="EncodingUsing">Encoding that the serializer should use (defaults to ASCII)</param>
        public SOAPSerializer(Encoding EncodingUsing = null)
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
        [SecuritySafeCritical]
        public string Serialize(object Object)
        {
            Object.ThrowIfNull("Object can not be null");
            using (var Stream = new MemoryStream())
            {
                var Serializer = new SoapFormatter();
                Serializer.Serialize(Stream, Object);
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
        [SecuritySafeCritical]
        public object Deserialize(string Data, Type ObjectType)
        {
            if (Data.IsNullOrEmpty())
                return null;
            using (var Stream = new MemoryStream(EncodingUsing.GetBytes(Data)))
            {
                var Formatter = new SoapFormatter();
                return Formatter.Deserialize(Stream);
            }
        }

        #endregion
    }
}