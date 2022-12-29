// Copyright 2013 Brian David Patterson <pattersonbriandavid@gmail.com>

using System;
using System.Collections.Generic;
using System.Linq;

#if NET20
using BLS.JSON.Utilities.LinqBridge;
#else

#endif

namespace BLS.JSON.Schema
{
    /// <summary>
    ///     Resolves <see cref="JsonSchema" /> from an id.
    /// </summary>
    public class JsonSchemaResolver
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="JsonSchemaResolver" /> class.
        /// </summary>
        public JsonSchemaResolver()
        {
            LoadedSchemas = new List<JsonSchema>();
        }

        /// <summary>
        ///     Gets or sets the loaded schemas.
        /// </summary>
        /// <value>The loaded schemas.</value>
        public IList<JsonSchema> LoadedSchemas { get; protected set; }

        /// <summary>
        ///     Gets a <see cref="JsonSchema" /> for the specified reference.
        /// </summary>
        /// <param name="reference">The id.</param>
        /// <returns>
        ///     A <see cref="JsonSchema" /> for the specified reference.
        /// </returns>
        public virtual JsonSchema GetSchema(string reference)
        {
            JsonSchema schema =
                LoadedSchemas.SingleOrDefault(s => string.Equals(s.Id, reference, StringComparison.Ordinal));

            if (schema == null)
                schema =
                    LoadedSchemas.SingleOrDefault(s => string.Equals(s.Location, reference, StringComparison.Ordinal));

            return schema;
        }
    }
}