// Copyright 2013 Brian David Patterson <pattersonbriandavid@gmail.com>

using System.Collections.Generic;
using BLS.JSON.Linq;
using BLS.JSON.Utilities;

namespace BLS.JSON.Schema
{
    /// <summary>
    ///     Contains the JSON schema extension methods.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        ///     Determines whether the <see cref="JToken" /> is valid.
        /// </summary>
        /// <param name="source">
        ///     The source <see cref="JToken" /> to test.
        /// </param>
        /// <param name="schema">The schema to test with.</param>
        /// <returns>
        ///     <c>true</c> if the specified <see cref="JToken" /> is valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValid(this JToken source, JsonSchema schema)
        {
            bool valid = true;
            source.Validate(schema, (sender, args) => { valid = false; });
            return valid;
        }

        /// <summary>
        ///     Determines whether the <see cref="JToken" /> is valid.
        /// </summary>
        /// <param name="source">
        ///     The source <see cref="JToken" /> to test.
        /// </param>
        /// <param name="schema">The schema to test with.</param>
        /// <param name="errorMessages">When this method returns, contains any error messages generated while validating. </param>
        /// <returns>
        ///     <c>true</c> if the specified <see cref="JToken" /> is valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValid(this JToken source, JsonSchema schema, out IList<string> errorMessages)
        {
            IList<string> errors = new List<string>();

            source.Validate(schema, (sender, args) => errors.Add(args.Message));

            errorMessages = errors;
            return (errorMessages.Count == 0);
        }

        /// <summary>
        ///     Validates the specified <see cref="JToken" />.
        /// </summary>
        /// <param name="source">
        ///     The source <see cref="JToken" /> to test.
        /// </param>
        /// <param name="schema">The schema to test with.</param>
        public static void Validate(this JToken source, JsonSchema schema)
        {
            source.Validate(schema, null);
        }

        /// <summary>
        ///     Validates the specified <see cref="JToken" />.
        /// </summary>
        /// <param name="source">
        ///     The source <see cref="JToken" /> to test.
        /// </param>
        /// <param name="schema">The schema to test with.</param>
        /// <param name="validationEventHandler">The validation event handler.</param>
        public static void Validate(this JToken source, JsonSchema schema, ValidationEventHandler validationEventHandler)
        {
            ValidationUtils.ArgumentNotNull(source, "source");
            ValidationUtils.ArgumentNotNull(schema, "schema");

            using (JsonValidatingReader reader = new JsonValidatingReader(source.CreateReader()))
            {
                reader.Schema = schema;
                if (validationEventHandler != null)
                    reader.ValidationEventHandler += validationEventHandler;

                while (reader.Read())
                {
                }
            }
        }
    }
}