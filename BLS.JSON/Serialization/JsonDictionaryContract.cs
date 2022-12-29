// Copyright 2013 Brian David Patterson <pattersonbriandavid@gmail.com>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using BLS.JSON.Utilities;

#if NET20
using BLS.JSON.Utilities.LinqBridge;
#endif

namespace BLS.JSON.Serialization
{
    /// <summary>
    ///     Contract details for a <see cref="Type" /> used by the <see cref="JsonSerializer" />.
    /// </summary>
    public class JsonDictionaryContract : JsonContainerContract
    {
        private readonly Type _genericCollectionDefinitionType;
        private readonly bool _isDictionaryValueTypeNullableType;
        private Func<object> _genericTemporaryDictionaryCreator;
        private MethodCall<object, object> _genericWrapperCreator;
        private Type _genericWrapperType;

        /// <summary>
        ///     Initializes a new instance of the <see cref="JsonDictionaryContract" /> class.
        /// </summary>
        /// <param name="underlyingType">The underlying type for the contract.</param>
        public JsonDictionaryContract(Type underlyingType)
            : base(underlyingType)
        {
            ContractType = JsonContractType.Dictionary;

            Type keyType;
            Type valueType;

            if (ReflectionUtils.ImplementsGenericDefinition(underlyingType, typeof (IDictionary<,>),
                                                            out _genericCollectionDefinitionType))
            {
                keyType = _genericCollectionDefinitionType.GetGenericArguments()[0];
                valueType = _genericCollectionDefinitionType.GetGenericArguments()[1];

                if (ReflectionUtils.IsGenericDefinition(UnderlyingType, typeof (IDictionary<,>)))
                    CreatedType = typeof (Dictionary<,>).MakeGenericType(keyType, valueType);

#if !(NET40 || NET35 || NET20 || SILVERLIGHT || WINDOWS_PHONE || PORTABLE40)
        IsReadOnlyOrFixedSize = ReflectionUtils.InheritsGenericDefinition(underlyingType, typeof(ReadOnlyDictionary<,>));
#endif
            }
#if !(NET40 || NET35 || NET20 || SILVERLIGHT || WINDOWS_PHONE || PORTABLE40)
      else if (ReflectionUtils.ImplementsGenericDefinition(underlyingType, typeof(IReadOnlyDictionary<,>), out _genericCollectionDefinitionType))
      {
        keyType = _genericCollectionDefinitionType.GetGenericArguments()[0];
        valueType = _genericCollectionDefinitionType.GetGenericArguments()[1];

        if (ReflectionUtils.IsGenericDefinition(UnderlyingType, typeof(IReadOnlyDictionary<,>)))
          CreatedType = typeof(ReadOnlyDictionary<,>).MakeGenericType(keyType, valueType);

        IsReadOnlyOrFixedSize = true;
      }
#endif
            else
            {
                ReflectionUtils.GetDictionaryKeyValueTypes(UnderlyingType, out keyType, out valueType);

                if (UnderlyingType == typeof (IDictionary))
                    CreatedType = typeof (Dictionary<object, object>);
            }

            if (keyType != null && valueType != null)
                ParametrizedConstructor = CollectionUtils.ResolveEnumableCollectionConstructor(CreatedType,
                                                                                               typeof (KeyValuePair<,>)
                                                                                                   .MakeGenericType(
                                                                                                       keyType,
                                                                                                       valueType));

            ShouldCreateWrapper = !typeof (IDictionary).IsAssignableFrom(CreatedType);

            DictionaryKeyType = keyType;
            DictionaryValueType = valueType;

            if (DictionaryValueType != null)
                _isDictionaryValueTypeNullableType = ReflectionUtils.IsNullableType(DictionaryValueType);

#if (NET20 || NET35)
      Type tempDictioanryType;

        // bug in .NET 2.0 & 3.5 that Dictionary<TKey, Nullable<TValue>> throws an error when adding null via IDictionary[key] = object
      // wrapper will handle calling Add(T) instead
      if (_isDictionaryValueTypeNullableType
        && (ReflectionUtils.InheritsGenericDefinition(CreatedType, typeof(Dictionary<,>), out tempDictioanryType)))
      {
        ShouldCreateWrapper = true;
      }
#endif
        }

        /// <summary>
        ///     Gets or sets the property name resolver.
        /// </summary>
        /// <value>The property name resolver.</value>
        public Func<string, string> PropertyNameResolver { get; set; }

        /// <summary>
        ///     Gets the <see cref="Type" /> of the dictionary keys.
        /// </summary>
        /// <value>
        ///     The <see cref="Type" /> of the dictionary keys.
        /// </value>
        public Type DictionaryKeyType { get; private set; }

        /// <summary>
        ///     Gets the <see cref="Type" /> of the dictionary values.
        /// </summary>
        /// <value>
        ///     The <see cref="Type" /> of the dictionary values.
        /// </value>
        public Type DictionaryValueType { get; private set; }

        internal JsonContract KeyContract { get; set; }

        internal bool ShouldCreateWrapper { get; private set; }
        internal ConstructorInfo ParametrizedConstructor { get; private set; }

        internal IWrappedDictionary CreateWrapper(object dictionary)
        {
            if (_genericWrapperCreator == null)
            {
                _genericWrapperType = typeof (DictionaryWrapper<,>).MakeGenericType(DictionaryKeyType,
                                                                                    DictionaryValueType);

                ConstructorInfo genericWrapperConstructor =
                    _genericWrapperType.GetConstructor(new[] {_genericCollectionDefinitionType});
                _genericWrapperCreator =
                    JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(genericWrapperConstructor);
            }

            return (IWrappedDictionary) _genericWrapperCreator(null, dictionary);
        }

        internal IDictionary CreateTemporaryDictionary()
        {
            if (_genericTemporaryDictionaryCreator == null)
            {
                Type temporaryDictionaryType = typeof (Dictionary<,>).MakeGenericType(DictionaryKeyType,
                                                                                      DictionaryValueType);

                _genericTemporaryDictionaryCreator =
                    JsonTypeReflector.ReflectionDelegateFactory.CreateDefaultConstructor<object>(temporaryDictionaryType);
            }

            return (IDictionary) _genericTemporaryDictionaryCreator();
        }
    }
}