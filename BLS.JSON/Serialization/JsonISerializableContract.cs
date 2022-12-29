// Copyright 2013 Brian David Patterson <pattersonbriandavid@gmail.com>

#if !(SILVERLIGHT || NETFX_CORE || PORTABLE || PORTABLE40)
using System;

namespace BLS.JSON.Serialization
{
    /// <summary>
    ///     Contract details for a <see cref="Type" /> used by the <see cref="JsonSerializer" />.
    /// </summary>
    public class JsonISerializableContract : JsonContainerContract
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="JsonISerializableContract" /> class.
        /// </summary>
        /// <param name="underlyingType">The underlying type for the contract.</param>
        public JsonISerializableContract(Type underlyingType)
            : base(underlyingType)
        {
            ContractType = JsonContractType.Serializable;
        }

        /// <summary>
        ///     Gets or sets the ISerializable object constructor.
        /// </summary>
        /// <value>The ISerializable object constructor.</value>
        public ObjectConstructor<object> ISerializableCreator { get; set; }
    }
}

#endif