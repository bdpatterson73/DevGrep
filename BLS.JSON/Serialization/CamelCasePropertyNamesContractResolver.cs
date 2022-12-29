// Copyright 2013 Brian David Patterson <pattersonbriandavid@gmail.com>

using BLS.JSON.Utilities;

namespace BLS.JSON.Serialization
{
    /// <summary>
    ///     Resolves member mappings for a type, camel casing property names.
    /// </summary>
    public class CamelCasePropertyNamesContractResolver : DefaultContractResolver
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CamelCasePropertyNamesContractResolver" /> class.
        /// </summary>
        public CamelCasePropertyNamesContractResolver()
            : base(true)
        {
        }

        /// <summary>
        ///     Resolves the name of the property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>The property name camel cased.</returns>
        protected internal override string ResolvePropertyName(string propertyName)
        {
            // lower case the first letter of the passed in name
            return StringUtils.ToCamelCase(propertyName);
        }
    }
}