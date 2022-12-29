// Copyright 2013 Brian David Patterson <pattersonbriandavid@gmail.com>

#if !SILVERLIGHT && !NET20 && !NETFX_CORE
using System;
using System.Reflection;
using BLS.JSON.Utilities;

namespace BLS.JSON.Serialization
{
    internal class LateBoundMetadataTypeAttribute : IMetadataTypeAttribute
    {
        private static PropertyInfo _metadataClassTypeProperty;

        private readonly object _attribute;

        public LateBoundMetadataTypeAttribute(object attribute)
        {
            _attribute = attribute;
        }

        public Type MetadataClassType
        {
            get
            {
                if (_metadataClassTypeProperty == null)
                    _metadataClassTypeProperty = _attribute.GetType().GetProperty("MetadataClassType");

                return (Type) ReflectionUtils.GetMemberValue(_metadataClassTypeProperty, _attribute);
            }
        }
    }
}

#endif