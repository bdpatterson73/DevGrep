// Copyright 2013 Brian David Patterson <pattersonbriandavid@gmail.com>

using System;
using BLS.JSON.Utilities;

#if !(PORTABLE || PORTABLE40 || NET35 || NET20 || WINDOWS_PHONE || SILVERLIGHT)
#endif

namespace BLS.JSON.Serialization
{
    /// <summary>
    ///     Contract details for a <see cref="Type" /> used by the <see cref="JsonSerializer" />.
    /// </summary>
    public class JsonPrimitiveContract : JsonContract
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="JsonPrimitiveContract" /> class.
        /// </summary>
        /// <param name="underlyingType">The underlying type for the contract.</param>
        public JsonPrimitiveContract(Type underlyingType)
            : base(underlyingType)
        {
            ContractType = JsonContractType.Primitive;

            TypeCode = ConvertUtils.GetTypeCode(underlyingType);
            IsReadOnlyOrFixedSize = true;
        }

        internal PrimitiveTypeCode TypeCode { get; set; }
    }
}