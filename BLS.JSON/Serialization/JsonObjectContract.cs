// Copyright 2013 Brian David Patterson <pattersonbriandavid@gmail.com>

using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security;
using BLS.JSON.Utilities;

namespace BLS.JSON.Serialization
{
    /// <summary>
    ///     Contract details for a <see cref="Type" /> used by the <see cref="JsonSerializer" />.
    /// </summary>
    public class JsonObjectContract : JsonContainerContract
    {
        /// <summary>
        ///     Gets or sets the object member serialization.
        /// </summary>
        /// <value>The member object serialization.</value>
        public MemberSerialization MemberSerialization { get; set; }

        /// <summary>
        ///     Gets or sets a value that indicates whether the object's properties are required.
        /// </summary>
        /// <value>
        ///     A value indicating whether the object's properties are required.
        /// </value>
        public Required? ItemRequired { get; set; }

        /// <summary>
        ///     Gets the object's properties.
        /// </summary>
        /// <value>The object's properties.</value>
        public JsonPropertyCollection Properties { get; private set; }

        /// <summary>
        ///     Gets the constructor parameters required for any non-default constructor
        /// </summary>
        public JsonPropertyCollection ConstructorParameters { get; private set; }

        /// <summary>
        ///     Gets or sets the override constructor used to create the object.
        ///     This is set when a constructor is marked up using the
        ///     JsonConstructor attribute.
        /// </summary>
        /// <value>The override constructor.</value>
        public ConstructorInfo OverrideConstructor { get; set; }

        /// <summary>
        ///     Gets or sets the parametrized constructor used to create the object.
        /// </summary>
        /// <value>The parametrized constructor.</value>
        public ConstructorInfo ParametrizedConstructor { get; set; }

        private bool? _hasRequiredOrDefaultValueProperties;

        internal bool HasRequiredOrDefaultValueProperties
        {
            get
            {
                if (_hasRequiredOrDefaultValueProperties == null)
                {
                    _hasRequiredOrDefaultValueProperties = false;

                    if (ItemRequired.GetValueOrDefault(Required.Default) != Required.Default)
                    {
                        _hasRequiredOrDefaultValueProperties = true;
                    }
                    else
                    {
                        foreach (JsonProperty property in Properties)
                        {
                            if (property.Required != Required.Default ||
                                ((property.DefaultValueHandling & DefaultValueHandling.Populate) ==
                                 DefaultValueHandling.Populate) && property.Writable)
                            {
                                _hasRequiredOrDefaultValueProperties = true;
                                break;
                            }
                        }
                    }
                }

                return _hasRequiredOrDefaultValueProperties.Value;
            }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="JsonObjectContract" /> class.
        /// </summary>
        /// <param name="underlyingType">The underlying type for the contract.</param>
        public JsonObjectContract(Type underlyingType)
            : base(underlyingType)
        {
            ContractType = JsonContractType.Object;

            Properties = new JsonPropertyCollection(UnderlyingType);
            ConstructorParameters = new JsonPropertyCollection(UnderlyingType);
        }

#if !(SILVERLIGHT || NETFX_CORE || PORTABLE40 || PORTABLE)
#if !(NET20 || NET35)
        [SecuritySafeCritical]
#endif
        internal object GetUninitializedObject()
        {
            // we should never get here if the environment is not fully trusted, check just in case
            if (!JsonTypeReflector.FullyTrusted)
                throw new JsonException(
                    "Insufficient permissions. Creating an uninitialized '{0}' type requires full trust.".FormatWith(
                        CultureInfo.InvariantCulture, NonNullableUnderlyingType));

            return FormatterServices.GetUninitializedObject(NonNullableUnderlyingType);
        }
#endif
    }
}