// Copyright 2013 Brian David Patterson <pattersonbriandavid@gmail.com>

using System;
using BLS.JSON.Utilities;

namespace BLS.JSON.Schema
{
    /// <summary>
    ///     Returns detailed information related to the <see cref="ValidationEventHandler" />.
    /// </summary>
    public class ValidationEventArgs : EventArgs
    {
        private readonly JsonSchemaException _ex;

        internal ValidationEventArgs(JsonSchemaException ex)
        {
            ValidationUtils.ArgumentNotNull(ex, "ex");
            _ex = ex;
        }

        /// <summary>
        ///     Gets the <see cref="JsonSchemaException" /> associated with the validation error.
        /// </summary>
        /// <value>The JsonSchemaException associated with the validation error.</value>
        public JsonSchemaException Exception
        {
            get { return _ex; }
        }

        /// <summary>
        ///     Gets the path of the JSON location where the validation error occurred.
        /// </summary>
        /// <value>The path of the JSON location where the validation error occurred.</value>
        public string Path
        {
            get { return _ex.Path; }
        }

        /// <summary>
        ///     Gets the text description corresponding to the validation error.
        /// </summary>
        /// <value>The text description.</value>
        public string Message
        {
            get { return _ex.Message; }
        }
    }
}